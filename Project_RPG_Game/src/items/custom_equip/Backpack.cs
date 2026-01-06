using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Backpack : Equipment , IBonusItem , IResultBonusOnGuild {
    public int CapacityPercentageBonus;
    
    public Backpack(string name,  Rarity rarity) : base(name, $"Grant +{(int)rarity}% of Bonus FoodStock for the Guild During Missions", rarity,"Project_RPG_Game/assets/item/ItemBackpack.png") {
        CapacityPercentageBonus = (int)rarity/100;
    }
    

    public Dictionary<ResultType, object> AppliedBonusOnGuild(Dictionary<ResultType, object> data, Guild guild) {
        if (data.ContainsKey(ResultType.GuildFoodStock) && (int)data[ResultType.GuildFoodStock] > 0) {
            data[ResultType.GuildFoodStock] = (int) data[ResultType.GuildFoodStock] * (1 + CapacityPercentageBonus);
            data.Add(ResultType.BonusFoodStock, CapacityPercentageBonus);
        }
        return data;
    }
}