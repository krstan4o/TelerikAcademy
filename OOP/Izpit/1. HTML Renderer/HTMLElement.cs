using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLRenderer
{
    public class HTMLElement : IElement
    {
        private string name;
        private string textContent;
        private List<IElement> childElements;

        public HTMLElement(string name = null)
        {
            this.Name = name;
            this.childElements = new List<IElement>();
        }

        public HTMLElement(string name = null, string textContent = null)
        {
            this.Name = name;
            this.TextContent = textContent;
            this.childElements = new List<IElement>();
        }

        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }

        public string TextContent
        {
            get
            {
                return this.textContent;
            }
            set
            {
                this.textContent = value;
            }
        }

        public IEnumerable<IElement> ChildElements
        {
            get { return this.childElements; }
        }

        public void AddElement(IElement element)
        {
            this.childElements.Add(element);
        }

        public void Render(StringBuilder output)
        {
            foreach (var item in this.ChildElements)
            {
                output.Append(item);
            }
        }

        public override string ToString()
        {
            bool hasName = false;
            if (this.Name != null)
            {
                hasName = true;
            }

            StringBuilder output = new StringBuilder();
           
            if (hasName)
            {
                output.Append("<");
                output.Append(this.Name);
                output.Append(">");
            }
            
            if (this.TextContent != null)
            {
                foreach (char ch in this.TextContent)
                {
                    switch (ch)
                    {
                        case '<': output.Append("&lt;");
                            break;
                        case '>': output.Append("&gt;");
                            break;
                        case '&': output.Append("&amp;");
                            break;
                        default:
                            output.Append(ch);
                            break;
                    }
                }
            }
            this.Render(output);

            if (hasName)
            {
                output.Append("</");
                output.Append(this.Name);
                output.Append(">");
            }
            
            return output.ToString();
        }
    }
}
