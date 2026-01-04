using Project_RPG_Game.characters;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;

public class UnLuck : Status , INegativeStatus, IAppliedOnce {
    public bool IsApplied = false;
    
    public UnLuck(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name =  $"UnLuck lvl {Modifier}";
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (!IsApplied) {
            hero.BonusLuck -= 10*Modifier;
            IsApplied = true;
        }
    }

    public void ClearEffect(Hero hero) {
        hero.BonusLuck += 10*Modifier;
    }
}