using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_RPG_Game.missions.result;

public abstract class OnCharacterResult:Result.Result {
    public int Hp;
    public int Food;
    public int Salary;
    public bool LevelUp;
    public int Xp;
    public Status Status;
    public bool Unequip;
    public bool All;
    // public bool Success;

    protected OnCharacterResult(bool all = true ,Status status = null, bool unequiprandom = false, int hp = 0, int food = 0, int salary = 0, bool levelUp = false, int xp = 0) {
        Status = status;
        Unequip = unequiprandom;
        Hp = hp;
        Food = food;
        Salary = salary;
        LevelUp = levelUp;
        Xp = xp;
        All = all;
    }
    
    public override Dictionary<Hero,Dictionary<string, object>> ExecuteResult(Mission mission) {
        
        Dictionary<Hero,Dictionary<string, object>> dataModified = new Dictionary<Hero,Dictionary<string, object>>();
        Dictionary<string, object> modif = new Dictionary<string, object>();
        
        
        foreach (var hero in mission.ActiveHeroes) {
            
            void AddToModif(string key, object value, Action<double> apply) {
                double newModifier = Modifier;
                bool dodge = false;
                int dice = new Random().Next(0, Modifier);
                if (value is int) {
                    if ((int)value < 0) {
                        newModifier = 1/Modifier;
                    }
                } 
                
                if (value is INegativeStatus) {
                    dodge = dice !=0;
                }
                if (value is bool) {
                    if ((bool)value) {
                        dodge = dice !=0;
                    }
                }
                if (!dodge) {
                    apply(newModifier);
                    modif.Add(key, value);
                }
            }
            
            
            
            if (Hp != 0) {
                if (Hp < 0){}
                AddToModif("hp", Hp,(newModifier) => hero.ModifyHp((int)(Hp * newModifier)));
            }
            if (Food != 0) {
                AddToModif("food",Food,(newModifier) => hero.ModifyFood((int)(Food * newModifier)));
            }
            if (Salary != 0) {
                AddToModif("salary",Salary,(newModifier) => hero.Salary += (int)(Salary * newModifier));
            }
            if (Xp != 0) {
                AddToModif("xp",Xp,(newModifier) => hero.ModifyXp((int)(Xp * newModifier)));
            }
            if (LevelUp) {
                AddToModif("levelUp",LevelUp,(newModifier) => hero.LevelUp());
            }
            if (Status is not null) {
                AddToModif("status", Status,(newModifier) => hero.AddStatus(Status,modifier:(int)newModifier));
            }
            if (Unequip && hero.EquipmentList.Count > 0) {
                Equipment item = hero.EquipmentList[new Random().Next(0, hero.EquipmentList.Count)];
                AddToModif("unequip",item,(newModifier) => hero.Unequip(item));
            }
            
            dataModified[hero] = modif;
            modif.Clear();
            
        }
        return dataModified;
        
    }



        
        
        
        
   
    

    
}