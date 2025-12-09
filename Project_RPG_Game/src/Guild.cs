using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Project_RPG_Game.characters;
using Project_RPG_Game.items;
using Project_RPG_Game.missions;

namespace Project_RPG_Game;

public class Guild {
    public int Money;
    public int FoodStock;
    public int Life = 3;
    public List<Hero> GuildHeroes = new List<Hero>();
    public List<Item> GuildInventory = new List<Item>();
    public List<Mission> GuildActiveMissions = new List<Mission>();

    public Guild( int startMoney, int startFood) {
        Money = startMoney;
        FoodStock = startFood;
        
    }

    public Guild(Guild guild) {
        Money = guild.Money;
        FoodStock = guild.FoodStock;
        Life = guild.Life;
        GuildHeroes = guild.GuildHeroes;
        GuildInventory = guild.GuildInventory;
        GuildActiveMissions = guild.GuildActiveMissions;
    }
    //--------------------------------------- Life ---------------------------------------
    
    public int Remove1Life() {
        Life--;
        if (Life < 0) {
            //! GAMEOVER
        }
        return Life;
    }
    
    
    
    //--------------------------------------- Money ---------------------------------------

    public int ModifyMoney(int money) {
        Money += money;
        if (Money < 0) {
            Money = 0;
            Remove1Life();
        }
        return Money;
    }
    //--------------------------------------- FoodStack ---------------------------------------

    public int ModifyFoodStock(int foodStock) {
        FoodStock += foodStock;
        if (FoodStock < 0) {
            FoodStock = 0;
        }
        return FoodStock;
    }
    
    
    
    //--------------------------------------- GuildHeroes ---------------------------------------

    public bool AddHero(Hero hero) {
        if (GuildHeroes.Count < 8) {
            GuildHeroes.Add(hero);
            hero.GuildMother = this;
            return true;
        }
        return false;
    }
    public bool KillHero(Hero hero) {
        if (GuildHeroes.Contains(hero)) {
            GuildHeroes.Remove(hero);
            return true;
        }
        return false;
    }
    //--------------------------------------- Inventory ---------------------------------------

    public bool AddInventory(Item item) {
        if (GuildInventory.Count < 12) {
            GuildInventory.Add(item);
            return true;
        }
        return false;
    }

    public bool RemoveInventory(Item item) {
        if (GuildInventory.Contains(item)) {
            GuildInventory.Remove(item);
            return true;
        }
        return false;
    }
    
    
    //--------------------------------------- Item---------------------------------------
    
    //? USELESS
    
    public bool UseUsable(Usable usable, Hero hero) {
        if (GuildInventory.Contains(usable) && GuildHeroes.Contains(hero)) {
            //! ajout de l'utilisation (usable a faire) : usable.UsuOn(hero)
            RemoveInventory(usable);
            return true;
        }
        return false;
    }
    
    public bool EquipEquipment(Equipment equip, Hero hero) {
        if (GuildInventory.Contains(equip) && GuildHeroes.Contains(hero)) {
            if (hero.Equip(equip)) {    // Check if a hero has available slot
                RemoveInventory(equip);
                return true;
            }
        }
        return false;
    }

    public bool UnequipEqsuipment(Equipment equip, Hero hero) {
        if (hero.EquipmentList.Contains(equip) && GuildHeroes.Contains(hero)) {
            if (AddInventory(hero.Unequip(equip))) {
                return true;
            }
        }
        return false;

    }
    
    
    //--------------------------------------- Mission---------------------------------------
    public void ExecuteMissions() {
        foreach (var mission in GuildActiveMissions.ToList()) {
            mission.ExecutionTimer--;
            if (mission.ExecutionTimer <= 0) {
                mission.ExecuteResult();
                GuildActiveMissions.Remove(mission);
            }
        }
    }

    public void AddMission(Mission mission) {
        GuildActiveMissions.Add(mission);
    }
    
    
    //--------------------------------------- Output ---------------------------------------



    public void PrintData() {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine(" // Money: " + Money +
                          " // FoodStock : " + FoodStock +
                          " // Life : " + Life +
                          " // Missions : " + GuildActiveMissions.Count +
                          " // Inventory : "+GuildInventory.Count +
                          " // Heroes : "+GuildHeroes.Count
                          
        );
        Console.WriteLine("------------------------------------------------");
        
    }




}