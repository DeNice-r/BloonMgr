using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BloonMgr
{
    class OrderEntry
    {
        private DateTime mDate;
        public UInt64 OrderID { get; set; }
        public static UInt64 Max_ID = 1;
        public DateTime Date
        {
            get
            {
                return mDate;
            }
            set
            {
                mDate = value;
            }
        }
        public String Phone { get; set; } = "";
        public String Notes { get; set; } = "";
        private ObservableCollection<OrderPart> mOrders;
        public ObservableCollection<OrderPart> Orders
        {
            get { return mOrders; }
            set
            {
                mOrders = value;
                Total = mOrders.Sum(x => x.Total);
            }
        }
        public Double Total { get; private set; } = 0;

        public OrderEntry()
        {
            Init(DateTime.Now, "", "", new ObservableCollection<OrderPart>());
        }

        public OrderEntry(String phone, String notes)
        {
            Init(DateTime.Now, phone, notes, new ObservableCollection<OrderPart>());
        }

        public OrderEntry(String phone, String notes, ObservableCollection<OrderPart> orders)
        {
            Init(DateTime.Now, phone, notes, orders);
        }

        public OrderEntry(DateTime date, String phone, String notes, ObservableCollection<OrderPart> orders)
        {
            Init(date, phone, notes, orders);
        }

        public OrderEntry(OrderEntry oe)
        {
            Init(oe.Date, oe.Phone, oe.Notes, new ObservableCollection<OrderPart>(oe.Orders), oe.OrderID);
        }

        private void Init(DateTime date, String phone, String notes, ObservableCollection<OrderPart> orders, UInt64 id = 0)
        {
            if (id == 0)
                OrderID = Max_ID++;
            else
            {
                OrderID = id;
                if(Max_ID < OrderID)
                    Max_ID = OrderID + 1;
            }
            mDate = date;
            Phone = phone;
            Notes = notes;
            Orders = orders;
        }
    }

    public class OrderPart
    {
        private Int32 ItemID;
        public String ItemName
        {
            get { return Products.GetName(ItemID); }
        }
        private Double mPrice;
        public Double Price
        {
            get { return mPrice; }
            set
            {
                mTotal = value * mQuantity;
                mPrice = value;
            }
        }
        private Int32 mQuantity;
        public Int32 Quantity
        {
            get { return mQuantity; }
            set
            {
                mTotal = value * mPrice;
                mQuantity = value;
            }
        }
        private Double mTotal;
        public Double Total
        {
            get { return mTotal; }
            set
            {
                mPrice = value / mQuantity;
                mTotal = value;
            }
        }

        public OrderPart(Int32 itemID, Double price = 0, Int32 quantity = 1)
        {
            ItemID = itemID;
            mPrice = price;
            mQuantity = quantity;
            mTotal = price * quantity;
        }
        public static OrderPart RandomOrder()
        {
            return new OrderPart(Products.RandomID(), Math.Round(Program.rnd.NextDouble() * 501, 2), Program.rnd.Next(1,31));
        }
    };
}
