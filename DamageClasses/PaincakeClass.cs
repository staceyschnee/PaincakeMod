using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace PaincakeMod.DamageClasses
{
    class PaincakeClass : DamageClass
    {
        public override void SetStaticDefaults()
        {
            ClassName.SetDefault("paincake damage");
        }

        protected override float GetBenefitFrom(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
                return 1f;

            return 0;
        }
        public override bool CountsAs(DamageClass damageClass)
        {
            return false;
        }
    }
}
