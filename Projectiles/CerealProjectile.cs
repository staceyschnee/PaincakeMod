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
	public class CerealProjectile : ModProjectile
	{ 
		public override void SetStaticDefaults()
		{
            Main.projFrames[Projectile.type] = 21;
        }

		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.SpikedSlimeSpike);
			Projectile.damage = 8;
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.knockBack = 3f;
			Projectile.friendly = true;
			//Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
			Projectile.timeLeft = 300;
			Projectile.CritChance = 2;
		}

        public override bool PreAI()
        {
            return base.PreAI();
        }

        public override void AI()
        {
            base.AI();
			if (Projectile.frameCounter++ == 0)
			{
				Projectile.frame = Main.rand.Next(0, 21);
			}
        }

        public override void OnKill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height); //makes dust based on tile
			SoundEngine.PlaySound(SoundID.CoinPickup, Projectile.position); //plays impact sound
		}
    }

}