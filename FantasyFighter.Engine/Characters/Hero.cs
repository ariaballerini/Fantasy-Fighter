using FantasyFighter.Engine;
using FantasyFighter.Engine.Engine;
using FantasyFighter.Engine.Items.Attack;
using FantasyFighter.Engine.Items.Consumable;
using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Characters
{
    public class Hero : Character, ICanAttack, ICanDefend
    {
        public string SpecialMove { get; set; }
        public int SpecialMovePoints { get; set; }

        public Inventory Inventory { get; private set; } = new Inventory();

        public Hero(string name, int health, int attackPoints, int defensePoints, string specialMove, int specialMovePoints)
             : base(name, health, attackPoints, defensePoints)
        {
            SpecialMove = specialMove;
            SpecialMovePoints = specialMovePoints;
        }

        // TODO: Weapons deal different damages to different monster?
        // When calculating the damage points taken by an enemy, you could take into account the type of enemy.
        // For instance, some mosters could be weaker or more resistant against some type of weapons.
        // A dragon could take less damages from swords and knifes, and suffer more damages from magical attacks.
        // In this case, you might need to modify the method to accept an enemy as parameter. Then you need to check
        // what kind of enemy you are facing (is it a Goblin or a Dragon?) to determine the effects of the main weapon.

        public virtual int Defend()
        {
            //calculate the hero's defense points
            //taking into account all the available modifiers

            int fullDefense;

            var protectingGears = this.Inventory.Items.OfType<IDefence>();

            int defenceBonus = 0;

            if (protectingGears.Any())
            {
                foreach(var gear in protectingGears)
                {
                    Console.WriteLine($"\n(You're using {gear.GetType().Name} to protect yourself)");
                    defenceBonus += gear.DefenseBonus;
                }
            }

            fullDefense = this.DefencePoints + defenceBonus;
            
            return fullDefense;
        }

        public virtual int Attack(bool extra)
        {
            //The player attacks the enemy using the primary weapon.
            //Calculate the player's damage points based on:
            // - initial value of attack points at instance creation (connects to Hero integer AttackPoints but..)
            // - Any modifier coming from the role (..takes the AttackPoints directly from the chosen role)
            // - Any modifier coming from consumable item (not implemented for attack)
            // - Any modifier coming from the primary weapon (weaponBonus based on the chosen role)
            // - Something else? => special move based on the chosen role

            int weaponBonus = this.Inventory.MainWeapon!.AttackBonus;
            int fullAttack;

            if (extra)
            {
                fullAttack = this.AttackPoints + weaponBonus + this.SpecialMovePoints;
            }

            else
            {
                fullAttack = this.AttackPoints + weaponBonus;
            }

            return fullAttack;
        }

        public void Consume<T>() where T : Consumable, new()
        {
            var item = this.Inventory.Items.OfType<T>().FirstOrDefault();

            if (item is null)
                Console.WriteLine("The item you are looking for is not in the Inventory");
            else
            {
                // Check if the item implements the IHeal interface
                if (item is IHeal healingItem)
                {
                    this.Health += healingItem.HealingPoints;
                    
                    Console.WriteLine($"Your health has been increased by {healingItem.HealingPoints} points. Now your health is {this.Health} points");
                }

                // remove the item from the inventory
                this.Inventory.Items.Remove(item);
            }
        }

        public void InspectInventory()
        {
            Console.WriteLine("You have the following items in your inventory:"); //prints the list of items
            foreach (var item in this.Inventory.Items)
            {
                Console.Write($"{item.GetType().Name}  ");
            }

            Console.Write($"\nMain Weapon: {this.Inventory.MainWeapon!.GetType().Name}"); //prints the name of the main weapon

            if (this.Inventory.SecondaryWeapon is not null)
                Console.Write($" - Secondary Weapon: {this.Inventory.SecondaryWeapon.GetType().Name}"); //prints the name of the secondary weapon
        }

        internal void ChangeWeapons()
        {
            if (this.Inventory.SecondaryWeapon is null)
                Console.WriteLine("You do not own a secondary weapon.");
            else
            {
                this.Inventory.SwitchWeapons();
                Console.WriteLine("Weapons switched.");
            }
        }
    }
}
