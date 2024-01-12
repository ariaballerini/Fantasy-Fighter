using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Items.Consumable
{
    public class Bandage : Consumable, IHeal
    {
        public int HealingPoints => 25;

    }
}
