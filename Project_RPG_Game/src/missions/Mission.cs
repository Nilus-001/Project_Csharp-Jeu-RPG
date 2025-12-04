using System;
using System.Collections.Generic;
using Project_RPG_Game.missions.Result;


namespace Project_RPG_Game.missions;


public class Mission {
    private static int _idIncrement = 0;
    public int Id;
    public string Name;
    public string Description;
    public Difficulty Difficulty;
    public int NbHero;
    public MissionType Type;
    public TerrainType TerrainType;
    public List<Hero> ActiveHeroes;
    public Result.Result SuccessResult; //! setup class Result
    public string SuccessDescritpion;
    public Result.Result LoseResult;   
    public string LoseDescritpion;

    public Mission(string name, string description, Difficulty difficulty, int nbHero, MissionType type, TerrainType terrainType, Result.Result successResult, string successDescritpion, Result.Result loseResult, string loseDescritpion) {
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
        
        _idIncrement++;
        Id = _idIncrement;
    }

    public bool SetActiveHero(Hero hero) {
        if (ActiveHeroes.Count < NbHero && !ActiveHeroes.Contains(hero)) {
            ActiveHeroes.Add(hero);
            return true;
        }
        return false;   
    }
    
    
    

    public Result.Result ReturnResult() {
        int heroPercentageModifier = 0;
        foreach (var hero in ActiveHeroes) {
            heroPercentageModifier += hero.GetPercentageModifier(this);
        }
        
        bool success = Global.DiceRoll((int)Difficulty + heroPercentageModifier);
        if (success) {
            //! to do print Success Description
            return SuccessResult;
        }
        
        //! to do print Lose Description
        return LoseResult;
        
    }

}

