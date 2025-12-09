using System.Reflection.Metadata.Ecma335;

namespace Project_RPG_Game.items;

public abstract class Item {
    public Rarity Rarity;
    public string Name;
    public string Description;

    public Item(string name, string description, Rarity rarity) {
        Name = name;
        Description = description;
        Rarity = rarity;
    }
    
    
    
    
}