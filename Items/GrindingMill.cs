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
	class GrindingMill : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grinding Mill");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 34;
			Item.value = 350;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.useTurn = true;
			Item.maxStack = 99;
			Item.createTile = ModContent.TileType<GrindingMillTile>();
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 4);
			recipe.AddIngredient(ItemID.StoneBlock, 10);
			recipe.AddIngredient(ItemID.IronBar, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
