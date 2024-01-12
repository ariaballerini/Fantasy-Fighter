using FantasyFighter.Characters;
using FantasyFighter.Engine.Characters.Enemies;
using FantasyFighter.Engine.Characters.Heroes;
using FantasyFighter.Engine.Engine;
using FantasyFighter.Engine.Items.Attack;
using FantasyFighter.Engine.Items.Consumable;
using FantasyFighter.Interfaces;
using System.ComponentModel.Design;

namespace FantasyFighter.Engine
{
    public class GameEngine
    {
        private List<Enemy> _enemies = new();

        private Enemy? _currentEnemy;

        private Hero? hero;

        public bool IsGameRunning { get; private set; }

        public GameEngine()
        {
            this._enemies = new List<Enemy>()
            {
                new Dragon("Red Dragon"),
                new Dragon("Golden Dragon"),
                new Goblin("Green Goblin"),
                new Troll("Big Troll"),
                new Slime("Slimy Slime"),
                new RatKing("RatKing Henry"),
            };
        }

        public void StartNewGame()
        {
            if (this.IsGameRunning)
            {
                return;
            }

            this.IsGameRunning = true;
            //Console.WriteLine($"IsGameRunning: {this.IsGameRunning}"); // Debug print

            TextEngine.DisplayTextFromFile("WelcomeMessage");

            // ask the player for a name
            string playerName = this.SetName();

            // ask the player to pick a role
            var playerRole = this.SetRole(playerName);

            // show hero stats
            this.DisplayNewHero((int)playerRole);

            Console.WriteLine("\nPress enter when you're ready...");
            Console.ReadLine();
        }

        public void NextTurn()
        {
            if (hero != null)
            {
                //Console.WriteLine($"Hero {hero.Name} initialized."); // Debug print

                int heroAttacks = 0;
                int heroDefends;
                int enemyAttacks = 0;
                int enemyDefends;
                int damages;

                while (this.IsGameRunning)
                /*first time goes straight, then CheckHealth method at the end checks health points
                * and GameOver eventually sets if game is not running anymore (=the conditions for ending the game
                * have already been met), in that case the WHILE is "false" and there are no turns.*/
                {
                    if (this._currentEnemy == null)
                    {
                        this.PickupNewEnemy(); // picks a random enemy in the list
                        if (_currentEnemy != null)
                        {
                            Console.WriteLine($"\n*{_currentEnemy.Name} appears*" +
                                          $"\nEnemy's health is {_currentEnemy.Health}.");
                        }
                        else
                        {
                            break;  //extra control to handle null exception and break the while loop
                        }
                    }

                    else
                    {
                    }

                    int? consume = null;
                    string consumeInput;

                    do
                    {
                        Console.WriteLine("\nCheck your gear before setting out.");
                        hero.InspectInventory();

                        Console.WriteLine("\n\nWant to make any update?" +
                                          "\n1) Use any consumable item" +
                                          "\n2) Switch weapons" +
                                          "\n3) Go on with game");

                        int max = 3;
                        consume = CheckNumberInput(max);

                        switch (consume)
                        {
                            case 1:
                                this.ConsumeItem();
                                break;
                            case 2:
                                hero.ChangeWeapons();
                                break;
                            case 3:
                                Console.WriteLine("\nTime to fight then!");
                                break;
                        }

                    } while (consume == null);

                    Console.Write($"Want to use {hero.SpecialMove}? ");

                    string answer = null;

                    do
                    {
                        answer = this.CheckAnswer();

                        if (answer.Equals("yes"))
                        {
                            bool extra = true;
                            heroAttacks = hero.Attack(extra);
                        }

                        else if (answer.Equals("no"))
                        {
                            bool extra = false;
                            heroAttacks = hero.Attack(extra);
                        }

                    } while (!answer.Equals("yes") && !answer.Equals("no"));

                    enemyDefends = _currentEnemy.Defend();

                    if ((hero.Inventory.MainWeapon is Wand || hero.Inventory.MainWeapon is Bagpipe)
                        && (_currentEnemy is Dragon || _currentEnemy is RatKing))
                    {
                        int magicAttack = 20;
                        damages = heroAttacks += magicAttack -= enemyDefends; //damage inflicted to the enemy if it's a "magic type"
                    }
                    else if (hero.Inventory.MainWeapon is Sword && (_currentEnemy is Goblin || _currentEnemy is Troll))
                    {
                        int swordAttack = 10;
                        damages = heroAttacks += swordAttack -= enemyDefends; //damage inflicted to the enemy if it's a "physical type"
                    }
                    else
                    {
                        damages = heroAttacks -= enemyDefends; //generic damage inflicted to the enemy in this turn
                    }

                    _currentEnemy.Health -= damages; //decreases enemy's health
                    Console.WriteLine($"\n{hero.Name} has attacked!");

                    this.CheckHealth(); //checks hero and enemy's health at the end of each turn
                    if (!this.IsGameRunning)  // checks if game must continue
                    {
                        break;
                    }

                    if (this._currentEnemy is ICanAttack attackingEnemy) //if the enemy is still alive but cannot attack, skip
                    {
                        Console.WriteLine($"\nHe's gearing up for an attack...");
                        bool extra = false;
                        enemyAttacks = attackingEnemy.Attack(extra); //calculates the enemy attack points

                        heroDefends = hero.Defend();

                        if ((hero is Bard || hero is Witch) && (_currentEnemy is Dragon || _currentEnemy is RatKing))
                        {
                            int magicDefence = 20;
                            damages = enemyAttacks -= magicDefence -= heroDefends; //damage inflicted to the hero if it's a "magic type"
                        }
                        else if ((hero is Amazon || hero is Warrior) && (_currentEnemy is Goblin || _currentEnemy is Troll))
                        {
                            int swordDefence = 10;
                            damages = enemyAttacks -= swordDefence -= heroDefends; //damage inflicted to the hero if it's a "physical type"
                        }
                        else
                        {
                            damages = enemyAttacks -= heroDefends; //generic damage inflicted to the hero in this turn
                        }


                        hero.Health -= damages; //decreases hero's health
                        Console.WriteLine($"\n{_currentEnemy.Name} has attacked!" +
                                          $"\nYour health is now {hero.Health}.");

                        this.CheckHealth();

                    }

                    else if (this._currentEnemy is null)
                    {
                        this.CheckHealth();
                    }

                    else if (this._currentEnemy is not ICanAttack NOTattackingEnemy)
                    {
                        Console.WriteLine($"\n{_currentEnemy.Name} cannot attack.");
                    }

                    else { }

                    if (!this.IsGameRunning)  // checks if game must continue
                    {
                        break;
                    }
                }
            }

            else
            {
                Console.WriteLine("Error. Hero hasn't been initialized.");
            }

        }

        private void ConsumeItem()
        {
            while (true)
            {
                Console.Write("You can consume food, potions or bandages. Which one? ");
                string nowconsume = Console.ReadLine().Trim().ToLower();

                if (nowconsume.Contains("food"))
                {
                    hero!.Consume<Food>();
                    break;
                }
                else if (nowconsume.Contains("potion"))
                {
                    hero!.Consume<Potion>();
                    break;
                }
                else if (nowconsume.Contains("bandage"))
                {
                    hero!.Consume<Bandage>();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please retry: ");
                }
            }
        }

        private void PickupNewEnemy()
        {
            if (_enemies.Count != 0)
            {
                this.RemoveDeadEnemies(); //removes dead enemies from the list before picking
                var rnd = new Random().Next(this._enemies.Count);
                this._currentEnemy = this._enemies[rnd];
            }

            else // if this was the last enemy in the list, the hero has won
            {
                Console.WriteLine("\nYou defeated all the enemies in this realm.");

                bool noenemiesleft = true;
                bool heroisdead = false;
                this.GameOver(noenemiesleft, heroisdead);
            }
        }

        public void RemoveDeadEnemies()
        {
            _enemies.RemoveAll(enemy => enemy.Health <= 0);
        }

        private string SetName()
        {
            string? name = null;

            {
                do
                {
                    Console.Write("\nPlease choose your name: ");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                        Console.WriteLine("The name cannot be empty or white spaces");


                } while (string.IsNullOrWhiteSpace(name));

                return name!;
            }
        }

        private int? SetRole(string playerName)
        {
            int? role = null;

            do
            {
                Console.WriteLine($"So be it, {playerName}, which role do you desire?" +
                    "            \n[1] Warrior\n[2] Cleric\n[3] Rogue\n[4] Bard\n[5] Witch\n[6] Amazon");

                int max = 6;
                role = CheckNumberInput(max);

                //builds hero based on the chosen role
                switch (role)
                {
                    case 1:
                        this.hero = new Warrior(playerName);
                        break;

                    case 2:
                        this.hero = new Cleric(playerName);
                        break;

                    case 3:
                        this.hero = new Rogue(playerName);
                        break;

                    case 4:
                        this.hero = new Bard(playerName);
                        break;

                    case 5:
                        this.hero = new Witch(playerName);
                        break;

                    case 6:
                        this.hero = new Amazon(playerName);
                        break;

                }

            } while (role == null);

            return role;
        }

        private int CheckNumberInput(int max)
        {
            var validInput = 0;

            while (true)
            {
                var inputToCheck = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(inputToCheck))
                {
                    Console.Write("Input cannot be empty: ");
                    continue;
                }

                if (!int.TryParse(inputToCheck, out validInput))
                {
                    Console.Write("Input must be a number: ");
                    continue;
                }

                if (validInput < 1 || validInput > max)
                {
                    Console.Write($"That's not a valid input. Please select a valid value between 1 and {max}: ");
                    continue;
                }

                break;
            }

            return validInput;
        }

        private string DisplayNewHero(int role)
        {
            string answer = null;

            Console.Write($"\nBehold, your character's attributes." +
                          $"\nHealth: {this.hero?.Health}" +
                          $"  Attack points: {this.hero?.AttackPoints}" +
                          $"  Defence points: {this.hero?.DefencePoints}" +
                          $"  Special Move: {this.hero?.SpecialMove}" +
                          $"\nWondering about the secrets and abilities of your character? ");

            do
            {
                answer = CheckAnswer();

                if (answer.Equals("yes"))
                {
                    Console.WriteLine("");

                    //shows description based on the chosen role
                    switch (role)
                    {
                        case 1:
                            TextEngine.DisplayTextFromFile("WarriorIntro");
                            break;

                        case 2:
                            TextEngine.DisplayTextFromFile("ClericIntro");
                            break;

                        case 3:
                            TextEngine.DisplayTextFromFile("RogueIntro");
                            break;

                        case 4:
                            TextEngine.DisplayTextFromFile("BardIntro");
                            break;

                        case 5:
                            TextEngine.DisplayTextFromFile("WitchIntro");
                            break;

                        case 6:
                            TextEngine.DisplayTextFromFile("AmazonIntro");
                            break;
                    }

                    Console.WriteLine("(Special move doubles the attack points)");
                }

                else
                {
                    Console.WriteLine("No more details needed? Let's jump right into the action!");
                    break;
                }

            } while (!answer.Equals("yes") && !answer.Equals("no"));

            return answer;
        }

        public string CheckAnswer()
        {
            string yesorno;

            do
            {
                yesorno = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(yesorno) || (!yesorno.Equals("yes") && !yesorno.Equals("no")))
                {
                    if (string.IsNullOrWhiteSpace(yesorno))
                    {
                        Console.Write("Please give an answer: ");
                    }
                    else
                    {
                        Console.Write("Please write a valid answer: ");
                    }
                }

            } while (string.IsNullOrWhiteSpace(yesorno) || (!yesorno.Equals("yes") && !yesorno.Equals("no")));

            return yesorno;
        }

        private void CheckHealth()
        {
            if (this.hero != null && this.hero.Health <= 0)  //if the hero's health is <= 0 or hero becomes null, hero is dead
            {
                Console.WriteLine("Oh no, you are dead.");
                this.hero = null;

                bool heroisdead = true;
                bool noenemiesleft = false;
                this.GameOver(heroisdead, noenemiesleft);
            }

            else if (_currentEnemy != null && _currentEnemy.Health <= 0) //if the enemy's health is <= 0, enemy is dead
            {
                Console.WriteLine($"{_currentEnemy.Name} defeated!");
                Console.WriteLine("********************************************************************************************");
                this.RemoveDeadEnemies();
                this._currentEnemy = null;
                hero!.Health += 25;
                Console.WriteLine($"Your health is now {hero.Health}.");
                return;
            }

            else if (_currentEnemy == null) //if enemy becomes null, enemy is dead
            {
                return;
            }

            else
            {
                Console.WriteLine($"{_currentEnemy.Name} is still alive, keep fighting! " +
                                  $"Health left: {_currentEnemy.Health}");
            }
        }

        public void GameOver(bool noenemiesleft, bool heroisdead)
        {
            Console.WriteLine();

            if (noenemiesleft == true)
                TextEngine.DisplayTextFromFile("Winner");


            else if (heroisdead == true)
                TextEngine.DisplayTextFromFile("Loser");

            this.IsGameRunning = false;
        }
    }
}

