using System.Linq;
using Project_RPG_Game.characters;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;

public class Blessed : Status , IAppliedOnce , IPositiveStatus {
    public bool IsApplied= false;
    public int Hpgive;
    public Blessed(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = $"Blessed";
    }
    
    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (!IsApplied) {
            Hpgive = hero.HpMax;
            float percent = hero.Hp / hero.XpMax;
            hero.HpMax *= 2;
            hero.ModifyFood((int)(hero.Hp * percent));
            IsApplied = true;
            foreach (var status in hero.StatusList.ToList()) {
                if (status != this) {
                    hero.StatusList.Remove(status);
                }
                
            }
        }
        
        
        
    }
    public void ClearEffect(Hero hero) {
        hero.HpMax -= Hpgive;
    }
}