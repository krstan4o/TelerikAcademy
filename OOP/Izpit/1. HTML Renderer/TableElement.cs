using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLRenderer
{
    public class TableElement:ITable
    {
        private string name;
        private string textContent;
        private IElement[,] elements;
        private int rows;
        private int cols;

        public TableElement(int rows, int cols)
        {
            this.Name = "table";
            this.Rows = rows;
            this.Cols = cols;
            this.elements = new IElement[rows, cols];
        }

        public int Rows
        {
            get { return this.rows; }
            private set { this.rows = value; }
        }

        public int Cols
        {
            get { return this.cols; }
            private set { this.cols = value; }
        }

        public IElement this[int row, int col]
        {
            get
            {
                return this.elements[row, col];
            }
            set
            {
                this.elements[row, col] = value;
            }
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
                this.textContent = null;
            }
        }

        public IEnumerable<IElement> ChildElements
        {
            get { return null; }
        }

        public void AddElement(IElement element)
        {
            throw new NotImplementedException();
        }

        public void Render(StringBuilder output)
        {
            for (int row = 0; row < this.Rows; row++)
            {
                output.Append("<tr>");
                for (int col = 0; col < this.Cols; col++)
                {
                    output.Append("<td>");
                    output.Append(this.elements[row, col]);
                    output.Append("</td>");
                }
                output.Append("</tr>");
            }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.Append("<table>");
            this.Render(output);
            output.Append("</table>");

            return output.ToString();
        }
    }
}
