using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaincakeMod.Items
{
	public class SplatSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SplatSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is a basic modded sword.");
		}

		public override void SetDefaults()
		{
			Item.damage = 5000;
			Item.DamageType = DamageClass.Melee;
			Item.width = 200;
			Item.height = 200;
			Item.useTime = 5;
			Item.useTurn = true;
			Item.useAnimation = 5;
			Item.useStyle = 1;
			Item.knockBack = 10f;
			Item.value = 10000;
			Item.rare = 2;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
			player.armorPenetration += 10000;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}