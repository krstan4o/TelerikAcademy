using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyRPG
{
    public class Ninja: Character, IFighter, IGatherer
    {
        private int atackPoints;

        public Ninja(string name, Point position, int owner):base(name,position,owner)
        {
            this.HitPoints = 1;
            this.AttackPoints = 0;
        }

        public int AttackPoints
        {
            get { return atackPoints; }
            private set { this.atackPoints = value; }
        }

        public int DefensePoints
        {
            get { return int.MaxValue; }
        }

        public int GetTargetIndex(List<WorldObject> availableTargets)
        {
            if (availableTargets != null)
            {
                if (availableTargets.Count > 0)
                {
                    int maxHitpoints = availableTargets.Max(x => x.HitPoints);
                    for (int i = 0; i < availableTargets.Count; i++)
                    {
                        if (availableTargets[i].HitPoints == maxHitpoints && availableTargets[i].Owner != 0
                            && availableTargets[i].Owner != this.Owner)
                        {
                            return i;
                        }
                    }
                }
               
            }
          
            return -1;
        }

        public bool TryGather(IResource resource)
        {
            if (resource.Type == ResourceType.Stone)
            {
                this.AttackPoints += resource.Quantity * 2;
                return true;
            }
            else if (resource.Type == ResourceType.Lumber)
            {
                this.AttackPoints += resource.Quantity;
                return true;
            }
            return false;
        }
    }
}
