using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Project_RPG_Game.items.custom_equip;
using Project_RPG_Game.items.custom_usable;

namespace Project_RPG_Game.items;

public static class ItemList 
{
    public static Func<Rarity, Item> CrystalAmplificator = (rarity) => new Amplificateur("Crystal Amplificator", rarity);
    public static Func<Rarity, Item> Backpack = (rarity) => new Backpack("Backpack", rarity);
    public static Func<Rarity, Item> CharmOfProtection = (rarity) => new CharmOfProtection("Charm of Protection", rarity);
    public static Func<Rarity, Item> CharmOfXp = (rarity) => new CharmOfXp("Charm of XP", rarity);
    public static Func<Rarity, Item> Coin = (rarity) => new Coin("Coin", rarity);
    public static Func<Rarity, Item> Hammer = (rarity) => new Hammer("Hammer", rarity);
    public static Func<Rarity, Item> Magnet = (rarity) => new Magnet("Magnet", rarity);
    public static Func<Rarity, Item> Padlock = (rarity) => new Padlock("Padlock", rarity);
    public static Func<Rarity, Item> ShieldOfTotalProtection = (rarity) => new ShieldOfTotalProtection("Shield of Total Protection", rarity);
    public static Func<Rarity, Item> ToolBelt = (rarity) => new ToolBelt("Tool Belt", rarity);
    public static Func<Rarity, Item> XpBottle = (rarity) => new XpBottle("Xp Bottle", rarity);
    public static Func<Rarity, Item> Antidote = (rarity) => new Antidote("Antidote");
    public static Func<Rarity, Item> SacOfCookies = (rarity) => new Food("Sac of Cookies", rarity);
    public static Func<Rarity, Item> Heal = (rarity) => new Heal("Heal", rarity);

    
}