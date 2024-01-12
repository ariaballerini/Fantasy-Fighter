using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Heroes
{
    public class Warrior : Hero
    {
        public Warrior(string name) : base(name, 150, 40, 40, "Thunderous Cleave", 80) //note: special move doubles the attack points
        {
            this.Inventory.SetupForWarrior();
        }
    }
}