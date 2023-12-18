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
	class TurkeyFryer : ModItem
	{
		public override void SetStaticDefaults()
		{

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 34;
			Item.value = 1200;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.useTurn = true;
			Item.maxStack = 9999;
			Item.createTile = ModContent.TileType<TurkeyFryerTile>();
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 4);
			recipe.AddIngredient(ItemID.Gel, 10);
			recipe.AddIngredient(ItemID.IronBar, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 4);
            recipe.AddIngredient(ItemID.Gel, 10);
            recipe.AddIngredient(ItemID.LeadBar, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
