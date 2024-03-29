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


namespace PaincakeMod.Tiles
{
	class GriddleTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 0;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);
			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Griddle"));

			//Can't use this since texture is vertical
			//AnimationFrameHeight = 34;
		}

#if false
		public override void AnimateTile(ref int frame, ref int frameCounter)
		{

			// Spend 9 ticks on each of 4 frames, looping
			if (++frameCounter >= 9)
			{
				frameCounter = 0;
				frame = ++frame % 4;
			}

		}
#endif

		//public override void KillMultiTile(int i, int j, int frameX, int frameY)
		//{
		//	Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<Griddle>());
		//}

	}
}
