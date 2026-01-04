using Project_RPG_Game.characters;

namespace Project_RPG_Game.items;

public class Equipment: Item {
    public Hero? AttachedTo;
    public int Durability;
    public int DurabilityMax;   // Nb of cycle
    
    public Equipment(string name, string description, Rarity rarity, string img) : base(name, description, rarity, img) {
        Durability = (int)rarity/10;
        DurabilityMax = (int)rarity/10;
    }

    public void LoseDurability() {
        Durability--;
        if (Durability <= 0) {
            AttachedTo.Unequip(this);
        }
    }

    public void Repair(int durability) {
        if (Durability + durability <= DurabilityMax) {
            Durability += durability;
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}