using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Engine.Items.Defense
{
    //An amulet infused with celestial energy.
    //Offers protection against dark forces and magic spells.
    public class Amulet : Item, IDefence
    {
        public int DefenseBonus => 5;
    }
}
