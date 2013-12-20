using System;
using System.Collections.Generic;
using System.Text;
using WarMachines.Interfaces;

namespace WarMachines.Machines
{
    public class Tank:Machine,ITank
    {
        private bool isInDefense;
        public Tank(string name,  double attackPoints, double deffensePoints)
            :base( name,  100, attackPoints, 
            deffensePoints)
        {
            this.isInDefense = true;
            this.DefensePoints += 30;
            this.AttackPoints -= 40;
        }

        public bool DefenseMode
        {
            get { return this.isInDefense; }
        }

        public void ToggleDefenseMode()
        {
            if (this.isInDefense)
            {
                this.isInDefense = false;
                this.AttackPoints += 40;
                this.DefensePoints -= 30;
            }
            else
            {
                this.isInDefense = true;
                this.AttackPoints -= 40;
                this.DefensePoints += 30;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.Append(" *Defense: ");
            if (this.DefenseMode)
            {
                sb.AppendLine("ON");
            }
            else
            {
                sb.AppendLine("OFF");
            }
            return sb.ToString();
        }
    }
}
