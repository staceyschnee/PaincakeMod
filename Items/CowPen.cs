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
	class CowPen : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 80;
			Item.height = 60;
			Item.value = 2243;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.useTurn = true;
			Item.maxStack = 9999;
			Item.createTile = ModContent.TileType<CowPenTile>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<CowItem>(), 6)
                .AddIngredient(ItemID.Wood, 30)
				.AddIngredient(ItemID.Hay, 20)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}