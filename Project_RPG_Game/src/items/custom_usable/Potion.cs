using System.Net.Http;
using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class Potion : Usable, IBonusItem{
    public Status Effect;
    
    public Potion(string name, string description, Status effect) : base(name, description , Rarity.Rare) {
        Effect = effect;
    }
    public override string useOn(Hero hero) {
        hero.AddStatus(Effect);
        return $"Applied Effect {Effect.Name} to {hero.Name}";
    }
}