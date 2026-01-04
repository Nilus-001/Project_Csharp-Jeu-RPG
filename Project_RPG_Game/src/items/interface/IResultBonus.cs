using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items;

public interface IResultBonus {

    public Dictionary<ResultType,object> AppliedBonus(Dictionary<ResultType,object> data, Hero hero);
}