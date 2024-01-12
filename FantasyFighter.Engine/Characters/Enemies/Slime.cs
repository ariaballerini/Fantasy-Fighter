using FantasyFighter.Characters;
using FantasyFighter.Engine.Engine;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Enemies
{
    public class Slime : Enemy, ICanDefend
    {
        public Slime(string name) : base(name, health: 20, attackPoints: 0, defensePoints: 10)
        {

        }
    }
}
