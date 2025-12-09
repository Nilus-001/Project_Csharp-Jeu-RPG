using Project_RPG_Game.characters;

namespace Project_RPG_Game;

public class Hungry : Status,INegativeStatus {
    public Hungry(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name =  $"Hungry lvl {Modifier}";
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        hero.ModifyFood((int)-(hero.FoodMax*0.1*Modifier));
        
    }

    
}