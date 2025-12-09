using Project_RPG_Game.classes;

namespace Project_RPG_Game.characters;





public class Character {
    private static int _idIncrement = 0;
    public int Id;
    public string Name;
    public int HpMax;
    public int FoodMax;
    public Race Race;
    public GameClass GameClass;
    public Rarity Rarity;
    

    public Character(string name, int hpMax, int foodMax,Rarity rarity, Race race, GameClass gameClass) {
        Name = name;
        HpMax = hpMax;
        FoodMax = foodMax;
        Race = race;
        GameClass = gameClass;
        
        Rarity = rarity;
        
        _idIncrement++;
        Id =  _idIncrement;
        
    }

}