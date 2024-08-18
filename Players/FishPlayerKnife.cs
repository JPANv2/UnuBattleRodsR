using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using Microsoft.Xna.Framework;
using UnuBattleRodsR.Projectiles;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {

        public float knifeRadius = 0f;
        public int knifeBaseDamage = 0;
        public float knifeKnockback = 0f;
        public int knifeCooldown = 0;
        public int knifeCooldownCounter = 0;
        public int knifeDebuff = 0;
        public bool[] knifedNPCs = new bool[Main.npc.Length];
        public bool HasHitWithKnife => _hitwithknife;
        private bool _hitwithknife = false;
        public Asset<Texture2D> slashTexture;

        public static SoundStyle knife1 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Knives/Knife1");
        public static SoundStyle knife2 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Knives/Knife2");
        public static SoundStyle knife3 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Knives/Knife3");

        public void resetKnives()
        {
            knifeRadius = 0f;
            knifeBaseDamage = 0;
            knifeKnockback = 0f;
            knifeCooldown = 0;
            //knifeCooldownCounter = 0;
            knifeDebuff = 0;
            _hitwithknife = false;
            slashTexture = null;
        }

        public void knifeUpdate()
        {
            if (knifeCooldown > 0 && knifeBaseDamage > 0 && knifeRadius > 0)
            {
                if (knifeCooldownCounter > 0)
                {
                    knifeCooldownCounter--;
                }
                if (knifeCooldownCounter <= 0)
                {
                    if (findAndDamageNearbyEnemies())
                    {
                        knifeCooldownCounter = knifeCooldown;
                        switch (Main.rand.Next(3))
                        {
                            case 0:
                                SoundEngine.PlaySound(knife1);
                                break;
                            case 1:
                                SoundEngine.PlaySound(knife2);
                                break;
                            default:
                                SoundEngine.PlaySound(knife3);
                                break;
                        }
                    }
                }
            }
        }

        private bool findAndDamageNearbyEnemies()
        {
            List<Bobber> lst = getOwnedAttatchedBobbers();
            knifedNPCs = new bool[Main.npc.Length];
            bool foundEnemy = false;
            //bool[] playersToIgnore = new bool[Main.player.Length]; players not included

            float radiusOffset = knifeRadius + (Player.width > Player.height ? Player.width : Player.height) / 2;

            foreach (Bobber b in lst)
            {
                Entity e = b.getStuckEntity();
                if (e is NPC)
                {
                    NPC n = e as NPC;
                    if (n.active && (!n.friendly || n.type == NPCID.Guide && Player.killGuide || n.type == NPCID.Clothier && Player.killClothier) &&
                        !((n.immortal || n.dontTakeDamage) && n.type != NPCID.TargetDummy))
                    {

                        float distance = Vector2.Distance(n.Center, Player.Center);

                        if (distance <= radiusOffset)
                        {
                            n.PlayerInteraction(Player.whoAmI);
                            damageNPC(n, knifeBaseDamage * 2);
                            if (knifeDebuff > 0)
                            {
                                n.AddBuff(knifeDebuff, knifeCooldown);
                            }
                            knifedNPCs[n.whoAmI] = true;
                            foundEnemy = true;

                        }
                    }
                }
            }

            for (int i = 0; i < 200; i++) //Main.npc.Length
            {
                NPC n = Main.npc[i];
                if (!knifedNPCs[i] && n.active&& (!n.friendly || n.type == NPCID.Guide && Player.killGuide || n.type == NPCID.Clothier && Player.killClothier) &&
                        !((n.immortal || n.dontTakeDamage) && n.type != NPCID.TargetDummy) &&
                        n.catchItem <= 0)
                {
                    float distance = Vector2.Distance(n.Center, Player.Center);

                    if (distance <= radiusOffset)
                    {
                        n.PlayerInteraction(Player.whoAmI);
                        damageNPC(n, knifeBaseDamage);
                        if (knifeDebuff > 0)
                        {
                            n.AddBuff(knifeDebuff, knifeCooldown);
                        }
                        knifedNPCs[i] = true;
                        foundEnemy = true;
                    }
                }
            }
            _hitwithknife = foundEnemy;
            return foundEnemy;
        }

        private void damageNPC(NPC npc, int knifeBaseDamage)
        {
            npc.netUpdate = true;
            if ((Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI != Main.myPlayer) || ((npc.immortal || npc.dontTakeDamage) && npc.type != NPCID.TargetDummy))
                return;
            FishPlayer fp = Main.player[Main.myPlayer].GetModPlayer<FishPlayer>();

            float baseDamage = knifeBaseDamage;
            if (baseDamage <= 0.49f) return;


            NPC.HitModifiers hitMod = npc.GetIncomingStrikeModifiers(ModContent.GetInstance<FishingDamage>(), 0, false);

            hitMod.Knockback.Base = 10;
            bool crit = false;
            if (Main.rand.Next(100) < Player.GetCritChance<FishingDamage>() + 4)
            {
                crit = true;
                hitMod.SetCrit();
            }
            int dir = 0;

            Player.OnHit(npc.Center.X, npc.Center.Y, npc);

            if (Player.GetArmorPenetration(hitMod.DamageType) > 0)
            {
                hitMod.ArmorPenetration += Player.GetArmorPenetration(hitMod.DamageType);
            }
            NPC.HitInfo info = hitMod.ToHitInfo(baseDamage, crit, 10, true, Player.luck);
            int num25 = npc.StrikeNPC(info, false, false);

            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.DPSSync);
                pk.Write(num25);
                pk.Send(Player.whoAmI, -1);
            }
            else
            {
                if (Player.accDreamCatcher)
                {
                    Player.addDPS(num25);
                }
            }

            if (Main.netMode != 0)
            {
                NetMessage.SendStrikeNPC(npc, info);
              
            }


        }  
    } 
}
