using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using PaincakeMod.Buffs;
using System;

namespace PaincakeMod.Items
{
    class DarkMapleSyrup : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 48;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.rare = ItemRarityID.LightRed;
            Item.maxStack = 9999;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.value = Item.buyPrice(gold: 1, silver: 25);
            Item.buffType = ModContent.BuffType<ExtraDarkMapleBuff>();
            Item.buffTime = 60 * 60 * 5; // The amount of time the buff declared in Item.buffType will last in ticks. 

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<MapleBottle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SapBucket>(), 3);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}
