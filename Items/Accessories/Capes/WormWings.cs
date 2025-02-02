using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Capes
{
    [AutoloadEquip(EquipType.Wings)]
    public class WormWings : ModItem
    {

        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(200, 5f, 1f);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 1;
            Item.height = 16;
            Item.width = 16;
            Item.accessory = true;
            Item.rare = ItemRarityID.Quest;
            Item.value = Item.sellPrice(0,5,0,0);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().wormSpawner = true;
            player.GetModPlayer<FishPlayer>().lifeforceArmorEffect = true;
            player.GetModPlayer<FishPlayer>().buffedByWorms = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<WormCape>(),1);
            recipe.AddIngredient(ItemID.SoulofFlight, 25);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }

    }
}

