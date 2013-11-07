using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareAcademy
{
    public class Teacher: ITeacher
    {
        private string name;
        private List<ICourse> teacherCources;
        public Teacher(string name)
        {
            this.Name = name;
            this.teacherCources = new List<ICourse>();
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

        public void AddCourse(ICourse course)
        {
            this.teacherCources.Add(course);
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append("Teacher: ");
            if (this.Name != null)
            {
                output.Append("Name=");
                output.Append(this.Name);
               
            }
            if (this.teacherCources.Count > 0)
            {
                output.Append("; "); 
                output.Append("Courses=[");
                for (int i = 0; i < this.teacherCources.Count; i++)
                {
                    output.Append(this.teacherCources[i].Name);
                    if (i < this.teacherCources.Count - 1)
                    {
                        output.Append(", ");
                    }
                }
                output.Append("]");
            }
            
            return output.ToString();
        }
    }
}
