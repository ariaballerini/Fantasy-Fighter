using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Engine.Items.Defense
{
    //Its sound temporarily weakens the attack of the enemy, making him fall asleep for 30 seconds
    public class Harp : Item, IDefence
    {
        public int DefenseBonus => 5;
    }
}

