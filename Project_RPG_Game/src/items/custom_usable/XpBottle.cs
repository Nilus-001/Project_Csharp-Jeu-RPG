using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class XpBottle : Usable, IBonusItem {
    public int Xp;
    
    public XpBottle(string name, string description, Rarity rarity, int xp) : base(name, description, rarity) {
        Xp = xp;
    }

    public override string useOn(Hero hero) {
        hero.ModifyXp(Xp);
        return $"Regenerated {Xp} of Xp to {hero.Name}";
    }
}