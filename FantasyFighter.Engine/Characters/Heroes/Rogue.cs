using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Heroes
{
    public class Rogue : Hero
    {
        public Rogue(string name) : base(name, 75, 50, 35, "Shadow Dance", 100) //note: special move doubles the attack points
        {
            this.Inventory.SetupForRogue();
        }
    }
}
