using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Project_RPG_Game.characters;
using Project_RPG_Game.classes;
using Project_RPG_Game.items;
using Project_RPG_Game.missions;
using Project_RPG_Game.races;

namespace Project_RPG_Game.generator;

public class RarityGenerator {
    public Rarity Rarity;
    
    public RarityGenerator(Rarity rarity) {
        Rarity = rarity;
    }
    
    
    //**-------------------------------------------------------- HERO ------------------------------------------------------------------
    
    private static Dictionary<string, List<string>>? GetHeroData() {
        string filepath = "C:\\Users\\Nils\\Documents\\1 - Cours\\2025-2026\\C# -- Benoit ESTIVAL\\code\\Project_RPG_Game\\Project_RPG_Game\\data\\Character\\InfoHero.json";
        string jsonContent = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);
    }

    public Hero HeroGenerator() {
        var raceList =  typeof(RaceList).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Race))
            .Select(f => (Race)f.GetValue(null))
            .ToList();
        var gameClassList = typeof(GameClassList).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => (GameClass)f.GetValue(null))
            .ToList();
        List<string> heroNameList = GetHeroData()["CharacterNames"];
        
        return new Hero(
            heroNameList[Global.Random(0,heroNameList.Count)],
            ListGeneratorInt(ListGen.HMax),
            ListGeneratorInt(ListGen.HMax),
            Rarity,
            raceList[Global.Random(1,raceList.Count)],
            gameClassList[Global.Random(0,gameClassList.Count)],
            ListGeneratorInt(ListGen.HSalary)
            );
    }

    private int ListGeneratorInt(List<int> dataList) {
        switch (Rarity) {
            case Rarity.Mythic:
                return Global.Random(dataList[4], dataList[5]);
            case Rarity.Legendary:
                return Global.Random(dataList[3], dataList[4]);
            case Rarity.Epic:
                return Global.Random(dataList[2], dataList[3]);
            case Rarity.Rare:
                return Global.Random(dataList[1], dataList[2]);
            default:
                return Global.Random(dataList[0], dataList[1]);
        }
    }
    //**-------------------------------------------------------- ITEM ------------------------------------------------------------------


    public Item ItemGenerator() {
        var itemList =  typeof(ItemList).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Func<Rarity, Item>))
            .Select(f => (Func<Rarity, Item>)f.GetValue(null))
            .ToList();

        return itemList[Global.Random(0, itemList.Count - 1)](Rarity);
    }
    
    
    
    
    
    
}