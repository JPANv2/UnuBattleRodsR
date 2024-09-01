using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using UnuBattleRodsR.Buffs;
using UnuBattleRodsR.Configs;
using UnuBattleRodsR.Items;
using UnuBattleRodsR.Items.BossBags;
using UnuBattleRodsR.Items.Consumables.Baits.SummonBaits;
using UnuBattleRodsR.Items.Currency;
using UnuBattleRodsR.Items.Materials;
using UnuBattleRodsR.Items.Rods.HardMode;
using UnuBattleRodsR.Items.Rods.NormalMode;
using UnuBattleRodsR.Items.Rods.PostMoonLord;
using UnuBattleRodsR.Items.Weapons.Cooler;
using UnuBattleRodsR.NPCs;
using UnuBattleRodsR.Players;
using UnuBattleRodsR.Projectiles.Bobbers.BaseBobber;

namespace UnuBattleRodsR
{
    public partial class UnuBattleRodsR : Mod
    {
        public static string GithubUserName { get { return "jpanv2"; } }
        public static string GithubProjectName { get { return "UnuBattleRodsR"; } }

        public UnuPlayerConfig playerConfig = ModContent.GetInstance<UnuPlayerConfig>();
        public UnuServerConfig serverConfig = ModContent.GetInstance<UnuServerConfig>();
        public FishToReplaceConfig fishToReplaceConfig = ModContent.GetInstance<FishToReplaceConfig>();
        public FishSteakRecipesConfig fishRecipes = ModContent.GetInstance<FishSteakRecipesConfig>();

        public Dictionary<int, List<RechargeRecipe>> rechargeableRecipesByResult = new Dictionary<int, List<RechargeRecipe>>();
        public Dictionary<int, List<RechargeRecipe>> rechargeableRecipesByDepleted = new Dictionary<int, List<RechargeRecipe>>();


        public static readonly bool DEBUG = true;
        
        public UnuBattleRodsR()
        {
        }
        public static int fishSteaksCurrencyID = -1;
        public override void Load()
        {
            fishSteaksCurrencyID = CustomCurrencyManager.RegisterCurrency(new FishCurrency(ModContent.ItemType<FishSteaks>(), 9999, "Mods.UnuBattleRodsR.Currencies.FishSteaks"));
            base.Load();
        }

        public override void Unload()
        {
            rechargeableRecipesByDepleted.Clear();
            rechargeableRecipesByResult.Clear();    
            base.Unload();
        }



        public static bool thoriumPresent = false;

        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bosses))
            {
                bosses.Call("LogBoss", this,
                    nameof(CoolerBoss),
                    5.39f,
                    () => ModContent.GetInstance<FishWorld>().downedCooler,
                    ModContent.NPCType<CoolerBoss>(),
                    new Dictionary<string, object>()
                    {
                        ["spawnItems"] = ModContent.ItemType<IceyWorm>(),
                    });
                //bosses.Call(parameters);
            }
            thoriumPresent = ModLoader.TryGetMod("ThoriumMod", out Mod Thorium);

            AddPartSupport();

           
        }

        public static int getTypeFromTag(string tag)
        {
            int type = 0;
            if (!Int32.TryParse(tag, out type))
            {
                if (ModLoader.TryGetMod(tag.Split(':')[0], out Mod m ))
                    type = m.Find<ModItem>(tag.Split(':')[1]).Type;
            }
            return type;
        }

        public static Item getItemFromTag(string tag, bool noMatCheck = false)
        {
            Item ret = new Item();
            int type = getTypeFromTag(tag);
            if (type != 0)
                ret.SetDefaults(type, noMatCheck);
            return ret;
        }

        public static string ItemToTag(Item itm)
        {
            String type = "" + itm.type;
            if (itm.ModItem != null)
            {
                type = itm.ModItem.Mod.Name + ":" + itm.ModItem.Name;
            }

            return type;
        }

        public static int getItemTypeFromTag(string tag)
        {
            int type = 0;
            if (!Int32.TryParse(tag, out type))
            {
                if (ModLoader.TryGetMod(tag.Split(':')[0], out Mod m))
                    type = m.Find<ModItem>(tag.Split(':')[1]).Type;
            }
            return type;
        }

        public static List<string> getStringListFromConfig(Preferences configuration, string tokenID)
        {
            List<string> ans = new List<string>();
            Newtonsoft.Json.Linq.JArray o = configuration.Get<Newtonsoft.Json.Linq.JArray>(tokenID, null);
            if (o != null)
            {
                foreach (Newtonsoft.Json.Linq.JToken j in o)
                {
                    ans.Add(j.ToString());
                }
            }
            return ans;
        }

       /* public override void AddRecipes()
        {
            AddLureRecipes();
            AddSelectiveRecipes();
        }*/

       
        /*
        Recipe recipe1Lure;
        Recipe recipe1Loser;
        Recipe recipe2Lure;
        Recipe recipe2Loser;
        Recipe recipe4Lure;
        Recipe recipe4LureHM;
        Recipe recipe4Loser;
        Recipe recipe4LoserHM;
        Recipe recipe8Lure;
        Recipe recipe8LureHM;
        Recipe recipe16Lure;
        Recipe recipe16LureHM;
        Recipe recipe32Lure;
        Recipe recipe32LureHM;

        public void AddLureRecipes()
        {
            recipe1Lure = Recipe.Create();
            recipe1Lure.AddIngredient(ItemID.Cobweb, 25);
            recipe1Lure.AddIngredient(ItemID.Hook, 1);
            recipe1Lure.AddTile(TileID.WorkBenches);
            recipe1Lure.SetResult(this, "ExtraLure");

            recipe1Loser = Recipe.Create();
            recipe1Loser.AddIngredient(ItemID.Cobweb, 25);
            recipe1Loser.AddIngredient(ItemID.Hook, 1);
            recipe1Loser.AddTile(TileID.WorkBenches);
            recipe1Loser.SetResult(this, "BobLoser");

            recipe2Lure = Recipe.Create();
            recipe2Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe2Lure.AddIngredient(this, "ExtraLure", 2);
            recipe2Lure.AddTile(TileID.WorkBenches);
            recipe2Lure.SetResult(this, "DoubleLure");

            recipe2Loser = Recipe.Create();
            recipe2Loser.AddIngredient(ItemID.Cobweb, 10);
            recipe2Loser.AddIngredient(this, "BobLoser", 2);
            recipe2Loser.AddTile(TileID.WorkBenches);
            recipe2Loser.SetResult(this, "DoubleBobLoser");

            recipe4Lure = new EasyRecipe(this);
            recipe4Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe4Lure.AddIngredient(this, "DoubleLure", 2);
            recipe4Lure.AddTile(TileID.WorkBenches);
            recipe4Lure.SetResult(this, "QuadLure");

            recipe4LureHM = new HardRecipe(this);
            recipe4LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe4LureHM.AddIngredient(this, "DoubleLure", 2);
            recipe4LureHM.AddIngredient(this, "StarMix", 6);
            recipe4LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe4LureHM.SetResult(this, "QuadLure");

            recipe4Loser = new EasyRecipe(this);
            recipe4Loser.AddIngredient(ItemID.Cobweb, 10);
            recipe4Loser.AddIngredient(this, "DoubleBobLoser", 2);
            recipe4Loser.AddTile(TileID.WorkBenches);
            recipe4Loser.SetResult(this, "QuadBobLoser");

            recipe4LoserHM = new HardRecipe(this);
            recipe4LoserHM.AddIngredient(ItemID.Cobweb, 25);
            recipe4LoserHM.AddIngredient(this, "DoubleBobLoser", 2);
            recipe4LoserHM.AddIngredient(this, "StarMix", 6);
            recipe4LoserHM.AddTile(TileID.TinkerersWorkbench);
            recipe4LoserHM.SetResult(this, "QuadBobLoser");

            recipe8Lure = new EasyRecipe(this);
            recipe8Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe8Lure.AddIngredient(this, "QuadLure", 2);
            recipe8Lure.AddTile(TileID.TinkerersWorkbench);
            recipe8Lure.SetResult(this, "OctoLure");

            recipe8LureHM = new HardRecipe(this);
            recipe8LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe8LureHM.AddIngredient(this, "QuadLure", 2);
            recipe8LureHM.AddIngredient(this, "EnergyAmalgamate", 4);
            recipe8LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe8LureHM.SetResult(this, "OctoLure");
                       
            recipe16Lure = new EasyRecipe(this);
            recipe16Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe16Lure.AddRecipeGroup("UnuBattleRodsR:HMTier2Bars", 5);
            recipe16Lure.AddIngredient(this, "OctoLure", 2);
            recipe16Lure.AddTile(TileID.TinkerersWorkbench);
            recipe16Lure.SetResult(this, "BoxOfLures");

            recipe16LureHM = new HardRecipe(this);
            recipe16LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe16LureHM.AddIngredient(ItemID.LunarBar, 5);
            recipe16LureHM.AddIngredient(this, "OctoLure", 2);
            recipe16LureHM.AddIngredient(this, "EnergyAmalgamate", 8);
            recipe16LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe16LureHM.SetResult(this, "BoxOfLures");
            
            
            recipe32Lure = new EasyRecipe(this);
            recipe32Lure.AddIngredient(ItemID.Cobweb, 10);
            recipe32Lure.AddIngredient(ItemID.ChlorophyteBar, 5);
            recipe32Lure.AddIngredient(this, "BoxOfLures", 2);
            recipe32Lure.AddTile(TileID.TinkerersWorkbench);
            recipe32Lure.SetResult(this, "BoxOfCountlessLures");

            recipe32LureHM = new HardRecipe(this);
            recipe32LureHM.AddIngredient(ItemID.Cobweb, 25);
            recipe32LureHM.AddIngredient(this, "BoxOfLures", 2);
            recipe32LureHM.AddIngredient(this, "FractaliteBar", 12);
            recipe32LureHM.AddTile(TileID.TinkerersWorkbench);
            recipe32LureHM.SetResult(this, "BoxOfCountlessLures");        

            recipe1Lure.Register();
            recipe1Loser.Register();
            recipe2Lure.Register();
            recipe2Loser.Register();

            recipe4LureHM.Register();
            recipe4LoserHM.Register();
            recipe8LureHM.Register();
            recipe16LureHM.Register();
            recipe32LureHM.Register();
            
            recipe4Lure.Register();
            recipe4Loser.Register();
            recipe8Lure.Register();
            recipe16Lure.Register();
            recipe32Lure.Register();

            


            Recipe recipeConversion = Recipe.Create(this, "BobLoser");
            recipeConversion.AddIngredient(this, "ExtraLure");
            recipeConversion.Register();

            recipeConversion = Recipe.Create(this, "ExtraLure");
            recipeConversion.AddIngredient(this, "BobLoser");
            recipeConversion.Register();

            recipeConversion = Recipe.Create(this, "DoubleBobLoser");
            recipeConversion.AddIngredient(this, "DoubleLure");
            recipeConversion.Register();

            recipeConversion = Recipe.Create(this, "DoubleLure");
            recipeConversion.AddIngredient(this, "DoubleBobLoser");
            recipeConversion.Register();

            recipeConversion = Recipe.Create(this, "QuadBobLoser");
            recipeConversion.AddIngredient(this, "QuadLure");
            recipeConversion.Register();

            recipeConversion = Recipe.Create(this, "QuadLure");
            recipeConversion.AddIngredient(this, "QuadBobLoser");
            recipeConversion.Register();

        }

        Recipe selectiveBobbers;

        Recipe doubleSelectiveBobbers;
        Recipe doubleSelectiveBobbersHM;
        Recipe turretBobbers;
        Recipe turretBobbersHM;

     
       

        public void AddSelectiveRecipes()
        {
            selectiveBobbers = Recipe.Create();
            selectiveBobbers.AddIngredient(this, "ExtraLure", 2);
            selectiveBobbers.AddIngredient(ItemID.IronBar, 10);
            selectiveBobbers.AddIngredient(this, "Sandpaper");
            selectiveBobbers.anyIronBar = true;
            selectiveBobbers.AddTile(TileID.TinkerersWorkbench);
            selectiveBobbers.SetResult(this, "SelectiveBobbers");

            doubleSelectiveBobbers = new EasyRecipe(this);
            doubleSelectiveBobbers.AddIngredient(this, "DoubleLure", 2);
            doubleSelectiveBobbers.AddIngredient(this, "Sandpaper");
            doubleSelectiveBobbers.AddTile(TileID.TinkerersWorkbench);
            doubleSelectiveBobbers.SetResult(this, "DoubleSelectiveBobbers");

            doubleSelectiveBobbersHM = new HardRecipe(this);
            doubleSelectiveBobbersHM.AddIngredient(this, "SelectiveBobbers", 2);
            doubleSelectiveBobbersHM.AddIngredient(this, "Sandpaper", 5);
            doubleSelectiveBobbersHM.AddIngredient(ItemID.IronBar, 10);
            doubleSelectiveBobbersHM.AddIngredient(this, "LesserEnergyAmalgamate", 2);
            doubleSelectiveBobbersHM.anyIronBar = true;
            doubleSelectiveBobbersHM.AddTile(TileID.TinkerersWorkbench);
            doubleSelectiveBobbersHM.SetResult(this, "DoubleSelectiveBobbers");


            turretBobbers = new EasyRecipe(this);
            turretBobbers.AddIngredient(ItemID.ChlorophyteBar, 10);
            turretBobbers.AddIngredient(this, "Sandpaper");
            turretBobbers.AddTile(TileID.TinkerersWorkbench);
            turretBobbers.SetResult(this, "TurretBobbers");

            turretBobbersHM = new HardRecipe(this);
            turretBobbersHM.AddIngredient(ItemID.ChlorophyteBar, 20);
            turretBobbersHM.AddIngredient(this, "Sandpaper");
            turretBobbersHM.AddIngredient(this, "EnergyAmalgamate", 4);
            turretBobbersHM.AddTile(TileID.TinkerersWorkbench);
            turretBobbersHM.SetResult(this, "TurretBobbers");

            selectiveBobbers.Register();
            doubleSelectiveBobbers.Register();
            doubleSelectiveBobbersHM.Register();
            turretBobbers.Register();
            turretBobbersHM.Register();

        }*/

        public static void sendChat(string msg)
        {
            sendChat(msg, Color.White);
        }

        public static void debugChat(string msg)
        {
            sendChat(msg, Color.Yellow);
        }

        public static void sendChatToAll(string msg)
        {
            sendChatToAll(msg, Color.White);
        }

        public static void sendChat(string msg, Color c)
        {
            if (Main.netMode == 0 || Main.netMode == 1) // Single Player
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        Main.NewText(toSend[i], c.R, c.G, c.B);
                }
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public static void sendChatToAll(string msg, Color c)
        {
            if (Main.netMode == 2) // Server
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(toSend[i]), c);
                }

            }
            else if (Main.netMode == 1)
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(toSend[i]), c, Main.myPlayer);
                }
            }
            else if (Main.netMode == 0) // Single Player
            {
                string[] toSend = msg.Split('\n');
                for (int i = 0; i < toSend.Length; i++)
                {
                    toSend[i] = toSend[i].Trim();
                    if (toSend[i] != "")
                        Main.NewText(toSend[i], c.R, c.G, c.B);
                }
            }
        }
    }
}
