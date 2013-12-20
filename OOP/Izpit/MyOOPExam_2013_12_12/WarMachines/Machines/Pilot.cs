using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarMachines.Interfaces;

namespace WarMachines.Machines
{
    public class Pilot:IPilot
    {
        private List<Machine> machines;
        private string name;

        public Pilot(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new FormatException("Pilot name cannot be null.");
            }
            this.name = name;
            this.machines = new List<Machine>();
        }
        public string Name 
        {
            get { return this.name; }           
        }

        public void AddMachine(IMachine machine)
        {
            var machinee = (Machine)machine;
            if (machinee != null)
            {
                this.machines.Add(machinee);

                this.machines.Sort();
            }
           
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Name + " - ");
            if (this.machines.Count == 0)
            {
                sb.Append("no machines");
            }
            else if (this.machines.Count == 1)
            {
                 sb.AppendLine("1 machine");
            }
            else
            {
                sb.AppendLine(this.machines.Count + " machines");
            }

            foreach (var machine in machines)
            {
                sb.Append(machine.ToString());
            }
            return sb.ToString();
        }
    }
}
