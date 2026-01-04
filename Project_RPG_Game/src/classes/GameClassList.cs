using System;
using System.Collections.Generic;
using Project_RPG_Game.classes.custom;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes;

public static class GameClassList {

    public static BasicGameClass Null = new BasicGameClass("???", 0, new List<MissionType> { MissionType.Event });
    
    public static BasicGameClass Courier = new BasicGameClass(
        "Courier", 
        90, 
        new List<MissionType> { MissionType.Deliver, MissionType.Distribute, MissionType.Transport }
    );

    public static BasicGameClass Miner = new BasicGameClass(
        "Miner", 
        80, 
        new List<MissionType> { MissionType.Mining, MissionType.Collect, MissionType.Transport }
    );

    public static BasicGameClass Guardian = new BasicGameClass(
        "Guardian", 
        65, 
        new List<MissionType> { MissionType.Protection, MissionType.Defend, MissionType.Escort }
    );

    public static BasicGameClass Mechanic = new BasicGameClass(
        "Mechanic", 
        85, 
        new List<MissionType> { MissionType.Repair, MissionType.Transport, MissionType.Decoding }
    );

    public static BasicGameClass Explorer = new BasicGameClass(
        "Explorer", 
        70, 
        new List<MissionType> { MissionType.Exploration, MissionType.Survive, MissionType.Tracking }
    );

    public static BasicGameClass Hunter = new BasicGameClass(
        "Hunter", 
        60, 
        new List<MissionType> { MissionType.Tracking, MissionType.Retrieve, MissionType.Survive }
    );

    public static BasicGameClass Artisan = new BasicGameClass(
        "Artisan", 
        95, 
        new List<MissionType> { MissionType.Collect, MissionType.Research, MissionType.Repair }
    );

    public static BasicGameClass Sage = new BasicGameClass(
        "Sage", 
        75, 
        new List<MissionType> { MissionType.Decoding, MissionType.Research, MissionType.Distribute }
    );

    public static BasicGameClass Fisher = new BasicGameClass(
        "Fisher", 
        85, 
        new List<MissionType> { MissionType.Collect, MissionType.Survive, MissionType.Transport }
    );

    public static BasicGameClass Engineer = new BasicGameClass(
        "Engineer", 
        80, 
        new List<MissionType> { MissionType.Mining, MissionType.Repair, MissionType.Decoding }
    );

    public static BasicGameClass Medic = new BasicGameClass(
        "Medic", 
        85, 
        new List<MissionType> { MissionType.Rescue, MissionType.Research, MissionType.Protection }
    );

    public static Thief Thief = new Thief(
        50,
        new List<MissionType>((MissionType[])Enum.GetValues(typeof(MissionType))),
        40
    );
}