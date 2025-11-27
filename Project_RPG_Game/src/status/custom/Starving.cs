namespace Project_RPG_Game;

public class Starving : Status,INegativeStatus {
    
    public Starving(int expirationIn) : base(expirationIn) {
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