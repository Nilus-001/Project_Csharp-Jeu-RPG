using System.Linq;
using Project_RPG_Game.characters;

namespace Project_RPG_Game.status.custom;

public class Blessed : Status , IAppliedOnce , IPositiveStatus {
    public bool IsApplied= false;
    public Blessed(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = $"Blessed";
    }
    
    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (!IsApplied) {
            hero.HpMax += hero.HpMax*2;
            IsApplied = true;
            foreach (var status in hero.StatusList.ToList()) {
                if (status != this) {
                    hero.StatusList.Remove(status);
                }
                
            }
        }
        
        
        
    }
    public void ClearEffect(Hero hero) {
        hero.HpMax += hero.HpMax/2;
    }
}