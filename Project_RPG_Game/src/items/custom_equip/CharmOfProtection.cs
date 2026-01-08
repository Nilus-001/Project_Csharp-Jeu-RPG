using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class CharmOfProtection : Equipment , IBonusItem , IResultBonus {
    public int PercentOfReduction;

    public CharmOfProtection(string name, Rarity rarity) : base(name, $"Grant +{(int)rarity}% of Damage Reduction During Missions", rarity,"Project_RPG_Game/assets/item/ItemCharmOfProtection.png") {
        PercentOfReduction =(int)rarity;
    }
    
    public Dictionary<ResultType, object> AppliedBonus(Dictionary<ResultType, object> data, Hero hero) {
        if (data.ContainsKey(ResultType.HeroHp) && (int)data[ResultType.HeroHp] < 0) {
            int bonus = (int)data[ResultType.HeroHp] * (1 - PercentOfReduction/100);
            data[ResultType.HeroHp] = bonus;
        }
        return data;
    }
}