using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace PaincakeMod.Items
{
    public class CerealSpoon : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ChickenEgg"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("Anything can be a weapon if you try hard enough.");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public enum CerealSpoonMode
		{
			Swing = 0,
			Stab = 1,
			Boomerang = 2
		};

		public CerealSpoonMode CurrentMode = CerealSpoonMode.Swing;

		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = ModContent.GetInstance<DamageClasses.PaincakeDamage>();
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3f;
			Item.value = 100;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
            Item.shootSpeed = 10f;
            Item.autoReuse = false;
			Item.consumable = false;
			Item.maxStack = 1;
		}

		// TODO: Remove this recipe once the boss is created
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 12);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override bool AltFunctionUse(Player player)
        {
			return true;
        }

        public override bool CanUseItem(Player player)
        {
			if (player.altFunctionUse == 2)
			{
                switch (CurrentMode)
                {
                    case CerealSpoonMode.Swing:
                        CurrentMode = CerealSpoonMode.Stab;
                        Item.useStyle = ItemUseStyleID.Rapier;
                        Item.noUseGraphic = true;
                        Item.shoot = ModContent.ProjectileType<Projectiles.CerealSpoonStabProjectile>();
                        Item.shootSpeed = 4f;
                        break;
                    case CerealSpoonMode.Stab:
                        CurrentMode = CerealSpoonMode.Boomerang;
                        Item.useStyle = ItemUseStyleID.Swing;
                        Item.noUseGraphic = true;
                        Item.shoot = ModContent.ProjectileType<Projectiles.CerealSpoonBoomerangProjectile>();
                        Item.shootSpeed = 12f;
                        break;
                    case CerealSpoonMode.Boomerang:
                        CurrentMode = CerealSpoonMode.Swing;
                        Item.useStyle = ItemUseStyleID.Swing;
                        Item.noUseGraphic = true;
                        Item.shoot = ModContent.ProjectileType<Projectiles.CerealSpoonSwingProjectile>();
                        Item.shootSpeed = 15f;
                        break;
                    default:
                        break;
                }
				return false;
            }
            if (CurrentMode == CerealSpoonMode.Boomerang)
            {
                return player.ownedProjectileCounts[Item.shoot] < 1;
            }
            return base.CanUseItem(player);
        }
    }
}

