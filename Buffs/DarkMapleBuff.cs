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
    public class DarkMapleBuff : ModBuff
    {
        public static readonly float DarkDamageBouns = 10;
        public static readonly int DarkCritBouns = 10;

        public override LocalizedText Description => base.Description.WithFormatArgs(DarkDamageBouns);

        public override void Update(Player player, ref int buffIndex)
        {
            // 5% paincake damage bonus
            // 5% paincake crit change bonus
            player.GetCritChance<DamageClasses.PaincakeDamage>() += DarkCritBouns;
            player.GetDamage< DamageClasses.PaincakeDamage >() += DarkDamageBouns / 100f;
        }
    }
}
