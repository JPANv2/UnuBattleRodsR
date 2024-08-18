using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Head)]
    public class BoneeBeeHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Bonee Bee Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 10\nIncreases Bob Speed by 8%");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 3;
            Item.defense = 7;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 10;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.08f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("BoneeBeeVest").Type && legs.type == Mod.Find<ModItem>("BoneeBeePants").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Spawns Bonee Bees to help you";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.boneeBees = true;
            pl.boneeBeesCooldown = 180;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Bone, 10);
            recipe.AddIngredient(ItemID.Hive, 8);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
