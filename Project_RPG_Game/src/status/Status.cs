using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Project_RPG_Game;

public abstract class Status {
    private static int _idIncrement = 0;
    public int Id;
    public int ExpirationIn;
    public int Modifier;


    public Status(int expirationIn, int modifier = 1) {
        _idIncrement++;
        Id = _idIncrement;
        ExpirationIn = expirationIn;
        Modifier = modifier;
    }

    public virtual void AppliedEffect(Hero hero) {
        if (ExpirationIn <= 0) {
            hero.StatusList.Remove(this);
        }
        
        ExpirationIn -= 1;
        
        
        
    }
    

    
}
