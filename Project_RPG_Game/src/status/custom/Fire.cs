using Project_RPG_Game.characters;

namespace Project_RPG_Game.status.custom;

public class Fire : Status, INegativeStatus {
    public Fire(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name =  $"Fire lvl {Modifier}";

    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        hero.ModifyHp(5*Modifier);
    }
    
}