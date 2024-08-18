using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Items.Minions;
using UnuBattleRodsR.Players;

namespace UnuBattleRodsR.Buffs.Minion
{
    public class Buddyfish: ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Buddy Fish");
            // Description.SetDefault("This pair will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FishPlayer modPlayer = player.GetModPlayer<FishPlayer>();
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Buddyfish").Type] > 0)
            {
                Buddylure buddyLure = ModContent.GetInstance<Buddylure>();
                int damage = 20;
                buddyLure.GetRealWeaponDamage(player, ref damage);
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Buddyfish").Type] < 2)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.One, Mod.Find<ModProjectile>("Buddyfish").Type, damage, 1);
                    Main.projectile[proj].owner = player.whoAmI;
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Buddyfish").Type] > 2)
                {
                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].owner == player.whoAmI)
                        {
                            if (Main.projectile[i].ModProjectile as Projectiles.Minions.Buddyfish != null)
                            {
                                Main.projectile[i].timeLeft = 0;
                                Main.projectile[i].Kill();
                            }
                        }
                    }
                }
                modPlayer.buddyfish = true;
            }
            
            if (!modPlayer.buddyfish)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
