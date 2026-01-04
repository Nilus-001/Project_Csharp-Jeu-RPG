using System;
using System.Collections.Generic;
using System.Linq;
using Project_RPG_Game.classes;
using Project_RPG_Game.items;
using Project_RPG_Game.missions;
using Project_RPG_Game.status;
using Project_RPG_Game.status.custom;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.characters;







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
    public int BonusLuck = 0;
    public Guild GuildMother;



    
    
    
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
        if (Level < 5 && StatusIsActive(this ,typeof(Frustration)) == null) {
            Xp += xp;
            if (Xp >= XpMax && Level < 5) {
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
        Status? injured = StatusIsActive(this, typeof(Injured));
        if (injured != null){
            Hp += hp * 2 * injured.Modifier;
        }else{
            Hp += hp;
        }
        if (Hp > HpMax) {
            Hp = HpMax;
        }else if (Hp <= 0) {
            Hp = 0;
            GuildMother.KillHero(this);
        }
    }

    
    
    //--------------------------------------- Food ---------------------------------------
    
    public void ModifyFood(int food) {
        
        Food += food;
        if (Food > FoodMax) {
            Food = FoodMax;
        }else if (Food <= 0)  {
            Food = 0;
            AddStatus(new Starving(100));
        }
        
    }
    //--------------------------------------- Equipment ---------------------------------------

    public bool Equip(Equipment equipment) {
        if (EquipmentList.Count < EquipmentSlot) {
            EquipmentList.Add(equipment);
            equipment.AttachedTo = this;
            return true;
        }
        return false;
    }

    public void Unequip(Equipment equipment) {
        equipment.AttachedTo = null;
        EquipmentList.Remove(equipment);
    }


    //--------------------------------------- Race / Class Effect ---------------------------------------

    public int GetBonusLuck(Mission mission) {
        return BonusLuck + Race.GetBonusMissionsLuck(mission);
    }

    //--------------------------------------- Status ---------------------------------------

    public void AppliedAllStatus() {
        if (StatusList.Count != 0) {
            foreach (var status in StatusList.ToList()) {
                if (status.ExpirationIn <= 0) {
                    if (status is IAppliedOnce statusOnce){
                        statusOnce.ClearEffect(this);
                        
                    }
                    StatusList.Remove(status);
                }
                else {
                    status.AppliedEffect(this);
                    
                }
            }
        }
        
        
    }

    
    public void AddStatus(Status status,int modifier=1) {
        if (StatusIsActive(this, typeof(Blessed)) == null) {
            
            Status? sameEffect = StatusIsActive(this, status.GetType());
            if (sameEffect != null) {
                status.Modifier += modifier ;
                StatusList.Remove(sameEffect);
            }
            StatusList.Add(status);
        }
        
    }



    public Status? StatusIsActive(Hero hero,Type status) {
        foreach (var stat in hero.StatusList) {
            if (stat.GetType() == status) {
                return stat;
            }
        }

        return null;
    }
    
    //--------------------------------------- Output ---------------------------------------


    public void PrintData() {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine(" // Name: " + Name +
                          " // Hp : " + Hp + "/"+HpMax +
                          " // Food : " + Food + "/" + FoodMax +
                          " // LVL : " + Level +
                          " // Xp : "+Xp+"/"+XpMax +
                          " // Salary : "+Salary+
                          " // EquipSlot : "+EquipmentSlot +
                          " // EquipementsList : "+ EquipmentList.Count +
                          " // StatusList : " + StatusList.Count
        );
        Console.WriteLine("------------------------------------------------");
        
    }
    





}