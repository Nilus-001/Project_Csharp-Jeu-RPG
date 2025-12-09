using Project_RPG_Game.characters;

namespace Project_RPG_Game.items.custom_usable;

public class Heal : Usable, IHealItem {
    public int Hp;

    public Heal(string name, string description, Rarity rarity, int hp) : base(name, description, rarity)  {
        Hp = hp;
    }

    public override string useOn(Hero hero) {
        hero.ModifyHp(Hp);
        return $"Regenerated {Hp} of Hp to {hero.Name}";
    }
}