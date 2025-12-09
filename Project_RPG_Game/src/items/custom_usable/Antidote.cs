using System.Linq;
using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class Antidote : Usable , IHealItem{
    public Antidote(string name, string description, Rarity rarity) : base(name, description, rarity) {
    }

    public override string useOn(Hero hero) {
        foreach (var status in hero.StatusList.ToList()){
            hero.StatusList.Remove(status);
        }
        return $"Clear all {hero.Name}'s statuses";
    }
}