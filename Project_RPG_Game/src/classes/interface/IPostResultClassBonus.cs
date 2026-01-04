using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes.@interface;

public interface IPostResultClassBonus {
    public Dictionary<object, Dictionary<ResultType, object>> ModifyResult(Mission mission, Hero thisHero, Dictionary<object, Dictionary<ResultType, object>> data);
}