
namespace Kata_RPG
{
    public class CombatEngine
    {
        
        public int CalculateMitigation(Character attacker, Character target, int damage)
        {
            var damageAfterMitigation = damage;
            
            if (target.Level > (attacker.Level + 5))
            {
                damageAfterMitigation = (int) (damage * .5);
            } else if (target.Level < (attacker.Level - 5))
            {
                damageAfterMitigation = (int) (damage * 1.5);
            } 
            return damageAfterMitigation;
        }
    }
}