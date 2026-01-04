using Project_RPG_Game.characters;
using Project_RPG_Game.classes;
using Project_RPG_Game.races;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;

public class Confusion : Status, IAppliedOnce , INegativeStatus{
    bool IsApplied = false;
    private GameClass savedHeroClass;
    private Race savedHeroRace;
    public Confusion(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = $"Confusing lvl {Modifier}";
    }
    
    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (!IsApplied) {
            savedHeroClass = hero.GameClass;
            savedHeroRace = hero.Race;
            hero.GameClass = GameClassList.Null;
            hero.Race = RaceList.Null;
            IsApplied = true;
        }
    }
    
    

    public void ClearEffect(Hero hero) {
        hero.GameClass =savedHeroClass;
        hero.Race = savedHeroRace;
    }
}