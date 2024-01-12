using FantasyFighter.Characters;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Enemies
{
    public class Goblin : Enemy, ICanAttack, ICanDefend
    {
        public Goblin(string name) : base(name, health: 30, attackPoints: 60, defensePoints: 15)
        {

        }
    }
}
