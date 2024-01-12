using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Engine.Items.Defense
{
    //A cloak that grants the wearer the ability to blend seamlessly with shadows, providing a temporary stealth advantage.
    public class ShadowCloak : Item, IDefence
    {
        public int DefenseBonus => 15;
    }
}
