using Project_RPG_Game.characters;

namespace Project_RPG_Game.items;

public abstract class Usable: Item {
        
    public Usable(string name, string description, Rarity rarity) : base(name, description, rarity) {
    }


    public abstract string useOn(Hero hero); 
    
    

}