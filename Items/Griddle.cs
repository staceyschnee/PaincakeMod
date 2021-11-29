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

namespace PaincakeMod.Items
{
	class Griddle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used for special crafting");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 24;
			Item.value = 650;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.useTurn = true;
			Item.maxStack = 999;
			Item.createTile = ModContent.TileType<GriddleTile>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Torch, 4)
				.AddIngredient(ItemID.StoneBlock, 6)
				.AddIngredient(ItemID.IronBar, 10)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.Torch, 4)
				.AddIngredient(ItemID.StoneBlock, 6)
				.AddIngredient(ItemID.LeadBar, 10)
				.AddTile(TileID.WorkBenches)
				.Register();

		}
	}
}
