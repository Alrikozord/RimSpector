using RimSpectorMod;
using UnityEngine;
using Verse;

namespace RimMod
{
    public class RimSpectorGadget : Mod
    {
        private DebugLogger _debugLogger;
        private EndpointBuilder _endpointBuilder;

        public static Settings Settings { get; private set; }

        public RimSpectorGadget(ModContentPack content) : base(content)
        {
            Log.Message("[RimSpector] initialized");

            Settings = GetSettings<Settings>();
            Settings.Write();

            _debugLogger = new DebugLogger(Settings);
            _endpointBuilder = new EndpointBuilder(Settings);

            _debugLogger.Log("[RimSpector]: running in debug mode.");
            _debugLogger.Log($"[RimSpector]: debug dump will be written to {Settings._debugDumpFolder}");
        }

        /// <summary>
        /// Override SettingsCategory to show up in the list of settings.
        /// Using .Translate() is optional, but does allow for localisation.
        /// </summary>
        /// <returns>The (translated) mod name.</returns>
        public override string SettingsCategory() => "RimSpector";

        /// <summary>
        /// The (optional) GUI part to set your settings.
        /// </summary>
        /// <param name="inRect">A Unity Rect with the size of the settings window.</param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label($"Find your data at {_endpointBuilder.ConfiguredSiteEndpoint}");
            if (listingStandard.ButtonText("Copy to clipboard"))
            {
                GUIUtility.systemCopyBuffer = _endpointBuilder.ConfiguredSiteEndpoint;
            }

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);

            Settings.Write();
        }
    }
}
