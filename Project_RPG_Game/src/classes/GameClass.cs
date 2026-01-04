using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes;

public abstract class GameClass {
    private static int _idIncrement = 0;
    public int Id;
    public int PercentageOfSuccess;
    public List<MissionType> Types;
    public Rarity Rarity;
    public string Name;
    
 

    public GameClass(string name, int percentageOfSuccess, List<MissionType> successTypes, Rarity rarity) {
        PercentageOfSuccess = percentageOfSuccess;
        Types = successTypes;
        Rarity = rarity;
        Name = name;

        _idIncrement++;
        Id = _idIncrement;
    }

    public string BonusResult(Mission mission, Hero thisHero) {
        if (Global.DiceRoll(PercentageOfSuccess)) {
            foreach (var type in Types) {
                if (type == mission.Type) {
                    return Bonus(mission, thisHero);
                }
            }
        }
        return "";
    }


    protected abstract string Bonus(Mission mission, Hero thisHero);


}