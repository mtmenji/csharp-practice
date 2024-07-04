using System;
using System.Collections.Generic;

public class Player
{
    private int hp = 50;
    private string weapon = "";
    private int weaponAttack = 0;
    private int weaponDurability = 0;
    private string armor = "";
    private int armorDefense = 0;
    private int armorDurability = 0;
    private readonly List<string> bag = new List<string> { "Test Weapon", "Test Armor" };

    public int Hp => hp;
    public string Weapon => weapon;
    public int WeaponAttack => weaponAttack;
    public int WeaponDurability => weaponDurability;
    public string Armor => armor;
    public int ArmorDefense => armorDefense;
    public int ArmorDurability => armorDurability;

    public void EquipWeapon(string weapon, int attack, int durability)
    {
        this.weapon = weapon;
        this.weaponAttack = attack;
        this.weaponDurability = durability;
    }

    public void EquipArmor(string armor, int defense, int durability)
    {
        this.armor = armor;
        this.armorDefense = defense;
        this.armorDurability = durability;
    }

    public bool IsBagEmpty() => bag.Count == 0;

    public int EquipItem()
    {
        if (bag.Count == 0)
        {
            return 0; // Return 0 to indicate no valid choice was made
        }

        Console.WriteLine("Bag items: " + string.Join(", ", bag));
        Console.WriteLine("Equip Item - Options:");
        Console.WriteLine("1. Equip a weapon.");
        Console.WriteLine("2. Equip armor.");
        Console.WriteLine("3. Cancel");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid choice.");
            return 0;
        }

        if (choice == 1)
        {
            List<string> weapons = bag.FindAll(item => item.Contains("Weapon"));
            if (weapons.Count > 0)
            {
                Console.WriteLine("Select a weapon to equip:");
                for (int i = 0; i < weapons.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + weapons[i]);
                }

                if (int.TryParse(Console.ReadLine(), out int weaponChoice) && weaponChoice > 0 && weaponChoice <= weapons.Count)
                {
                    string selectedWeapon = weapons[weaponChoice - 1];
                    EquipWeapon("Blunt Sword", 2, 2); // Replace with actual weapon stats based on selectedWeapon
                    Console.WriteLine("Equipped " + selectedWeapon + ".");
                    bag.Remove(selectedWeapon);
                    return 1; // Return 1 to indicate weapon equipped
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    return 0; // Return 0 for invalid choice
                }
            }
            else
            {
                Console.WriteLine("No weapons available to equip.");
                return 0; // Return 0 for no weapons available
            }
        }
        else if (choice == 2)
        {
            List<string> armors = bag.FindAll(item => item.Contains("Armor"));
            if (armors.Count > 0)
            {
                Console.WriteLine("Select armor to equip:");
                for (int i = 0; i < armors.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + armors[i]);
                }

                if (int.TryParse(Console.ReadLine(), out int armorChoice) && armorChoice > 0 && armorChoice <= armors.Count)
                {
                    string selectedArmor = armors[armorChoice - 1];
                    EquipArmor("Leather Armor", 2, 2); // Replace with actual armor stats based on selectedArmor
                    Console.WriteLine("Equipped " + selectedArmor + ".");
                    bag.Remove(selectedArmor);
                    return 2; // Return 2 to indicate armor equipped
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    return 0; // Return 0 for invalid choice
                }
            }
            else
            {
                Console.WriteLine("No armor available to equip.");
                return 0; // Return 0 for no armor available
            }
        }
        else if (choice == 3)
        {
            Console.WriteLine("Equip item cancelled.");
            return 3; // Return 3 to indicate cancel
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return 0; // Return 0 for invalid choice
        }
    }

    public void DiscardItem()
    {
        Console.WriteLine("Bag items: " + string.Join(", ", bag));
        if (bag.Count == 0)
        {
            Console.WriteLine("Your bag is empty.");
            return;
        }

        Console.WriteLine("Enter the index of the item to discard (starting from 0):");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < bag.Count)
        {
            bag.RemoveAt(index);
            Console.WriteLine("Item discarded.");
        }
        else
        {
            Console.WriteLine("Invalid index.");
        }
    }

    public void AttackEnemy(Enemy enemy)
    {
        int roll = Dice.Roll();
        int normalDamage = roll switch
        {
            2 => 6,
            3 => 4,
            4 => 3,
            5 or 7 or 9 => 2,
            6 or 8 or 10 => 0,
            11 => 4,
            12 => 5,
            _ => 0
        };

        int totalDamage = normalDamage;
        bool weaponBroke = false;

        if (normalDamage > 0)
        {
            totalDamage += weaponAttack;
            if (weaponDurability > 0)
            {
                weaponDurability--;
                if (weaponDurability == 0)
                {
                    weapon = "";
                    weaponAttack = 0;
                    weaponBroke = true;
                }
            }
        }
        else
        {
            Console.WriteLine("Your attack missed!");
            return;
        }

        string attackMessage = "You attacked the enemy for " + totalDamage + " damage.";

        if (!string.IsNullOrEmpty(weapon))
        {
            attackMessage = "Your attack of " + normalDamage +
                            " combined with your " + weapon + " attack of " + weaponAttack +
                            " deals a total of " + totalDamage + " damage.";
        }

        enemy.DecreaseHp(totalDamage);
        Console.WriteLine(attackMessage);

        if (weaponBroke)
        {
            Console.WriteLine("Your weapon broke!");
        }
    }

    public void HealPlayer()
    {
        int roll = Dice.Roll();
        int heal = roll switch
        {
            2 => 7,
            3 => 5,
            4 => 4,
            5 => 3,
            6 or 8 => 2,
            7 or 9 => 3,
            10 => 4,
            11 => 5,
            12 => 6,
            _ => 0
        };

        hp += heal;
        if (hp > 50)
        {
            hp = 50;  // Cap health at 50
        }
        Console.WriteLine("You healed yourself for " + heal + " HP.");
    }

    public void Gather()
    {
        int roll = Dice.Roll();
        switch (roll)
        {
            case 2:
                if (string.IsNullOrEmpty(weapon) && string.IsNullOrEmpty(armor))
                {
                    Console.WriteLine("Nothing to lose.");
                }
                else
                {
                    Console.WriteLine("A thief attacked you, losing either your weapon or armor!");
                    if (string.IsNullOrEmpty(weapon))
                    {
                        armor = "";
                        armorDefense = 0;
                        armorDurability = 0;
                        Console.WriteLine("You lost your armor.");
                    }
                    else
                    {
                        weapon = "";
                        weaponAttack = 0;
                        weaponDurability = 0;
                        Console.WriteLine("You lost your weapon.");
                    }
                }
                break;
            case 3:
                Console.WriteLine("You find an empty hut. No items found.");
                break;
            case 4:
                bag.Add("Wooden Stick");
                Console.WriteLine("You gathered a Wooden Stick.");
                break;
            case 5:
                bag.Add("Blunt Sword");
                Console.WriteLine("You gathered a Blunt Sword.");
                break;
            case 6:
                bag.Add("Rusty Dagger");
                Console.WriteLine("You gathered a Rusty Dagger.");
                break;
            case 7:
                bag.Add("Cedar Bow");
                Console.WriteLine("You gathered a Cedar Bow.");
                break;
            case 8:
                bag.Add("Wooden Shield");
                Console.WriteLine("You gathered a Wooden Shield.");
                break;
            case 9:
                bag.Add("Leather Armor");
                Console.WriteLine("You gathered a Leather Armor.");
                break;
            case 10:
                bag.Add("Mail Armor");
                Console.WriteLine("You gathered a Mail Armor.");
                break;
            case 11:
                bag.Add("Magic Cloak");
                Console.WriteLine("You gathered a Magic Cloak.");
                break;
            case 12:
                bag.Add("Shield Potion");
                Console.WriteLine("You gathered a Shield Potion.");
                break;
        }
    }

    public void PrintInventory()
    {
        if (bag.Count == 0)
        {
            Console.WriteLine("Your bag is empty.");
        }
        else
        {
            Console.WriteLine("Inventory items: " + string.Join(", ", bag));
        }
    }

    public void DecreaseHp(int amount)
    {
        hp -= Math.Max(amount, 0);
    }

    public void DecreaseArmorDurability()
    {
        armorDurability--;
    }
}
