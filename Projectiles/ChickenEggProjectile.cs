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
	public class ChickenEggProjectile : ModProjectile
	{ 
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("ChickenEgg"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("Its an egg silly.");
		}

		public override void SetDefaults()
		{
			Projectile.damage = 1000;
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.aiStyle = 2;
			Projectile.timeLeft = 600;
		}

        public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height); //makes dust based on tile
			SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position); //plays impact sound
		}

	}

}