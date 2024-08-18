using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using UnuBattleRodsR.Configs;
using Terraria.ModLoader.Config;
using System.Collections.Generic;

namespace UnuBattleRodsR.Items.Currency
{
        public class FishSteaks : ModItem
        {
            public override void SetStaticDefaults()
            {
            // DisplayName.SetDefault("Fish Steaks");
            // Tooltip.SetDefault("Made from Fish, used as Currency.");
            Item.ResearchUnlockCount = 99;
        }

            public override void SetDefaults()
            {
                Item.width = 20;
                Item.height = 20;
                Item.value = Item.sellPrice(0,0,1,0);
                Item.rare = 1;
                Item.maxStack = 999;
            }

            public override void AddRecipes()
            {
                Dictionary<ItemDefinition,int> fishRecipes = ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes;
                foreach (ItemDefinition itm in ModContent.GetInstance<FishSteakRecipesConfig>().fishRecipes.Keys)
                {
                    Recipe recipe = CreateRecipe(fishRecipes[itm]);
                    recipe.AddIngredient(itm.Type);
                    recipe.Register();
                }
            }
        }
    }

    public class FishCurrency : CustomCurrencySingleCoin
    {

        public FishCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
        {
            this.CurrencyTextKey = CurrencyTextKey;
            CurrencyTextColor = Color.Salmon;
        }
    }