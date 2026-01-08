using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Amplificateur : Equipment ,IBonusItem , IResultBonus {
    public int AmplificationValue;
    public Amplificateur(string name, Rarity rarity) : base(name,$"Add {(int)rarity/10} to the strength of Status acquired During Missions " , rarity,"Project_RPG_Game/assets/item/ItemCrystalAmplificator.png") {
        AmplificationValue = (int)rarity/10;

    }

    public Dictionary<ResultType, object> AppliedBonus(Dictionary<ResultType, object> data, Hero hero) {
        if (data.ContainsKey(ResultType.HeroStatus) && data[ResultType.HeroStatus] is Status status) {
            data[ResultType.HeroStatus] = status.Modifier += AmplificationValue;
        }
        return data;
    }
}