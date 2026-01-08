using System.Collections.Generic;
using System.Linq;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Hammer : Equipment,IResultBonusOnGuild,IBonusItem {
    public int AdditionalDurability;
    
    public Hammer(string name, Rarity rarity) : base(name,$"Augment by {(int)rarity / 10} the Durability Max of Equipment acquired During Missions ", rarity,"Project_RPG_Game/assets/item/ItemHammer.png") {
        AdditionalDurability = (int)rarity / 10;
    }
    
    public Dictionary<ResultType, object> AppliedBonusOnGuild(Dictionary<ResultType, object> data, Guild guild) {
        if (data.ContainsKey(ResultType.GuildGainItem) && data[ResultType.GuildGainItem] is List<Item> itemList) {
            foreach (var item in itemList.ToList()) {
                if (item is Equipment equipment) {
                    equipment.DurabilityMax +=  AdditionalDurability;
                    equipment.Durability = DurabilityMax;
                }
            }
        }
        return data;
    }

}