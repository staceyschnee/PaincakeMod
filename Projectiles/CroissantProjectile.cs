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
	public class CroissantProjectile : ModProjectile
	{ 
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Projectile.damage = 10;
			Projectile.width = 20;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.aiStyle = ProjAIStyleID.Boomerang;
            Projectile.timeLeft = 100;
		}

        public override void OnKill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height); //makes dust based on tile
			SoundEngine.PlaySound(SoundID.Grass, Projectile.position); //plays impact sound
		}

	}

}