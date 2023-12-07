using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using PaincakeMod.Items;
using PaincakeMod.Projectiles;
using Microsoft.Xna.Framework;

namespace PaincakeMod.NPCs
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class BostonCream : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 2; // Main.npcFrameCount[NPCID.Zombie];

			// By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
			NPCID.Sets.DontDoHardmodeScaling[Type] = false;
			// Enemies can pick up coins, let's prevent it for this NPC
			NPCID.Sets.CantTakeLunchMoney[Type] = false;
			Main.npcCatchable[Type] = false;

			//NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			//{ // Influences how the NPC looks in the Bestiary
			//	Velocity = 1f; // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			//};
			//NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 30;
			NPC.damage = 14;
			NPC.defense = 3;
			NPC.lifeMax = 40;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 45f;
			NPC.knockBackResist = 1f;
			NPC.aiStyle = NPCAIStyleID.Slime;
			NPC.frame.Height = 30;

		
			AnimationType = NPCID.BlueSlime; // Use vanilla Blue Slime AI. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
											 //Banner = Item.NPCtoBanner(NPCID.Zombie); // Makes this NPC get affected by the normal zombie banner.
											 //BannerItem = Item.BannerToItem(Banner); // Makes kills of this NPC go towards dropping the banner it's associated with.
											 //SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // Associates this NPC with the ExampleSurfaceBiome in Bestiary
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 3)); // Drop 1-3 Jelly
		}

        public override void AI()
        {
            base.AI();
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if ((Main.netMode != NetmodeID.MultiplayerClient) && (NPC.HasValidTarget))
            {
				if (Main.rand.NextBool(160) && !player.ZoneForest)
				{
					if ((player.Center - NPC.Center).Length() < 800)
					{
						var source = NPC.GetSource_FromAI();
						Vector2 position = NPC.Center;
						Vector2 targetPosition = Main.player[NPC.target].Center;
						Vector2 direction = targetPosition - position;
						direction.Normalize();
						float speed = 10f;
                        int sprinkleCount = Main.rand.Next(8, 16);
                        int damage = NPC.damage / sprinkleCount + 1; //If the projectile is hostile, the damage passed into NewProjectile will be applied doubled, and quadrupled if expert mode, so keep that in mind when balancing projectiles if you scale it off NPC.damage (which also increases for expert/master)
						for (int i = 0; i < sprinkleCount; i++)
						{
							int type = ModContent.ProjectileType<Projectiles.SprinkleProjectile>();
							Vector2 newDirection = direction.RotateRandom(1.2f);
							Projectile.NewProjectile(source, position, newDirection * speed, type, damage, 0f, Main.myPlayer);
						}
					}
				}
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return Math.Max(SpawnCondition.OverworldDaySlime.Chance, Math.Max(SpawnCondition.SurfaceJungle.Chance, SpawnCondition.UndergroundJungle.Chance));
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Boston Cream slime")
			});
		}
    }
}