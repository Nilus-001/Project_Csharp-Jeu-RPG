using System.Collections.Generic;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes;

public abstract class GameClass {
    private static int _idIncrement = 0;
    public int Id;
    public int PercentageOfSuccess;
    public List<MissionType> Types;
    
 

    public GameClass(int percentageOfSuccess, List<MissionType> successTypes) {
        PercentageOfSuccess = percentageOfSuccess;
        Types = successTypes;

        _idIncrement++;
        Id = _idIncrement;
    }

    


    public abstract void BonusResult(Mission mission);


}