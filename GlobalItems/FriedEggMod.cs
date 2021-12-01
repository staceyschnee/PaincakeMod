
using Microsoft.Xna.Framework;
using log4net;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria.Audio;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;
using Terraria.GameContent.Creative;
using System.Linq;
using Terraria.ModLoader.Config;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader.Assets;
using ReLogic.Content.Sources;
using ReLogic.Graphics;

namespace PaincakeMod.Items
{
    class FriedEggMod : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            return item.type == ItemID.FriedEgg;
        }
       
        public override void AddRecipes()
        {
            Recipe recipe = Mod.CreateRecipe(ItemID.FriedEgg, 1);
            recipe.AddIngredient(ModContent.ItemType<ChickenEgg>(), 2);
            recipe.AddTile(ModContent.GetInstance<Tiles.GriddleTile>());
            recipe.Register();
        }
    }
}
