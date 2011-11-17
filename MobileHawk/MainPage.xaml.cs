using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MobileHawk
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Output class
        private Output o;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            o = new Output("192.168.0.12", 6200);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            o.send("f");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            o.send("s");
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            o.send("l");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            o.send("r");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            o.send("b");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AccelerometerPage.xaml", UriKind.Relative));
        }
    }
}