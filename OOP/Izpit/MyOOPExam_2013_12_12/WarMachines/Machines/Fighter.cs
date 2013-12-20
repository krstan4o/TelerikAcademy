using System;
using System.Collections.Generic;
using System.Text;
using WarMachines.Interfaces;

namespace WarMachines.Machines
{
    public class Fighter:Machine, IFighter
    {
        private bool isInStelth;
        public Fighter(string name, double attackPoints, double deffensePoints, bool stelth)
            :base(name, 200,attackPoints,deffensePoints)
        {
            this.isInStelth = stelth;
        }

        public bool StealthMode
        {
            get { return this.isInStelth; }
        }

        public void ToggleStealthMode()
        {
            if (this.isInStelth)
            {
                this.isInStelth = false;
            }
            else
            {
                this.isInStelth = true;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" *Stealth: ");
            if (this.StealthMode)
            {
                sb.Append("ON");
            }
            else
            {
                sb.Append("OFF");
            }
            return sb.ToString();
        }
    }
}
