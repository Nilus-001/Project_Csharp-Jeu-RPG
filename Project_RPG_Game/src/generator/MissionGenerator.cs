
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using Project_RPG_Game.items;
using Project_RPG_Game.items.custom_equip;
using Project_RPG_Game.items.custom_usable;
using Project_RPG_Game.missions;
using Project_RPG_Game.status.custom;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.generator;





public class MissionGenerator {
    public Difficulty Difficulty;
    public static Func<int> HpGen = () =>  Global.Random(0, 33) ;
    public static Func<int> FoodGen = () => Global.Random(0, 30);

    public MissionGenerator(Difficulty difficulty) {
        Difficulty = difficulty;
    }
        
        
    
    //**-------------------------------------------------------- MISSION ------------------------------------------------------------------
    private static Dictionary<MissionType, Dictionary<string, List<string>>>? GetMissionData() {
        string filepath = ".\\Project_RPG_Game\\data\\Missions\\InfoMission.json";
        string jsonContent = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<Dictionary<MissionType, Dictionary<string, List<string>>>>(jsonContent);
    }
    
    
    
    public Mission MissioGenerator() {
        int randomDescription = Global.Random(0,10);
        var dataDescription = GetMissionData().ElementAt(Global.Random(0, 16));
        List<string> terrainList = dataDescription.Value["place"];
        TerrainType terrain = (TerrainType)Enum.Parse(typeof(TerrainType),terrainList[Global.Random(0,terrainList.Count)]);

        Mission mission = new Mission(
            dataDescription.Value["name"][0],
            dataDescription.Value["description"][randomDescription],
            Difficulty,
            Global.Random(1, NbHeroGenerator() + 2),
            dataDescription.Key,
            terrain,
            ResultGenerator(),
            dataDescription.Value["success"][randomDescription],
            ResultGenerator(-1),
            dataDescription.Value["loses"][randomDescription],
            MissionExecutionTimerGenerator()
            
        );
        return mission;
    }

    private int NbHeroGenerator() {
        switch (Difficulty) {
            case Difficulty.Win or Difficulty.Easy :
                return 1;
            case Difficulty.Normal :
                return 2;
            case Difficulty.Hard :
                return 3;
            case Difficulty.Gamble or Difficulty.Impossible :
                return 4;
        }
        return 0;
    }
    private int MissionExecutionTimerGenerator() {
        switch (Difficulty) {
            case Difficulty.Normal or Difficulty.Hard or Difficulty.Gamble or Difficulty.Impossible :
                return Global.Random(1,3);
            default:
                return 1;
        }
    }


    
    
    //**-------------------------------------------------------- EVENTS ------------------------------------------------------------------
    private static Dictionary<string, List<Dictionary<string,string>>>? GetEventData() {
        string filepath = ".\\Project_RPG_Game\\data\\Missions\\InfoEvent.json";
        string jsonContent = File.ReadAllText(filepath);
        return JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string,string>>>>(jsonContent);
    }
    
    
    public Event EventGenerator() {
        
        
        var dataDescription = GetEventData()["Events"][Global.Random(0,GetEventData()["Events"].Count)];
        
        return new Event(
            dataDescription["name"],
            dataDescription["description"],
            ResultGenerator(),
            dataDescription["success"],
            ResultGenerator(-1),
            dataDescription["loses"]
            );
    }
    
    
    
    
    
    //**-------------------------------------------------------- RESULT ------------------------------------------------------------------

    public Result ResultGenerator(int win = 1) { // win = 1 --> bonus   & win = -1 -->  malus
        int nbBonusResult = Global.Random(NbHeroGenerator() - 1, NbHeroGenerator() + 2);
        
        Dictionary<ResultType,object> resultsData = new Dictionary<ResultType,object>
        {
            { ResultType.GuildMoney , ResultGeneratorInt(ListGen.Money)},
            { ResultType.HeroXp , ResultGeneratorInt(ListGen.Xp)},
            { ResultType.HeroHp , 0},
            { ResultType.GuildFoodStock , 0},
            { ResultType.HeroFood , FoodGen()},
            { ResultType.HerolevelUp , false},
            { ResultType.HeroUnequip , false},
            { ResultType.HeroStatus , null},
            { ResultType.GuildGainItem , null},
            { ResultType.GuildLoseItem , Selector.None},
        };

        Dictionary<ResultType, Func<int,object>> basicBonus = new Dictionary<ResultType, Func<int,object>>
        {
            {ResultType.HeroHp, (mod) => HpGen()*mod},
            {ResultType.GuildFoodStock,(mod) => ResultGeneratorInt(ListGen.FoodStock) * mod},
            {ResultType.HerolevelUp,(mod) => {
                if (mod == 1) { return true; }
                return false;
            }},
            {ResultType.HeroStatus,(mod) => {
                if (mod == 1) { return StatusGenerator(true); }
                return StatusGenerator();
            }},
            {ResultType.HeroUnequip, (mod) => {
                if (mod == 1){ return false; }
                return true;
            }},
            {ResultType.GuildGainItem,(mod) => {
                if (mod == 1) { return ItemGenerator(); }
                return null;
            }},
            {ResultType.GuildLoseItem, (mod) => {
                if (mod == 1) { return Selector.None; }
                return ResultGeneratorSelector();
            }}
            
        };
        
        for (int i = 0; i < nbBonusResult; i++) {
                var loot = basicBonus.ElementAt(Global.Random(0,basicBonus.Count));
                resultsData[loot.Key] = loot.Value(win);
                basicBonus.Remove(loot.Key);
               
        }
       
        return new Result(
            (int)resultsData[ResultType.HeroXp],
            (int)resultsData[ResultType.HeroHp],
            guildMoney:(int)resultsData[ResultType.GuildMoney],
            guildFoodStock:(int)resultsData[ResultType.GuildFoodStock],
            heroFood:(int)resultsData[ResultType.HeroFood]* -1 + win * 10,
            heroLevelUp:(bool)resultsData[ResultType.HerolevelUp],
            heroUnequipRandom:(bool)resultsData[ResultType.HeroUnequip],
            heroStatus:(Status)resultsData[ResultType.HeroStatus],
            guildGainItem:(Item)resultsData[ResultType.GuildGainItem],
            guildLoseItem:(Selector)resultsData[ResultType.GuildLoseItem]
            );
    }
    
    private int ResultGeneratorInt(List<int> dataList) {
        switch (Difficulty) {
            case Difficulty.Impossible:
                return Global.Random(dataList[4],dataList[5]);
            case Difficulty.Gamble :
                return Global.Random(dataList[3],dataList[4]);
            case Difficulty.Hard :
                return Global.Random(dataList[2],dataList[3]);
            case Difficulty.Normal :
                return Global.Random(dataList[1],dataList[2]);
            default:
                return Global.Random(dataList[0], dataList[1]);
        }
    }
    
    private Selector ResultGeneratorSelector() {
        List<Selector> selectors = new List<Selector>([Selector.One,Selector.Two,Selector.Three,Selector.Half,Selector.All]);
        switch (Difficulty) {
            case Difficulty.Normal :
                return selectors[Global.Random(0,3)];
            case Difficulty.Hard :
                return selectors[Global.Random(0,4)];
            case Difficulty.Gamble or Difficulty.Impossible:
                return selectors[Global.Random(0,selectors.Count)];
            default:
                return selectors[0];
        }   
    }
    
    
    //**-------------------------------------------------------- STATUS ------------------------------------------------------------------

    public Status StatusGenerator(bool positive = false) {
        var statusList = typeof(StatusList).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Func<int, Status>))
            .Select(f => (Func<int, Status>)f.GetValue(null))
            .ToList();
        
        
        while (true) {
            var selectStatus =  statusList[Global.Random(0,statusList.Count)](StatusModifierGenerator());
            if (positive && selectStatus is IPositiveStatus) {
                return selectStatus;
            }
            if (!positive && selectStatus is INegativeStatus) {
                return selectStatus;
            }
        }
    }

    private int StatusModifierGenerator() {
        switch (Difficulty) {
            case Difficulty.Gamble or Difficulty.Impossible :
                return Global.Random(3,6);
            case Difficulty.Normal :
                return Global.Random(1,3);
            case Difficulty.Hard :
                return 3;
            default:
                return 1;
        }
    }

    //**-------------------------------------------------------- ITEM ------------------------------------------------------------------
    
    public Item ItemGenerator() {
        var itemList = typeof(ItemList).GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(Func<Rarity, Item>))
            .Select(f => (Func<Rarity, Item>)f.GetValue(null))
            .ToList();
        
        var itemSelected = (Func<Rarity,Item>)itemList[Global.Random(0, itemList.Count)];
        return itemSelected(RarityGenerator());
    }
    private Rarity RarityGenerator() {
        List<Rarity> rarities = new List<Rarity>([Rarity.Common,Rarity.Rare,Rarity.Epic,Rarity.Legendary,Rarity.Mythic]);
        switch (Difficulty) {
            case Difficulty.Normal :
                return rarities[Global.Random(0,3)];
            case Difficulty.Hard :
                return rarities[Global.Random(1,4)];
            case Difficulty.Gamble or Difficulty.Impossible:
                return rarities[Global.Random(2,rarities.Count)];
            default:
                return rarities[0];
        }   
    }
    
    
}







