﻿using System;
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
    public class SilverSpatula : ModItem
    {
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = ModContent.GetInstance<DamageClasses.PaincakeDamage>();
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 12;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3f;
			Item.value = 100;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.consumable = false;
			Item.maxStack = 1;
			Item.shoot = ModContent.ProjectileType<Projectiles.PaincakeProjectile>();
			Item.shootSpeed = 17f;
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
			recipe.AddIngredient(ItemID.SilverBar, 8);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}

