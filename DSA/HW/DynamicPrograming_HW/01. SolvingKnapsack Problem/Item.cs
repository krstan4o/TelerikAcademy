using System;

namespace _01.KnapsackProblem
{
    public class Item
    {
        public string Name { get; set; }
        public int Weigth { get; set; }
        public int Value { get; set; }

        public Item(string name, int weigth, int value)
        {
            this.Name = name;
            this.Weigth = weigth;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Name + " -> w: " + this.Weigth + ", v: " + this.Value;
        }
    }
}