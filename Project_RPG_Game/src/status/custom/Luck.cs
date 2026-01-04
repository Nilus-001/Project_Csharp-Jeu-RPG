using Project_RPG_Game.characters;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;

public class Luck : Status ,IPositiveStatus,IAppliedOnce {
    public bool IsApplied = false;
    public Luck(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name =  $"Luck lvl {Modifier}";
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (!IsApplied) {
            hero.BonusLuck += 10*Modifier;
            IsApplied = true;
        }
    }

    public void ClearEffect(Hero hero) {
        hero.BonusLuck -= 10*Modifier;
    }
}