using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class CharmOfXp : Equipment , IBonusItem , IResultBonus{
    public int PercentOfBonus;
    
    
    public CharmOfXp(string name, Rarity rarity) : base(name, $"Grant +{(int)rarity}% of Bonus Xp During Missions", rarity,"Project_RPG_Game/assets/item/ItemCharmOfXp.png") {
        PercentOfBonus = (int)rarity;
    }


    public Dictionary<ResultType,object> AppliedBonus(Dictionary<ResultType,object> data , Hero hero) {
        int bonus = (int)data[ResultType.HeroXp] * (1 + PercentOfBonus/100);
        data[ResultType.HeroXp] = bonus;
        data.Add(ResultType.BonusXp, bonus);
        return data;
    }
}