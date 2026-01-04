using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class XpBottle : Usable, IBonusItem {
    public int Xp;
    
    public XpBottle(string name, Rarity rarity) : base(name, $"Give {(int)rarity} Xp", rarity,"Project_RPG_Game/assets/item/ItemXpBottle.png") {
        Xp = (int)rarity;
    }

    public override string useOn(Hero hero) {
        hero.ModifyXp(Xp);
        return $"Regenerated {Xp} of Xp to {hero.Name}";
    }
}