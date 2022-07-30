using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;
using System.IO;
using Verse.AI;
using Contracts;
using RimSpectorMod;
using RimSpectorMod.Mapper;
using System.Runtime.Serialization.Json;
using System.Globalization;
using System.Threading;

namespace RimMod
{
    public class RimSpectorMod : Mod
    {
        private BackgroundWorker _worker;
        private DataContractJsonSerializer _serializer;

        public Settings Settings { get; }


        public RimSpectorMod(ModContentPack content) : base(content)
        {
            Log.Message("RimSpector initialized");

            Settings = GetSettings<Settings>();
            Log.Message($"RimSpector: loaded Settings {Settings}");
            Log.Message($"RimSpector: _id {Settings._id}");
            Log.Message($"RimSpector: _baseUrl {Settings._baseUrl}");
            Log.Message($"RimSpector: _updateInterval {Settings._updateInterval}s");

            _serializer = new DataContractJsonSerializer(typeof(Payload), new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss"),
                IgnoreExtensionDataObject = true,
                KnownTypes = new[]
                {
                    typeof(Payload),
                    typeof(IdeoPayload),
                    typeof(RolePayload),
                    typeof(PreceptPayload),
                    typeof(PawnPayload),
                    typeof(SkillPayload),
                    typeof(Contracts.Passion),
                    typeof(ThingPayload),
                    typeof(AgePayload),
                    typeof(TraitPayload),
                    typeof(BackstoryPayload),
                    typeof(HealthPayload),
                    typeof(PsycastPayload),
                }
            });

            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_DoWork;
            _worker.WorkerReportsProgress = false;
            _worker.WorkerSupportsCancellation = false;
            _worker.RunWorkerAsync();
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Log.Message($"RimSpector: Starting loop with interval {Settings._updateInterval}");

            while (true)
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromSeconds(Settings._updateInterval));

                    var payload = GetFilledPayload();

                    using (var stream = new MemoryStream())
                    {
                        _serializer.WriteObject(stream, payload);

                        var json = Encoding.Default.GetString(stream.ToArray());

                        File.WriteAllText("C:/temp/payload.json", json);
                        Log.Message($"RimSpector: written payload");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex?.InnerException?.ToString());
                    Log.Error($"Error during {nameof(_worker_DoWork)}");
                }
            }
        }

        private Payload GetFilledPayload()
        {
            try
            {
                var pawns = PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists;

                var ideos = pawns?
                    .GroupBy(p => p.Ideo)
                    .OrderByDescending(p => p.Count())
                    .Select(p => p.Key)
                    .ToList();

                var payload = new Payload();
                payload.Timestamp = DateTime.UtcNow;
                payload.Id = Guid.Parse(Settings?._id);

                if (pawns?.Any() ?? false)
                {
                    try
                    {
                        payload.Pawns = PawnMapper.Map(pawns).ToList();
                        payload.Ideos = IdeologyMapper.Map(ideos).ToList();
                        //payload.History = GetHistoryPayload();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error during {nameof(GetFilledPayload)}");
                        Log.Error(ex.Message);

                        throw;
                    }
                }
                return payload;
            }
            catch (Exception ex)
            {
                Log.Error(ex?.ToString());

                throw;
            }

        }

        //private IEnumerable<IdeoPayload> GetIdeologyPayload(IEnumerable<Pawn> pawns)
        //{
        //    var relevantIdeos = pawns?
        //                               .GroupBy(p => p.Ideo)
        //                               .OrderByDescending(p => p.Count())
        //                               .Select(p => p.Key);
        //    return _mapper.Map<IEnumerable<IdeoPayload>>(relevantIdeos);
        //}
        //private IEnumerable<HistoryPayload> GetHistoryPayload()
        //{
        //    throw new NotImplementedException();

        //    var historyRecorder = new HistoryAutoRecorderGroup();
        //    historyRecorder.ExposeData();

        //    return _mapper.Map<IEnumerable<HistoryPayload>>(historyRecorder?.recorders);
        //}

        /// <summary>
        /// Override SettingsCategory to show up in the list of settings.
        /// Using .Translate() is optional, but does allow for localisation.
        /// </summary>
        /// <returns>The (translated) mod name.</returns>
        public override string SettingsCategory() => "RimSpector";

        public override void WriteSettings()
        {
            base.WriteSettings();
        }

        /// <summary>
        /// The (optional) GUI part to set your settings.
        /// </summary>
        /// <param name="inRect">A Unity Rect with the size of the settings window.</param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label($"Your export url is {Settings._baseUrl}/{Settings._id}");
            listingStandard.ButtonText("Copy to clipboard");
            string buffer = string.Empty;
            listingStandard.TextFieldNumericLabeled<int>("Update interval (in sec)", ref Settings._updateInterval, ref buffer, 30, int.MaxValue);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);

            Settings.Write();
        }
    }
}
