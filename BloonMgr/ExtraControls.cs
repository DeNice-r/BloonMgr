using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace BloonMgr
{
    public class XTextBox : StackPanel
    {
        TextBox textBox = new TextBox();
        TextBlock hint = new TextBlock();

        public String Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public String HintText {
            get
            {
                return hint.Text;
            }
            set
            {
                hint.Text = value;
            }
        }

        public double FontSize
        {
            get
            {
                return textBox.FontSize;
            }
            set
            {
                textBox.FontSize = value;
                hint.FontSize = value;
            }
        }

        public TextWrapping TextWrapping
        {
            get
            {
                return textBox.TextWrapping;
            }
            set
            {
                textBox.TextWrapping = value;
            }
        }

        public XTextBox()
        {
            Children.Add(textBox); Children.Add(hint);
            hint.Width = textBox.Width = Width;
            hint.Height = textBox.Height = Height;
            textBox.TextChanged += XTB_TextChanged;
        }

        private void XTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox.Text.Length > 0)
                hint.Visibility = System.Windows.Visibility.Visible;
            else
                hint.Visibility = Visibility.Hidden;
        }
    }
}
