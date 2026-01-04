using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.classes.@interface;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.classes.custom;

public class Thief : GameClass , IPostResultClassBonus {
    public int TheftPercentage;
    public Dictionary<object, Dictionary<ResultType, object>> Data;
    
    public Thief(int percentageOfSuccess, List<MissionType> successTypes, int theftPercentage) : base("Thief",percentageOfSuccess, successTypes, Rarity.Rare) {
        TheftPercentage = theftPercentage;
    }


    protected override string Bonus(Mission mission, Hero thisHero) {
        int theft = 0;
        foreach (var heroContent in Data) {
            if (heroContent.Key is Hero hero) {
                if (hero != thisHero) {
                    int heroXp = (int)heroContent.Value[ResultType.HeroXp];
                    int th = (int)( heroXp * (TheftPercentage/100));
                    theft += th;
                    
                    heroContent.Value[ResultType.HeroXp] = heroXp - th;
                    heroContent.Value.Add(ResultType.GameClassTheft,$"Xp Stolen by {thisHero.Name}");
                }
            }
        }

        Data[thisHero][ResultType.HeroXp] = (int) Data[thisHero][ResultType.HeroXp] + theft;
        Data[thisHero].Add(ResultType.GameClassTheft,$"Xp Stolen : {theft} ");
        
        return $"Bonus triggered : Class {GetType().Name}";
    }


    
    public Dictionary<object, Dictionary<ResultType, object>> ModifyResult(Mission mission, Hero thisHero, Dictionary<object, Dictionary<ResultType, object>> data) {
        Data = data;
        BonusResult(mission, thisHero);
        return Data;
    }
}