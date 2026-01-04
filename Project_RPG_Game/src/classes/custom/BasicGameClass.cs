using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.classes.@interface;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes.custom;

public class BasicGameClass : GameClass , IPreResultClassBonus  {
    public BasicGameClass(string name, int percentageOfSuccess, List<MissionType> successTypes) : base(name,percentageOfSuccess, successTypes,Rarity.Classic) {
    }

    protected override string Bonus(Mission mission, Hero thisHero) {
        mission.SuccessResult.Modifier += 1;
        mission.LoseResult.Modifier += 1;
        return $"Bonus triggered : Class {Name}";
    }
}






//------------------------------------------ All Basic Game Class ----------------------------------------------
