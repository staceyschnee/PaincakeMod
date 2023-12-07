using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Enums;
using Terraria.Utilities;

namespace PaincakeMod.Items
{
    public class CerealBox : ModItem
    {
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = ModContent.GetInstance<DamageClasses.PaincakeDamage>();
			Item.width = 30;
			Item.height = 39;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5f;
			Item.value = 1001;
			Item.noMelee = true;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.CoinPickup;
			Item.autoReuse = false;
			Item.consumable = false;
			Item.maxStack = 1;
			Item.shoot = ModContent.ProjectileType<Projectiles.CerealProjectile>();
			Item.shootSpeed = 15f;
		}

		public override bool AllowPrefix(int pre) 
		{
			return true;
		}

		public override bool RangedPrefix() 
		{
			return true;
		}

        //public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        //{
        //    base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        //}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int numberProjectiles = 5 + Main.rand.Next(5); // 5 - 9 shots

            for (int i = 0; i < numberProjectiles; i++)
            {
                // Rotate the velocity randomly by 30 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                // Decrease velocity randomly for nicer visuals.
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);

                // Create a projectile.
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
			return false;
        }


        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Hay, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}

