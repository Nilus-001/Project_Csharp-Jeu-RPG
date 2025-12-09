using System;
using System.Collections;
using System.Collections.Generic;
using Project_RPG_Game.missions;

namespace Project_RPG_Game;

public class Race {
    private static int _idincrement = 0;
    public int Id;
    public Dictionary<TerrainType,int> TerrainModifier;

    public Race(Dictionary<TerrainType, int> typesModifier) {
        TerrainModifier = typesModifier;
        
        _idincrement++;
        Id = _idincrement;
    }

    public int GetBonusMissionsLuck(Mission mission) {
        int percentageBonus = 0;
        foreach (var terrain in TerrainModifier) {
            if (terrain.Key == mission.TerrainType) {
                percentageBonus += terrain.Value;
            }
        }
        return percentageBonus;
    }
    
    
}