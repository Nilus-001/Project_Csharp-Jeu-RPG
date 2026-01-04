using Avalonia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Project_RPG_Game.characters;
using Project_RPG_Game.classes;
using Project_RPG_Game.classes.custom;
using Project_RPG_Game.generator;
using Project_RPG_Game.items.custom_equip;
using Project_RPG_Game.missions;
using Project_RPG_Game.races;
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

        Guild MyGuild = new Guild(100, 500);
        
        Hero Adam = new Hero("Adam",
            100,
            100 ,
            Rarity.Common ,
            RaceList.Child,
            GameClassList.Medic,
            20);
        Hero Eve = new Hero("Eve",
            100,
            100 ,
            Rarity.Rare ,
            RaceList.MsClaus,
            GameClassList.Artisan,
            20);
        
        Mission guerre = new Mission(
            "guerre",
            "Aller battre le dieux de la mort qui tu",
            Difficulty.Win,
            1,
            MissionType.Decoding, 
            TerrainType.HumanCity,
            new Result(40,heroSalary:-9,guildLoseItem:Selector.Half,heroHp:-50,heroStatus:new Blessed(3),guildFoodStock:133),
            "You win the war",
            new Result(10,heroHp:-1),
            "You lose the war"
            );
        MissionGenerator generator = new MissionGenerator(Difficulty.Normal);
        Mission misstest = generator.MissioGenerator();
        MyGuild.AddHero(Adam);
        MyGuild.AddHero(Eve);
        RarityGenerator persoGen = new RarityGenerator(Rarity.Epic);
        Hero mike = persoGen.HeroGenerator();
        mike.PrintData();

        Event events = generator.EventGenerator();
        events.SetActive(MyGuild);
        events.PrintData();
        events.ExecuteResult();

        // foreach (var hero in MyGuilf.GuildHeroes) {
        //     Console.WriteLine("Guild Hero: " + hero.Name);
        // }

        // Adam.PrintData();
        // Eve.PrintData();
        
        // MyGuild.AddMission(misstest);
        //
        // misstest.PrintData();
        // misstest.SetActiveHero(Eve);
        //
        //
        //
        //
        // MyGuild.ExecuteMissions();
        // MyGuild.ExecuteMissions();
        // MyGuild.ExecuteMissions();
        
        MyGuild.PrintData();
        
        
        Eve.PrintData();
        
        
        
        



    } 
        

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}