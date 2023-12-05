using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace PaincakeMod.Projectiles
{
	public class BlueberryPaincakeProjectile : ModProjectile
	{ 
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ChickenEgg"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("Its an egg silly.");
		}

		public override void SetDefaults()
		{
			Projectile.damage = 12;
			Projectile.width = 10;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.penetrate = 3;
			Projectile.aiStyle = 2;
			Projectile.timeLeft = 800;
		}

        public override void OnKill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height); //makes dust based on tile
			SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position); //plays impact sound
		}

	}

}