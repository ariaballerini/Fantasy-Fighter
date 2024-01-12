using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Heroes
{
    public class Cleric : Hero
    {
        public Cleric(string name) : base(name, 150, 25, 25, "Divine Aegis", 50)  //note: special move doubles the attack points
        {
            this.Inventory.SetupForCleric();
        }
    }
}