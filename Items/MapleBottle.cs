using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using PaincakeMod.Constants;
using PaincakeMod.Tiles;

namespace PaincakeMod.Items
{
    class MapleBottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 48;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 9999;
            Item.value = 375;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.LiquidsHoneyWater;
            Item.autoReuse = false;
            Item.consumable = true;
            Item.useTurn = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.Glass, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }


        public override bool ConsumeItem(Player player)
        {
            Point MouseWorldTile = Main.MouseWorld.ToTileCoordinates();
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<SyrupCookingPotTile>())
            {
                return true;
            }
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<TurkeyFryerTile>())
            {
                return true;
            }
            return false;
        }

        public override void OnConsumeItem(Player player)
        {
            base.OnConsumeItem(player);
        }

        public override bool? UseItem(Player player)
        {
            Point MouseWorldTile = Main.MouseWorld.ToTileCoordinates();
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<SyrupCookingPotTile>())
            {
                return true;
            }
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<TurkeyFryerTile>())
            {
                return true;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            Point MouseWorldTile = Main.MouseWorld.ToTileCoordinates();
            // find one of the maple syrup making accessories
            // once found, call that items start syrup produciton method.
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<SyrupCookingPotTile>())
            {
                SyrupCookingPotTile SyrupPot = ModContent.GetInstance<SyrupCookingPotTile>();

                PaincakePotStatus Status = SyrupPot.GetPotStatusAtLocation(MouseWorldTile.X, MouseWorldTile.Y);
                // checked the state of the syrup pot (Working or Ready)
                // if working return false;
                if (Status != PaincakePotStatus.Finished)
                {
                    return false;
                }
                bool didCollect = SyrupPot.CollectSyrup(MouseWorldTile.X, MouseWorldTile.Y);

                if (didCollect)
                {
                    return true;
                }
                return false;
            }
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<TurkeyFryerTile>())
            {
                TurkeyFryerTile TurkeyFryer = ModContent.GetInstance<TurkeyFryerTile>();

                PaincakePotStatus Status = TurkeyFryer.GetPotStatusAtLocation(MouseWorldTile.X, MouseWorldTile.Y);
                // checked the state of the syrup pot (Working or Ready)
                // if working return false;
                if (Status != PaincakePotStatus.Finished && Status != PaincakePotStatus.CollectMore)
                {
                    return false;
                }
                bool didCollect = TurkeyFryer.CollectSyrup(MouseWorldTile.X, MouseWorldTile.Y);

                if (didCollect)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
