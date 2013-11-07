using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareAcademy
{
    public abstract class Course:ICourse
    {
        private string name;
        private ITeacher teacher;
        private List<string> topics;

        public Course(string name, ITeacher teacher)
        {
            this.Name = name;
            this.Teacher = teacher;
            this.topics = new List<string>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public ITeacher Teacher
        {
            get
            {
                return this.teacher;
            }
            set
            {
                this.teacher = value;
            }
        }

        public void AddTopic(string topic)
        {
            this.topics.Add(topic);
        }

        public override string ToString()
        {
            bool isLocalCourse = false;
            if (this.GetType() == typeof (LocalCourse))
            {
                isLocalCourse = true;
            }
            StringBuilder output = new StringBuilder();
            if (isLocalCourse)
            {
                output.Append("LocalCourse: ");
            }
            else
            {
                //OffsiteCourse
                output.Append("OffsiteCourse: ");
            }

            if (this.Name != null)
            {
                output.Append("Name=");
                output.Append(this.Name + "; ");    
            }

            if (this.Teacher != null)
            {
                if (!string.IsNullOrEmpty(this.Teacher.Name))
                {
                    output.Append("Teacher=");
                    output.Append(this.Teacher.Name + "; ");
                }
            }
            if (this.topics.Count > 0)
            {
                output.Append("Topics=[");
                for (int i = 0; i < this.topics.Count; i++)
                {
                    output.Append(this.topics[i]);
                    if (i < this.topics.Count - 1)
                    {
                        output.Append(", ");
                    }
                }
                output.Append("]; ");
            }

          

            return output.ToString();
        }
    }
}
