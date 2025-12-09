using System;
using System.Collections;
using System.Collections.Generic;
using Project_RPG_Game.items;

namespace Project_RPG_Game.missions.Result;

public class Result {
    public int Modifier = 1;
    //---------------------- Hero ---------
    public int HeroHp;
    public int HeroFood;
    public int HeroSalary;
    public bool HeroLevelUp;
    public int HeroXp;
    public List<Status> HeroStatus;
    public bool HeroUnequipRandom;
    //----------------------- Guild --------
    public int GuildMoney;
    public int GuildFoodStock;
    public Selector GuildLoseItem;
    public List<Item> GuildGainItem ;
    
    protected Dictionary<object,Dictionary<string, object>> DataModified = new Dictionary<object,Dictionary<string, object>>();
    protected Dictionary<string, object> IndividualModif = new Dictionary<string, object>();
    
    
    
    public Result(
        int heroHp = 0,
        int heroFood = 0,
        int heroSalary = 0,
        bool heroLevelUp = false,
        int heroXp = 0,
        List<Status> heroStatus = null,
        bool heroUnequipRandom = false,
        int guildMoney = 0,
        int guildFoodStock = 0,
        Selector guildLoseItem = Selector.None,
        List<Item> guildGainItem = null) {
        
        HeroHp = heroHp;
        HeroFood = heroFood;
        HeroSalary = heroSalary;
        HeroLevelUp = heroLevelUp;
        HeroXp = heroXp;
        HeroStatus = heroStatus;
        HeroUnequipRandom = heroUnequipRandom;
        GuildMoney = guildMoney;
        GuildFoodStock = guildFoodStock;
        GuildLoseItem = guildLoseItem;
        GuildGainItem = guildGainItem;
    }

    public Dictionary<object, Dictionary<string, object>> ExecuteResult(Mission mission) {
        
        //------------------------- Guild -------------------------------------------------------------------------------
        Guild guild = mission.ActiveHeroes[0].GuildMother;
            
        
        
        AddToModif("FoodStock", GuildFoodStock,(newModifier) => guild.ModifyFoodStock((int)(GuildFoodStock * newModifier)));
        AddToModif("Money", GuildMoney,(newModifier) => guild.ModifyMoney((int)(GuildMoney * newModifier)));
        
        
        
        List<Item> itemlose = new List<Item>();
        for (int i = 0; i <=  Global.SelectorIntReturn(GuildLoseItem, guild.GuildInventory)-1; i++) {
            itemlose.Add(guild.GuildInventory[i]);
        }
        if (itemlose.Count > 0) {
            AddToModif("LoseItem", itemlose, (newModifier) => {
                    foreach (var item in itemlose) {
                        guild.RemoveInventory(item);
                    }
                }
            );
        }
        
        if (GuildGainItem is not null) {
            AddToModif("GainItem",GuildGainItem, (newModifier) => {
                    foreach (var item in GuildGainItem) {
                        guild.AddInventory(item);
                    }
                }
            );
        }
        
        
        DataModified[guild] = IndividualModif;
        IndividualModif.Clear();
        
        //------------------------- Hero -------------------------------------------------------------------------------
        
        foreach (var hero in mission.ActiveHeroes) {
            
            AddToModif("hp", HeroHp,(newModifier) => hero.ModifyHp((int)(HeroHp * newModifier)));
            AddToModif("food",HeroFood,(newModifier) => hero.ModifyFood((int)(HeroFood * newModifier)));
            AddToModif("salary",HeroSalary,(newModifier) => hero.Salary += (int)(HeroSalary * newModifier));
            AddToModif("xp",HeroXp,(newModifier) => hero.ModifyXp((int)(HeroXp * newModifier)));
            
            if (HeroLevelUp) {
                AddToModif("levelUp",HeroLevelUp,(newModifier) => hero.LevelUp());
            }
            if (HeroStatus is not null) {
                AddToModif("status", HeroStatus, (newModifier) => {
                        foreach (var stat in HeroStatus) {
                            hero.AddStatus(stat, modifier: (int)newModifier);
                        }
                    }
                );
                    
            }
            if (HeroUnequipRandom && hero.EquipmentList.Count > 0) {
                Equipment item = hero.EquipmentList[new Random().Next(0, hero.EquipmentList.Count)];
                AddToModif("unequip",item,(newModifier) => hero.Unequip(item));
            }
            
            DataModified[hero] = IndividualModif;
            IndividualModif.Clear();
        }
        
        return DataModified;
    }
    
    
    
    
    
    
    
    protected void AddToModif(string key, object value, Action<double> apply) {
        double newModifier = Modifier;
        bool dodge = false;
        int dice = new Random().Next(0, Modifier);
        
        if (value is int val) {
            if (val < 0) {
                newModifier = 1 / Modifier;
            }
        } 
        if (value is INegativeStatus || (value is bool boolean && boolean )) {
            dodge = dice !=0;
        }
       
        if (!dodge) {
            apply(newModifier);
            IndividualModif.Add(key, value);
        }
    }

}