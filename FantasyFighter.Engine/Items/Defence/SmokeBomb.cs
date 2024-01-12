using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Engine.Items.Defense
{
    //A small, ethereal bomb that, when detonated, creates a cloud of magic smoke.
    //This smoke can obscure vision, create an illusion and provide a momentary escape.
    public class SmokeBomb : Item, IDefence
    {
        public int DefenseBonus => 10;
    }
}
