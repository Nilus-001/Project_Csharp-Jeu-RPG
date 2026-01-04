using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Amplificateur : Equipment ,IBonusItem , IResultBonus {
    public int AmplificationValue;
    public Amplificateur(string name, Rarity rarity) : base(name,$"Multiply by {(int)rarity} the strength of Status acquired During Missions " , rarity,"Project_RPG_Game/assets/item/ItemCrystalAmplificator.png") {
        AmplificationValue = (int)rarity;

    }

    public Dictionary<ResultType, object> AppliedBonus(Dictionary<ResultType, object> data, Hero hero) {
        if (data[ResultType.HeroStatus] is Status status) {
            data[ResultType.HeroStatus] = status.Modifier += AmplificationValue;
            data.Add(ResultType.BonusAmplification, AmplificationValue);
        }
        return data;
    }
}