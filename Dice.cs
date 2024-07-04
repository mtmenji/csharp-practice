using System;

public class Dice
{
    private static readonly Random Random = new Random();

    public static int Roll()
    {
        int die1 = Random.Next(1, 7); // Roll first die (1-6)
        int die2 = Random.Next(1, 7); // Roll second die (1-6)
        int sum = die1 + die2;
        return sum;
    }
}
