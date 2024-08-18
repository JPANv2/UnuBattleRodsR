using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Capes
{
    [AutoloadEquip(EquipType.Back, EquipType.Front)]
    public class WormCape : ModItem
    {

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.ResearchUnlockCount = 1;
            Item.height = 16;
            Item.width = 16;
            Item.accessory = true;
            Item.rare = ItemRarityID.Quest;
            Item.value = Item.sellPrice(0,2,50,0);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<FishPlayer>().wormSpawner = true;
            player.GetModPlayer<FishPlayer>().lifeforceArmorEffect = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.BlackThread, 1);
            recipe.AddIngredient(ItemID.Worm, 40);
            recipe.AddIngredient(ItemID.EnchantedNightcrawler, 10);
            recipe.AddTile(TileID.Loom);
            recipe.Register();
        }

    }
}

