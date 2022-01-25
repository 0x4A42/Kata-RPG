namespace Kata_RPG
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Character c1 = new Character(500, 1, "Jordan", "Melee");
            Character c2 = new Character(500, 6, "Victor", "Ranged");
            c1.DealDamage(c2);

        }
    }
}