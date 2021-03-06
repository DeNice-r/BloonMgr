using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using System.IO;

namespace BloonMgr
{
    public partial class Program : Window
    {
        private ObservableCollection<OrderEntry> OrderList;
        //private OrderEntry SelectedOrder;
        private OrderPart SelectedPart;
        private OrderEntry TempOrder;
        private OrderPart TempPart;
        private bool UnsavedChanges = false;
        private bool isNewOrder = false;
        public static Random rnd = new Random();
        private int selectedYear = DateTime.Today.Year;
        //private string selectedYearString {
        //    get {
        //        return selectedYear.ToString();
        //    }
        //}
        public Program()
        {
            InitializeComponent();
            ProcessYear();

        }

        private void OrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ogrid = (DataGrid)sender;
            if (ogrid.SelectedIndex == -1)
            {
                TempOrder = new OrderEntry();
                OrderDate.SelectedDate = TempOrder.Date;
                OrderNotes.Text = TempOrder.Notes;
                OrderPhone.Text = TempOrder.Phone;
                OrderpartList.ItemsSource = TempOrder.Orders;
            }
            else
            {
                TempOrder = new OrderEntry((OrderEntry)ogrid.SelectedItem);
                OrderDate.SelectedDate = TempOrder.Date;
                OrderNotes.Text = TempOrder.Notes;
                OrderPhone.Text = TempOrder.Phone;
                OrderpartList.ItemsSource = TempOrder.Orders;
            }

        }

        private void ProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selItem = (OrderPart)((DataGrid)sender).SelectedItem;
            ProductName.Text = selItem.ItemName;
        }

        //private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var sel = Search.SelectedItem;
        //    if (don.Keys.ToArray().Contains(sel))
        //    {
        //        var seldon = don[sel.ToString()];
        //        if (!donname.IsVisible)
        //        {
        //            var temp = new Thread(DonAnimate);
        //            temp.IsBackground = true;
        //            temp.Start();
        //        }
        //        SortedSet<string> audinfo = audience;
        //        audinfo.Add("");
        //        donrelaud.ItemsSource = audinfo;
        //        donrelaud.SelectedItem = seldon.relatedAud;
        //        donrelatedsubjects.ItemsSource = seldon.relatedSubjects;
        //        donrelatedsubjects.Columns[0].Width = 320;
        //        donname.Text = sel.ToString();
        //        dond1.IsChecked = seldon.possDays[0];
        //        dond2.IsChecked = seldon.possDays[1];
        //        dond3.IsChecked = seldon.possDays[2];
        //        dond4.IsChecked = seldon.possDays[3];
        //        dond5.IsChecked = seldon.possDays[4];
        //    }
        //    else if (group.Keys.ToArray().Contains(sel))
        //    {
        //        if (!groupname.IsVisible)
        //        {
        //            var temp = new Thread(GroupAnimate);
        //            temp.IsBackground = true;
        //            temp.Start();
        //        }
        //        dbugrouprelinfo();
        //        groupname.Text = sel.ToString();
        //        grouprelaud.Text = group[sel.ToString()].relatedAud;
        //        groupstudyingweeks.Text = group[sel.ToString()].StudyingWeeks.ToString();
        //    }
        //    else if (subject.Keys.ToArray().Contains(sel))
        //    {
        //        if (!subjectname.IsVisible)
        //        {
        //            var temp = new Thread(SubjectAnimate);
        //            temp.IsBackground = true;
        //            temp.Start();
        //        }
        //        subjectname.Text = subject[sel.ToString()].subjectName;
        //        subjectrelaud.Text = subject[sel.ToString()].relatedAud;
        //    }
        //    else if (audience.Contains(sel))
        //    {
        //        if (!audname.IsVisible)
        //        {
        //            var temp = new Thread(AudAnimate);
        //            temp.IsBackground = true;
        //            temp.Start();
        //        }
        //        audname.Text = sel.ToString();
        //    }
        //}

        private void SearchHandle1(object sender, MouseEventArgs e)
        {
            //if (e.GetPosition(grid).Y >= 0 && e.GetPosition(grid).Y <= 30 && e.GetPosition(grid).X >= 30 && e.GetPosition(grid).X <= 915)
            //{
            //    if (!(don.Keys.ToList().All(x => database.Contains(x)) && group.Keys.ToList().All(x => database.Contains(x)) && subject.Keys.ToList().All(x => database.Contains(x)) && audience.All(x => database.Contains(x))))
            //    {
            //        database = new List<string>();
            //        database.AddRange(don.Keys.ToArray());
            //        database.AddRange(group.Keys.ToArray());
            //        database.AddRange(subject.Keys.ToArray());
            //        database.AddRange(audience.ToArray());
            //    }
            //    Search.ItemsSource = database.Where(x => x.ToLower().Contains(Search.Text.ToLower())).ToArray();

            //    if (!Search.IsDropDownOpen)
            //        Search.IsDropDownOpen = true;
            //}
        }

        private void SearchHandle2(object sender, EventArgs e)
        {
            //if (!(don.Keys.ToList().All(x => database.Contains(x)) && group.Keys.ToList().All(x => database.Contains(x)) && subject.Keys.ToList().All(x => database.Contains(x)) && audience.All(x => database.Contains(x))))
            //{
            //    database = new List<string>();
            //    database.AddRange(don.Keys.ToArray());
            //    database.AddRange(group.Keys.ToArray());
            //    database.AddRange(subject.Keys.ToArray());
            //    database.AddRange(audience.ToArray());
            //}
            //Search.ItemsSource = database.Where(x => x.ToLower().Contains(Search.Text.ToLower())).ToArray();

            //if (!Search.IsDropDownOpen)
            //    Search.IsDropDownOpen = true;
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            isNewOrder = true;
            OrderGrid.SelectedIndex = -1;
        }

        private void OrderpartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var ogrid = (DataGrid)sender;
                SelectedPart = (OrderPart)ogrid.SelectedItem;
                ProductName.Text = SelectedPart.ItemName;
                ProductPrice.Text = SelectedPart.Price.ToString();
                ProductQty.Text = SelectedPart.Quantity.ToString();
                ProductTotal.Text = SelectedPart.Total.ToString();
            }
            catch
            {
                SelectedPart = null;
                ProductName.Text = "";
                ProductPrice.Text = "";
                ProductQty.Text = "";
                ProductTotal.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Double pprice = Convert.ToDouble(ProductPrice.Text);
            Int32 pqty = Convert.ToInt32(ProductQty.Text);
            String denom = (String)ProductName.Text;
            if (denom != "" && true)
                TempOrder.Orders.Add(new OrderPart(Products.GetID(denom), pprice, pqty));
            UnsavedChanges = true;
        }

        private void ProductPriceOrQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            var curField = (TextBox)sender;
            try
            {
                var ci = ProductPrice.CaretIndex;
                ProductPrice.Text = ProductPrice.Text.Replace('.', ',');
                ProductPrice.CaretIndex = ci;
                var pprice = Convert.ToDouble(ProductPrice.Text);
                var pqty = Convert.ToInt32(ProductQty.Text);
                ProductTotal.Text = (pprice * pqty).ToString();
            }
            catch
            {
                ProductTotal.Text = "-";
            }
        }


        // TODO: Не сохраняется / не изменяется в таблице (пройти в дебуге пошагово для начала)
        private void OrderSave_Click(object sender, RoutedEventArgs e)
        {
            TempOrder.Date = (DateTime)OrderDate.SelectedDate;
            TempOrder.Notes = OrderNotes.Text;
            TempOrder.Phone = OrderPhone.Text;
            if (isNewOrder)
            {
                OrderList.Add(new OrderEntry(TempOrder));
                isNewOrder = false;
                OrderGrid.SelectedIndex = -1;
            }
            else
            {
                ((OrderEntry)OrderGrid.SelectedItem).SetTo(TempOrder);
                TempOrder = null;
                OrderGrid.SelectedIndex = -1;
            }
            OrderGrid.Items.Refresh();
            Products.Save();
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var r = MessageBox.Show("Удалить этот заказ?", "Удаление заказа", MessageBoxButton.OKCancel);
            if (r == MessageBoxResult.OK)
            {
                OrderList.RemoveAt(OrderGrid.SelectedIndex);
            }
        }

        private void LoadOrders()
        {
            try
            {
                OrderList = JsonConvert.DeserializeObject<ObservableCollection<OrderEntry>>(File.ReadAllText("OrderList_" + selectedYear + ".json"));
                OrderGrid.ItemsSource = OrderList;
            }
            catch (FileNotFoundException)
            {
                OrderList = new ObservableCollection<OrderEntry>();
            }
        }

        private void SaveOrders()
        {
            File.WriteAllText("OrderList_" + selectedYear + ".json", JsonConvert.SerializeObject(OrderList));
        }

        private void ProcessYear(int year = 0)
        {
            if (year == 0)
                year = selectedYear;
            else
                selectedYear = year;
            LoadOrders();
            CurrentYearDisplay.Content = selectedYear;
            PreviousYearButton.Content = (selectedYear - 1);
            PreviousYearButton.IsEnabled = File.Exists("OrderList_" + (selectedYear - 1) + ".json");
            NextYearButton.Content = (selectedYear + 1);
            NextYearButton.IsEnabled = File.Exists("OrderList_" + (selectedYear + 1) + ".json");
        }

        private void PrevNextYearButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            ProcessYear(Convert.ToInt32(button.Content));
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveOrders();
        }

    }
}
