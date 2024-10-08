﻿using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace UnuBattleRodsR.Configs
{
    public class FishSteakRecipesConfig : ModConfig
    {
        public FishSteakRecipesConfig()
        {
            fishRecipes = new Dictionary<ItemDefinition, int>();
            fishRecipes[new ItemDefinition(ItemID.Tuna)] = 6;
            fishRecipes[new ItemDefinition(ItemID.Salmon)] = 4;
            fishRecipes[new ItemDefinition(ItemID.Bass)] = 2;
            fishRecipes[new ItemDefinition(ItemID.AtlanticCod)] = 3;
            fishRecipes[new ItemDefinition(ItemID.Trout)] = 2;
            fishRecipes[new ItemDefinition(ItemID.RedSnapper)] = 5;
            fishRecipes[new ItemDefinition(ItemID.DoubleCod)] = 6;
            fishRecipes[new ItemDefinition(ItemID.GoldenCarp)] = 25;

            fishRecipesNotAuto = new List<ItemDefinition>();

            fishRecipesNotAuto.Add(new ItemDefinition(ItemID.GoldenCarp));
        }
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Fish Steak Value")]
        [Tooltip("List of items that can be converted into Fish Steaks. Changing it requires reload.")]
        [ReloadRequired]
        public Dictionary<ItemDefinition, int> fishRecipes = new Dictionary<ItemDefinition, int>();

        [Label("Fish that the Slicer won't cut")]
        [Tooltip("List of items that can't be converted into Fish Steaks automatically using the Fish Slicer or Killing Gate. Changing it does not require reload.")]
        public List<ItemDefinition> fishRecipesNotAuto = new List<ItemDefinition>();

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            return false;
        }
    }
}
