using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Project_RPG_Game;

public enum Rarity {
    Classic,
    Common,
    Rare,
    Epic,
    Legendary,
    Mythic
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
    Win = 100,
    Easy = 70,
    Normal = 40,
    Hard = 15,
    Gamble = 0,
    Impossible = -50
}


public enum MissionType {
    Exploration,
    Tracking, 
    Deliver,
    Rescue,
    
    Mining,
    Collect,
    Research,
    Decoding,
    
    Repair,
    Distribute,
    Transport,
    Retrieve,
    
    Escort,
    Protection,
    Defend,
    Survive,

}
public enum TerrainType {
    FrostPeak,
    SnowValley,
    PineForest,
    
    CrystalCavern,
    IceMine,
    
    Workshop,
    Depot,
    ToyFactory,
    PackagingHall,
    
    ElfVillage,
    HumanCity,
    Library,
    
    FrozenLake,
    Lake, 
    Sea,
    SouthPole,
    WinterSanctum,
}

public static class RaceTypes {
    public static Race Null = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.HumanCity , 0}
    });
    
    
    public static Race SantaClaus = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.HumanCity, 30 },
        { TerrainType.WinterSanctum, 10 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.ToyFactory, -10 },
        { TerrainType.Library, -10 },
        { TerrainType.SouthPole, -40 }
    });

    public static Race MrsClaus = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage ,15 },
        { TerrainType.Library,20 },
        { TerrainType.ToyFactory,10},
        { TerrainType.IceMine,-20 },
        { TerrainType.FrostPeak ,-10},
        { TerrainType.FrozenLake,-15 },
    });

    public static Race GuardElf = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage,10 },
        { TerrainType.SnowValley,10 },
        { TerrainType.PineForest,10 },
        { TerrainType.Lake ,-10 },
        { TerrainType.Sea,-10 },
        { TerrainType.Workshop,-10},
    });

    public static Race ChiefElf = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage, 10},
        { TerrainType.PackagingHall,10},
        { TerrainType.ToyFactory,10},
        { TerrainType.Depot,10},
        { TerrainType.CrystalCavern,-10},
        { TerrainType.FrostPeak ,-10},
        { TerrainType.SnowValley ,-10},
        { TerrainType.IceMine,-10},
    });

    public static Race ToymakerElf = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage,15},
        { TerrainType.PackagingHall,15},
        { TerrainType.ToyFactory,15},
        { TerrainType.Depot,15},
        { TerrainType.Workshop,15},
        { TerrainType.PineForest,-20},
        { TerrainType.FrozenLake ,-20},
        { TerrainType.Lake ,-20},
        { TerrainType.Sea,-20},
    });
    public static Race Child = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.HumanCity, 20 },
        { TerrainType.ElfVillage, 10 },
        { TerrainType.Workshop, 5 },
        { TerrainType.FrozenLake, -10 },
        { TerrainType.FrostPeak, -20 },
    });

    public static Race Pixie = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.PineForest, 20 },
        { TerrainType.CrystalCavern, 15 },
        { TerrainType.ElfVillage, 10 },
        { TerrainType.Workshop, -15 },
        { TerrainType.SouthPole, -25 },
    });

    public static Race FrostSoul = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 25 },
        { TerrainType.FrozenLake, 20 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.HumanCity, -20 },
        { TerrainType.Workshop, -15 },
    });

    public static Race IceQueen = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 30 },
        { TerrainType.CrystalCavern, 20 },
        { TerrainType.FrozenLake, 10 },
        { TerrainType.ElfVillage, -20 },
        { TerrainType.HumanCity, -30 },
    });

    public static Race SnowMan = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.SnowValley, 20 },
        { TerrainType.FrozenLake, 15 },
        { TerrainType.PineForest, 5 },
        { TerrainType.Depot, -10 },
        { TerrainType.Sea, -30 },
    });

    public static Race IceGolem = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.CrystalCavern, 20 },
        { TerrainType.IceMine, 20 },
        { TerrainType.FrostPeak, 10 },
        { TerrainType.PackagingHall, -15 },
        { TerrainType.HumanCity, -25 },
    });

    public static Race FrostGiant = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 30 },
        { TerrainType.SnowValley, 15 },
        { TerrainType.CrystalCavern, 10 },
        { TerrainType.ElfVillage, -20 },
        { TerrainType.WinterSanctum, -20 },
    });

    public static Race Yeti = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 25 },
        { TerrainType.PineForest, 15 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.HumanCity, -30 },
        { TerrainType.ToyFactory, -10 },
    });

    public static Race Deer = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.PineForest, 20 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.Lake, 10 },
        { TerrainType.Sea, -20 },
        { TerrainType.Depot, -10 },
    });

    public static Race WhiteBunny = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.SnowValley, 15 },
        { TerrainType.PineForest, 15 },
        { TerrainType.ElfVillage, 5 },
        { TerrainType.IceMine, -15 },
        { TerrainType.Sea, -20 },
    });

    public static Race WhiteFox = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.PineForest, 20 },
        { TerrainType.SnowValley, 20 },
        { TerrainType.FrozenLake, 10 },
        { TerrainType.Workshop, -15 },
        { TerrainType.HumanCity, -20 },
    });

    public static Race PolarBear = new Race(new Dictionary<TerrainType, int>
    {
        { TerrainType.FrozenLake, 25 },
        { TerrainType.SouthPole, 20 },
        { TerrainType.Sea, 10 },
        { TerrainType.ElfVillage, -20 },
        { TerrainType.Library, -10 },
    });
}







public static class BonusMissions {
    // public static Mission Goodgame ;

}
public enum MalusMissions {
    
}
public enum LongTimeMissions {
    
}

public enum BonusEvents {
    
}
public enum MalusEvents {
    
}




public static class Global {
    
    public static bool DiceRoll(int percentage) {
        int dice = new Random().Next(0, 100);
        return percentage > dice;
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

