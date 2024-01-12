using FantasyFighter.Characters;
using FantasyFighter.Engine.Engine;
using FantasyFighter.Interfaces;
using System.Xml.Linq;

namespace FantasyFighter.Engine.Characters.Enemies
{
    public class Dragon : Enemy, ICanAttack, ICanDefend
    {
        public Dragon(string name) : base(name, 100, 80, 50)
        {
        }

        public override int Attack(bool extra)
        {
            Console.WriteLine("Be careful as dragons have fire-breathing skills, " +
                              "so his attacks can be fatal...");

            int fullAttack;
            int firebonus;

            var roll = Dices.Roll();
            if (Dices.Roll() > 3)
            {
                firebonus = 30;
                Console.WriteLine($"\nIt's using fire-breath!");
            }

            else firebonus = 0;

            fullAttack = this.AttackPoints + firebonus;

            return fullAttack;
        }
    }
}
