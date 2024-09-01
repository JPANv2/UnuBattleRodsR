using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using UnuBattleRodsR.Buffs.Minion;
using UnuBattleRodsR.Common;
using UnuBattleRodsR.Configs;

namespace UnuBattleRodsR.Items.Minions
{
    public class Buddylure : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            // DisplayName.SetDefault("Buddy Lure");
            // Tooltip.SetDefault("Summons two buddy fish to fight for you.");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = ModContent.GetInstance<FishingDamage>();
            Item.mana = 1;
            Item.width = 26;
            Item.height = 28;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = 1;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 9;
            Item.UseSound = SoundID.Item44;
            Item.shoot = Mod.Find<ModProjectile>("Buddyfish").Type;
            Item.shootSpeed = 10f;
            Item.buffType = Mod.Find<ModBuff>("Buddyfish").Type; 
            Item.ResearchUnlockCount = 1;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            StatModifier one = new StatModifier(1, 1, 0, 0);
            if (ModContent.GetInstance<UnuServerConfig>().allowBorrowDamage)
            {
                FieldInfo f = typeof(DamageClassLoader).GetField("DamageClasses", BindingFlags.Static | BindingFlags.NonPublic);
                if (f == null)
                {
                    return;
                }
                float maxDamageMult = 0f;
                List<DamageClass> allLoadedClasses = (List<DamageClass>)f.GetValue(null);
                for (int i = 0; i < allLoadedClasses.Count; i++)
                {
                    if (allLoadedClasses[i].Type != ModContent.GetInstance<FishingDamage>().Type && player.GetDamage(allLoadedClasses[i]).ApplyTo(1f) > maxDamageMult)
                    {
                        maxDamageMult = player.GetDamage(allLoadedClasses[i]).ApplyTo(1f);
                        one = player.GetDamage(allLoadedClasses[i]);
                    }
                }
            }
            damage = damage.CombineWith(one);
        }*/

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!target.boss) modifiers.FinalDamage *= 2;
        }

        public virtual void GetRealWeaponDamage(Player player, ref int damage)
        {
            damage = NPC.downedMoonlord ? 60 : (Main.hardMode ? 15 : 5);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
           
            player.AddBuff(Item.buffType, 2);

            
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            GetRealWeaponDamage(player, ref projectile.originalDamage);
            projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            GetRealWeaponDamage(player, ref projectile.originalDamage);

    
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
            int idx = tooltips.FindIndex(x => x.Name == "Damage");
            if (idx >= 0)
            {
                tooltips.RemoveAt(idx);
                int dmg = 0;
                GetRealWeaponDamage(Main.player[Main.myPlayer], ref dmg);
                StatModifier mod = Main.player[Main.myPlayer].GetDamage<FishingDamage>();
                ModifyWeaponDamage(Main.player[Main.myPlayer], ref mod);
                dmg = (int)(Math.Round(mod.ApplyTo(dmg)));
                tooltips.Insert(idx, new TooltipLine(Mod, "Damage", dmg + " Fishing damage"));
            }
        }
    }

}
