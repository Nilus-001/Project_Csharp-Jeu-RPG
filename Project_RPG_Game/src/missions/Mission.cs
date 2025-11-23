using System.Collections.Generic;
using Project_RPG_Game.missions.Lose;
using Project_RPG_Game.missions.Success;

namespace Project_RPG_Game.missions;

public abstract class Mission {
    public string Name;
    public string Description;
    public Difficulty Difficulty;
    public int NbHero;
    public List<MissionType> Types;   //! setup required in info_enumerated
    public List<Hero> ActiveHeroes;
    public SuccessResult SuccessResult; //! setup class SuccessResult
    public LoseResult LoseResult;   //! setup class LoseResult

}