using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR.Items.Accessories.Wires
{
    public class LinkCable: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Link Cable");
            // Tooltip.SetDefault("Damage increased by ammount bobbers stuck to different enemies.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(0,1,0,0);
            Item.rare = 2;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 25);
            recipe.AddRecipeGroup("UnuBattleRodsR:Tier3Bars", 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
        public override void UpdateEquip(Player player)
        {
            float ans = 0;
            int bobberCount = 0;
            List<int> uniqueStuck = new List<int>();
            for(int i = 0; i < Main.projectile.Length; i++)
            {
                if(Main.projectile[i].owner == player.whoAmI && Main.projectile[i].ModProjectile != null && Main.projectile[i].ModProjectile is Bobber)
                {
                    bobberCount++;
                    Bobber b = (Bobber)(Main.projectile[i].ModProjectile);
                    if (!uniqueStuck.Contains(b.npcIndex))
                    {
                        uniqueStuck.Add(b.npcIndex);
                    }
                }
            }
            
            if(bobberCount <= 8)
            {
                ans = (uniqueStuck.Count - 1) * 0.05f;
            }
            else
            {
                ans = (uniqueStuck.Count - 1) * 0.02f;
            }



            player.GetDamage<FishingDamage>() += ans;
            
        }
    }
}
