using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcademyRPG
{
    public class EngineExtended:Engine
    {
        public EngineExtended() : base() { }

        public override void ExecuteCreateObjectCommand(string[] commandWords)
        {
            switch (commandWords[1])
            {
                case "knight": 
                    string name = commandWords[2];
                    Point position = Point.Parse(commandWords[3]);
                    int owner = int.Parse(commandWords[4]);
                    this.AddObject(new Knight(name, position, owner));
                    break;
                case "house":
                    position = Point.Parse(commandWords[2]);
                    owner = int.Parse(commandWords[3]);
                    this.AddObject(new House(position, owner));
                    break;
                case "giant":
                    name = commandWords[2];
                    position = Point.Parse(commandWords[3]);
                    this.AddObject(new Giant(name, position));
                    break;
                case "rock":
                    position = Point.Parse(commandWords[3]);
                    int hitPoints = int.Parse(commandWords[2]);
                    this.AddObject(new Rock(hitPoints, position));
                    break;
                case "ninja":
                     name = commandWords[2];
                     position = Point.Parse(commandWords[3]);
                     owner = int.Parse(commandWords[4]);
                     this.AddObject(new Ninja(name, position, owner));
                     break;
            }
            base.ExecuteCreateObjectCommand(commandWords);
        }
    }
}
