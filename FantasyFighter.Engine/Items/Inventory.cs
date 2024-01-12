using FantasyFighter.Engine.Items.Attack;
using FantasyFighter.Engine.Items.Consumable;
using FantasyFighter.Engine.Items.Defense;

namespace FantasyFighter.Items
{
    public class Inventory
    {
        public Weapon? MainWeapon { get; private set; }

        public Weapon? SecondaryWeapon { get; private set; }

        public List<Item> Items { get; private set; } = new List<Item>();

        public void SwitchWeapons()
        {
            var tmp = this.MainWeapon;

            this.MainWeapon = this.SecondaryWeapon;

            this.SecondaryWeapon = tmp;
        }

        internal void SetupForWarrior()
        {
            this.MainWeapon = new Sword();
            this.SecondaryWeapon = new Knife();

            this.Items.Add(new Armour());
            this.Items.Add(new Potion());
            this.Items.Add(new Bandage());
            this.Items.Add(new Food());
        }

        internal void SetupForCleric()
        {
            this.MainWeapon = new Knife();
            
            this.Items.Add(new Amulet());
            this.Items.Add(new Bandage());
            this.Items.Add(new Bandage());
            this.Items.Add(new Food());
        }

        internal void SetupForRogue()
        {
            this.MainWeapon = new DoubleKnife();
            this.SecondaryWeapon = new Knife();

            this.Items.Add(new ShadowCloak());
            this.Items.Add(new Potion());
            this.Items.Add(new Bandage());
            this.Items.Add(new Food());
        }

        internal void SetupForBard()
        {
            this.MainWeapon = new Bagpipe();
            this.SecondaryWeapon = new Knife();

            this.Items.Add(new Harp());
            this.Items.Add(new Bandage());
            this.Items.Add(new Bandage());
            this.Items.Add(new Food());
        }

        internal void SetupForWitch()
        {
            this.MainWeapon = new Wand();

            this.Items.Add(new SmokeBomb());
            this.Items.Add(new Potion());
            this.Items.Add(new Potion());
            this.Items.Add(new Food());
        }

        internal void SetupForAmazon()
        {
            this.MainWeapon = new Sword();
            this.SecondaryWeapon = new Knife();

            this.Items.Add(new Armour());
            this.Items.Add(new Potion());
            this.Items.Add(new Bandage());
            this.Items.Add(new Food());
        }
    }
}
