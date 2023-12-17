
using Microsoft.Xna.Framework;
using log4net;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria.Audio;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;
using Terraria.GameContent.Creative;
using System.Linq;
using Terraria.ModLoader.Config;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader.Assets;
using ReLogic.Content.Sources;
using ReLogic.Graphics;
using PaincakeMod.Tiles;
using Terraria.Enums;

namespace PaincakeMod.Items
{
    class MahagonyTreeMod : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            base.Drop(i, j, type);
            if (type == TileID.Trees)
            {
                for (int h = j + 1; h < j + 20; h++)
                {
                    // This should cause a 5% change to drop a cacoa pod when a player shakes or chops down a mahogany tree
                    if (Framing.GetTileSafely(i, h).HasTile && Framing.GetTileSafely(i, h).TileType != TileID.Trees)
                    {
                        if (Framing.GetTileSafely(i, h).TileType == TileID.JungleGrass)
                        {
                            if (Main.rand.NextBool(20)) //1-in-20, or 5% chance
                            {
                                Item.NewItem(null, new Vector2(i * 16, j * 16), ModContent.ItemType<CacaoPod>());
                            }
                        }
                        break;
                    }
                }
            }
        }

  

        //This will cause trees to have a 5% chance of dropping gold ore
/*        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (type == TileID.Trees)
            {
                for (int h = j + 1; h < j + 20; h++)
                {
                    //We MUST check for fail to be false, as KillTile is actually called every time a tile is hit by an axe/pickaxe.  We only want to give ore when the tile is mined.
                    if (Framing.GetTileSafely(i, h).HasTile && Framing.GetTileSafely(i, h).TileType != TileID.Trees)
                    {
                        if (Framing.GetTileSafely(i, h).TileType == TileID.JungleGrass)
                        {
                            if (Main.rand.NextBool(20)) //1-in-20, or 5% chance
                            {
                                Item.NewItem(null, new Vector2(i * 16, j * 16), ModContent.ItemType<CacaoPod>());
                            }
                        }
                        break;
                    }
                }
            }
        } 
*/
    }
}
