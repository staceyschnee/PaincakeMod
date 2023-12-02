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

namespace PaincakeMod.NPCs
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class JellyDonut : ModNPC
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
			NPC.damage = 8;
			NPC.defense = 1;
			NPC.lifeMax = 17;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 45f;
			NPC.knockBackResist = 0f;
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

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySlime.Chance;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Jelly donut slime")
			});
		}

#if CHICKEN

		public override void FindFrame(int frameHeight)
		{

			if (NPC.velocity.X != 0)
			{
				NPC.frameCounter += 1;
				if (NPC.frameCounter < 10)
				{
					NPC.frame.Y = (int)0 * frameHeight;
				}
				else if (NPC.frameCounter < 20)
				{
					NPC.frame.Y = (int)1 * frameHeight;
				}
				else
				{
					NPC.frameCounter = 0;
				}
			}
		}


		public override void HitEffect(NPC.HitInfo hit)
		{
			// Spawn confetti when this zombie is hit.
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);

				dust.velocity.X += Main.rand.NextFloat(-0.05f, 0.05f);
				dust.velocity.Y += Main.rand.NextFloat(-0.05f, 0.05f);

				dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
			}
		}
#endif
    }
}