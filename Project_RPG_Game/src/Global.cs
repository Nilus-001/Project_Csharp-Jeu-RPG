using System;
using System.Collections.Generic;

namespace Project_RPG_Game;

public enum Rarity {
    Classic = 1,
    Common = 10,
    Rare = 20,
    Epic = 40,
    Legendary = 50,
    Mythic = 70
}

public enum Selector {
    None,
    One,
    Two,
    Three,
    Half,
    All
}
public enum Difficulty { 
    //? Base Percentages of winning :
    Win = 110,
    Easy = 70,
    Normal = 50,
    Hard = 20,
    Gamble = 0,
    Impossible = -50
}


public static class ListGen {
    public static List<int> Money = new List<int>([7, 15, 30, 50, 100, 150]);
    public static List<int> Xp = new List<int>([10, 15, 20, 35, 50, 51]);
    public static List<int> FoodStock = new List<int>([7, 20, 35, 45, 55, 70]);
    
    public static List<int> HMax = new List<int>([70, 90, 110, 130, 150, 200]);
    public static List<int> HSalary = new List<int>([1, 3, 7, 10, 14, 20]);
    
}

public static class Global {
    
    public static bool DiceRoll(int percentage) {
        int dice = new Random().Next(0, 100);
        return percentage > dice;
    }

    public static int Random(int min, int max) {
        return new Random().Next(min, max);
    }
    
    public static int SelectorIntReturn<T>(Selector selector,List<T> list) {
        switch (selector) {
            case Selector.One:
                if (list.Count != 0) {
                    return 1;
                }
                break;
            case Selector.Two:
                if (list.Count >= 2) {
                    return 2;
                }
                break;
            case Selector.Three:
                if (list.Count >= 3) {
                    return 3;
                }
                break;
            case Selector.Half:
                return (int)(list.Count / 2);
            case Selector.All:
                return list.Count;
        }
        return 0;
        
    }




}

