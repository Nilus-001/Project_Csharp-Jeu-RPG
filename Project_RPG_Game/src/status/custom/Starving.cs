using Project_RPG_Game.characters;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;

public class Starving : Status,INegativeStatus {
    
    public Starving(int expirationIn) : base(expirationIn) {
        Name =  $"Starving lvl {Modifier}";
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (hero.Food > 0) {
            hero.StatusList.Remove(this);
        }
        else {
            hero.Hp /= 2;
        }
        
    }

   
}