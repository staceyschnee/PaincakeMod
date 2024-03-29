﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using Terraria.ObjectData;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.UI;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;

using PaincakeMod.Items;
using PaincakeMod.Constants;


namespace PaincakeMod.Tiles
{
	class ChickenCoupTile : ModTile
	{

		class Location : IEquatable<Location>
		{
			public int _X { get; set; }
			public int _Y { get; set; }
			public long _eggCount {get; set; }
			public long _eggProduced {get; set; }

			public Location(int X, int Y, long eggCount)
			{
				_X = X;
				_Y = Y;
				_eggCount = eggCount;
				_eggProduced = eggCount;
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
				return (X - _X < 4 && X - _X >= -4 
						&& Y - _Y < 3 && Y - _Y >= -3);
			}

			public long getEggs()
			{
				long count = _eggCount - _eggProduced;
				_eggProduced = _eggCount;
				return count;
			}
			public void addEggs()
			{
				long totalEggs = Main.GameUpdateCount / PaincakeConstants.eggProducedFrameCount;
				_eggCount = totalEggs;
			}
		}

		List<Location> CoupLocations = new List<Location>();

		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Width = 4; // because the template is for 3x3 not 4x3
			//TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);
			TileObjectData.newTile.DrawYOffset = 2;
			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.ChickenCoup"));
			//Can't use this since texture is vertical
			AnimationFrameHeight = 56;
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) 
		{
			bool found = false;
			if (CoupLocations.Count > 0)
			{
				foreach (Location loc in CoupLocations)
				{
					if (loc.Equals(i, j))
					{
						found = true;
						break;
					}
				}
			}
			if (!found)
			{
				CoupLocations.Add(new Location(i, j, 0));
			}
		}


		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			++frameCounter;
			// Spend 9 ticks on each of 4 frames, looping
			if (frameCounter == PaincakeConstants.eggProducedFrameCount - 120)
			{ 
				frame = ++frame % 2;
			}
			else if (frameCounter == PaincakeConstants.eggProducedFrameCount)
			{
				/*if (!SearchedWorld)
				{
					for (int width = 0; width < Main.maxTilesX; width++)
					{
						for (int height = 0; height < Main.maxTilesY / 2; height++)
						{
							Tile tile = Framing.GetTileSafely(width, height);
							if (tile.TileType == Type)
							{
								bool found = false;
								if (CoupLocations.Count > 0)
								{
									foreach (Location loc in CoupLocations)
									{
										if (loc.Equals(width, height))
										{
											found = true;
											break;
										}
									}
								}
								if (!found)
								{
									CoupLocations.Add(new Location(width, height));
								}
							}
						}
					}
					SearchedWorld = true;
				} */
				foreach (Location pos in CoupLocations)
				{
					pos.addEggs();
					Item.NewItem(new EntitySource_TileBreak(pos._X, pos._Y), pos._X * 16, pos._Y * 16, 20, 15, ModContent.ItemType<ChickenEgg>(), (int)pos.getEggs());
				}
				frameCounter = 0;
				frame = ++frame % 2;
			}
		}

		public override void PlaceInWorld(int i, int j, Item item)
        {
			long totalEggs = Main.GameUpdateCount / PaincakeConstants.eggProducedFrameCount;
			CoupLocations.Add(new Location(i, j, totalEggs));		
			base.PlaceInWorld(i, j, item);
        }

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Location loc = new Location(i, j, 0);
			CoupLocations.Remove(loc);
			//CoupCount--;
			//Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 60, 48, ModContent.ItemType<ChickenCoup>(), 1);
		}



	}
}
