
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project_RPG_Game.classes;
using Project_RPG_Game.missions;


namespace Project_RPG_Game;







public class Hero : Character {
    public int Hp;
    public int Food;
    public int Salary;
    public int Level;
    public int Xp;
    public int XpMax;
    public List<Status> StatusList = [];
    public List<Equipment> EquipmentList = [];
    public int EquipmentSlot;



    
    
    
    public Hero(string name, int hpMax, int foodMax ,Rarity rarity, Race race, GameClass gameclass, int salary) : 
        base(name, hpMax,foodMax, rarity, race, gameclass) {
        Hp = hpMax;
        Food = foodMax;
        Salary = salary;
        Level = 0;
        Xp = 0;
        XpMax = 100;
        EquipmentSlot = 1;
    }
    
    //--------------------------------------- XP ---------------------------------------

    public void ModifyXp(int xp) {
        if (Level < 5) {
            Xp += xp;
            if (Xp >= XpMax) {
                Xp -= XpMax ;
                LevelUp();
                
            }if (Xp <= 0) {
                Xp = 0;
            }
        }
    }
    public void LevelUp() {
        if (Level < 5) {
            Level++;
            XpMax += 50;
            //-------Modify Stat-------
            HpMax = (int) (HpMax*1.1);
            FoodMax =  (int) (FoodMax*1.1);
            EquipmentSlot++;
            Salary = (int) (Salary*1.1);
            StatusList.Clear();
            

        }
    }
    //--------------------------------------- HP ---------------------------------------

    public void ModifyHp(int hp) {
        Hp += hp;
        if (Hp > HpMax) {
            Hp = HpMax;
        }else if (Hp <= 0) {
            //! mort 
        }
    }
    
    //--------------------------------------- Food ---------------------------------------
    
    public void ModifyFood(int food) {
        
        Food += food;
        if (Food > FoodMax) {
            Food = FoodMax;
        }else if (Food <= 0)  {
            Food = 0;
            StatusList.Add(new Starving(100));
        }
    }
    //--------------------------------------- Equipment ---------------------------------------

    public void Equip(Equipment equiment) {
        EquipmentList.Add(equiment);
    }

    public Equipment Unequip(Equipment equiment) {
        EquipmentList.Remove(equiment);
        return equiment;
    }
    
    //--------------------------------------- Status ---------------------------------------

    public void AppliedAllStatus() {
        foreach (Status status in StatusList) {
            status.AppliedEffect(this);
        }
        
    }
    
    //--------------------------------------- Race / Class Effect ---------------------------------------

    public int GetPercentageModifier(Mission mission) {
        return Race.GetPercetageModifier(mission);
    }

    //--------------------------------------- Status ---------------------------------------

    public void AddStatus(Status status,int modifier=1) {
        
        Status SameEffect = StatusIsActive(this, GetType());
        if (SameEffect != null) {
            status.Modifier += modifier ;
            StatusList.Remove(SameEffect);
        }
        StatusList.Add(status);
    }



    private Status StatusIsActive(Hero hero,Type status) {
        foreach (var stat in hero.StatusList) {
            if (stat.GetType() == status) {
                return stat;
            }
        }

        return null;
    }
    
    
    
    
    





}