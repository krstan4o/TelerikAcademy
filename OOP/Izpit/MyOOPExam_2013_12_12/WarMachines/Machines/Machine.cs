using System;
using System.Collections.Generic;
using System.Text;
using WarMachines.Interfaces;

namespace WarMachines.Machines
{
    public abstract class Machine : IMachine, IComparable<Machine>
    {
        private string name;
        private double healthPoints;
        private double attackPoints;
        private double defensePoints;
        private IPilot pilot;

        private List<string> targets;

        public Machine(string name, double healthPoints, double attackPoints,
            double deffensePoints)
        {
            this.Name = name;
            this.HealthPoints = healthPoints;
            this.AttackPoints = attackPoints;
            this.DefensePoints = deffensePoints;
            this.targets = new List<string>();          
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new FormatException("Machine name cannot be null.");
                }
                this.name = value;
            }
        }

        public IPilot Pilot
        {
            get
            { 
                return this.pilot;
            }
            set
            {
                if (value == null)
                {
                    throw new FormatException("Machine pilot cannot be null.");
                }
                this.pilot = value;
            }
        }

        public double HealthPoints
        {
            get
            {
                return this.healthPoints;
            }
            set
            {
                if (value == null)
                {
                    throw new FormatException("Machine health cannot be null.");
                }
                this.healthPoints = value;
            }
        }

        public double AttackPoints
        {
            get
            {
                return this.attackPoints;
            }
            set
            {
                if (value == null)
                {
                    throw new FormatException("Machine attack cannot be null.");
                }
                this.attackPoints = value;
            }
        }

        public double DefensePoints
        {
            get
            {
                return this.defensePoints;
            }
            set
            {
                if (value == null)
                {
                    throw new FormatException("Machine defense cannot be null.");
                }
                this.defensePoints = value;
            }
        }

        public IList<string> Targets
        {
            get
            {
                return this.targets;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("- " + this.Name);
            sb.AppendLine(" *Type: " + this.GetType().Name);
            sb.AppendLine(" *Health: " + this.HealthPoints);
            sb.AppendLine(" *Attack: " + this.AttackPoints);
            sb.AppendLine(" *Defense: " + this.DefensePoints);
            sb.Append(" *Targets: ");
            if (this.Targets.Count <= 0)
            {
                sb.AppendLine("None");
            }
            else
            {
                for (int i = 0; i < this.Targets.Count; i++)
                {
                    if (i == this.Targets.Count - 1)
                    {
                        sb.AppendLine(this.Targets[i]);
                    }
                    else
                    {
                        sb.Append(this.Targets[i] + ", ");
                    }
                }
            }
           
            return sb.ToString();
        }
        
        public void Attack(string target)
        {
            this.Targets.Add(target);
        }

        public int CompareTo(Machine other)
        {
            int hpCompare = this.HealthPoints.CompareTo(other.HealthPoints);
            if (hpCompare == 0)
            {
                return this.Name.CompareTo(other.Name);
            }
            else
            {
                return hpCompare;
            }
        }
    }
}
