﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using Terraria.ObjectData;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using PaincakeMod.Items;
using PaincakeMod.Constants;
using System.Net.Mail;
using Terraria.GameContent.Metadata;


namespace PaincakeMod.Tiles
{
    class TapBucketTile : ModTile
    {
        internal interface ILoadable
        {
            void Load();
            void Unload();
        }
        class Location : IEquatable<Location>
        {
            public int _X { get; set; }
            public int _Y { get; set; }
            public long _bucketCount { get; set; }
            public long _bucketProduced { get; set; }

            public Location(int X, int Y, long bucketCount)
            {
                _X = X;
                _Y = Y;
                _bucketCount = bucketCount;
                _bucketProduced = 0;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Location objAsLocation = obj as Location;
                if (objAsLocation == null) return false;
                else return Equals(objAsLocation);
            }
            public override int GetHashCode()
            {
                return _X * Main.maxTilesX + _Y;
            }
            public bool Equals(Location other)
            {
                if (other == null) return false;
                return (this.Equals(other._X, other._Y));
            }

            public bool Equals(int X, int Y)
            {
                return (X - _X < 1 && X - _X >= -1
                        && Y - _Y < 1 && Y - _Y >= -1);
            }

            public long getBuckets()
            {
                long count = _bucketCount - _bucketProduced;
                _bucketProduced = _bucketCount;
                return count;
            }

            public long checkBuckets()
            {
                long count = _bucketCount - _bucketProduced;
                return count;
            }
            public void addBuckets(int days)
            {
                _bucketCount += days;
            }
            public void setBuckets(int days)
            {
                _bucketCount = days;
            }
        }

        List<Location> BucketLocations = new List<Location>();

        public override void SetStaticDefaults()
        {

            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileNoFail[Type] = true;
            //TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.Width = 1;
            TileObjectData.newTile.LavaDeath = false;
            //TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.Tree, TileObjectData.newTile.Height, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(PlacementPreviewHook_CheckIfCanPlace, 1, 0, true);

            //TileObjectData.newAlternate.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newAlternate.Height = 1;
            TileObjectData.newAlternate.Width = 1;
            TileObjectData.newAlternate.LavaDeath = false;
            //TileObjectData.newAlternate.Origin = new Point16(0, 0);
            TileObjectData.newAlternate.UsesCustomCanPlace = true;
            TileObjectData.newAlternate.CoordinateHeights = new[] { 16 };
            TileObjectData.newAlternate.CoordinateWidth = 16;
            TileObjectData.newAlternate.StyleHorizontal = true;
            TileObjectData.newAlternate.StyleWrapLimit = 2;
            TileObjectData.newAlternate.DrawFlipHorizontal = true;
            //TileObjectData.newAlternate.DrawXOffset = 0;
            TileObjectData.newAlternate.CoordinatePadding = 2;
            TileObjectData.newAlternate.AnchorLeft = new AnchorData(AnchorType.Tree, TileObjectData.newTile.Height, 0);
            TileObjectData.newAlternate.AnchorBottom = AnchorData.Empty;
            TileObjectData.newAlternate.HookCheckIfCanPlace = new PlacementHook(TapBucketTile.PlacementPreviewHook_CheckIfCanPlace, 1, 0, true);
            TileObjectData.addAlternate(1);

            //TileObjectData.newTile.DrawFlipHorizontal = true;
            //TileObjectData.newTile.AnchorValidTiles = new[] { (int)TileID.Trees, (int)TileID.SnowBlock, (int)TileID.Grass };
            //TileObjectData.newTile.UsesCustomCanPlace = true;
            //TileObjectData.newTile.DrawXOffset = -2;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(240, 240, 200), Language.GetText("MapObject.TapBucket"));

            RegisterItemDrop(ModContent.ItemType<TapBucket>(), 1);
            //Can't use this since texture is vertical
            AnimationFrameHeight = 18;
        }

        public static int PlacementPreviewHook_CheckIfCanPlace(int i, int j, int type, int style = 0, int direction = 1, int alternate = 0)
        {
            //Mod.Logger.InfoFormat("tap bucket CheckIfCanPlace {0},{1} type {2} alternate {3}", i, j, type, alternate);
            //Mod.Logger.Info("Placing tap bucket");
            bool treeFound = false;
            int treeX = i;
            // TODO: Add check to be sure there isn't a tap bucket on this side of the tree.
            // TODO: Also check to be sure taht the tree is more than 15 blocks tall and has a top
            if (alternate == 0)
            {
                if (Framing.GetTileSafely(i + 1, j).HasTile && Framing.GetTileSafely(i + 1, j).TileType == TileID.Trees)
                {
                    treeX = i + 1;
                    treeFound = true;
                }
            }
            if (alternate == 1)
            {
                if (Framing.GetTileSafely(i - 1, j).HasTile && Framing.GetTileSafely(i - 1, j).TileType == TileID.Trees)
                {
                    treeX = i - 1;
                    treeFound = true;
                }
            }
            if (treeFound)
            {
                for (int h = j + 1; h < j + 20; h++)
                {
                    //Mod.Logger.InfoFormat("Find Biome tile {0},{1} is type {2}", treeX, h, Framing.GetTileSafely(treeX, h).TileType);
                    if (Framing.GetTileSafely(treeX, h).HasTile && Framing.GetTileSafely(treeX, h).TileType != TileID.Trees)
                    {
                        if (Framing.GetTileSafely(treeX, h).TileType == TileID.SnowBlock)
                        {
                            if (Math.Abs(j - h) < 2)
                            {
                                //Mod.Logger.InfoFormat("Too close to the ground bucket {0} ground {1}", j, h);
                                return 1;
                            }
                            //Mod.Logger.Info("Can Place Bucket");
                            return 0;
                        }
                        //Mod.Logger.Info("Wrong Biome found");
                        return 1;
                    }
                }
                //Mod.Logger.Info("No non tree tiles found");
                return 1;
            }
            //Mod.Logger.Info("No Tree Found");
            return 1;
        }

        bool oldIsDay = true;
        int dayCount = 0;

        public void UpdateTimeDayInfo()
        {
            if (Main.dayTime != oldIsDay) 
            {
                if (Main.dayTime == true && oldIsDay == false)
                {
                    dayCount++;
                    UpdateAllBucketsNewDay();
                }
                oldIsDay = Main.dayTime;
            }
        }

        public bool UpdateAllBucketsNewDay()
        {
            if (BucketLocations.Count > 0)
            {
                foreach (Location loc in BucketLocations)
                {
                    if (loc.checkBuckets() == 0)
                    {
                        loc.setBuckets(dayCount);
                    }
                }
            }
            return true;
        }


        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            bool found = false;
            if (Main.dayTime == true && oldIsDay == false) // || (Main.time < 5 && Main.time > 0 && Main.time < oldTimeTicks)))
            {
                dayCount++;
//                Mod.Logger.InfoFormat("animate add DayCount {0} oldisDay {1} oldtimeticks {2}", dayCount, oldIsDay, oldTimeTicks);
                UpdateAllBucketsNewDay();
            }
            oldIsDay = Main.dayTime;

            if (BucketLocations.Count > 0)
            {
                foreach (Location loc in BucketLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        found = true;
                        loc.setBuckets(dayCount);
                        //Mod.Logger.InfoFormat("animate set bucket {0},{1} to {2}", i, j, dayCount);

                        if (loc.checkBuckets() == 0)
                        {
                            // Set bucket back to looking empty
                            frameYOffset = 0;
                        }
                        else
                        {
                            frameYOffset = 18;
                        }
                        break;
                    }
                }
            }
            if (!found)
            {
                //Mod.Logger.InfoFormat("animate adding bucket {0},{1} to {2}", i, j, dayCount);
                int bucketsToAdd = (dayCount > 0) ? 1 : 0;
                BucketLocations.Add(new Location(i, j, dayCount));
                if (bucketsToAdd > 0)
                {
                    frameYOffset = 18;
                }
            }
        }

        public override bool CanPlace(int i, int j)
        {
            // This is moved to GlobalTile because of a bug in tModLoader
            Mod.Logger.InfoFormat("Can Place {0},{1}", i, j);
            //return true;
            return base.CanPlace(i, j);
        }
        public override bool RightClick(int i, int j)
        {
            if (BucketLocations.Count > 0)
            {
                foreach (Location loc in BucketLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        if (loc.checkBuckets() > 0)
                        {
                            loc.getBuckets();
                            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 24, 32, ModContent.ItemType<SapBucket>(), 1);
                        }
                        break;
                    }
                }
            }
            return base.RightClick(i, j);
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            BucketLocations.Add(new Location(i, j, 0));
            base.PlaceInWorld(i, j, item);
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
        {
            if (Framing.GetTileSafely(i - 1, j).TileType == 5)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Location loc = new Location(i, j, 0);
            BucketLocations.Remove(loc);
            base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
        }

        public override void Unload()
        {
            BucketLocations.Clear();
            base.Unload();
        }

    }
}
