using System;

namespace _03.FindStudentFirstName
{
   public class Student
    {
        private string fName;
        private string lName;
        private byte age;
        public Student(string fName, string lName): this(fName, lName, 0)
        {
        
        }

        public Student(string fName, string lName, byte age)
        {
            this.age = age;
            this.fName = fName;
            this.lName = lName;
        }

        public byte Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }

        public string LName
        {
            get
            {
                return this.lName;
            }
            set
            {
                this.lName = value;
            }
        }

        public string FName
        {
            get
            {
                return this.fName;
            }
            set
            {
                this.fName = value;
            }
        }

        public override string ToString()
        {
            return this.fName + " " + this.lName + " Age: " + age;
        }


    }
}