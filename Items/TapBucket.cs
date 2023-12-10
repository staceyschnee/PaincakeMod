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
	class TapBucket : ModItem
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("Used for special crafting");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 32;
			Item.value = 800;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item10;
			Item.autoReuse = false;
			Item.consumable = true;
			Item.useTurn = true;
			Item.maxStack = 9999;
			Item.createTile = ModContent.TileType<TapBucketTile>();
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IronBar, 10)
				.AddTile(TileID.Anvils)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.LeadBar, 10)
				.AddTile(TileID.Anvils)
				.Register();

		}
	}
}
