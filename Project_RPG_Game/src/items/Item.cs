using System.Reflection.Metadata.Ecma335;

namespace Project_RPG_Game.items;

public abstract class Item {
    public Rarity Rarity;
    public string Name;
    public string Description;
    public string Img;

    public Item(string name, string description, Rarity rarity, string img) {
        Name = name;
        Description = description;
        Rarity = rarity;
        Img = img;
    }
    
    public override string ToString() {
        return Name;
    }
    
    
}