using System.Reflection.Metadata.Ecma335;

namespace Project_RPG_Game;

public abstract class Status {
    private static int _idIncrement = 0;
    public int Id;
    public int ExpirationIn;


    public Status(int expirationIn) {
        _idIncrement++;
        Id = _idIncrement;
        ExpirationIn = expirationIn;
    }

    public virtual void AppliedEffect(Hero hero) {
        if (ExpirationIn <= 0) {
            hero.StatusList.Remove(this);
        }
        ExpirationIn -= 1;
        
        
        
        
        
        
    }
    
}
