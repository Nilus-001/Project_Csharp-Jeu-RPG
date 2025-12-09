using System.Collections.Generic;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes.custom;

public class Miner : GameClass{
    
    public Miner(int percentageOfSuccess,List<MissionType> types) : base(percentageOfSuccess, types) {}

    public override string BonusResult(Mission mission) {
        if (Global.DiceRoll(PercentageOfSuccess)) {
            foreach (var type in Types) {
                if (type == mission.Type) {

                    mission.SuccessResult.Modifier = 2;
                    return $"Bonus triggered : {GetType().Name}";


                }
            }
        }
        return "";
    }
    
    
    
    
    
    
    
}