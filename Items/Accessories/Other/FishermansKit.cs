using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Items.Accessories.Other
{
    public class FishermansKit : ModItem
    {

        public class AccessoryGrouping
        {
            public string key;
            public List<int> accessoryTypes;
            public bool blocking = true;
        }

        public class AccessoryGroupingWithTop : AccessoryGrouping
        {
            public int mainItem;
        }

        public static List<AccessoryGrouping> allowedAccessories = new List<AccessoryGrouping>();
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your inventory!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            List<Item> collectible = gatherUseableEquips(player,player.inventory);
            collectible.AddRange(gatherUseableEquips(player, player.bank.item));
            collectible.AddRange(gatherUseableEquips(player, player.bank2.item));
            collectible.AddRange(gatherUseableEquips(player, player.bank3.item));
            collectible.AddRange(gatherUseableEquips(player, player.bank4.item));

            updateAllCollectedItems(player, collectible);
        }

        public static bool ItemInAllowedAccessories(Item itm)
        {
            for(int i = 0; i < allowedAccessories.Count; i++)
            {
                var ag = allowedAccessories[i] as AccessoryGroupingWithTop;
                if (ag != null)
                {
                    if(ag.mainItem == itm.type)
                        return true;
                }
                if (allowedAccessories[i].accessoryTypes.Contains(itm.type))
                {
                    return true;
                }
            }
            return false;
        }

        public static AccessoryGrouping GetItemGroup(Item itm)
        {
            for (int i = 0; i < allowedAccessories.Count; i++)
            {
                var ag = allowedAccessories[i] as AccessoryGroupingWithTop;
                if (ag != null)
                {
                    if (ag.mainItem == itm.type)
                        return ag;
                }
            }
            for (int i = 0; i < allowedAccessories.Count; i++)
            {
                var ag = allowedAccessories[i] as AccessoryGroupingWithTop;
                if (allowedAccessories[i].accessoryTypes.Contains(itm.type) && ag == null)
                {
                    return allowedAccessories[i];
                }
            }
            return null;
        }

        public static List<Item> gatherUseableEquips(Player player, Item[] itemCollection)
        {
            List<int> kitVisistedItems = player.GetModPlayer<FishPlayer>().kitVisistedItems;
            List<Item> ans = new List<Item>();
            for (int i = 0; i < itemCollection.Length; i++)
            {
                if (!kitVisistedItems.Contains(itemCollection[i].type) && ItemInAllowedAccessories(itemCollection[i])){
                    AccessoryGrouping ag = GetItemGroup(itemCollection[i]);
                    if(ag is AccessoryGroupingWithTop && (ag as AccessoryGroupingWithTop).mainItem == itemCollection[i].type)
                    {
                        kitVisistedItems.AddRange(ag.accessoryTypes);
                    }
                    else if(!(ag is AccessoryGroupingWithTop) && ag.blocking)
                    {
                        kitVisistedItems.AddRange(ag.accessoryTypes);
                    }
                    ans.Add(itemCollection[i]);
                    kitVisistedItems.Add(itemCollection[i].type);
                }
            }
            return ans;
        }

        public static void updateAllCollectedItems(Player p , List<Item> toUpdate)
        {
            for (int i = 0; i < toUpdate.Count; i++)
            {
                updateEquips(p, toUpdate[i]);
            }
        }

        public static void UpdateEquipsFromInv(Player p, Item[] itemCollection)
        {
            List<Item> toUpdate = gatherUseableEquips(p, itemCollection);
            updateAllCollectedItems(p, toUpdate);
        }

        public static void updateEquips(Player p, Item itm)
        {
            Player.DefenseStat oldDefence = p.statDefense;
            //p.GrantPrefixBenefits(itm);
            p.ApplyEquipFunctional(itm, true);
            if (itm.ModItem != null)
            {
                itm.ModItem.UpdateEquip(p);
                itm.ModItem.UpdateAccessory(p, true);
            }
           /* p.ApplyEquipVanity(itm);
            p.UpdateVisibleAccessories(itm, false);*/
            p.statDefense = oldDefence;
        }
    }

    public class FishermansKit1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (bank)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your piggy bank!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishermansKit.UpdateEquipsFromInv(player, player.bank.item);
        }
    }

    public class FishermansKit2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (safe)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your safe!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishermansKit.UpdateEquipsFromInv(player, player.bank2.item);
        }
    }
    public class FishermansKit3: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (Defender's Forge)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your Defender's Forge!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit4");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishermansKit.UpdateEquipsFromInv(player, player.bank3.item);
        }
    }

    public class FishermansKit4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Fisherman's Kit (Void Vault)");
            // Tooltip.SetDefault("Allows you to use several types of Fishing Accessories directly from your Void Vault!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "RetractableFasterFishingKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit1");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit2");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "FishermansKit3");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            FishermansKit.UpdateEquipsFromInv(player, player.bank4.item);
        }
    }
}
