
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Project_RPG_Game;







public class Hero : Character {
    public int Hp;
    public int Food;
    public int Level;
    public int Xp;
    public int XpMax;
    public List<Status> StatusList;
    public List<Equipment> EquipmentList;
    public int EquipmentSlot;



    
    
    
    public Hero(string name, int hpMax, int foodMax ,Rarity rarity, Race race, GameClass gameclass, int salary) : 
        base(name, hpMax,foodMax, rarity, race, gameclass, salary) {
        Hp = hpMax;
        Food = foodMax;
        Level = 0;
        Xp = 0;
        XpMax = 100;
        EquipmentSlot = 1;
    }
    
    //--------------------------------------- XP ---------------------------------------

    public void GainXp(int xp) {
        if (Level < 5) {
            Xp += xp;
            if (Xp >= XpMax) {
                Xp -= XpMax ;
                LevelUp();
                
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

    public void GainHp(int hp) {
        Hp += hp;
        if (Hp > HpMax) {
            Hp = HpMax;
        }
    }

    public void LoseHp(int hp) {
        Hp -= hp;
        if (Hp <= 0) {
            //! mort 
        }
    }
    
    //--------------------------------------- Food ---------------------------------------

    public void GainFood(int food) {
        Food += food;
        if (Food > FoodMax) {
            Food = FoodMax;
        }
    }
    
    public void LoseFood(int food) {
        
        Food -= food;
        if (Food <= 0)  {
            Food = 0;
            if (!StatusIsActive(typeof(ExtremelyHungry))) {
                StatusList.Add(new ExtremelyHungry(100));
            }
        }
    }
    //--------------------------------------- Equipment ---------------------------------------

    public void Equip(Equipment equiment) {
        EquipmentList.Add(equiment);
    }

    public void Unequip(Equipment equiment) {
        EquipmentList.Remove(equiment);
    }
    
    //--------------------------------------- Status ---------------------------------------

    public void AppliedAllStatus() {
        foreach (Status status in StatusList) {
            status.AppliedEffect(this);
        }
        
    }
    

   





    
    
    
    
    private bool StatusIsActive(Type status) {
        foreach (var stat in StatusList) {
            return stat.GetType() == status;
        }
        return false;

        
    }





}