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
    public class AmberMapleBuff : ModBuff
    {
        public static readonly float AmberDamageBouns = 15;
        public static readonly int AmberCritBouns = 15;

        public override LocalizedText Description => base.Description.WithFormatArgs(AmberDamageBouns);

        public override void Update(Player player, ref int buffIndex)
        {
            // 5% paincake damage bonus
            // 5% paincake crit change bonus
            player.GetCritChance<DamageClasses.PaincakeDamage>() += AmberCritBouns;
            player.GetDamage< DamageClasses.PaincakeDamage >() += AmberDamageBouns / 100f;
        }
    }
}
