using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace RimMod
{
    public class Settings : ModSettings
    {
        public string _privateKey = Guid.NewGuid().ToString();
        public string _id = Guid.NewGuid().ToString();
        public int _updateInterval = 30;
        public string _baseUrl = "https://localhost:7246";

        /// <summary>
        /// The part that writes our settings to file. Note that saving is by ref.
        /// </summary>
        public override void ExposeData()
        {
            
            base.ExposeData();
            Scribe_Values.Look(ref _privateKey, "privateKey", Guid.NewGuid().ToString(), true);
            Scribe_Values.Look(ref _id, "id", Guid.NewGuid().ToString(), true);
            Scribe_Values.Look(ref _updateInterval, "updateInterval", 30);
            Scribe_Values.Look(ref _baseUrl, "baseUrl", "https://localhost:7246");            
        }
    }
}
