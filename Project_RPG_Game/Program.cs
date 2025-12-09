using Avalonia;
using System;
using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.classes;
using Project_RPG_Game.classes.custom;
using Project_RPG_Game.missions;
using Project_RPG_Game.missions.Result;
using Project_RPG_Game.status.custom;
using Tmds.DBus.Protocol;

namespace Project_RPG_Game;

class Program {
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) { 
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
   
        //---------------Personnal Testing-----------------

        Guild MyGuilf = new Guild(100, 500);
        
        Hero Adam = new Hero("Adam",
            100,
            100 ,
            Rarity.Common ,
            RaceTypes.Child,
            new Miner(30,new List<MissionType>([MissionType.Decoding])),
            20);
        Hero Eve = new Hero("Eve",
            100,
            100 ,
            Rarity.Rare ,
            RaceTypes.MrsClaus,
            new Miner(30,new List<MissionType>([MissionType.Decoding])),
            20);
        
        Mission guerre = new Mission(
            "guerre",
            "Aller battre le dieux de la mort qui tu",
            Difficulty.Easy,
            1,
            MissionType.Decoding, 
            TerrainType.HumanCity,
            new Result(heroSalary:2),
            "You win the war",
            new Result(heroHp:-1,guildLoseItem:Selector.Half),
            "You lose the war"
            
            
            );

        MyGuilf.AddHero(Adam);
        MyGuilf.AddHero(Eve);

        foreach (var hero in MyGuilf.GuildHeroes) {
            Console.WriteLine("Guild Hero: " + hero.Name);
        }

        // Adam.PrintData();
        // Eve.PrintData();
        
        MyGuilf.AddMission(guerre);
        guerre.SetActiveHero(Eve);
        
        MyGuilf.ExecuteMissions();
        
        Eve.PrintData();
        
       











    } 
        

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}