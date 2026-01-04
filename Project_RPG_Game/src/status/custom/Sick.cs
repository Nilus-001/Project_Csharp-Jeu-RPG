using System;
using Project_RPG_Game.characters;
using Project_RPG_Game.status.@interface;

namespace Project_RPG_Game.status.custom;

public class Sick : Status , INegativeStatus {
    public Sick(int expirationIn, int modifier = 1) : base(expirationIn, modifier) {
        Name =  $"Sick";
    }

    public override void AppliedEffect(Hero hero) {
        base.AppliedEffect(hero);
        if (ExpirationIn % 3 == 0) {
            hero.Hp =  hero.HpMax /3;
        }

        if (ExpirationIn % 2 == 0) {
            Guild guild = hero.GuildMother;
            if (Global.DiceRoll(70)) {
                guild.GuildHeroes[new Random().Next(0,guild.GuildHeroes.Count)]
                    .AddStatus(new Sick(6));
            }
        }
    }
}