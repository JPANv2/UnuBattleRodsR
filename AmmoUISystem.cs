using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Players.AmmoUI;

namespace UnuBattleRodsR
{
    public class AmmoUISystem : ModSystem
    {
        public UserInterface AmmoUI;
        public UserInterface AmmoRecharger;

        public override void Load()
        {
            AmmoUI = new UserInterface();
            AmmoRecharger = new UserInterface();
            base.Load();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if(AmmoUI?.CurrentState != null)
                AmmoUI?.Update(gameTime);
            if (AmmoRecharger?.CurrentState != null)
                AmmoRecharger?.Update(gameTime);

        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int invIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (invIndex != -1)
            {
                layers.Insert(invIndex, new LegacyGameInterfaceLayer(
                    "UnusBattlerodsR: Rod Ammo",
                    delegate {
                        FishPlayer cur = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                        bool shouldDisplay = !(cur == null || !(cur.IsBattlerodHeld || cur.IsBattlerodOnHotbar) || !Main.playerInventory || cur.Player.chest != -1 || Main.CreativeMenu.Enabled || Main.npcShop > 0);
                        if (shouldDisplay && AmmoUI?.CurrentState != null )
                        {
                            GameTime gt = new GameTime();
                            AmmoUI.Update(gt);
                            if (AmmoUI?.CurrentState != null)
                            AmmoUI.Draw(Main.spriteBatch, gt);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(invIndex, new LegacyGameInterfaceLayer(
                    "UnusBattlerodsR: Ammo Recharger",
                    delegate {
                        FishPlayer cur = Main.LocalPlayer.GetModPlayer<FishPlayer>();
                        if (cur.AmmoRecharger >= 0)
                        {
                            if(AmmoRecharger.CurrentState == null)
                            {
                                AmmoRecharger.SetState(new AmmoRechargerUI());
                            }
                            GameTime gt = new GameTime();
                            AmmoRecharger.Update(gt);
                            if (AmmoRecharger?.CurrentState != null)
                                AmmoRecharger.Draw(Main.spriteBatch, gt);
                        }
                        else
                        {
                            AmmoRecharger.SetState(null);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

    }
}
