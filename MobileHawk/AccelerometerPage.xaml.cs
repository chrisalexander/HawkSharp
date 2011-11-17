using System;
using System.Windows;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;

namespace MobileHawk
{
    public partial class AccelerometerPage : PhoneApplicationPage
    {
        // Enabled flag
        private bool enabled = false;

        // Use the motion API
        private Motion m;

        // Output class
        private Output o;

        // Start orientation
        const int DEFAULT_ORIENTATION = 9001;
        const int WAIT_SAMPLES = 50;
        private int startOrientation = DEFAULT_ORIENTATION;
        private int startCount = 0;
        private PageOrientation orientation;

        // Current state
        private int state = States.OFF;

        // How far you have to turn left/right before you make a motion
        const int ZERO_WIDTH = 30;
        // How far forward/back you have to tilt before you make a motion
        const int ZERO_HEIGHT = 15;

        /**
         * States the robot motion can be in
         */
        class States
        {
            public const int OFF = 0;
            public const int FORWARD = 1;
            public const int BACKWARD = 2;
            public const int LEFT = 3;
            public const int RIGHT = 4;
        }

        /**
         * Constructor
         */
        public AccelerometerPage()
        {
            InitializeComponent();
            
            // Initialise the output
            o = new Output("192.168.0.12", 6200);
        }

        /**
         * Enable / disable button
         */
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (enabled)
            {
                enabled = false;
                button1.Content = "Enable";
                textBlock1.Text = "Hold phone vertically then press";
                stateTextBlock.Text = "Disabled";
                stop();
            }
            else
            {
                enabled = true;
                button1.Content = "Disable";
                textBlock1.Text = "To stop, press";
                stateTextBlock.Text = "Enabled";
                startOrientation = DEFAULT_ORIENTATION;
                start();
            }
        }

        /**
         * Start taking and processing accelerometer readings
         */
        private void start()
        {
            if (!Motion.IsSupported)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Motion API not supported"));
            }

            if (m == null)
            {
                m = new Motion();
                m.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<MotionReading>>(motionChange);
            }

            try
            {
                m.Start();
            }
            catch (Exception e)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Unable to init Motion API"));
            }
        }

        /**
         * Stop taking accelerometer readings
         */
        private void stop()
        {
            o.send("s");
            m.Stop();
            this.state = States.OFF;
        }

        /**
         * Handler for accelerometer readings
         */
        public void motionChange(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => handleMotionChange(e.SensorReading));
        }

        /**
         * Do something with the motion reading in the UI thread
         */
        public void handleMotionChange(MotionReading e)
        {
            if (m.IsDataValid)
            {
                // Round a bit
                int yaw = (int) MathHelper.ToDegrees(e.Attitude.Yaw);
                int pitch = (int)MathHelper.ToDegrees(e.Attitude.Pitch);
                int roll = (int)MathHelper.ToDegrees(e.Attitude.Roll);

                // Output
                yawTextBlock.Text = "Yaw: " + yaw.ToString("0") + "°";
                pitchTextBlock.Text = "Pitch: " + pitch.ToString("0") + "°";
                rollTextBlock.Text = "Roll: " + roll.ToString("0") + "°";

                // If it's just been started we need to put in a initial position
                if (startOrientation == DEFAULT_ORIENTATION)
                {
                    if (startCount < WAIT_SAMPLES)
                    {
                        startCount++;
                        return;
                    }
                    startOrientation = roll;
                    if (roll > 0)
                    {
                        orientation = PageOrientation.LandscapeRight;
                    }
                    else
                    {
                        orientation = PageOrientation.LandscapeLeft;
                    }
                }

                // Figure out what state we are going to move to - check u/d then l/r
                if (roll > startOrientation+ZERO_HEIGHT) 
                {
                    if (orientation == PageOrientation.LandscapeLeft)
                    {
                        if (state != States.FORWARD)
                        {
                            setState(States.FORWARD);
                        }
                    }
                    else
                    {
                        if (state != States.BACKWARD)
                        {
                            setState(States.BACKWARD);
                        }
                    }
                }
                else if (roll < startOrientation - ZERO_HEIGHT)
                {
                    if (orientation == PageOrientation.LandscapeLeft)
                    {
                        if (state != States.BACKWARD)
                        {
                            setState(States.BACKWARD);
                        }
                    }
                    else
                    {
                        if (state != States.FORWARD)
                        {
                            setState(States.FORWARD);
                        }
                    }
                }
                else if (pitch > ZERO_WIDTH)
                {
                    if (orientation == PageOrientation.LandscapeLeft)
                    {
                        if (state != States.RIGHT)
                        {
                            setState(States.RIGHT);
                        }
                    }
                    else
                    {
                        if (state != States.LEFT)
                        {
                            setState(States.LEFT);
                        }
                    }
                }
                else if (pitch < -ZERO_WIDTH)
                {
                    if (orientation == PageOrientation.LandscapeLeft)
                    {
                        if (state != States.LEFT)
                        {
                            setState(States.LEFT);
                        }
                    }
                    else
                    {
                        if (state != States.RIGHT)
                        {
                            setState(States.RIGHT);
                        }
                    }
                }
                else 
                {
                    if (state != States.OFF)
                    {
                        setState(States.OFF);
                    }
                }
            }
        }

        /**
         * Sets the state it is in
         */
        private void setState(int state)
        {
            this.state = state;
            string message = "Off";
            switch (state)
            {
                case States.FORWARD:
                    message = "Forward";
                    o.send("f");
                    break;
                case States.BACKWARD:
                    message = "Backward";
                    o.send("b");
                    break;
                case States.LEFT:
                    message = "Left";
                    o.send("l");
                    break;
                case States.RIGHT:
                    message = "Right";
                    o.send("r");
                    break;
                default:
                    o.send("s");
                    break;
            }
            message = "State: " + message;
            stateTextBlock.Text = message;
        }

        /**
         * Back button
         */
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (enabled)
            {
                this.button1_Click(sender, e);
            }
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        /**
         * Emergency stop
         */
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            o.send("s");
            enabled = true;
            this.button1_Click(sender, e);
        }
    }
}