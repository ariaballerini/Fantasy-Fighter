using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Items.Consumable
{
    public class Food : Consumable, IHeal
    {
        public int HealingPoints => 35;
    }
}
