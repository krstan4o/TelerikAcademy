using System;

namespace GSMCore
{
    public class Battery    //Task 1 - Define class Battery in GSM class
    {
        private Type model;
        private double hoursIdle;
        private double hoursTalk;
        public enum Type // Task 3 - Add enum type field in the battery class...
        {
            LiIon, NiMH, NiCD
        }

        public Battery()
        {
        }

        public Battery(Type model, double hoursIdle, double hoursTalk)
        {
            this.Model = model;
            this.HoursIdle = hoursIdle;
            this.HoursTalk = hoursTalk;
        }

        public Type Model {get;set; }
       

        public double HoursIdle
        {
            get { return hoursIdle; }
            set 
            {
                if (value < 48 || value > 800)
                {
                    throw new ArgumentOutOfRangeException("Please enter hours idle for the battery betwean 48 and 150");
                }
                hoursIdle = value;
            }
        }

        public double HoursTalk
        {
            get { return hoursTalk; }
            set 
            {

                if (value < 3 || value > 150)
                {
                    throw new ArgumentOutOfRangeException("Please enter hours talk for the battery betwean 3 and 150");
                }
                hoursTalk = value;
            }
        }
    }
}
