using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;


public class Fatigue : Status, INegativeStatus { // Cancel Mission 
    public Fatigue(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = "Fatigue";
    }
}
public class Frustration : Status, INegativeStatus { // Cancel Xp gain
    public Frustration(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = "Frustration";
    }
}
public class Injured : Status, INegativeStatus { // Cancel Heal
    public Injured(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name = "Injured";
    }
}

