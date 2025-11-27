using System.Collections;
using System.Collections.Generic;

namespace Project_RPG_Game.missions.Result;

public abstract class Result {
    private int _IdIcrement;
    public int Id;
    public int Modifier = 1;

    protected Result() {
        _IdIcrement++;
        Id = _IdIcrement;
    }
    
    public abstract Dictionary<Hero,Dictionary<string, object>> ExecuteResult(Mission mission);
    
    

}