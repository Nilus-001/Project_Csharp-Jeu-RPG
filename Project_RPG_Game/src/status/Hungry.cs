namespace Project_RPG_Game;

public class Hungry : Status {
    public Hungry(int expirationIn) : base(expirationIn) {
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        hero.LoseFood((int)(hero.FoodMax*0.1));
        
    }
}