using System;

public class Program
{
    public static void Main(string[] args)
    {
        Player player = new Player();
        Enemy enemy = new Enemy("Goblin", 25);
        bool isGameOver = false;

        Console.WriteLine("Welcome to the simple text-based game!");

        while (!isGameOver)
        {
            Console.WriteLine("\nYour current HP: " + player.Hp);
            if (!string.IsNullOrEmpty(player.Weapon))
            {
                Console.WriteLine("Equipped Weapon: " + player.Weapon + " (Attack: " + player.WeaponAttack + ", Durability: " + player.WeaponDurability + ")");
            }
            if (!string.IsNullOrEmpty(player.Armor))
            {
                Console.WriteLine("Equipped Armor: " + player.Armor + " (Defense: " + player.ArmorDefense + ", Durability: " + player.ArmorDurability + ")");
            }

            Console.WriteLine("\nChoose an action:");
            Console.WriteLine("1. Attack the enemy.");
            Console.WriteLine("2. Heal yourself.");
            Console.WriteLine("3. Gather resources.");
            Console.WriteLine("4. Equip an item.");
            Console.WriteLine("5. Discard an item.");
            Console.WriteLine("6. Check inventory.");
            Console.WriteLine("7. Quit the game.");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    player.AttackEnemy(enemy);
                    break;
                case 2:
                    player.HealPlayer();
                    break;
                case 3:
                    player.Gather();
                    break;
                case 4:
                    player.EquipItem();
                    break;
                case 5:
                    player.DiscardItem();
                    break;
                case 6:
                    player.PrintInventory();
                    break;
                case 7:
                    isGameOver = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid action.");
                    break;
            }

            if (enemy.GetHp() > 0 && !isGameOver)
            {
                Console.WriteLine("\nEnemy's turn:");
                enemy.AttackPlayer(player);
            }

            if (player.Hp <= 0)
            {
                Console.WriteLine("You have been defeated. Game over.");
                isGameOver = true;
            }
            else if (enemy.GetHp() <= 0)
            {
                Console.WriteLine("You have defeated the enemy. You win!");
                isGameOver = true;
            }
        }

        Console.WriteLine("Thank you for playing!");
    }
}
