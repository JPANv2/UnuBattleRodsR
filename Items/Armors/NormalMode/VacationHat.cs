using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Armors.NormalMode
{
    [AutoloadEquip(EquipType.Head)]
    public class VacationHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Vacation Hat");
            // Tooltip.SetDefault("Increases Fishing Skill by 3");
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 7;
            Item.rare = 1;
            Item.defense = 1;
            Item.value = Item.sellPrice(0, 0, 1, 0);
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == this.Item.type && body.type == Mod.Find<ModItem>("VacationVest").Type && legs.type == Mod.Find<ModItem>("VacationPants").Type;
        }


        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases Fishing Damage and Bob Speed by 5%";
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            player.GetDamage<FishingDamage>() += 0.05f;
            pl.bobberSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 2);
        
            recipe.AddIngredient(ItemID.Cactus, 2);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }
    }
}
