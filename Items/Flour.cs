using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace PaincakeMod.Items
{
    class Flour : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It's just flour.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Hay, 2);
            recipe.AddTile(ModContent.GetInstance<Tiles.GrindingMillTile>());
            recipe.Register();
        }
    }
}
