using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMCore
{
    public class GSM //Task 1 - Define class GSM
    {
        static readonly GSM iphone4s = new GSM("Apple", "iPhone-4S", 420, "Owner", new Battery(Battery.Type.LiIon,300, 14),
           new Display(3.5, 200000));// Task 6 - Static field Iphone4S
        private string model;
        private string manufacturer;
        private int price;
        private string owner;
        private Battery battery;
        private readonly Display display;
        private  List<Call> callHistory=new List<Call>();   //Task 9 - write proparty Call history to store List of Calls

       

        public GSM(string manufacturer, string model)   //Task 2 - define constructors
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.battery = null;
            this.owner = null;
            this.display = null;
        }


        public GSM(string manufacturer, string model, int price, string owner, Battery battery, Display display)    //Task 2 - define constructors
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Price = price;
            this.Owner = owner;
            this.battery = battery;
            this.display = display;
        }

        public string Manufacturer     //Task 5 - Encapsulate the fields using propertyes
        {
            get
            {
                return this.manufacturer;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Please enter GSM manufacturer.");
                }
                this.manufacturer = value;
            }
        }

        public string Model     //Task 5 - Encapsulate the fields using propertyes
        {
            get
            {
                return this.model;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Please enter GSM model.");
                }
                this.model = value;
            }
        }

        public int Price     //Task 5 - Encapsulate the fields using propertyes
        {
            get
            {
                return this.price;
            }
            set
            {
                 if (value < 10 || value > 3000)
                 {
                     throw new ArgumentException("Please enter a real price.");
                 }
                 this.price = value;
            }
        }

        public string Owner     //Task 5 - Encapsulate the fields using propertyes
        {
            get
            {
                return this.owner;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Please enter GSM owner name.");
                }
                else
                {
                        foreach (var item in value)
                        {
                            if (char.IsDigit(item) || char.IsPunctuation(item)) 
                            {
                                throw new ArgumentException("Invalid owner please enter firstName secoundName");
                            }
                        }
                }
                this.owner = value;
            }
        }

        public List<Call> CallHistory
        {
           get { return this.callHistory; }
        }


        public static GSM IPhone4S {get{return iphone4s;}}   // Task 6 - Static property Iphone4S
       
  
      
        public override string ToString() // Task 4 - overide ToString()
        {
            if (this.battery != null && this.display != null && this.owner != null)
            {
                return string.Format("GSM of {0}\nManufacturer: {1}\nModel: {2}\nDisplay\nSize: {3} \"\nCollors: {4} colors\n" +
                "Battery\nBattery type: {5}\nHours Talk: {6}\nHours Idle: {7}\n\nPrice: {8} $\n"
                , this.owner.ToString(), this.manufacturer, this.model, this.display.Size, this.display.Colors, this.battery.Model,
                this.battery.HoursTalk, this.battery.HoursIdle, this.price);
            }
            else if (this.owner == null && this.battery != null)
            {
                return string.Format("Info about IPhone-4S\nManufacturer: {0}\nModel: {1}\nDisplay\nSize: {2} \"\nCollors: {3} colors\n" +
                "Battery\nBattery type: {4}\nHours Talk: {5}\nHours Idle: {6}\n\nPrice: {7} $\n", this.manufacturer, this.model, this.display.Size, this.display.Colors, this.battery.Model,
                this.battery.HoursTalk, this.battery.HoursIdle, this.price);
            }
            else
            {
                return string.Format("GSM\nManufacturer: {0}\nModel: {1}\n", manufacturer, model);
            }
        }

        public void AddCall(string number,long duration,int year,int month,int day,int hour,int minute,int secound) //Task 10 - Add Method to add calls in the call history
        {
            callHistory.Add(new Call(new DateTime(year,month,day,hour,minute,secound),number,duration));
        }

        public void DeleteCall(Call call)   //Task 10 - Delete Method to remove calls in the call history
        {
            callHistory.Remove(call);
        }

        public void ClearCallHistory()  //Task 10 - Add Method to clear the call history
        {
            callHistory.Clear();
        }

        public decimal TotalCallsPrice(decimal pricePerMinute)  //Task 11 - Add Method for calculate the total price of calls
        {                                                       //with a parameter for price per minute.
            decimal sum = 0;
            foreach (var item in callHistory)
            {
                sum += item.Duration;
            }
            decimal result = sum / 60;
            return result * pricePerMinute;
        }
    }
}
