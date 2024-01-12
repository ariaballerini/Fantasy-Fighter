using FantasyFighter.Characters;
using FantasyFighter.Engine.Engine;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Enemies
{
    public class Troll : Enemy, ICanAttack
    {
        public Troll(string name) : base(name, health: 50, attackPoints: 65, defensePoints: 25)
        {
        }

        public override int Attack(bool extra)
        {
            Console.WriteLine("Pay attention as this enemy owns a knife, so his attacks can be heavier...");

            int fullAttack;
            int knifebonus;

            var roll = Dices.Roll();
            if (Dices.Roll() > 3)
            {
                knifebonus = 5;
                Console.WriteLine($"\nIt's taking a knife!");
            }

            else knifebonus = 0;
            
            fullAttack = this.AttackPoints + knifebonus;

            return fullAttack;
        }
    }
}
