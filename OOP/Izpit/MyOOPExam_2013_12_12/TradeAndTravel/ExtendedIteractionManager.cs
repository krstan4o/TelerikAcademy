using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeAndTravel
{
    public class ExtendedIteractionManager:InteractionManager
    {      
        private void HandleCraftInteraction(string[] commandWords, Person actor)
        {
            string typeToCraft = commandWords[2];
            string newItemName = commandWords[3];
          
            switch (typeToCraft)
            {
                case "weapon": this.CraftWeapon(newItemName, actor);
                    break;
                case "armor":this.CraftArmor(newItemName, actor);
                    break;
                default:
                    break;
            }
           
        }
      
        private void CraftWeapon(string newItemName, Person actor)
        {
            var ironItems = actor.ListInventory().Where(x => x.ItemType == ItemType.Iron);
            var woodItems = actor.ListInventory().Where(x => x.ItemType == ItemType.Wood);
            if (ironItems != null && woodItems != null && ironItems.Count() > 0 && woodItems.Count() > 0)
            {
                var item = new Weapon(newItemName, null);
                actor.AddToInventory(item);
                ownerByItem.Add(item, actor);
                item.UpdateWithInteraction("craft");
            }                       
        }
        private void CraftArmor(string newItemName, Person actor) 
        {
            var ironItems = actor.ListInventory().Where(x => x.ItemType == ItemType.Iron);          
            if (ironItems != null && ironItems.Count() > 0 )
            {
               
                var item = new Armor(newItemName, null);
                actor.AddToInventory(item);
                ownerByItem.Add(item, actor);
                item.UpdateWithInteraction("craft");
            }         
        }        

        protected override Person CreatePerson(string personTypeString, string personNameString, Location personLocation)
        {
            Person person = null;
            switch (personTypeString)
            {
                case "merchant":
                    person = new Merchant(personNameString, personLocation);
                    break;
                default: return base.CreatePerson(personTypeString, personNameString, personLocation);                  
            }
            return person;
        }

        protected override Location CreateLocation(string locationTypeString, string locationName)
        {
            Location location = null;
            switch (locationTypeString)
            {
                case "mine":
                    location = new Mine(locationName);
                    break;
                case "forest":
                    location = new Forest(locationName);
                    break;
                default: return base.CreateLocation(locationTypeString, locationName);            
            }
            return location;
           
        }

        protected override Item CreateItem(string itemTypeString, string itemNameString, Location itemLocation, Item item)
        {
            switch (itemTypeString)
            {
                case "weapon":
                    item = new Weapon(itemNameString, itemLocation);
                    break;
                case "wood":
                    item = new Wood(itemNameString, itemLocation);
                    break;
                case "iron":
                    item = new Iron(itemNameString, itemLocation);
                    break;
                default: return base.CreateItem(itemTypeString, itemNameString, itemLocation, item);
            }
            return item;
            
        }

        private void HandleGatherInteraction(string[] commandWords, Person actor)
        {
            if (actor.Location.LocationType == LocationType.Forest)
            {
                var inventoryItems = actor.ListInventory().Where(x => x.ItemType == ItemType.Weapon);
                if (inventoryItems != null && inventoryItems.Count() > 0)
                {
                    var item = new Wood(commandWords[2], null);
                    actor.AddToInventory(item);
                    ownerByItem.Add(item, actor);
                    item.UpdateWithInteraction("gather");
                }
            }
            else if (actor.Location.LocationType == LocationType.Mine)
            {
                var inventoryItems = actor.ListInventory().Where(x => x.ItemType == ItemType.Armor);
                if (inventoryItems != null && inventoryItems.Count() > 0)
                {
                    var item = new Iron(commandWords[2], null);
                    actor.AddToInventory(item);
                    ownerByItem.Add(item, actor);
                }
            }

        }
        protected override void HandleItemCreation(string itemTypeString, string itemNameString, string itemLocationString)
        {
            var itemLocation = locationByName[itemLocationString];

            Item item = null;
            item = CreateItem(itemTypeString, itemNameString, itemLocation, item);

            ownerByItem[item] = null;
            strayItemsByLocation[itemLocation].Add(item);
          // base.HandleItemCreation(itemTypeString, itemNameString, itemLocationString);
        }
        protected override void HandlePersonCommand(string[] commandWords, Person actor)
        {
            switch (commandWords[1])
            {
                case "gather":
                    HandleGatherInteraction(commandWords, actor);
                    break;
                case "craft": HandleCraftInteraction(commandWords, actor);
                    break;
                default:
                    break;
            }
            base.HandlePersonCommand(commandWords, actor);
        }

        protected override void HandlePersonCreation(string personTypeString, string personNameString, string personLocationString)
        {
            var personLocation = locationByName[personLocationString];

            Person person = CreatePerson(personTypeString, personNameString, personLocation);

            personByName[personNameString] = person;
            peopleByLocation[personLocation].Add(person);
            moneyByPerson[person] = 100;
            base.HandlePersonCreation(personTypeString, personNameString, personLocationString);
        }

        protected override void HandleLocationCreation(string locationTypeString, string locationName)
        {
            Location location = CreateLocation(locationTypeString, locationName);

            locations.Add(location);
            strayItemsByLocation[location] = new List<Item>();
            peopleByLocation[location] = new List<Person>();
            locationByName[locationName] = location;
           // base.HandleLocationCreation(locationTypeString, locationName);
        }
    }
}
