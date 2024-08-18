using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Emblems
{
    public class FishingEmblem: ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RangerEmblem);
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<FishingDamage>() += 0.15f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return !(equippedItem.type == incomingItem.type || 
                (equippedItem.type == this.Type && incomingItem.type == ModContent.ItemType<FishingEmblemSpeed>()) || 
                (incomingItem.type == this.Type && equippedItem.type == ModContent.ItemType<FishingEmblemSpeed>()));
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe();
            rec.AddIngredient<FishingEmblemSpeed>();
            rec.Register();

            rec = CreateRecipe();
            rec.ReplaceResult(ItemID.AvengerEmblem);
            rec.AddIngredient<FishingEmblem>();
            rec.AddIngredient(ItemID.SoulofMight, 5);
            rec.AddIngredient(ItemID.SoulofFright, 5);
            rec.AddIngredient(ItemID.SoulofSight, 5);
            rec.AddTile(TileID.TinkerersWorkbench);
            rec.Register();
        }
    }
    public class FishingEmblemSpeed : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RangerEmblem);
            Item.ResearchUnlockCount = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FishPlayer pl = player.GetModPlayer<FishPlayer>();
            pl.bobberSpeed += 0.12f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return !(equippedItem.type == incomingItem.type ||
                (equippedItem.type == this.Type && incomingItem.type == ModContent.ItemType<FishingEmblem>()) ||
                (incomingItem.type == this.Type && equippedItem.type == ModContent.ItemType<FishingEmblem>()));
        }

        public override void AddRecipes()
        {
            Recipe rec = CreateRecipe();
            rec.AddIngredient<FishingEmblem>();
            rec.Register();

            rec = CreateRecipe();
            rec.ReplaceResult(ItemID.AvengerEmblem);
            rec.AddIngredient<FishingEmblemSpeed>();
            rec.AddIngredient(ItemID.SoulofMight, 5);
            rec.AddIngredient(ItemID.SoulofFright, 5);
            rec.AddIngredient(ItemID.SoulofSight, 5);
            rec.AddTile(TileID.TinkerersWorkbench);
            rec.Register();
        }
    }
}
