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
    [AutoloadEquip(EquipType.Body)]
    public class BoneeBeeVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Bonee Bee Vest");
            // Tooltip.SetDefault("Increases Fishing Skill by 15\nIncreases Fishing Damage by 10%");
            ArmorIDs.Body.Sets.HidesHands[Item.bodySlot] = false;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 11;
            Item.rare = 3;
            Item.defense = 7;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        
        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 15;
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            player.GetDamage<FishingDamage>() += 0.10f;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.Hive, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
