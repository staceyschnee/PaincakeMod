using log4net;
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
	class CowPenTile : ModTile
	{
		class Location : IEquatable<Location>
		{
			public int _X { get; set; }
			public int _Y { get; set; }
			public long _milkCount {get; set; }
			public long _milkProduced {get; set; }

			public Location(int X, int Y, long milkCount)
			{
				_X = X;
				_Y = Y;
				_milkCount = milkCount;
				_milkProduced = milkCount;
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
				return (X - _X < 8 && X - _X >= -8 
						&& Y - _Y < 2 && Y - _Y >= -2);
			}

			public long getmilk()
			{
				long count = _milkCount - _milkProduced;
				_milkProduced = _milkCount;
				return count;
			}
			public void addmilk()
			{
				long totalmilk = Main.GameUpdateCount / PaincakeConstants.milkProducedFrameCount;
				_milkCount = totalmilk;
			}
		}

		List<Location> penLocations = new List<Location>();

		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.Width = 8; 
            //TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.CoordinatePadding = 2;
            //TileObjectData.newTile.DrawYOffset = -2;
            TileObjectData.addTile(Type);
			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.CowPen"));
			//Can't use this since texture is vertical
			AnimationFrameHeight = 38;
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset) 
		{
			bool found = false;
			if (penLocations.Count > 0)
			{
				foreach (Location loc in penLocations)
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
				penLocations.Add(new Location(i, j, 0));
			}
		}


		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
            // Spend 20 ticks on each of 10 frames, looping
            if (frameCounter++ % 20 == 0)
			{
				frame = ++frame % 10;
			}
			if (frameCounter == PaincakeConstants.milkProducedFrameCount)
			{
				foreach (Location pos in penLocations)
				{
					pos.addmilk();
					Item.NewItem(new EntitySource_TileBreak(pos._X, pos._Y), pos._X * 16, pos._Y * 16, 20, 15, ItemID.MilkCarton, (int)pos.getmilk());
				}
			}
		}

		public override void PlaceInWorld(int i, int j, Item item)
        {
			long totalmilk = Main.GameUpdateCount / PaincakeConstants.milkProducedFrameCount;
			penLocations.Add(new Location(i, j, totalmilk));		
			base.PlaceInWorld(i, j, item);
        }

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Location loc = new Location(i, j, 0);
			penLocations.Remove(loc);
		}



	}
}
