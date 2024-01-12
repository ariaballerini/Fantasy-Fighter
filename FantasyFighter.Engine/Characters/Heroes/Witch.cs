using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Heroes
{
    public class Witch : Hero
    {
        public Witch(string name) : base(name, 75, 50, 35, "Arcane Tempest", 100) //note: special move doubles the attack points
        {
            this.Inventory.SetupForWitch();
        }
    }
}
