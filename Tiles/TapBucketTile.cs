using System;
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


namespace PaincakeMod.Tiles
{
    class TapBucketTile : ModTile
    {
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
                _bucketProduced = bucketCount;
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
                return (X - _X < 2 && X - _X >= -2
                        && Y - _Y < 2 && Y - _Y >= -2);
            }

            public long getbuckets()
            {
                long count = _bucketCount - _bucketProduced;
                _bucketProduced = _bucketCount;
                return count;
            }

            public long checkbuckets()
            {
                long count = _bucketCount - _bucketProduced;
                return count;
            }
            public void addbuckets()
            {
                _bucketCount++;
            }
        }

        List<Location> BucketLocations = new List<Location>();

        public override void SetStaticDefaults()
        {

            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileSolidTop[Type] = false;
            Main.tileNoAttach[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.LavaDeath = false;
            //TileObjectData.newTile.AnchorLeft = new AnchorData(AnchorType.SolidSide, 2, 1);
            //TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidSide, 2, 1);
            //TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidWithTop, 2, 1);
            //TileObjectData.newTile.DrawXOffset = -2;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(240, 240, 200), Language.GetText("MapObject.TapBucket"));

            //Can't use this since texture is vertical
            AnimationFrameHeight = 36;
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            bool found = false;
            if (BucketLocations.Count > 0)
            {
                foreach (Location loc in BucketLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        found = true;
                        if (Main.dayTime == true && Main.time < 5)
                        {
                            // add a sap bucket and change to filled
                            if (loc.checkbuckets() == 0)
                            {
                                loc.addbuckets();
                            }
                            frameYOffset = 36;
                        }
                        else
                        {
                            if (loc.checkbuckets() == 0)
                            {
                                // Set bucket back to looking empty
                                frameYOffset = 0;
                            }
                            else
                            {
                                frameYOffset = 36;
                            }
                        }
                        break;
                    }
                }
            }
            if (!found)
            {
                BucketLocations.Add(new Location(i, j, 0));
            }
        }

        public override bool RightClick(int i, int j)
        {
            if (BucketLocations.Count > 0)
            {
                foreach (Location loc in BucketLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        if (loc.checkbuckets() > 0)
                        {
                            loc.getbuckets();
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

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Location loc = new Location(i, j, 0);
            BucketLocations.Remove(loc);
        }
    }
}
