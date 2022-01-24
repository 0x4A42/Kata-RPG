namespace Kata_RPG
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Character c1 = new Character(500, 1, true, "Jordan");
            Character c2 = new Character(500, 6, true, "Victor");
            c1.DealDamage(c2);
            c2.DealDamage(c1);
            c1.DealDamage(c1);
        }
    }
}