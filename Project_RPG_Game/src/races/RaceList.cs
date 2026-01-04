using System.Collections.Generic;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.races;



public static class RaceList {
    public static Race Null = new Race("???",new Dictionary<TerrainType, int>
    {
        { TerrainType.HumanCity , 0}
        
    },"Project_RPG_Game/assets/characters/interogation.jpg");
    
    
    public static Race SantaClaus = new Race("SantaClaus",new Dictionary<TerrainType, int>
    {
        { TerrainType.HumanCity, 30 },
        { TerrainType.WinterSanctum, 10 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.ToyFactory, -10 },
        { TerrainType.Library, -10 },
        { TerrainType.SouthPole, -40 }
    },"Project_RPG_Game/assets/characters/CharacterSantaClaus.PNG"
        );

    public static Race MsClaus = new Race("MsClaus",new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage ,15 },
        { TerrainType.Library,20 },
        { TerrainType.ToyFactory,10},
        { TerrainType.IceMine,-20 },
        { TerrainType.FrostPeak ,-10},
        { TerrainType.FrozenLake,-15 },
    },"Project_RPG_Game/assets/characters/CharacterMsClaus.PNG"
        );

    public static Race GuardElf = new Race("GuardElf",new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage,10 },
        { TerrainType.SnowValley,10 },
        { TerrainType.PineForest,10 },
        { TerrainType.Lake ,-10 },
        { TerrainType.Sea,-10 },
        { TerrainType.Workshop,-10},
    },"Project_RPG_Game/assets/characters/CharacterGuardElf.PNG"
        );

    public static Race ChiefElf = new Race("ChiefElf",new Dictionary<TerrainType, int>
    {
        { TerrainType.ElfVillage, 10},
        { TerrainType.PackagingHall,10},
        { TerrainType.ToyFactory,10},
        { TerrainType.Depot,10},
        { TerrainType.CrystalCavern,-10},
        { TerrainType.FrostPeak ,-10},
        { TerrainType.SnowValley ,-10},
        { TerrainType.IceMine,-10},
    },"Project_RPG_Game/assets/characters/CharacterChiefElf.PNG"
        );

    public static Race ToymakerElf = new Race("ToymakerElf",new Dictionary<TerrainType, int>
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
    },"Project_RPG_Game/assets/characters/CharacterToymakerElf.png"
        );
    public static Race Child = new Race("Child",new Dictionary<TerrainType, int>
    {
        { TerrainType.HumanCity, 20 },
        { TerrainType.ElfVillage, 10 },
        { TerrainType.Workshop, 5 },
        { TerrainType.FrozenLake, -10 },
        { TerrainType.FrostPeak, -20 },
    },$"Project_RPG_Game/assets/characters/CharacterChild{Global.Random(1,5)}.PNG"
        );

    public static Race Pixie = new Race("Pixie",new Dictionary<TerrainType, int>
    {
        { TerrainType.PineForest, 20 },
        { TerrainType.CrystalCavern, 15 },
        { TerrainType.ElfVillage, 10 },
        { TerrainType.Workshop, -15 },
        { TerrainType.SouthPole, -25 },
    },"Project_RPG_Game/assets/characters/CharacterPixie.PNG"
        );

    public static Race FrostSoul = new Race("FrostSoul",new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 25 },
        { TerrainType.FrozenLake, 20 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.HumanCity, -20 },
        { TerrainType.Workshop, -15 },
    },"Project_RPG_Game/assets/characters/CharacterFrostSoul.png"
        );

    public static Race IceQueen = new Race("IceQueen",new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 30 },
        { TerrainType.CrystalCavern, 20 },
        { TerrainType.FrozenLake, 10 },
        { TerrainType.ElfVillage, -20 },
        { TerrainType.HumanCity, -30 },
    },"Project_RPG_Game/assets/characters/CharacterIceQueen.PNG"
        );

    public static Race SnowMan = new Race("SnowMan",new Dictionary<TerrainType, int>
    {
        { TerrainType.SnowValley, 20 },
        { TerrainType.FrozenLake, 15 },
        { TerrainType.PineForest, 5 },
        { TerrainType.Depot, -10 },
        { TerrainType.Sea, -30 },
    },"Project_RPG_Game/assets/characters/CharacterSnowMan.png"
        );

    public static Race IceGolem = new Race("IceGolem",new Dictionary<TerrainType, int>
    {
        { TerrainType.CrystalCavern, 20 },
        { TerrainType.IceMine, 20 },
        { TerrainType.FrostPeak, 10 },
        { TerrainType.PackagingHall, -15 },
        { TerrainType.HumanCity, -25 },
    },"Project_RPG_Game/assets/characters/CharacterIceGolem.PNG");

    public static Race FrostGiant = new Race("FrostGiant",new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 30 },
        { TerrainType.SnowValley, 15 },
        { TerrainType.CrystalCavern, 10 },
        { TerrainType.ElfVillage, -20 },
        { TerrainType.WinterSanctum, -20 },
    },"Project_RPG_Game/assets/characters/CharacterFrostGiant.PNG"
        );

    public static Race Yeti = new Race("Yeti",new Dictionary<TerrainType, int>
    {
        { TerrainType.FrostPeak, 25 },
        { TerrainType.PineForest, 15 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.HumanCity, -30 },
        { TerrainType.ToyFactory, -10 },
    },"Project_RPG_Game/assets/characters/CharacterYeti.png"
        );

    public static Race Deer = new Race("Deer",new Dictionary<TerrainType, int>
    {
        { TerrainType.PineForest, 20 },
        { TerrainType.SnowValley, 10 },
        { TerrainType.Lake, 10 },
        { TerrainType.Sea, -20 },
        { TerrainType.Depot, -10 },
    },"Project_RPG_Game/assets/characters/CharacterDeer.PNG");

    public static Race WhiteBunny = new Race("WhiteBunny",new Dictionary<TerrainType, int>
    {
        { TerrainType.SnowValley, 15 },
        { TerrainType.PineForest, 15 },
        { TerrainType.ElfVillage, 5 },
        { TerrainType.IceMine, -15 },
        { TerrainType.Sea, -20 },
    },"Project_RPG_Game/assets/characters/CharacterRabbit.PNG");

    public static Race WhiteFox = new Race("WhiteFox",new Dictionary<TerrainType, int>
    {
        { TerrainType.PineForest, 20 },
        { TerrainType.SnowValley, 20 },
        { TerrainType.FrozenLake, 10 },
        { TerrainType.Workshop, -15 },
        { TerrainType.HumanCity, -20 },
    },"Project_RPG_Game/assets/characters/CharacterFox.PNG");

    public static Race PolarBear = new Race("PolarBear",new Dictionary<TerrainType, int>
    {
        { TerrainType.FrozenLake, 25 },
        { TerrainType.SouthPole, 20 },
        { TerrainType.Sea, 10 },
        { TerrainType.ElfVillage, -20 },
        { TerrainType.Library, -10 },
    },"Project_RPG_Game/assets/characters/CharacterBear.PNG");
}