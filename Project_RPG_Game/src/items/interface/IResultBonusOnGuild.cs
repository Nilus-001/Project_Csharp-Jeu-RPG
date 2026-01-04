using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items;

public interface IResultBonusOnGuild {
    
    public Dictionary<ResultType,object> AppliedBonusOnGuild(Dictionary<ResultType,object> data, Guild guild);

}