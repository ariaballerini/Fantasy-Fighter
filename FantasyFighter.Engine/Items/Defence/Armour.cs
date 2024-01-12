using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Engine.Items.Defense
{
    public class Armour : Item, IDefence
    {
        public int DefenseBonus => 15;
    }
}
