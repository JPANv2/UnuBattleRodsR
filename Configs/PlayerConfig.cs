using System.Collections.Generic;
using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace UnuBattleRodsR.Configs
{
    public class UnuPlayerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Start with Wooden Battlerod")]
        [DefaultValue(true)]
        public bool startWithRod;

        [Label("Start with Poison Bait")]
        [DefaultValue(true)]
        public bool startWithBait;
    }
    public enum AmmoMode
    {
        NoAmmo,
        Old,
        AmmoFirst,
        DedicatedFirst,
        DedicatedOnly
    }
    public enum Difficulties
    {
        Vanilla,
        Calamity,
        Battlerods
    }

    public enum TensionMode
    {
        TensionOnly,
        TensionOnBosses,
        NoTension
    }

    public class UnuDificultyConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Balance")]
        [Tooltip("If this mod should be balanced around the Vanilla Terraria Expert Mode experience, or stick to the original (un)balanced Battlerods.")]
        [DefaultValue(Difficulties.Vanilla)]
        public Difficulties difficulty = Difficulties.Vanilla;

        [Label("Tension only")]
        [Tooltip("If Battlerods only do damage if there is tension on the wire, or always, or only tension on Bosses, and no tension on regular enemies. Defaults to No Tension Needed")]
        [DefaultValue(TensionMode.TensionOnBosses)]
        public TensionMode tensionMode = TensionMode.TensionOnBosses;

        [Label("Rod Ammo Mode")]
        [Tooltip("If you should use no Rod Ammo (Baits, Discardables, Turrets or Options), the old system (only on Ammo slot), use both Ammo and Rod Pouch, with ammo being picked first or the Rod Pouch picked first, or use the Rod pouch exclusively. Defaults to Rod Pouch First.")]
        [DefaultValue(AmmoMode.DedicatedFirst)]
        public AmmoMode ammoMode = AmmoMode.DedicatedFirst;

        [Label("Old Escalation")]
        [Tooltip("If Battlerods should use the old (fixed increase) escalation code, or the new (tension modifier) one. Defaults to false")]
        [DefaultValue(false)]
        public bool oldEscalation = false;

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            return false;
        }
    }

    public class UnuServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Allow rod prefixes")]
        [Tooltip("If this mod should roll prefixes for the rods. Disable it for a less easy time.")]
        [DefaultValue(true)]
        public bool allowPrefixes = true;

        [Label("Rod Tension Bonus")]
        [Tooltip("If rods from this mod will benefit from the Rod Tension Bonuses.")]
        [DefaultValue(true)]
        public bool rodBonus = true;

        [Label("Rod Tension Bonus - Damage")]
        [Tooltip("If this mod has Rod Tension bonus, if it multipies damage.")]
        [DefaultValue(false)]
        public bool rodDamageBonus = false;

        [Label("Rod Tension Bonus - Bob Speed")]
        [Tooltip("If this mod has Rod Tension bonus, if it reduces Bob speed.")]
        [DefaultValue(true)]
        public bool rodBobbingBonus = true;

        [Label("Slightly Random Bobs")]
        [Tooltip("If bobbers should bob at a fixed interval (false) or can vary between 1/2 and 3/2 times the base bob time")]
        [DefaultValue(true)]
        public bool randomizedBobs = true;

       /* [Label("Allow borrowing Damage stars from other classes")]
        [Tooltip("If this mod should use the old system of having the max stat of the vanilla classes count towards Fishing Damage.")]
        [DefaultValue(false)]
        public bool allowBorrowDamage = false;
       */
        [Label("Allow this mod's fished items")]
        [Tooltip("If the crates added by this mod should appear at all when fishing.")]
        [DefaultValue(true)]
        public bool allowFishedItems = true;

        [Label("Bobbers always break")]
        [Tooltip("If bobbers should never fall from the target when detatching.")]
        [DefaultValue(false)]
        public bool dontFallOnFloor = false;

        [Label("Explosives damage everyone")]
        [Tooltip("Should Grenade and Dynamite Bobbers damage everyone (including the player that triggered it), or not.")]
        [DefaultValue(true)]
        public bool explosivesDamageEveryone = true;

        [Label("Items that should not be sold")]
        [Tooltip("Items that the Sell Gate and Killing Gate will not replace with coins.")]
        public List<ItemDefinition> noSellItems = new List<ItemDefinition>();

        [Label("Items that should always be sold")]
        [Tooltip("Items that the Sell Gate and Killing Gate will replace with coins even if their max stack is not 1.\n\nWarning:Anything added here will take precedence to any other replacing, so if all fish from the replace option are here, no crates will appear.\nSame with Fish Steaks.")]
        public List<ItemDefinition> forceSellItems = new List<ItemDefinition>();

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
        {
            return false;
        }

    }
}
