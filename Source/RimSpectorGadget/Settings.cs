using System;
using Verse;

namespace RimMod
{
    public class Settings : ModSettings
    {
        public bool _debug = false;
        public string _debugDumpFolder = "C:/temp/payload.json";
        public string _privateKey = Guid.NewGuid().ToString();
        public string _id = Guid.NewGuid().ToString();
        public int _updateInterval = 30;
        public string _baseUrl = "https://rimspector.azurewebsites.net";
        public string _apiPath = "/api/v1/RimSpectorData";

        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _debug, "debug", forceSave: true);
            Scribe_Values.Look(ref _debugDumpFolder, "debugDumpFolder", forceSave: true);
            Scribe_Values.Look(ref _privateKey, "privateKey", forceSave: true);
            Scribe_Values.Look(ref _id, "id", forceSave: true);
            //Scribe_Values.Look(ref _updateInterval, "updateInterval");
            //Scribe_Values.Look(ref _baseUrl, "baseUrl");
            //Scribe_Values.Look(ref _apiPath, "apiPath");
        }
    }
}
