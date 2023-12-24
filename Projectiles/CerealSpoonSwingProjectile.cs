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
	public class CerealSpoonSwingProjectile : ModProjectile
	{

        public const int FadeInDuration = 0;
        public const int FadeOutDuration = 0;

        public const int TotalDuration = 20;
        public const float ShootSpeed = 10f;
        public const float RotationStep = MathHelper.Pi * 1.2f / TotalDuration;
        public const float initialAngle = -MathHelper.PiOver4 * 1.2f;
        public const float initialDistance = 20f;


        // The "width" of the blade
        public float CollisionWidth => 10f * Projectile.scale;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Projectile.damage = 15;
			Projectile.width = 10;
			Projectile.height = 41;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 50;
		}

        public override void OnKill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height); //makes dust based on tile
			SoundEngine.PlaySound(SoundID.Grass, Projectile.position); //plays impact sound
		}

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Timer += 1;
            if (Timer >= TotalDuration)
            {
                // Kill the projectile if it reaches it's intended lifetime
                Projectile.Kill();
                return;
            }

            // Keep locked onto the player, but extend further based on the given velocity (Requires ShouldUpdatePosition returning false to work)
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: true);

            // Set spriteDirection based on moving left or right. Left -1, right 1
            Projectile.spriteDirection = player.direction * -1;

            if (player.direction == -1)
            {
                Vector2 center;
                center.X = initialDistance;
                center.Y = -initialDistance;
                center = center.RotatedBy(-initialAngle - MathHelper.PiOver4);
                Vector2 positionCorrection = new Vector2(14f, -6f);
                float rotationAngleAdd = (Timer - 1) * RotationStep;
                float rotationAngle = -initialAngle - rotationAngleAdd;
                float movementAngle = Timer * RotationStep + -initialAngle;
                center = center.RotatedBy(-rotationAngleAdd);
                Projectile.Center = playerCenter + center + positionCorrection;
                Projectile.velocity.X = ShootSpeed * (float)Math.Cos(movementAngle);
                Projectile.velocity.Y = ShootSpeed * (float)Math.Sin(movementAngle);
                Projectile.rotation = rotationAngle;
            }
            else
            {
                Vector2 center;
                center.X = -initialDistance;
                center.Y = -initialDistance;
                center = center.RotatedBy(initialAngle + MathHelper.PiOver4);
                float rotationAngleAdd = (Timer - 1) * RotationStep;
                float rotationAngle = initialAngle + rotationAngleAdd;
                float movementAngle = Timer * RotationStep + initialAngle + MathHelper.PiOver2;
                Vector2 positionCorrection = new Vector2(6f, -6f);
                center = center.RotatedBy(rotationAngleAdd);
                Projectile.Center = playerCenter + center + positionCorrection;
                Projectile.velocity.X = ShootSpeed * (float)Math.Cos(movementAngle);
                Projectile.velocity.Y = ShootSpeed * (float)Math.Sin(movementAngle);
                Projectile.rotation = rotationAngle;
            }

            // The code in this method is important to align the sprite with the hitbox how we want it to
            SetVisualOffsets();
        }



        private void SetVisualOffsets()
        {
            // 32 is the sprite size (here both width and height equal)
            const int HalfSpriteWidth = 32 / 2;
            //const int HalfSpriteHeight = 32 / 2;

            int HalfProjWidth = Projectile.width / 2;
            int HalfProjHeight = Projectile.height / 2;

            // Vanilla configuration for "hitbox in middle of sprite"
            //DrawOriginOffsetX = 0;
            //DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
            //DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);

            //Vanilla configuration for "hitbox towards the end"
            if (Projectile.spriteDirection == 1) {
            	DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
            	DrawOffsetX = (int)-DrawOriginOffsetX * 2;
            	DrawOriginOffsetY = 0;
            }
            else {
            	DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth);
            	DrawOffsetX = 0;
            	DrawOriginOffsetY = 0;
            }
        }

    }

}