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
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using PaincakeMod.Items;
using PaincakeMod.Constants;
using Microsoft.Xna.Framework.Input;


namespace PaincakeMod.Tiles
{
    class SyrupCookingPotTile : ModTile
    {
        const int SyrupCookingPotFrameCount = 120;
        const int SyrupCookingPotEmptyFrame = 0;
        const int SyrupCookingPotFinishedFrame = SyrupCookingPotFrameCount - 1;
        const int SyrupStylesPerFrame = 3;
        const int WorkingAnimationFrames = (SyrupCookingPotFinishedFrame - 2) / SyrupStylesPerFrame;
        const int ExtraDarkSyrupCookingTicks = 60 * 60; // 1 game hour; 1 minute real time
        const int TicksPerAnimationFrame = ExtraDarkSyrupCookingTicks / WorkingAnimationFrames;

        class Location : IEquatable<Location>
        {
            public int _X { get; set; }
            public int _Y { get; set; }
            public long _syrupFinishedTicks { get; set; }
            public long _syrupElapsedTicks { get; set; }

            public Location(int X, int Y, long syrupFinishedTicks)
            {
                _X = X;
                _Y = Y;
                _syrupFinishedTicks = Main.GameUpdateCount + syrupFinishedTicks;
                _syrupElapsedTicks = Main.GameUpdateCount;
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

            public long getSyrupTicksLeft()
            {
                _syrupElapsedTicks = Main.GameUpdateCount;
                long ticksLeft = _syrupFinishedTicks - _syrupElapsedTicks;
                return ticksLeft;
            }

            public void UpdateSyrupTicks()
            {
                _syrupElapsedTicks = Main.GameUpdateCount;
            }
        }

        List<Location> PotLocations = new List<Location>();

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            //TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 12;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.SyrupCookingPot"));

            //Can't use this since texture is vertical
            AnimationFrameHeight = 38;
        }

        static int frameCounter = 0;

        public PaincakePotStatus GetPotStatusAtLocation(int i, int j)
        {
            if (PotLocations.Count > 0)
            {
                foreach (Location loc in PotLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        if (loc.getSyrupTicksLeft() > 0)
                        {
                            return PaincakePotStatus.Processing;
                        }
                        return PaincakePotStatus.Finished;
                    }
                }
            }
            return PaincakePotStatus.Empty;
        }

        public int GetPotTicksLeftAtLocation(int i, int j)
        {
            if (PotLocations.Count > 0)
            {
                foreach (Location loc in PotLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        return (int)loc.getSyrupTicksLeft();
                    }
                }
            }
            return -1;
        }


        public bool StartPotWorking(int i, int j)
        {
            if (PotLocations.Count > 0)
            {
                foreach (Location loc in PotLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        return false;
                    }
                }
            }
            PotLocations.Add(new Location(i, j, ExtraDarkSyrupCookingTicks));
            return true;
        }

        public bool CollectSyrup(int i, int j)
        {
            if (PotLocations.Count > 0)
            {
                foreach (Location loc in PotLocations)
                {
                    if (loc.Equals(i, j))
                    {
                        if (loc.getSyrupTicksLeft() <= 0)
                        {
                            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 42, 48, ModContent.ItemType<ExtraDarkMapleSyrup>(), 1);
                            PotLocations.Remove(new Location(i, j, 0));
                            return true;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
            //return base.HasSmartInteract(i, j, settings);
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return base.CanKillTile(i, j, ref blockDamaged);
        }

        public override void MouseOver(int i, int j)
        {
            Main.LocalPlayer.noThrow = 2;
        }

        public override bool RightClick(int i, int j)
        {
            return base.RightClick(i, j);
            if (GetPotStatusAtLocation(i, j) != PaincakePotStatus.Empty)
            {
                return false;
            }
            else
            {
                StartPotWorking(i, j);
                return true;
            }
        }

        int frameCount = 0;

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            // TODO: Add dust to animation
            // TODO: add light to fire

            int TileNumber = SyrupCookingPotFinishedFrame;

            switch (GetPotStatusAtLocation(i, j))
            {
                case PaincakePotStatus.Empty:
                    TileNumber = SyrupCookingPotEmptyFrame;
                    break;
                case PaincakePotStatus.Processing:
                    frameCount++;
                    int StyleNumber = WorkingAnimationFrames - (int) (GetPotTicksLeftAtLocation(i, j) / TicksPerAnimationFrame);
                    TileNumber = (StyleNumber * SyrupStylesPerFrame) + ((frameCount / 8) % SyrupStylesPerFrame) + 1;
                    TileNumber = Math.Min(TileNumber, SyrupCookingPotFinishedFrame - 1);
                    break;
                case PaincakePotStatus.Finished:
                    TileNumber = SyrupCookingPotFinishedFrame;
                    break;
                default:
                    TileNumber = SyrupCookingPotFinishedFrame; 
                    break;
            }

			frameXOffset = (TileNumber % 12) * 36;
			frameYOffset = (TileNumber / 12) * 38;

            //base.AnimateIndividualTile(type, i, j, ref frameXOffset, ref frameYOffset);
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            //PotLocations.Add(new Location(i, j, 0));
            base.PlaceInWorld(i, j, item);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Location loc = new Location(i, j, 0);
            PotLocations.Remove(loc);
            base.KillMultiTile(i, j, frameX, frameY);
        }


    }
}
