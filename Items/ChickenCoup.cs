using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using PaincakeMod.Tiles;
using PaincakeMod.NPCs;

namespace PaincakeMod.Items
{
	class ChickenCoup : ModItem
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Want come eggs?");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 53;
			Item.height = 48;
			Item.value = 1050;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.useTurn = true;
			Item.maxStack = 999;
			Item.createTile = ModContent.TileType<ChickenCoupTile>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<ChickenItem>(), 6)
				.AddIngredient(ItemID.Wood, 10)
				.AddIngredient(ItemID.Hay, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}