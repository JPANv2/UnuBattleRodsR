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

namespace UnuBattleRodsR
{
    public class AmmoUISystem : ModSystem
    {
        public UserInterface AmmoUI;

        public override void Load()
        {
            AmmoUI = new UserInterface();

            base.Load();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if(AmmoUI?.CurrentState != null)
                AmmoUI?.Update(gameTime);

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
            }
        }

    }
}
