using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HawkSharp.Motor;

namespace MotorUI
{
    public partial class MainWindow : Window
    {
        private MotorHawk hawk;
        private bool keyactive = false;

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(keydown);
            this.KeyUp += new KeyEventHandler(keyup);

            hawk = new MotorHawk(1);
            hawk.stop();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            hawk.forwards();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            hawk.left();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            hawk.right();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            hawk.backwards();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            hawk.stop();
        }

        /**
         * Handles key down
         */
        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                hawk.left();
                keyactive = true;
            }
            else if (e.Key == Key.Right)
            {
                hawk.right();
                keyactive = true;
            }
            else if (e.Key == Key.Up)
            {
                hawk.forwards();
                keyactive = true;
            }
            else if (e.Key == Key.Down)
            {
                hawk.backwards();
                keyactive = true;
            }
        }

        /**
         * Handles key up
         */
        private void keyup(object sender, KeyEventArgs e)
        {
            if (keyactive)
            {
                hawk.stop();
                keyactive = false;
            }
        }
    }
}
