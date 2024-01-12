using FantasyFighter.Engine.Engine;

namespace FantasyFighter.Characters
{  
    
    public abstract class Character
    {

        public string Name { get; private set; }

        public int Health { get; set; }

        public int AttackPoints { get; set; }

        public int DefencePoints { get; set; }

        public Character(string name, int health, int attackPoints, int defensePoints)
        {
            Name = name;
            Health = health;
            AttackPoints = attackPoints;
            DefencePoints = defensePoints; 
        }

        public override string ToString()
        {
            return $"Name: {this.Name}" +
                   $"\nHealth: {this.Health}" +
                   $"\nAttack Points: {this.AttackPoints}" +
                   $"\nDefense Points: {this.DefencePoints}";
        }
    }
}
