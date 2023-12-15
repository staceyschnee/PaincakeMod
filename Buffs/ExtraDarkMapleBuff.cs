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
    public class ExtraDarkMapleBuff : ModBuff
    {
        public static readonly float ExtraDarkDamageBouns = 5;
        public static readonly int ExtraDarkCritBouns = 5;

        public override LocalizedText Description => base.Description.WithFormatArgs(ExtraDarkDamageBouns);

        public override void Update(Player player, ref int buffIndex)
        {
            // 5% paincake damage bonus
            // 5% paincake crit change bonus
            player.GetCritChance<DamageClasses.PaincakeDamage>() += ExtraDarkCritBouns;
            player.GetDamage< DamageClasses.PaincakeDamage >() += ExtraDarkDamageBouns / 100f;
        }
    }
}
