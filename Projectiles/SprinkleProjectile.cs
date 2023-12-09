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
	public class SprinkleProjectile : ModProjectile
	{ 
		public override void SetStaticDefaults()
		{
            Main.projFrames[Projectile.type] = 8;
        }

		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.SpikedSlimeSpike);
			Projectile.damage = 5;
			Projectile.width = 4;
			Projectile.height = 12;
			Projectile.knockBack = 1f;
			//Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 2;
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
				Projectile.frame = Main.rand.Next(0, 8);
				Projectile.hostile = Main.rand.NextBool(2);
			}
        }

        public override void OnKill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height); //makes dust based on tile
			SoundEngine.PlaySound(SoundID.Coins, Projectile.position); //plays impact sound
		}
    }

}