namespace Project_RPG_Game;

public class Hungry : Status,INegativeStatus {
    public Hungry(int expirationIn) : base(expirationIn) {
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        hero.ModifyFood((int)-(hero.FoodMax*0.1*Modifier));
        
    }
}