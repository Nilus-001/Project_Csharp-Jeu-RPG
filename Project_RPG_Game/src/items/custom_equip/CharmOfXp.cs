using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class CharmOfXp : Equipment , IBonusItem , IResultBonus{
    public int PercentOfBonus;
    
    
    public CharmOfXp(string name, Rarity rarity) : base(name, $"Grant +{(int)rarity}% of Bonus Xp During Missions", rarity,"Project_RPG_Game/assets/item/ItemCharmOfXp.png") {
        PercentOfBonus = (int)rarity/100;
    }


    public Dictionary<ResultType,object> AppliedBonus(Dictionary<ResultType,object> data , Hero hero) {
        data[ResultType.HeroXp] = (int)data[ResultType.HeroXp] * (1 + PercentOfBonus);
        data.Add(ResultType.BonusXp, PercentOfBonus);
        return data;
    }
}