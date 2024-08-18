using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace UnuBattleRodsR.Items.Pets
{
    public class CratePetItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crate Charm");
            // Tooltip.SetDefault("Summons a Crate Pet that greatly helps you fish crates.");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CrimsonHeart);
            Item.shoot = Mod.Find<ModProjectile>("CratePetProjectile").Type;
            Item.buffType = Mod.Find<ModBuff>("CratePetBuff").Type;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "MimicCrate", 20);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }

}

