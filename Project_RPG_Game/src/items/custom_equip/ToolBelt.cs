using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class ToolBelt : Equipment,IResultBonus, IBonusItem{
    public ToolBelt(string name, Rarity rarity) : base(name,$"Prevent the hero to Lose Equipment During Missions ", rarity,"Project_RPG_Game/assets/item/ItemToolBelt.png") {
    }

    public Dictionary<ResultType, object> AppliedBonus(Dictionary<ResultType, object> data, Hero hero) {
        if (data.ContainsKey(ResultType.HeroUnequip)) {
            data[ResultType.HeroUnequip] = "Cancel";
        }
        return data;
    }
}