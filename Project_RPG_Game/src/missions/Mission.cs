using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project_RPG_Game.characters;
using Project_RPG_Game.classes;
using Project_RPG_Game.classes.@interface;
using Project_RPG_Game.items;
using Project_RPG_Game.status.custom;


namespace Project_RPG_Game.missions;


public class Mission {
    private static int _idIncrement = 0;
    public int Id;
    public string Name;
    public string Description;
    public Difficulty Difficulty;
    public int ExecutionTimer;
    public int NbHero;
    public MissionType Type;
    public TerrainType TerrainType;
    public List<Hero> ActiveHeroes = new List<Hero>();
    public Result SuccessResult;
    public string SuccessDescritpion;
    public Result LoseResult;   
    public string LoseDescritpion;

    public Mission(string name,
        string description,
        Difficulty difficulty,
        int nbHero,
        MissionType type,
        TerrainType terrainType, 
        Result successResult,
        string successDescritpion,
        Result loseResult,
        string loseDescritpion,
        int executionTimer = 1) {
        
        Name = name;
        Description = description;
        Difficulty = difficulty;
        NbHero = nbHero;
        Type = type;
        TerrainType = terrainType;
        SuccessResult = successResult;
        SuccessDescritpion = successDescritpion;
        LoseResult = loseResult;
        LoseDescritpion = loseDescritpion;
        ExecutionTimer = executionTimer;

        _idIncrement++;
        Id = _idIncrement;
    }

    public bool SetActiveHero(Hero hero) {
        if (ActiveHeroes.Count < NbHero && hero.StatusIsActive(hero ,typeof(Fatigue)) == null) {
            ActiveHeroes.Add(hero);
            return true;
        }
        return false;
    }
    
    
    public virtual ArrayList ExecuteResult() {
        ArrayList resume = new ArrayList();
        
        int heroBonusLuck = 0;
        
        foreach (var hero in ActiveHeroes.ToList()) {
            heroBonusLuck += hero.GetBonusLuck(this);
            
            if (hero.GameClass is IPreResultClassBonus) {
                hero.GameClass.BonusResult(this,hero);
            }

            foreach (var equip in hero.EquipmentList.ToList()) {
                equip.LoseDurability();
            }
        }
        
        bool success = Global.DiceRoll((int)Difficulty + heroBonusLuck);
        if (success) {
            resume.Add(SuccessDescritpion);
            var data = SuccessResult.ReturnResult(this);
            resume.Add(data);
            ResultExecutor(data);
            
            
        }else{
             resume.Add(LoseDescritpion);
             var data = LoseResult.ReturnResult(this);
             resume.Add(data);
             ResultExecutor(data);
        }
        
        
        return resume;
        
    }
    
    protected void ResultExecutor(Dictionary<object, Dictionary<ResultType, object>> data) {
        foreach (var target in data) {
            //-----------------------Guild EXE-------------------------------
            if (target.Key is Guild guild) {
                Dictionary<ResultType, object> guildData = target.Value;
                foreach (var content in guildData) {
                    if (content.Value.ToString() != "Cancel") {
                        switch (content.Key) {
                            case ResultType.GuildFoodStock:
                                guild.ModifyFoodStock((int)content.Value);
                                break;
                            case ResultType.GuildMoney:
                                guild.ModifyMoney((int)content.Value);
                                break;
                            case ResultType.GuildLoseItem:
                                foreach (var item in (List<Item>)content.Value) {
                                    guild.RemoveInventory(item);
                                }
                                break;
                            case ResultType.GuildGainItem:
                                guild.AddInventory((Item)content.Value);
                                break;
                        }
                    }               
                }
            }

            if (target.Key is Hero hero) { 
                foreach (var content in target.Value) {
                    if (content.Value.ToString() != "Cancel") {
                        switch (content.Key) {
                            
                            case ResultType.HeroXp:
                                hero.ModifyXp((int)content.Value);
                                break;
                            case ResultType.HeroHp:
                                hero.ModifyHp((int)content.Value);
                                break;
                            case ResultType.HeroFood:
                                hero.ModifyFood((int)content.Value);
                                break;
                            case ResultType.HeroSalary:
                                hero.Salary += (int)content.Value;
                                break;
                            case ResultType.HerolevelUp:
                                hero.LevelUp();
                                break;
                            case ResultType.HeroStatus:
                                hero.AddStatus((Status)content.Value);
                                break;
                            case ResultType.HeroUnequip:
                                hero.Unequip((Equipment)content.Value);
                                break;
                        }
                    }
                }
            }
            
        }
        
    }
    
    
    public void PrintData() {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine(" // Name: " + Name +
                          " // Description : " + Description +
                          " // Difficulty : " + Difficulty +
                          " // ExecutionTimer : " + ExecutionTimer +
                          " // NbHero :  "+NbHero +
                          " // Type : "+Type+
                          " // TerrainType : "+TerrainType +
                          " // ActiveHeroes : "+ ActiveHeroes.Count +
                          " // SuccessDescritpion : " + SuccessDescritpion +
                          " \n\n// SuccessResult : " + SuccessResult.PrintData() +
                          " \n// LoseDescritpion : " + LoseDescritpion +
                          " \n\n// LoseResult : " + LoseResult.PrintData()
        );
        Console.WriteLine("------------------------------------------------");
        
    }
    
    
    

}

