using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class Food : Usable , IHealItem{
    public int Nutrition;
    
    public Food(string name, string description, int nutrition) : base(name, description, Rarity.Classic) {
        Nutrition = nutrition;
    }

    public override string useOn(Hero hero) {
        hero.ModifyFood(Nutrition);
        return $"Regenerated {Nutrition} of Food to {hero.Name}";
    }
}