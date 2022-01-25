using System;

namespace Kata_RPG
{
    public enum CombatType
    {
        Melee,
        Ranged
    }

    public class Character
    {
        private const int MinNameLength = 0;
        private const int MaxNameLength = 20;
        private const int MeleeRange = 2;
        private const int RangedRange = 20;
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
        public CombatType CombatType { get; private set; }
        public int CharacterRange { get; private set; }

        public Character()
        {
            Health = 1000;
            Level = 1;
            IsAlive = true;
            Name = "Hero";
            _combatEngine = new CombatEngine();
            SetCombatType("Melee");
        }

        public Character(int health, int level, string name, string combatType)
        {
            SetHealth(health);
            SetLevel(level);
            IsAlive = true;
            SetName(name);
            _combatEngine = new CombatEngine();
            SetCombatType(combatType);
        }

        private void SetHealth(int health)
        {
            if (health >= MinHealth && health <= MaxHealth)
            {
                Health = health;
            }
            else
            {
                Health = 1000;
                Console.Error.Write("Cannot set health outside the bounds of {0}-{1}. Defaulting to 1000.", MinHealth,
                    MaxHealth);
            }
        }

        private void SetLevel(int level)
        {
            if (level >= MinLevel && level <= MaxLevel)
            {
                Level = level;
            }
            else
            {
                Console.Error.Write("Cannot set level outside the bounds of {0}-{1}. Defaulting to 1.", MinLevel,
                    MaxLevel);
                Level = 1;
            }
        }

        private void SetName(string name)
        {
            if (name.Length > MinNameLength && name.Length <= MaxNameLength)
            {
                Name = name;
            }
            else
            {
                Console.Error.Write("Name cannot be less than {0} or greater than {1}, defaulting to 'Hero'.",
                    MinNameLength, MaxNameLength);
                Name = "Hero";
            }
        }

        private void SetCombatType(string combatType)
        {
            switch (combatType.ToUpper())
            {
                case "MELEE":
                    CombatType = CombatType.Melee;
                    break;
                case "RANGED":
                    CombatType = CombatType.Ranged;
                    break;
                default:
                    Console.WriteLine("Sorry, the class you entered could not be recognised. Defaulting to Melee." +
                                      "You can change this through the menu.");
                    CombatType = CombatType.Melee;
                    break;
            }

            SetCharacterRange();
        }

        private void SetCharacterRange()
        {
            switch (CombatType)
            {
                case CombatType.Melee:
                    CharacterRange = MeleeRange;
                    break;
                case CombatType.Ranged:
                    CharacterRange = RangedRange;
                    break;
                default:
                    CharacterRange = MeleeRange;
                    break;
            }
        }

        public bool DetermineIfCanAttack(Character attacker, Character target)
        {
            switch (attacker.CombatType)
            {
                case CombatType.Melee when target.CombatType == CombatType.Melee:
                    return true;
                case CombatType.Melee when target.CombatType == CombatType.Ranged:
                    return false;
                case CombatType.Ranged when target.CombatType == CombatType.Ranged:
                    return true;
                case CombatType.Ranged when target.CombatType == CombatType.Ranged:
                    return true;
                default:
                    return false;
            }
           
        }

        public void DealDamage(Character target)
        {
            if (this != target)
            {
                if (target.Health == 0)
                {
                    Console.WriteLine("You cannot damage that which is already dead.");
                }
                else
                {
                    if (DetermineIfCanAttack(this, target))
                    {
                        var rand = new Random();
                        var damageAfterMitigation = _combatEngine.CalculateMitigation(this, target,
                            rand.Next(MinDamageHeal, MaxDamageHeal));
                        Console.WriteLine("{0} deals {1} to {2}.", Name, damageAfterMitigation, target.Name);
                        target.ReceiveDamage(damageAfterMitigation);  
                    }
                    else
                    {
                        Console.WriteLine("{0} is not in range to attack {1}.", Name, target.Name);
                    }
                }
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