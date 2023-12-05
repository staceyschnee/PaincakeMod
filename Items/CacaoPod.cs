using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using PaincakeMod.Tiles;

namespace PaincakeMod.Items
{
    class CacaoPod : ModItem
    {
        public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 48;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MudBlock, 2);
            recipe.AddTile(ModContent.GetInstance<Tiles.GriddleTile>());
            recipe.Register();
        }
    }
}
