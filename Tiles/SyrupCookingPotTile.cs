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
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using PaincakeMod.Items;
using PaincakeMod.Constants;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Microsoft.CodeAnalysis;
using Terraria.GameContent.Drawing;
using Terraria.Utilities;


namespace PaincakeMod.Tiles
{
    class SyrupCookingPotTile : ModTile
    {
        const int SyrupCookingPotFrameCount = 78;
        const int SyrupCookingPotEmptyFrame = 0;
        const int SyrupCookingPotFinishedFrame = SyrupCookingPotFrameCount - 1;
        const int SyrupStylesPerFrame = 2;
        const int WorkingAnimationFrames = (SyrupCookingPotFinishedFrame - 1) / SyrupStylesPerFrame;
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


        private Asset<Texture2D> flameTexture;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileLighted[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            //TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 10;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.SyrupCookingPot"));

            //Can't use this since texture is vertical
            DustType = -1;
            AnimationFrameHeight = 38;

            // Assets
            if (!Main.dedServ)
            {
                flameTexture = ModContent.Request<Texture2D>(Texture + "_Flame");

            }
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
                    TileNumber = (StyleNumber * SyrupStylesPerFrame) + ((frameCount / 16) % SyrupStylesPerFrame) + 1;
                    TileNumber = Math.Min(TileNumber, SyrupCookingPotFinishedFrame - 1);
                    break;
                case PaincakePotStatus.Finished:
                    TileNumber = SyrupCookingPotFinishedFrame;
                    break;
                default:
                    TileNumber = SyrupCookingPotFinishedFrame; 
                    break;
            }

			frameXOffset = (TileNumber % 10) * 36;
			frameYOffset = (TileNumber / 10) * 38;

            //base.AnimateIndividualTile(type, i, j, ref frameXOffset, ref frameYOffset);
        }


        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            var PotStatus = GetPotStatusAtLocation(i, j);
            if (Main.gamePaused || PotStatus == PaincakePotStatus.Empty)
            {
                return;
            }
            if (!Lighting.UpdateEveryFrame || new FastRandom(Main.TileFrameSeed).WithModifier(i, j).Next(4) == 0)
            {
                Tile tile = Main.tile[i, j];
                int dustChance = (PotStatus == PaincakePotStatus.Processing) ? 2: 5;
                // Only emit dust from the top tiles, and only if toggled on. This logic limits dust spawning under different conditions.
                if (tile.TileFrameY == 0 && Main.rand.NextBool(dustChance) && ((Main.drawToScreen && Main.rand.NextBool(dustChance + 1)) || !Main.drawToScreen))
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(i * 16 + 2, j * 16 - 4), 4, 8, DustID.Cloud, 0f, 0f, 100);

                    dust.position.X += Main.rand.Next(-4, 4);
                    dust.position.Y += Main.rand.Next(4, 12);
                    dust.alpha += Main.rand.Next(100);
                    dust.velocity *= 0.2f;
                    dust.velocity.Y -= 0.5f + Main.rand.Next(10) * 0.1f;
                    dust.fadeIn = 0.5f + Main.rand.Next(10) * 0.1f;
                }
            }
        }



        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            var tile = Main.tile[i, j];

            if (!TileDrawing.IsVisible(tile))
            {
                return;
            }

            //if (tile.TileFrameY < 36)
            {
                Color color = new Color(255, 255, 255, 120);

                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }

                int width = 16;
                int offsetY = 0;
                int height = 16;
                short frameX = tile.TileFrameX;
                short frameY = tile.TileFrameY;
                int addFrX = 0;
                int addFrY = 0;

                TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY); // calculates the draw offsets
                TileLoader.SetAnimationFrame(Type, i, j, ref addFrX, ref addFrY); // calculates the animation offsets

                Rectangle drawRectangle = new Rectangle(tile.TileFrameX + addFrX, tile.TileFrameY + addFrY, 16, 16);

                // The flame is manually drawn separate from the tile texture so that it can be drawn at full brightness.
                Main.spriteBatch.Draw(flameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, drawRectangle, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
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
