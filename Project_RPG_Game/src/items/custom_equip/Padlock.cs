using System.Collections.Generic;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Padlock : Equipment , IBonusItem , IResultBonusOnGuild{
    public Padlock(string name, Rarity rarity) : base(name, $"Prevent the Guild to Lose Item During Missions ", rarity,"Project_RPG_Game/assets/item/ItemPadlock.png") {
    }

    public Dictionary<ResultType, object> AppliedBonusOnGuild(Dictionary<ResultType, object> data, Guild guild) {
        data[ResultType.GuildLoseItem] = "Cancel";
        return data;
    }
}