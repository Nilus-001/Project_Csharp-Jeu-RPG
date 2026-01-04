using System;
using System.Collections.Generic;
using Project_RPG_Game.items;
using Project_RPG_Game.status;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.missions;

public enum ResultType { // "> 100" = peut ou est negatif | "< 100" = est toujours positif
    HeroHp = 101,
    HeroFood = 102,
    HeroSalary = 103,
    HeroXp = 104,
    HerolevelUp = 001,
    HeroStatus = 105,
    HeroUnequip = 106,
    GuildMoney = 107,
    GuildFoodStock = 108,
    GuildLoseItem = 109,
    GuildGainItem = 002,
    
    BonusXp,
    BonusProtection,
    BonusMoney,
    BonusFoodStock,
    BonusSalary,
    BonusAmplification,
    BonusAdditionalDurability,
    
    GameClassTheft,
}

public class Result {
    public int Modifier = 1;
    //---------------------- Hero ---------
    public int HeroHp;
    public int HeroFood;
    public int HeroSalary;
    public bool HeroLevelUp;
    public int HeroXp;
    public Status HeroStatus;
    public bool HeroUnequipRandom;
    //----------------------- Guild --------
    public int GuildMoney;
    public int GuildFoodStock;
    public Selector GuildLoseItem;
    public Item GuildGainItem ;
    
    protected Dictionary<object,Dictionary<ResultType, object>> DataModified = new Dictionary<object,Dictionary<ResultType, object>>();
    protected Dictionary<ResultType, object> IndividualModif = new Dictionary<ResultType, object>();
    
    
    public Result(
        int heroXp,
        
        int heroHp = 0,
        int heroFood = 0,
        int heroSalary = 0,
        bool heroLevelUp = false,
        Status? heroStatus = null,
        bool heroUnequipRandom = false,
        int guildMoney = 0,
        int guildFoodStock = 0,
        Selector guildLoseItem = Selector.None,
        Item? guildGainItem = null) {
        
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

    public Dictionary<object, Dictionary<ResultType, object>> ReturnResult(Mission mission) {
        DataModified.Clear();
         
        //------------------------- Guild -------------------------------------------------------------------------------
        Guild guild = mission.ActiveHeroes[0].GuildMother;
        
        if (GuildFoodStock != 0){AddToModif(ResultType.GuildFoodStock, GuildFoodStock);}
        if (GuildMoney != 0){AddToModif(ResultType.GuildMoney, GuildMoney);}
        
        
        List<Item> itemlose = new List<Item>();
        for (int i = 0; i <=  Global.SelectorIntReturn(GuildLoseItem, guild.GuildInventory)-1; i++) {
            itemlose.Add(guild.GuildInventory[i]);
        }
        if (itemlose.Count > 0) {
            AddToModif(ResultType.GuildLoseItem, itemlose);
        }
        
        if (GuildGainItem is not null) {
            AddToModif(ResultType.GuildGainItem,GuildGainItem);
        }



        Dictionary<ResultType, object> guildModif = new Dictionary<ResultType, object>(IndividualModif);
        IndividualModif.Clear();
        
        //------------------------- Hero -------------------------------------------------------------------------------
        
        foreach (var hero in mission.ActiveHeroes) {
            
            if (HeroXp != 0) {AddToModif(ResultType.HeroXp,HeroXp);}
            if (HeroHp != 0) {AddToModif(ResultType.HeroHp, HeroHp);}
            if (HeroFood != 0) {AddToModif(ResultType.HeroFood,HeroFood);}
            if (HeroSalary != 0) {AddToModif(ResultType.HeroSalary,HeroSalary);}
            
            if (HeroLevelUp) {
                AddToModif(ResultType.HerolevelUp,HeroLevelUp);
            }
            if (HeroStatus is not null) {
                AddToModif(ResultType.HeroStatus, HeroStatus);
            }
            if (HeroUnequipRandom && hero.EquipmentList.Count > 0) {
                Equipment item = hero.EquipmentList[new Random().Next(0, hero.EquipmentList.Count)];
                AddToModif(ResultType.HeroUnequip,item);
            }
            
            
            // -------------------------- Charms Effect -------------------------
            foreach (var equip in hero.EquipmentList) {
                if (equip is IResultBonus charm) {
                    IndividualModif = charm.AppliedBonus(IndividualModif , hero);
                }
                if (equip is IResultBonusOnGuild tool) {
                    guildModif = tool.AppliedBonusOnGuild(guildModif, guild);
                }
            }
            
            DataModified.Add(hero,new Dictionary<ResultType, object>(IndividualModif));
            DataModified[guild] =  new Dictionary<ResultType, object>(guildModif);
            
            
            IndividualModif.Clear();
        }

        
        
        return DataModified;
    }
    
    
    
    
    
    
    
    protected void AddToModif(ResultType key, object value) {
        double newModifier = Modifier;
        int dice = new Random().Next(0, Modifier);
        
        if ((int)key > 100) {
            if (value is int val) {
                if (val < 0 && key != ResultType.HeroSalary || val > 0 && key == ResultType.HeroSalary) {
                    newModifier = 1 / newModifier;
                }
                value = (int) (val * newModifier);
            }
            else if (value is Status stat && value is IPositiveStatus) {
                stat.Modifier =(int)newModifier;
            }
            else if (dice != 0){
                value = "Cancel";
                Console.WriteLine(key + " : " + value +"= cancel" );
            }
            
        }
       
        IndividualModif.Add(key, value);
       
    }



    public string PrintData() {
        return $"HeroHp : {HeroHp} HeroFood : {HeroFood} HeroSalary : {HeroSalary} HeroLevelUp : {HeroLevelUp} HeroXp : {HeroXp}HeroStatus : {HeroStatus} HeroUnequipRandom : {HeroUnequipRandom} GuildMoney : {GuildMoney} GuildFoodStock : {GuildFoodStock} GuildLoseItem : {GuildLoseItem} GuildGainItem : {GuildGainItem}";
    }

}