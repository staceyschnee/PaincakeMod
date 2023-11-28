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
    class PaincakeClass : DamageClass
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
        public virtual StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass != Generic)
            {
                return StatInheritanceData.None;
            }
            return StatInheritanceData.Full;
        }
    }
}
