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

namespace HawkClient
{
    public partial class MainWindow : Window
    {
        // The client
        private Client c;

        // Whether a key is down
        private bool keyactive = false;
        // The last key pressed
        private string lastKey = "";

        /**
         * Constructor - startup threads
         */
        public MainWindow()
        {
            InitializeComponent();

            c = new Client("192.168.0.12", 6200);

            this.KeyDown += new KeyEventHandler(keydown);
            this.KeyUp += new KeyEventHandler(keyup);
        }

        /**
         * Destructor - cleanup threads
         */
        ~MainWindow()
        {
            c.stop();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            c.send("s");
        }

        /**
         * Handles key down
         */
        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && lastKey != "l")
            {
                c.send("l");
                keyactive = true;
                lastKey = "l";
            }
            else if (e.Key == Key.Right && lastKey != "r")
            {
                c.send("r");
                keyactive = true;
                lastKey = "r";
            }
            else if (e.Key == Key.Up && lastKey != "f")
            {
                c.send("f");
                keyactive = true;
                lastKey = "f";
            }
            else if (e.Key == Key.Down && lastKey != "b")
            {
                c.send("b");
                keyactive = true;
                lastKey = "b";
            }
        }

        /**
         * Handles key up
         */
        private void keyup(object sender, KeyEventArgs e)
        {
            if (keyactive)
            {
                c.send("s");
                keyactive = false;
                lastKey = "";
            }
        }
    }
}
