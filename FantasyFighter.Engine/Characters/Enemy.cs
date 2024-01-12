using FantasyFighter.Engine.Engine;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Characters
{
    public abstract class Enemy : Character
    {
        public Enemy(string name, int health, int attackPoints, int defensePoints) : base(name, health, attackPoints, defensePoints)
        {
        }

        public virtual int Attack(bool extra)
        {
            //Console.WriteLine("Basic attack"); //debug print

            return this.AttackPoints;
        }

        public virtual int Defend()
        {
            // Now you have to calculate the defense points for the enemy
            // The calculation should look similar to hero attack, but in this case you have
            // to consider the modifier(s) for the enemies (both generic and specific).

            return this.DefencePoints;
        }
    }
}
