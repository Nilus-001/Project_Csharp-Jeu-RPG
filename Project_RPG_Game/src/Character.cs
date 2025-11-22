namespace Project_RPG_Game;





public class Character {
    private static int _idIncrement = 0;
    public int Id;
    public string Name;
    public int HpMax;
    public int FoodMax;
    public Race Race;
    public GameClass GameClass; 
    public Rarity Rarity;
    public int Salary;


    public Character(string name, int hpMax, int foodMax,Rarity rarity, Race race, GameClass gameClass, int salary) {
        Name = name;
        HpMax = hpMax;
        FoodMax = foodMax;
        Race = race;
        GameClass = gameClass;
        Salary = salary;
        Rarity = rarity;
        
        _idIncrement++;
        Id =  _idIncrement;
        
    }

}