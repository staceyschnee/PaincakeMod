using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using System;
using Terraria.Localization;

namespace PaincakeMod.Buffs
{
    public class GoldenMapleBuff : ModBuff
    {
        public static readonly float GoldenDamageBouns = 20;
        public static readonly int GoldenCritBouns = 20;

        public override LocalizedText Description => base.Description.WithFormatArgs(GoldenDamageBouns);

        public override void Update(Player player, ref int buffIndex)
        {
            // 5% paincake damage bonus
            // 5% paincake crit change bonus
            player.GetCritChance<DamageClasses.PaincakeDamage>() += GoldenCritBouns;
            player.GetDamage< DamageClasses.PaincakeDamage >() += GoldenDamageBouns / 100f;
        }
    }
}
