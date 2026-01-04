using System;
using Project_RPG_Game.status.custom;

namespace Project_RPG_Game;

public static class StatusList {
    private static int DurationTime => Global.Random(2, 7);

    public static Func<int, Status> Confusion = (i) => new Confusion(DurationTime);
    public static Func<int, Status> Fire = (i) => new Fire(DurationTime, i);
    public static Func<int, Status> Frustration = (i) => new Frustration(DurationTime);
    public static Func<int, Status> Hungry = (i) => new Hungry(DurationTime, i);
    public static Func<int, Status> Sick = (i) => new Sick(6);
    public static Func<int, Status> UnLuck = (i) => new UnLuck(DurationTime, i);
    public static Func<int, Status> Injured = (i) => new Injured(DurationTime);
    public static Func<int, Status> Fatigue = (i) => new Fatigue(DurationTime);
    public static Func<int, Status> Luck = (i) => new Luck(3, i);
    public static Func<int, Status> Blessed = (i) => new Blessed(3, i);
}