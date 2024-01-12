using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Heroes
{
    public class Amazon : Hero
    {
        public Amazon(string name) : base(name, 150, 40, 40, "Serpent's Wrath", 80)  //note: special move doubles the attack points
        {
            this.Inventory.SetupForAmazon();
        }
    }
}
