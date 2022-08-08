using RimSpectorMod;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using UnityEngine;
using Verse;

namespace RimMod
{
    public class RimSpectorMod : Mod
    {
        private BackgroundWorker _worker;
        private DebugLogger _debugLogger;
        private DebugFileDumper _debugFileDumper;
        private EndpointBuilder _endpointBuilder;
        private PayloadBuilder _payloadBuilder;
        private WebHelper _webHelper;

        public Settings Settings { get; }

        public RimSpectorMod(ModContentPack content) : base(content)
        {
            Log.Message("RimSpector initialized");

            Settings = GetSettings<Settings>();
            Settings.Write();

            var serializerProvider = new SerializerProvider();
            _debugLogger = new DebugLogger(Settings);

            _debugFileDumper = new DebugFileDumper(Settings, serializerProvider);
            _endpointBuilder = new EndpointBuilder(Settings);
            _payloadBuilder = new PayloadBuilder(Settings);
            _webHelper = new WebHelper(Settings, _endpointBuilder, _debugLogger, serializerProvider);

            Log.Message($"RimSpector: your endpoint {_endpointBuilder.ConfiguredApiEndpoint}");

            _debugLogger.Log("RimSpector: running in debug mode.");
            _debugLogger.Log($"RimSpector: debug dump will be written to {Settings._debugDumpFolder}");

            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.WorkerReportsProgress = false;
            _worker.WorkerSupportsCancellation = false;
            _worker.RunWorkerAsync();
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _debugLogger.Log($"RimSpector: Starting loop with interval {Settings._updateInterval}");

            while (true)
            {
                Log.Error($"looping");
                //TODO Check resilience. There are some null reference exceptions that cause this to break
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(Settings._updateInterval));

                    var payload = _payloadBuilder.Build();

                    _webHelper.Post(payload);
                    _debugFileDumper.DumpIfDebug(payload);
                }
                catch (Exception ex)
                {
                    Log.Error($"Error caught in {nameof(_worker_DoWork)}");
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    Log.Error(ex.InnerException.Message);
                }
            }
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
            listingStandard.Label($"Your export url is {_endpointBuilder.ConfiguredSiteEndpoint}");
            if (listingStandard.ButtonText("Copy to clipboard"))
            {
                _debugLogger.Log("button true");
                GUIUtility.systemCopyBuffer = _endpointBuilder.ConfiguredSiteEndpoint;
            }

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);

            Settings.Write();
        }
    }
}
