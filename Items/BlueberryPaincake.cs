using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

using PaincakeMod.Projectiles;

namespace PaincakeMod.Items
{
	public class BlueberryPaincake : ModItem
	{ 
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ChickenEgg"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("Its an egg silly.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 200;
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.shootSpeed = 11f;
			//Item.noMelee = true;
			Item.DamageType = ModContent.GetInstance<DamageClasses.PaincakeDamage>();
			Item.width = 32;
            Item.height = 32;
			//Item.useTime = 15;
			//Item.useAnimation = 20;
			//Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1f;
			Item.value = 10;
			Item.rare = ItemRarityID.White; 
			//Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.maxStack = 9999;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.BlueberryPaincakeProjectile>();
			Item.ammo = ModContent.ItemType<Paincake>();
        }

		/*public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
#if false
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// NewProjectile returns the index of the projectile it creates in the NewProjectile array.
			// Here we are using it to gain access to the projectile object.
			Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
			//Projectile projectile = Main.projectile[projectileID];

			//ExampleProjectileModifications globalProjectile = projectile.GetGlobalProjectile<ChichenEggProjectile>();
			// For more context, see ExampleProjectileModifications.cs
			//globalProjectile.SetTrail(Color.Brown);
			//globalProjectile.sayTimesHitOnThirdHit = true;
			//globalProjectile.applyBuffOnHit = true;

			// We do not want vanilla to spawn a duplicate projectile.
			return false;
		}*/
//#endif
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.MilkCarton, 1);
            recipe.AddIngredient(ItemID.BlueBerries, 1);
            recipe.AddIngredient(ModContent.ItemType<ChickenEgg>(), 2);
			recipe.AddIngredient(ModContent.ItemType<Flour>(), 2);
			recipe.AddTile(ModContent.GetInstance<Tiles.GriddleTile>());
			recipe.Register();
		}
	}
}