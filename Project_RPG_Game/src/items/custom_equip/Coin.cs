using System.Collections.Generic;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.items.custom_equip;

public class Coin : Equipment,IBonusItem,IResultBonus {
    public Coin(string name,Rarity rarity) : base(name, $"Reduce by 1 the Salary for each completed Missions ", rarity,"Project_RPG_Game/assets/item/ItemCoin.png") {
        
    }

    public Dictionary<ResultType, object> AppliedBonus(Dictionary<ResultType, object> data, Hero hero) {
        if (data.ContainsKey(ResultType.HeroSalary)) {
            int bonus = (int)data[ResultType.HeroSalary] - 1;
            data[ResultType.HeroSalary] = bonus ;
            data.Add(ResultType.BonusSalary,bonus);
        }
        else {
            data[ResultType.HeroSalary] = -1;
            data.Add(ResultType.BonusSalary, -1);
        }
        return data;
    }
}