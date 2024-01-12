using FantasyFighter.Characters;
using FantasyFighter.Engine.Engine;
using FantasyFighter.Interfaces;

namespace FantasyFighter.Engine.Characters.Enemies
{
    public class RatKing : Enemy, ICanAttack, ICanDefend
    {
        public RatKing(string name) : base(name, health: 80, attackPoints: 70, defensePoints: 40)
        {

        }

        public override int Defend()
        {
            Console.WriteLine("Keep an eye on the rats; they are masters of concealment and can dodge your attacks...");

            int fullDefense;
            int shadowCloak;

            var roll = Dices.Roll();
            if (Dices.Roll() > 3)
            {
                shadowCloak = 15;
                Console.WriteLine($"\nDamn, it's hiding!");
            }

            else shadowCloak = 0;

            fullDefense = this.DefencePoints + shadowCloak;

            return fullDefense;
        }
    }
}
