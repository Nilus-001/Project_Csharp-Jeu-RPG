using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Magnet : Equipment , IBonusItem ,IResultBonusOnGuild {
    public int MoneyBonusPercentage;
    
    public Magnet(string name , Rarity rarity) : base(name, $"Grant +{(int)rarity}% of Bonus Money for the Guild During Missions", rarity,"Project_RPG_Game/assets/item/ItemMagnet.png") {
        MoneyBonusPercentage = (int)rarity;
    }

    public Dictionary<ResultType, object> AppliedBonusOnGuild(Dictionary<ResultType, object> data, Guild guild) {
        if ((int)data[ResultType.GuildMoney] > 0) {
            int bonus = (int)data[ResultType.GuildMoney] * (1 + MoneyBonusPercentage / 100);
            data[ResultType.GuildMoney] = bonus ;
        }

        return data;
    }
}