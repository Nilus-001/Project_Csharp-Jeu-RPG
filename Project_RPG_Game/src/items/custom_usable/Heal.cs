using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class Heal : Usable, IHealItem {
    public int Hp;

    public Heal(string name, Rarity rarity) : base(name, $"Regen {(int)rarity} Hp", rarity,"Project_RPG_Game/assets/item/ItemHeal.png")  {
        Hp = (int)rarity;
    }

    public override string useOn(Hero hero) {
        hero.ModifyHp(Hp);
        return $"Regenerated {Hp} of Hp to {hero.Name}";
    }
}