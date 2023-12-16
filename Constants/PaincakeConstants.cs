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


namespace PaincakeMod.Constants
{
	public class PaincakeConstants
	{
		public const long eggProducedFrameCount = 720;
        public const long milkProducedFrameCount = 7200;
    }

	public enum PaincakePotStatus
	{
		Empty = 0,
		NeedsMore = 1,
		Processing = 2,
		Finished = 3
	}
}
