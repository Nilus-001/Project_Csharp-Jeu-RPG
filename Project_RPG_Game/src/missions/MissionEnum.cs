namespace Project_RPG_Game.missions;

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
    
    Event,

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


// public static class MissionList {

    // public static Mission RequireNightWatch(int xp, int money,int hp) => new Mission(
    //     "Night Watch",
    //     $"The night is dark. Monsters can attack at any moment. There's no time to rest. ",
    //     Difficulty.Normal,
    //     1,
    //     MissionType.Defend,
    //     TerrainType.ElfVillage,
    //     new Result(xp,guildMoney:money),
    //     "A peaceful night, no problems: Nothing to report.",
    //     new Result(xp,guildMoney:money,heroHp:hp),
    //     "A troop of monsters tried to attack the village, but your scream of fear scared them away."
    //     );

    // public static Mission RequireMaintenance(int xp, int money) => new Mission(
    //     "Maintenance",
    //     $"The village needs maintenance; someone should take care of it.",
    //     Difficulty.Win,
    //     1,
    //     MissionType.Repair,
    //     TerrainType.ElfVillage,
    //     new Result(xp,guildMoney:money),
    //     "You've worked hard, here's your money.",
    //     null,
    //     ""
    // );


    // public static Mission Exploration(Difficulty difficulty,int nb,TerrainType terrain) => new Mission(
    //          "Exploration",
    //          $"Lets explore the world ! We need new resources.",
    //         difficulty,
    //          nb,
    //          MissionType.Exploration,
    //          terrain,
    //          
    //          
             
             
    // );

// }