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
    public class CopperSpatula : ModItem
    {
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = ModContent.GetInstance<DamageClasses.PaincakeDamage>();
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3f;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.consumable = false;
			Item.maxStack = 1;
			Item.shoot = ModContent.ProjectileType<Projectiles.PaincakeProjectile>();
			Item.shootSpeed = 10f;
			Item.useAmmo = ModContent.ItemType<Items.Paincake>();
		}

		public override bool AllowPrefix(int pre) 
		{
			return true;
		}

		public override bool MeleePrefix() 
		{
			return true;
		}

		public override bool RangedPrefix() 
		{
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.CopperBar, 8);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}

