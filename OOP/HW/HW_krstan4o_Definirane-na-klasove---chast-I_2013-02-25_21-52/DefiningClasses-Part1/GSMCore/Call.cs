using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMCore
{
    public class Call  //Task 8 - write class call
    {
        private string dialedPhone;
        private long duration;
        private DateTime dateAndTime;

        public Call(DateTime dateAndTime, string dialedPhone, long duration)
        {
            this.DateAndTime = dateAndTime;
            this.DialedPhone = dialedPhone;
            this.Duration = duration;
            
        }

        public DateTime DateAndTime
        {
            get { return this.dateAndTime; }
            set 
            {
                this.dateAndTime = value; 
            }
        }

        public string DialedPhone
        {
            get { return this.dialedPhone; } 
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Please enter dialed phone number.");
                }
                this.dialedPhone = value;
            } 
        
        }

        public long Duration
        {
            get { return this.duration; }
            set 
            {
                if (value < 1 || value > 5000) 
                {
                    throw new ArgumentOutOfRangeException("Please enter a valid duration is secounds for the call.");
                }
                this.duration = value;
            }
        }
        public override string ToString() 
        {
            return string.Format("Call: {0}\nPhone: {1}\nDuration: {2}\n", this.dateAndTime, this.DialedPhone, this.Duration);
        }
    }
}
