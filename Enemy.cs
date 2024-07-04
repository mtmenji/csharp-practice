public class Enemy
{
    public string Name { get; }
    private int hp;

    public Enemy(string name, int hp)
    {
        Name = name;
        this.hp = hp;
    }

    public int GetHp() => hp;

    public void DecreaseHp(int amount)
    {
        hp -= Math.Max(amount, 0);
    }

    public void AttackPlayer(Player player)
    {
        int roll = Dice.Roll();
        int damage = roll switch
        {
            2 or 3 or 4 => 6,
            5 or 6 => 4,
            7 or 8 => 3,
            9 or 10 => 2,
            11 or 12 => 0,
            _ => 0
        };

        if (damage > 0)
        {
            damage -= player.ArmorDefense;
            if (player.ArmorDurability > 0)
            {
                player.DecreaseArmorDurability();
                if (player.ArmorDurability == 0)
                {
                    player.EquipArmor("", 0, 0);
                    Console.WriteLine("Your armor broke!");
                }
            }
        }
        else
        {
            Console.WriteLine("The enemy tried to attack, but missed!");
            return;
        }

        player.DecreaseHp(Math.Max(damage, 0));
        Console.WriteLine("The enemy attacked you for " + damage + " damage.");
    }
}
