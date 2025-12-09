namespace Project_RPG_Game.status.custom;


public class Fatigue : Status, INegativeStatus {
    public Fatigue(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = "Fatigue";
    }
}
public class Frustration : Status, INegativeStatus {
    public Frustration(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = "Frustration";
    }
}
public class Injured : Status, INegativeStatus {
    public Injured(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = "Injured";
    }
}

