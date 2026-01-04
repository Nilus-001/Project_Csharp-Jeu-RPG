using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Project_RPG_Game.characters;

namespace Project_RPG_Game;

public abstract class Status {
    private static int _idIncrement = 0;
    public int Id;
    public string Name;
    public int ExpirationIn;
    public int Modifier;
    


    public Status(int expirationIn, int modifier = 1) {
        _idIncrement++;
        Id = _idIncrement;
        ExpirationIn = expirationIn;
        Modifier = modifier;
    }

    public virtual void AppliedEffect(Hero hero) {
        ExpirationIn -= 1;
        
        
        
    }
    

    public override string ToString() {
        return Name;
    }
    

    
}
