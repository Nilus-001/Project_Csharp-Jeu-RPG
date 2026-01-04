using System.Collections;
using Project_RPG_Game.characters;

namespace Project_RPG_Game.missions;

public class Event : Mission {
    
    public Event(
        string name,
        string description,
        Result successResult,
        string successDescritpion, 
        Result loseResult, 
        string loseDescritpion
        ) : base(
        name, 
        description, 
        Difficulty.Win,
        8, 
        MissionType.Event, 
        TerrainType.ElfVillage, 
        successResult, 
        successDescritpion, 
        loseResult, 
        loseDescritpion, 
        0) {
    }
    
    public void SetActive(Guild guild) {
        foreach (var hero in guild.GuildHeroes) {
            bool isBusy = false ;
            foreach (var mission in guild.GuildActiveMissions) {
                    if (mission.ActiveHeroes.Contains(hero)) {
                        isBusy = true;
                        break;
                    }
            }
            if (!isBusy) {
                ActiveHeroes.Add(hero);
            }
        }
    }


    public override ArrayList ExecuteResult() {
        ArrayList resume = new ArrayList();

        bool success = Global.DiceRoll(50);
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
}