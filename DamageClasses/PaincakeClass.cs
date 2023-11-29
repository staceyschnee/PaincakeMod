using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using System.Configuration;

namespace PaincakeMod.DamageClasses
{
    class PaincakeDamage : DamageClass
    {
        public override void SetStaticDefaults()
        {
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
                return true;

            return false;
        }
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass != Generic)
            {
                return StatInheritanceData.None;
            }
            return StatInheritanceData.Full;
        }
    }
}
