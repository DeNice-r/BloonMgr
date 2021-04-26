using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Newtonsoft.Json;

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
        public Program()
        {
            InitializeComponent();
            OrderList = new ObservableCollection<OrderEntry> { new OrderEntry("0501728331", "по рекламе", new ObservableCollection<OrderPart>() { new OrderPart(50, 3), new OrderPart(21, 10) }),
                        new OrderEntry("0977299902", "инстаграмм", new ObservableCollection<OrderPart>() { new OrderPart(85, 2), new OrderPart(150), new OrderPart(23000, 25) })};

            for(int x = 0; x < 100; x++)
            {
                OrderList.Add(new OrderEntry(DateTime.Today.AddDays(x), "050" + rnd.Next(1000000, 10000000), "по рекламе " + x, new ObservableCollection<OrderPart>() { OrderPart.RandomOrder(), OrderPart.RandomOrder(), OrderPart.RandomOrder() }));
                OrderList.Add(new OrderEntry(DateTime.Today.AddDays(-x), "097" + rnd.Next(1000000, 10000000), "инстаграмм " + -x, new ObservableCollection<OrderPart>() { OrderPart.RandomOrder(), OrderPart.RandomOrder(), OrderPart.RandomOrder() }));
            }
            OrderGrid.ItemsSource = OrderList;
        }

        private void OrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ogrid = (DataGrid)sender;
            if (ogrid.SelectedIndex == -1) return;

            TempOrder = new OrderEntry((OrderEntry)ogrid.SelectedItem);
            OrderDate.SelectedDate = TempOrder.Date;
            OrderNotes.Text = TempOrder.Notes;
            OrderPhone.Text = TempOrder.Phone;
            OrderpartList.ItemsSource = TempOrder.Orders;
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
            TempOrder = new OrderEntry();
        }

        private void OrderpartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { 
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
            TempOrder.Date = (DateTime) OrderDate.SelectedDate;
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
                OrderGrid.SelectedItem = new OrderEntry(TempOrder);
                TempOrder = null;
                OrderGrid.SelectedIndex = -1;
            }
            OrderGrid.Items.Refresh();
        }
    }
}
