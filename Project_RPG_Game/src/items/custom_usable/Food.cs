using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class Food : Usable , IHealItem{
    public int Nutrition;
    
    public Food(string name,Rarity rarity) : base(name,$"Regen {(int)rarity} food", rarity,"Project_RPG_Game/assets/item/ItemSacOfCookies.png") {
        Nutrition = (int)rarity;
    }

    public override string useOn(Hero hero) {
        hero.ModifyFood(Nutrition);
        return $"Regenerated {Nutrition} of Food to {hero.Name}";
    }
}