using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Heroes
{
    public class Bard : Hero
    {
        public Bard(string name) : base(name, 75, 30, 40, "Melody of Distortion", 60)  //note: special move doubles the attack points
        {
            this.Inventory.SetupForBard();
        }
    }
}
