using System;
using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;
using Project_RPG_Game.status;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.items.custom_equip;

public class ShieldOfTotalProtection : Equipment , IBonusItem, IResultBonusOnGuild , IResultBonus{
    public ShieldOfTotalProtection(string name,Rarity rarity) : base(name,$"Grant a Total Protection against All negative result During Mission " , rarity,"Project_RPG_Game/assets/item/ItemShieldOfTotalProtection.png") {
    }
    
    public Dictionary<ResultType, object> AppliedBonusOnGuild(Dictionary<ResultType, object> data, Guild guild) {
        return DataUpdate(data);
    }

    public Dictionary<ResultType, object> AppliedBonus(Dictionary<ResultType, object> data, Hero hero) {
        return DataUpdate(data);
    }

    private Dictionary<ResultType, object> DataUpdate(Dictionary<ResultType, object> data) {
        foreach (KeyValuePair<ResultType, object> pair in data) {
            if (pair.Value is int val) {
                if (val < 0 && pair.Key != ResultType.HeroSalary || val > 0 && pair.Key == ResultType.HeroSalary) {
                    data[pair.Key] = "Cancel";
                }
            }
            else if ((int) pair.Key > 100 && pair.Value is not IPositiveStatus) {
                data[pair.Key] = "Cancel";
            }
        }
        
        return data;
        
    }
}