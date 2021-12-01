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
	class GrindingMillTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			//TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Grinding Mill");
			AddMapEntry(new Color(200, 200, 200), name);
			 
			//Can't use this since texture is vertical
			AnimationFrameHeight = 34;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			
			// Spend 9 ticks on each of 4 frames, looping
			if (++frameCounter >= 9) {
				frameCounter = 0;
				frame = ++frame % 4;
			}

		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 48, ModContent.ItemType<GrindingMill>());
		}

	}
}
