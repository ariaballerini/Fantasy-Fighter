using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Items.Consumable
{
    public class Potion : Consumable, IHeal
    {
        public int HealingPoints => 50;
    }
}
