using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using HawkSharp.Exceptions;

namespace HawkSharp.Motor
{
    public class MotorHawk : Hawk
    {
        [DllImport(HAWK_DLL_PATH)]
        static extern int Sys_GetMotorHawkCount();

        [DllImport(HAWK_DLL_PATH)]
        static extern int Motor_SetDCMotors(int board, int m1speed, int m1dir, int m2speed, int m2dir);

        /**
         * Motor identification constants
         */
        public const int MOTOR_LEFT = 0;
        public const int MOTOR_RIGHT = 1;

        /**
         * Motor speed constants
         */
        public const int MAX_MOTOR_SPEED = 250;

        /**
         * Current motor speeds
         */
        private int speed_left = 0;
        private int speed_right = 0;

        private int mh;
        public int motorhawk
        {
            get
            {
                return mh;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("MotorHawk selection must be greater than 0");
                }
                if (value > Sys_GetMotorHawkCount())
                {
                    throw new InsufficientMotorHawksException("There are insufficient MotorHawks currently connected");
                }
                mh = value;
            }
        }

        /**
         * Constructor checks the number of motor hawks connected
         */
        public MotorHawk(int board) : base()
        {
            int count = Sys_GetMotorHawkCount();
            if (count < 1)
            {
                throw new InsufficientMotorHawksException("There are no MotorHawks connected");
            }
            motorhawk = board;
            Motor_SetType(motorhawk, Hawk.TYPE_MOTOR);
        }

        /**
         * Set a single motor's speeds
         */
        public void setMotor(int motor, int speed)
        {
            checkMotor(motor);
            checkMotorSpeed(speed);

            if (motor == MOTOR_LEFT)
            {
                this.setMotors(speed, this.speed_left);
            }
            else if (motor == MOTOR_RIGHT)
            {
                this.setMotors(this.speed_right, speed);
            }
        }

        /**
         * Set both motor's speeds simultaneously
         */
        public void setMotors(int leftspeed, int rightspeed)
        {
            checkMotorSpeed(leftspeed);
            checkMotorSpeed(rightspeed);

            this.speed_left = leftspeed;
            this.speed_right = rightspeed;

            int leftdirection = 1;
            int rightdirection = 1;
            if (leftspeed == 0)
            {
                leftdirection = 0;
            }
            else if (leftspeed < 0)
            {
                leftspeed = -leftspeed;
                leftdirection = 2;
            }
            if (rightspeed == 0)
            {
                rightdirection = 0;
            }
            else if (rightspeed < 0)
            {
                rightspeed = -rightspeed;
                rightdirection = 2;
            }

            Motor_SetDCMotors(this.motorhawk, rightspeed, rightdirection, leftspeed, leftdirection);
        }

        /**
         * Makes both wheels go maximum forwards
         */
        public void forwards()
        {
            this.setMotors(MAX_MOTOR_SPEED, MAX_MOTOR_SPEED);
        }

        /**
         * Maximum reverse both wheels
         */
        public void backwards()
        {
            this.setMotors(-MAX_MOTOR_SPEED, -MAX_MOTOR_SPEED);
        }

        /**
         * Full stop, Mr. Data
         */
        public void stop()
        {
            this.setMotors(0, 0);
        }

        /**
         * Hard left
         */
        public void left()
        {
            this.setMotors(-MAX_MOTOR_SPEED, MAX_MOTOR_SPEED);
        }

        /**
         * Hard right
         */
        public void right()
        {
            this.setMotors(MAX_MOTOR_SPEED, -MAX_MOTOR_SPEED);
        }

        /**
         * Helper for validating motor speed input
         */
        private void checkMotorSpeed(int speed)
        {
            if (speed < -MAX_MOTOR_SPEED || speed > MAX_MOTOR_SPEED)
            {
                throw new MotorSpeedOutOfRangeException("Speed must be of magnitude " + MAX_MOTOR_SPEED.ToString() + " or less");
            }
        }

        /**
         * Checks that the motor parameter is valid
         */
        private void checkMotor(int motor)
        {
            if (motor != MOTOR_LEFT && motor != MOTOR_RIGHT)
            {
                throw new MotorOutOfRangeException("Motor must be a valid value");
            }
        }
    }
}
