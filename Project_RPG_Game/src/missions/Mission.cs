using System;
using System.Collections;
using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions.Result;
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
    public Result.Result SuccessResult;
    public string SuccessDescritpion;
    public Result.Result LoseResult;   
    public string LoseDescritpion;

    public Mission(string name,
        string description,
        Difficulty difficulty,
        int nbHero,
        MissionType type,
        TerrainType terrainType, 
        Result.Result successResult,
        string successDescritpion,
        Result.Result loseResult,
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

    public void SetActiveHero(Hero hero) {
        if (ActiveHeroes.Count < NbHero && hero.StatusIsActive(hero ,typeof(Fatigue)) == null) {
            ActiveHeroes.Add(hero);
        }
        
    }
    
    
    

    public ArrayList ExecuteResult() {
        ArrayList resume = new ArrayList();
        
        int heroBonusLuck = 0;
        string bonusEventClass = "";
        foreach (var hero in ActiveHeroes) {
            heroBonusLuck += hero.GetBonusLuck(this);
            bonusEventClass += "///"+hero.GameClass.BonusResult(this);
        }
        
        
        
        bool success = Global.DiceRoll((int)Difficulty + heroBonusLuck);
        if (success) {
            resume.Add(SuccessDescritpion);
            resume.Add(SuccessResult.ExecuteResult(this));
            
        }else{
             resume.Add(LoseDescritpion);
             resume.Add(LoseResult.ExecuteResult(this));
        }
        
        resume.Add(bonusEventClass);
        return resume;
        
    }

}

