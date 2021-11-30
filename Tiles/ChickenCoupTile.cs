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
using PaincakeMod.Items;


namespace PaincakeMod.Tiles
{
	class ChickenCoupTile : ModTile
	{
		int LocationX = -1;
		int LocationY = -1;
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Width = 4; // because the template is for 4x2 not 4x3
			//TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("ChickenCoup");
			AddMapEntry(new Color(150, 150, 150), name);

			//Can't use this since texture is vertical
			AnimationFrameHeight = 52;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			++frameCounter;
			// Spend 9 ticks on each of 4 frames, looping
			if (frameCounter == 300)
			{ 
				frame = ++frame % 2;
			}
			else if (frameCounter == 360)
			{
				frameCounter = 0;
				frame = ++frame % 2;
				if (LocationX != -1)
				{
					Item.NewItem(LocationX * 16, LocationY * 16, 20, 20, ModContent.ItemType<ChickenEgg>());
				}
			}

		}

		public override ushort GetMapOption(int i, int j)
		{
			LocationX = i;
			LocationY = j;
			return 0;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			LocationX = -1;
			LocationY = -1;
			Item.NewItem(i * 16, j * 16, 60, 48, ModContent.ItemType<ChickenCoup>());
		}

	}
}
