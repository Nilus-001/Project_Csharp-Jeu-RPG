namespace Project_RPG_Game;

public class GameClass {
    private static int id_increment = 0;
    public int id;

    public GameClass() {
        id_increment++;
        id = id_increment;
    }
}