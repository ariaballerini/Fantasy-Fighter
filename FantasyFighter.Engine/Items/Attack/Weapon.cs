using FantasyFighter.Interfaces;
using FantasyFighter.Items;

namespace FantasyFighter.Engine.Items.Attack
{
    public abstract class Weapon : Item, IWeapon
    {
        public abstract int AttackBonus { get; }
    }
}
