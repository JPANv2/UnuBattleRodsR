using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Armors.NormalMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Projectiles;
using UnuBattleRodsR.Configs;
using HurtModifiers = Terraria.Player.HurtModifiers;
using HurtInfo = Terraria.Player.HurtInfo;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Dusts;
using Terraria.GameInput;
using ReLogic.Utilities;
using Terraria.Audio;
using UnuBattleRodsR.Items.Consumables.Baits.DebuffBaits;
using UnuBattleRodsR.Items.Consumables.Discardables;
using Terraria.UI;
using UnuBattleRodsR.Players.AmmoUI;
using Terraria.ModLoader.IO;
using UnuBattleRodsR.Buffs.RodAmmo;

namespace UnuBattleRodsR.Players
{
    public partial class FishPlayer : ModPlayer
    {
        public bool IsBattlerodHeld => this.Player.HeldItem != null && this.Player.HeldItem.ModItem != null && this.Player.HeldItem.ModItem as BattleRod != null;

        public bool IsBattlerodOnHotbar
        {
            get
            {
                for(int i = 0; i< 10; i++)
                {
                    if (this.Player.inventory[i] != null && this.Player.inventory[i].ModItem != null && this.Player.inventory[i].ModItem as BattleRod != null)
                        return true;
                }
                return false;
            }
        }

        public bool IsBattlerodOnInventory {
            get{
                if(this.IsBattlerodOnHotbar)
                    return true;
                for (int i = 10; i < 50; i++)
                {
                    if (this.Player.inventory[i] != null && this.Player.inventory[i].ModItem != null && this.Player.HeldItem.ModItem as BattleRod != null)
                        return true;
                }
                return false;
            }
        }

        public BattleRod HeldBattlerod
        {
            get
            {
                if (IsBattlerodHeld)
                {
                    return this.Player.HeldItem.ModItem as BattleRod;
                }
                return null;
            }
        }


        public bool MasterBaiter = false;

        public List<int> kitVisistedItems = new List<int>();

        public bool beingReeled = false;
        public int multilineFishing = 0;
        public bool sinkBobber = false;

        public int isHooked = 0;
        public int isSealed = 0;


        public bool seals = false;
        public bool wormicide = false;


        public bool redirectThorns = false;
        public bool linkDamage = false;
        public float linkedDamage = 0f;

        

        public bool destroyBobber = false;
        public bool aimBobber = false;
       
       
        public int projectileDestroyPercentage = 0;


        /*actually per 10000, meaning 1 = 0.01% chance*/
        public int cratePercent = 0;
        public int moneyPercent = 0;
        public int questPercent = 0;


        public float fallOnFloorPercentage = 0;


        public bool lifeforceArmorEffect = false;
        public bool fractaliteArmorEffect = false;
        public bool wormSpawner = false;

        public bool TurretMode = false;
        public int maxBobbersPerEnemy = -1;
        public bool smartBobberDistribution = false;
        public int smartBobberRange = 64;

        public int baitDispersalRange = 0;

        public int currentReelGear = 0;
        public int currentMaxReelGear = 0;

        public bool bobbersCatchItems = false;

        public bool retractAttach = false;
        public bool targetedBobber = false;
        public bool targetedBobberSticky = false;
        public bool targetedBobberMagnetic = false;

        public int timeSinceSweat = 0;
        public override void ResetEffects()
        {
            kitVisistedItems.Clear();
            buddyfish = false;
            resetManaShields();
            resetBaits();
            resetFishingModifiers();
            resetKnives();
            resetFishermenBuffs();
            resetEscalation();
            resetBees();
            ResetRodModifiers();
            // bobberDamage = 1.0f;
            // 
            //bobberCrit = 0;

            currentMaxReelGear = 0;

            beingReeled = false;
            multilineFishing = 0;
            sinkBobber = false;
            seals = false;
            wormicide = false;
            redirectThorns = false;
            linkDamage = false;
            linkedDamage = 0f;
            
            
            destroyBobber = false;
            aimBobber = false;
            bobberShootSpeed = 1.0f;
            
            projectileDestroyPercentage = 0;
            

            cratePercent = 0;
            moneyPercent = 0;
            questPercent = 0;

            

            lifeforceArmorEffect = false;
            fractaliteArmorEffect = false;
            wormSpawner = false;

           
            maxBobbersPerEnemy = -1;
            smartBobberDistribution = false;
            smartBobberRange = 64;
            
            fallOnFloorPercentage = 0;
            baitDispersalRange = 0;
            bobbersCatchItems = false;


            if (Player.HeldItem.ModItem != null && Player.HeldItem.ModItem is BattleRod)
            {
                Player.HeldItem.autoReuse = false;
            }

            retractAttach = false;
            targetedBobber = false;
            targetedBobberSticky = false;
            targetedBobberMagnetic = false;

            List<int> toRemove = new List<int>();
            for (int i = 0; i < activeTurrets.Count; i++) {
                foreach(int k in activeTurrets[i].timer.Keys)
                {
                    if (Main.projectile[k] == null || !Main.projectile[k].active || Main.projectile[k].ModProjectile == null || Main.projectile[k].ModProjectile as Bobber == null)
                    {
                        toRemove.Add(k);
                    }
                }
            }
            for (int i = 0; i < activeTurrets.Count; i++)
            {
                foreach (int k in toRemove)
                {
                    if (activeTurrets[i].timer.ContainsKey(k))
                    {
                        activeTurrets[i].timer.Remove(k);
                    }
                    if (activeTurrets[i].cycle.ContainsKey(k))
                    {
                        activeTurrets[i].cycle.Remove(k);
                    }
                }
            }

           

            base.ResetEffects();
        }

        public override void PreUpdate()
        {
            if (!Main.dedServ) {
                if ((IsBattlerodHeld || IsBattlerodOnHotbar) && Main.playerInventory && Player.chest == -1)
                {
                    if (IsBattlerodHeld)
                    {
                        UserInterface ammoUI = ModContent.GetInstance<AmmoUISystem>().AmmoUI;
                        if (ammoUI.CurrentState == null)
                        {
                            UIStateBaitAmmo sba = new UIStateBaitAmmo();
                            ammoUI.SetState(sba);
                            sba.Activate();
                        }
                        else if ((ammoUI.CurrentState as UIStateBaitAmmo).selectedBattlerod != this.HeldBattlerod)
                        {
                            UIStateBaitAmmo sba = new UIStateBaitAmmo();
                            ammoUI.SetState(sba);
                            sba.Activate();
                        }
                    }
                }
                else
                {
                    UserInterface ammoUI = ModContent.GetInstance<AmmoUISystem>().AmmoUI;
                    if (ammoUI.CurrentState != null)
                    {
                        ammoUI.CurrentState.Deactivate();
                    }
                    ammoUI.SetState(null);
                }
            }
            base.PreUpdate();
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            List<Item> items = new List<Item>();
            if (ModContent.GetInstance<UnuPlayerConfig>().startWithRod)
            {
                Item rod = new Item();
                rod.SetDefaults(ModContent.ItemType<WoodenBattlerod>());
                items.Add(rod);
            }
            if (ModContent.GetInstance<UnuPlayerConfig>().startWithBait)
            {
                Item bait = new Item();
                bait.SetDefaults(ModContent.ItemType<PoisonApprenticeBait>());
                bait.stack = 10;
                items.Add(bait);
            }
            return items;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (isSealed > 0)
            {
                modifiers.FinalDamage.Scale(0.8f);
            }
            if (Player.armor[0].type == ModContent.ItemType<FlinxHat>())
            {
                if (Main.hardMode)
                {
                    modifiers.Knockback.Scale(10);
                }
                else
                {
                    modifiers.Knockback.Scale(2);
                }
            }

        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            FishProjectileInfo info = proj.GetGlobalProjectile<FishProjectileInfo>();
            if (!info.hasBeenCalculated)
            {
                if (hasAttachedBobber())
                {
                    info.isDodged = projectileDestroyPercentage > Main.rand.Next(10000);
                }
                info.hasBeenCalculated = true;
            }
            if (proj.hostile && info.isDodged)
            {
                //proj.damage = 0;
                return false;
            }
            return true;
        }

        public override void OnHurt(HurtInfo info)
        {
            manaShieldDamageReduction(info);
        }
        public override void ModifyHurt(ref HurtModifiers modifiers)
        {
            if (modifiers.PvP)
            {
                if (isSealed > 0)
                {
                    modifiers.FinalDamage.Scale(0.8f);
                }
            }
        }

        public override void PostHurt(HurtInfo info)
        {
            if (linkDamage)
            {
                activateLinkDamage(info.Damage, false);
            }
        }

      

        private void activateLinkDamage(double damage, bool crit)
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].ModProjectile != null && Main.projectile[i].ModProjectile is Bobber)
                {
                    Bobber b = Main.projectile[i].ModProjectile as Bobber;
                    if (b.npcIndex > 0 && b.npcIndex < Main.npc.Length)
                    {
                        Player.ApplyDamageToNPC(Main.npc[b.npcIndex], (int)(damage * linkedDamage), 0, 1, crit);
                    }
                    else if (b.npcIndex > Main.npc.Length && b.npcIndex < Main.npc.Length + Main.player.Length)
                    {
                        Terraria.Player foe = Main.player[b.npcIndex - Main.npc.Length];
                        foe.Hurt(PlayerDeathReason.ByProjectile(Player.whoAmI, b.Projectile.whoAmI), (int)(damage * linkedDamage), 0, true, false);
                    }
                }
            }
        }

        protected virtual void Sweat()
        {
            int proj = -1;
            float maxTension = -1;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].type == Player.HeldItem.shoot)
                {
                    Bobber b = Main.projectile[i].ModProjectile as Bobber;
                    if (b != null && b.isStuck())
                    {
                        if (b.currentTension > maxTension)
                        {
                            maxTension = b.currentTension;
                            proj = i;
                        }
                    }
                }
            }
            if (proj < 0)
            {
                return;
            }
            switch ((Main.projectile[proj].ModProjectile as Bobber).tensionRange())
            {
                case 3:
                    Dust.NewDust(this.Player.Top, 12, 12, ModContent.DustType<SweatDust>(), Main.rand.NextFloat() * 2, Main.rand.NextFloat() * 2);
                    Dust.NewDust(this.Player.Top, 12, 12, ModContent.DustType<SweatDust>(), Main.rand.NextFloat() * 2, Main.rand.NextFloat() * 2);
                    return;
                case 2:
                    Dust.NewDust(this.Player.Top, 12, 12, ModContent.DustType<SweatDust>(), Main.rand.NextFloat() * 2, Main.rand.NextFloat() * 2);
                    return;
                case 1:
                    if (timeSinceSweat % 30 == 0)
                        Dust.NewDust(this.Player.Center, 12, 12, ModContent.DustType<SweatDust>(), Main.rand.NextFloat(), Main.rand.NextFloat());
                    return;
                case 0:
                default: break;
            }
        }

        public override void UpdateEquips()
        {
            bool cb = false;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI)
                {
                    if (Main.projectile[i].type == Mod.Find<ModProjectile>("CorruptBobber").Type & !cb && ((Bobber)Main.projectile[i].ModProjectile).isStuck())
                    {
                        Player.moveSpeed += 0.15f;
                        cb = true;
                    }

                }
            }
            timeSinceSweat = (timeSinceSweat + 1) % 60;
            if((timeSinceSweat % 15) == 0)
            {
                Sweat();
            }
            base.UpdateEquips();
        }


        public List<Bobber> getOwnedAttatchedBobbers()
        {
            List<Bobber> ans = new List<Bobber>();
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].ModProjectile != null)
                {
                    Bobber b = Main.projectile[i].ModProjectile as Bobber;
                    if (b != null && b.npcIndex > -1)
                    {
                        ans.Add(b);
                    }
                }
            }
            return ans;
        }

        public bool hasAttachedBobber()
        {
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].owner == Player.whoAmI && Main.projectile[i].ModProjectile != null)
                {
                    Bobber b = Main.projectile[i].ModProjectile as Bobber;
                    if (b != null && b.npcIndex > -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void PostUpdateEquips()
        {
            //int slot = getLargestDamageBonus();
            updateLinkDamage();
            if (ModContent.GetInstance<UnuDificultyConfig>().oldEscalation)
            {
                escalationUpdate();   
            }
            escalationManaUpdate();

            if (fractaliteArmorEffect)
            {
                if (Player.wingsLogic == 0)
                {
                    //player.wings = 29;
                    Player.wingsLogic = 29;
                    Player.wingTimeMax += 180;
                }
                else
                {
                    Player.wingTimeMax *= 2;
                }
            }
            if (currentReelGear > currentMaxReelGear)
                currentReelGear = currentMaxReelGear;
            else if (currentReelGear < -currentMaxReelGear)
                currentReelGear = -currentMaxReelGear;
            
            if (Main.netMode == NetmodeID.SinglePlayer || (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer))
            {
                beesUpdate();
                knifeUpdate();
            }
            if (activeTurrets.Count > 0)
            {
                if (Player.HasBuff<ActiveTurretBuff>())
                {
                    if (decreaseTurretTime())
                    {
                        initTurrets();
                    }
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i] != null && Main.projectile[i].active && Main.projectile[i].ModProjectile != null && Main.projectile[i].owner == Player.whoAmI)
                        {
                            if (Main.projectile[i].ModProjectile is Bobber)
                            {
                                Bobber b = Main.projectile[i].ModProjectile as Bobber;
                                updateTurrets(b, i);
                            }
                        }
                    }
                }
                else
                {
                    activeTurrets.Clear();
                }
            }
        }

        public Vector2 newSpeed = Vector2.Zero;
        public Vector2 newCenter = new Vector2(-10000, -10000);


        public override void PostUpdate()
        {
            //checkForMimicLikeSpawns();
            resetBaits();
            resetTurretsIfBuffOver();


            if (newCenter.X > -10000 && newCenter.Y > -10000 && !IncreaseTension)
            {
                //Main.NewText("Position: " + newCenter.X +" : " + newCenter.Y + " ;");
                if (WorldGen.InWorld((int)(newCenter.X / 16.0f), (int)(newCenter.Y / 16.0f)))
                {
                    Player.Center = new Vector2(newCenter.X, newCenter.Y);
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].ModProjectile != null)
                        {
                            Bobber b = Main.projectile[i].ModProjectile as Bobber;
                            if (b != null && b.npcIndex - Main.npc.Length == Player.whoAmI)
                            {
                                b.Projectile.Center = new Vector2(newCenter.X, newCenter.Y);
                            }
                        }
                    }
                }
                // player.velocity = new Vector2(newSpeed.X, newSpeed.Y);
                newCenter = new Vector2(-10000, -10000);
            }
            

            /*if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                
            }*/

        }
        

        private void updateLinkDamage()
        {
            if (linkDamage)
            {
                linkedDamage += Player.thorns;
                if (Player.turtleThorns)
                    linkedDamage += 1f;
                if (redirectThorns)
                {
                    Player.turtleThorns = false;
                    Player.thorns = 0f;
                }
            }
        }

        public override void UpdateBadLifeRegen()
        {
            updateFrostfire();
            updateSolarfire();
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            drawFrostfire(drawInfo,ref r, ref g, ref b, ref a, ref fullBright);
            drawSolarfire(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        } 

        public override void OnEnterWorld()
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {

            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.SyncAllPlayersRodAmmo);
                pk.Write((short)Main.myPlayer);
                pk.Send();
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                //downloadAppliedBaits();
                ModPacket pk = Mod.GetPacket();
                pk.Write((byte)UnuBattleRodsR.Message.SyncAllPlayersRodAmmo);
                pk.Write((short)-1);
                pk.Send();
            }
        }

     

    

        SlotId gearSoundSlot;

        SoundStyle gear1 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Reels/Gear1");
        SoundStyle gear2 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Reels/Gear2");
        SoundStyle gear3 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Reels/Gear3");
        SoundStyle gear4 = new SoundStyle("UnuBattleRodsR/Items/Accessories/Reels/Gear4");
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if(Player.HeldItem != null && Player.HeldItem.ModItem as BattleRod != null)
            {
                if (triggersSet.Left && triggersSet.Right)
                {
                    Player.velocity.X = 0;
                    IncreaseTension = true;
                }
                else
                {
                    IncreaseTension = false;
                }
            }
            else
            {
                IncreaseTension = false;
            }
            if (FishWorld.GearShift.JustPressed)
            {
                var oldGear = currentReelGear;
                if (TurretMode)
                {
                    explodeTurretOnCommand = true;
                }
                else
                {
                    if (triggersSet.Down)
                    {
                        currentReelGear--;
                        if (currentReelGear < -currentMaxReelGear)
                            currentReelGear = -currentMaxReelGear;
                    }
                    else
                    {
                        currentReelGear++;
                        if (currentReelGear > currentMaxReelGear)
                            currentReelGear = currentMaxReelGear;
                    }
                    var v = new AdvancedPopupRequest();
                    v.Color = Color.White;
                    v.DurationInFrames = 50;
                    if (currentMaxReelGear == 0)
                        v.Text = "No Gears!";
                    else if (currentReelGear == 0)
                        v.Text = "Neutral Gear";
                    else if (currentReelGear < 0)
                        v.Text = "Slow Gear " + (-currentReelGear);
                    else
                        v.Text = "Fast Gear " + currentReelGear;
                    PopupText.NewText(v, Player.Top + new Vector2(0, -16));
                }
                if(oldGear < currentReelGear)
                {
                    SoundEngine.PlaySound(Main.rand.NextBool() ? gear1 : gear3);
                }else if (oldGear >= currentReelGear)
                {
                    SoundEngine.PlaySound(Main.rand.NextBool() ? gear2 : gear4);
                }
            }
            else
            {
                explodeTurretOnCommand = false;
            }
            if(FishWorld.TurretMode.JustPressed)
            {
                TurretMode = !TurretMode;
                if(TurretMode)
                {
                    var v = new AdvancedPopupRequest();
                    v.Color = Color.Orange;
                    v.DurationInFrames = 50;
                    v.Text = "Not Sticky!";
                    PopupText.NewText(v, Player.Top + new Vector2(0, -16));
                }
                else 
                {
                    var v = new AdvancedPopupRequest();
                    v.Color = Color.Green;
                    v.DurationInFrames = 50;
                    v.Text = "Sticky!";
                    PopupText.NewText(v, Player.Top + new Vector2(0, -16));
                }
            }
        }

        public override void SaveData(TagCompound tag)
        {
            for(int i = 0; i < DedicatedBaits.Length; i++)
            {
                if (DedicatedBaits[i] != null && !DedicatedBaits[i].IsAir)
                    tag["baits" + i] = TagIO.Serialize(DedicatedBaits[i]);
            }
            for (int i = 0; i < DedicatedDiscardables.Length; i++)
            {
                if (DedicatedDiscardables[i] != null && !DedicatedDiscardables[i].IsAir)
                    tag["discards" + i] = TagIO.Serialize(DedicatedDiscardables[i]);
            }
            for (int i = 0; i < DedicatedTurrets.Length; i++)
            {
                if (DedicatedTurrets[i] != null && !DedicatedTurrets[i].IsAir)
                    tag["turrets" + i] = TagIO.Serialize(DedicatedTurrets[i]);
            }
            for (int i = 0; i < DedicatedOptions.Length; i++)
            {
                if (DedicatedOptions[i] != null && !DedicatedOptions[i].IsAir)
                    tag["minions" + i] = TagIO.Serialize(DedicatedOptions[i]);
            }
        }

        public override void LoadData(TagCompound tag)
        {
            for (int i = 0; i < DedicatedBaits.Length; i++)
            {
                if (tag.ContainsKey("baits" + i))
                {
                    Item v = (Item)(TagIO.Deserialize(typeof(Item), tag["baits" + i]));
                    if (v != null)
                        DedicatedBaits[i] = v;
                }
            }
            for (int i = 0; i < DedicatedDiscardables.Length; i++)
            {
                if (tag.ContainsKey("discards" + i))
                {
                    Item v = (Item)(TagIO.Deserialize(typeof(Item), tag["discards" + i]));
                    if (v != null)
                        DedicatedDiscardables[i] = v;
                }
            }
            for (int i = 0; i < DedicatedTurrets.Length; i++)
            {
                if (tag.ContainsKey("turrets" + i))
                {
                    Item v = (Item)(TagIO.Deserialize(typeof(Item), tag["turrets" + i]));
                    if (v != null)
                        DedicatedTurrets[i] = v;
                }
            }
            for (int i = 0; i < DedicatedOptions.Length; i++)
            {
                if (tag.ContainsKey("minions" + i))
                {
                    Item v = (Item)(TagIO.Deserialize(typeof(Item), tag["minions" + i]));
                    if (v != null)
                        DedicatedOptions[i] = v;
                }
            }
        }
    }
}
