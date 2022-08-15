using Contracts;
using RimMod;
using System;
using System.Threading;
using Verse;

namespace RimSpectorMod
{
    internal class RimSpectorComponent : GameComponent
    {
        private DebugLogger _debugLogger;
        private PayloadBuilder _payloadBuilder;
        private DebugFileDumper _debugFileDumper;
        private EndpointBuilder _endpointBuilder;
        private WebHelper _webHelper;
        private EventlessTimer _timer;
        private bool _initlialized = false;

        public RimSpectorComponent() : base() { }

        public RimSpectorComponent(Game game) : this() { }

        public override void FinalizeInit()
        {
            Initialize();
            _debugLogger.Log($"[RimSpector]: init completed");

            UpdateData();
        }

        private void Initialize()
        {
            try
            {
                var serializerProvider = new SerializerProvider();
                _debugLogger = new DebugLogger(RimSpectorGadget.Settings);
                _debugFileDumper = new DebugFileDumper(RimSpectorGadget.Settings, serializerProvider);
                _endpointBuilder = new EndpointBuilder(RimSpectorGadget.Settings);
                _webHelper = new WebHelper(RimSpectorGadget.Settings, _endpointBuilder, _debugLogger, serializerProvider);
                _payloadBuilder = new PayloadBuilder(RimSpectorGadget.Settings);
                _timer = new EventlessTimer(Math.Max(RimSpectorGadget.Settings._updateInterval * 1000, 10_000))
                {
                    Enabled = true
                };
                _initlialized = true;
                _debugLogger.Log("[RimSpector]: Component Initialized");

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                Log.Error(ex.InnerException.Message);
                Log.Error(ex.InnerException.StackTrace);
                throw;
            }
        }

        public override void GameComponentUpdate()
        {
            base.GameComponentUpdate();

            if (!_initlialized)
                return;

            if (!_timer.IsElapsed)
                return;

            _debugLogger.Log($"[RimSpector]: updating payload");
            _timer.Reset();

            UpdateData();
        }

        public void UpdateData()
        {
            var payload = _payloadBuilder.Build();
            var thread = new Thread(PostAndDumpPayload);
            thread.Start(payload);
        }

        private void PostAndDumpPayload(object data)
        {
            Payload payload = data as Payload;
            if (payload is null)
                return;

            _webHelper.Post(payload);
            _debugFileDumper.DumpIfDebug(payload);
        }
    }
}
