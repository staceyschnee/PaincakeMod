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
using PaincakeMod.Constants;
using Microsoft.Xna.Framework;


namespace PaincakeMod.Items
{
	class SapBucket : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 32;
			Item.value = 1650;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.LiquidsHoneyWater;
			Item.autoReuse = false;
			Item.consumable = true;
			Item.useTurn = false;
			Item.maxStack = 9999;
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
				SyrupCookingPotTile SyrupPot =  ModContent.GetInstance<SyrupCookingPotTile>();

                PaincakePotStatus Status = SyrupPot.GetPotStatusAtLocation(MouseWorldTile.X, MouseWorldTile.Y);
                // checked the state of the syrup pot (Working or Ready)
                // if working return false;
                if (Status != PaincakePotStatus.Empty)
				{
					return false;
				}
				bool didStart = SyrupPot.StartPotWorking(MouseWorldTile.X, MouseWorldTile.Y);

                if (didStart)
				{					
                    return true;
                }
                return false;
			}
            if (Framing.GetTileSafely(MouseWorldTile.X, MouseWorldTile.Y).TileType == ModContent.TileType<TurkeyFryerTile>())
            {
                TurkeyFryerTile TurkeyFryer = ModContent.GetInstance<TurkeyFryerTile>();
                Mod.Logger.Info("turkeyfryertyle");
                PaincakePotStatus Status = TurkeyFryer.GetPotStatusAtLocation(MouseWorldTile.X, MouseWorldTile.Y);
                // checked the state of the syrup pot (Working or Ready)
                // if working return false;
                if (Status != PaincakePotStatus.Empty && Status != PaincakePotStatus.NeedsMore)
                {
                    return false;
                }
                bool didStart = TurkeyFryer.StartWorking(MouseWorldTile.X, MouseWorldTile.Y);

                if (didStart)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
