using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Accessories.Emblems;
using UnuBattleRodsR.Items.Accessories.Other;
using UnuBattleRodsR.Items.Rods;
using UnuBattleRodsR.Items.Rods.Battlerods;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items
{
    public class BoxGlobalItem : GlobalItem
    {

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.fishingPole > 0 && (item.ModItem == null || !(item.ModItem is BattleRod)))
            {
                int lures = player.GetModPlayer<FishPlayer>().multilineFishing;
                if (lures > 0)
                {
                    for (int i = 1; i < lures; i++)
                    {
                        Vector2 pos = new Vector2(position.X + Main.rand.Next(5), position.Y + Main.rand.Next(5));
                        Vector2 speed = new Vector2(velocity.X + Main.rand.Next(lures) - (lures / 2), velocity.Y + Main.rand.Next(lures) - (lures / 2));
                        Projectile.NewProjectile(source, pos, speed, type, damage, knockback, player.whoAmI);
                    }
                }
                return true;
            }
            else
            {
                return base.Shoot(item,player,source,position,velocity,type,damage,knockback);
            }
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if(item.type == ItemID.DungeonFishingCrate || item.type == ItemID.DungeonFishingCrateHard || item.type == ItemID.LockBox)
            {
                itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DungeonBattlerod").Type, 6, 1, 1));
            }
            if(item.type == ItemID.QueenBeeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BeeBattlerod>(), 3, 1, 1));
            }
            if (item.type == ItemID.DeerclopsBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeerclopsBattlerod>(), 3, 1, 1));
            }
            if (item.type == ItemID.QueenSlimeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SlimeQueenBattlerod>(), 3, 1, 1));
            }
            if (item.type == ItemID.FishronBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FishronBattlerod>(), 3, 1, 1));
            }
            if (item.type == ItemID.FairyQueenBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmpresssPersonalBattlerod>(), 3, 1, 1));
            }
            if(item.type == ItemID.WallOfFleshBossBag)
            {
                itemLoot.Add(ItemDropRule.OneFromOptions(3, ModContent.ItemType<FishingEmblem>(), ModContent.ItemType<FishingEmblemSpeed>()));
            }

        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            
            if (FishermansKit.ItemInAllowedAccessories(item)){
                var ag = FishermansKit.GetItemGroup(item);
                tooltips.Add(new TooltipLine(this.Mod, "FishermansKitUsed", "Allowed in the Fisherman's Kit, on group " + ag.key + ( ag.blocking ? " (exclusive)":" (not exclusive)")));
            }
        }
    }
}
