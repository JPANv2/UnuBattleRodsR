using System.Collections.Generic;
using System.ComponentModel;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace UnuBattleRodsR.Configs
{
    public class FishToReplaceConfig : ModConfig
    {
        public FishToReplaceConfig()
        {
            fishToReplace = new List<ItemDefinition>
            {
                new ItemDefinition(ItemID.WoodenCrate),
                new ItemDefinition(ItemID.WoodenCrateHard),
                new ItemDefinition(ItemID.Bass),
                new ItemDefinition(ItemID.Salmon),
                new ItemDefinition(ItemID.Trout),
                new ItemDefinition(ItemID.Tuna),
                new ItemDefinition(ItemID.Shrimp),
                new ItemDefinition(ItemID.AtlanticCod),
                new ItemDefinition(ItemID.NeonTetra),
                new ItemDefinition(ItemID.RedSnapper),
                new ItemDefinition(ItemID.Honeyfin),
                new ItemDefinition(ItemID.Obsidifish),
                new ItemDefinition(ItemID.Flounder),
                new ItemDefinition(ItemID.RockLobster)
            };
        }
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Replace all fish")]
        [Tooltip("This mod will be able to replace any fished items with its own. If set, the below list is ignored.")]
        [DefaultValue(false)]
        public bool replaceAllFish = false;

        [Label("Fish to Replace")]
        [Tooltip("List of fished items that can be replaced with this mod's items. Changing it does not require reload.")]
        public List<ItemDefinition> fishToReplace;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            return false;
        }
    }
}
