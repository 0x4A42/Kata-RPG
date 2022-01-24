using System;

namespace Kata_RPG
{
    public class Character
    {

        private const int MinHealth = 0;
        private const int MaxHealth = 1000;
        private const int MinLevel = 1;
        private const int MaxLevel = 60;
        private const int MinDamageHeal = 0;
        private const int MaxDamageHeal = 650;
        private readonly CombatEngine _combatEngine;

        public int Health { get; private set; }
        public int Level { get; private set; }
        public bool IsAlive { get; private set; }

        public string Name { get; private set; }
        
        public Character()
        {
            Health = 1000;
            Level = 1;
            IsAlive = true;
            Name = "Hero";
            _combatEngine = new CombatEngine();

        }

        public Character(int health, int level, bool isAlive, string name)
        {
            SetHealth(health);
            SetLevel(level);
            IsAlive = isAlive;
            SetName(name);
            _combatEngine = new CombatEngine();
        }

        private void SetHealth(int health)
        {
            if (health >= MinHealth && health <= MaxHealth)
            {
                Health = health;
            } else
            {
                Health = 1000;
                Console.Error.Write("Cannot set health outside the bounds of {0}-{1}. Defaulting to 1000.", MinHealth, MaxHealth);
            }
        }
        
        private void SetLevel(int level)
        {
            if (level >= MinLevel && level <= MaxLevel)
            {
                Level = level;
            } else
            {
                Console.Error.Write("Cannot set level outside the bounds of {0}-{1}. Defaulting to 1.", MinLevel, MaxLevel);
                Level = 1;
            }
        }

        private void SetName(string name)
        {
            if (name.Length > 0)
            {
                Name = name;
            }
            else
            {
                Console.Error.Write("Name cannot be blank, defaulting to 'Hero'.");
                Name = "Hero";
            }
        }

        public void DealDamage(Character target)
        {
            if (this != target)
            {
                var rand = new Random();
                var damageDealt = rand.Next(MinDamageHeal, MaxDamageHeal);
                var damageAfterMitigation = _combatEngine.CalculateMitigation(this, target, damageDealt);
                Console.WriteLine("{0} deals {1} to {2}.", Name, damageAfterMitigation, target.Name);
                target.ReceiveDamage(damageAfterMitigation); 
            }
            else
            {
                Console.WriteLine("You cannot damage yourself!");
            }
          
        }

        public void Heal()
        {
            var rand = new Random();
            var healingToDo = rand.Next(MinDamageHeal, MaxDamageHeal);
            ReceiveHealing(healingToDo);
            Console.WriteLine("{0} heals for {1}.", Name, healingToDo);
        }
        
        private void ReceiveHealing(int healingToReceive)
        {
            if (IsAlive)
            {
                if (Health + healingToReceive > 1000)
                {
                    Health = 1000;
                }
                else
                {
                    Health = (Health + healingToReceive);
                }
            }
            else
            {
                Console.WriteLine("{0} could not be heal themselves as they are dead.", Name);
            }
        }

       
        private void ReceiveDamage(int damageTaken)
        {
            
            if (Health - damageTaken <= 0)
            {
                Health = 0;
                IsAlive = false;
                Console.WriteLine("{0} has died.", Name);
            }
            else
            {
                Health = (Health - damageTaken);
            }
        }
        
    }
}