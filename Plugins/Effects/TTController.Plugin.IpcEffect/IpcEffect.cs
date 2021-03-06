﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TTController.Common;
using TTController.Common.Plugin;

namespace TTController.Plugin.IpcEffect
{
    public class IpcEffectConfig : EffectConfigBase
    {
        [DefaultValue(null)] public string IpcName { get; internal set; } = null;
    }

    public class IpcEffect : IpcEffectBase<IpcEffectConfig>
    {
        private readonly Dictionary<PortIdentifier, List<LedColor>> _colorMap;

        public override string IpcName => Config.IpcName;
        public override string EffectType => "PerLed";

        public IpcEffect(IpcEffectConfig config) : base(config)
        {
            _colorMap = new Dictionary<PortIdentifier, List<LedColor>>();
        }

        protected override void OnDataReceived(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return;

            try
            {
                var document = JArray.Parse(data);
                foreach (var child in document.Children())
                {
                    var port = child["Port"].ToObject<PortIdentifier>();
                    _colorMap[port] = child["Colors"].ToObject<List<LedColor>>();
                }
            }
            catch (JsonReaderException) { }
        }

        public override IDictionary<PortIdentifier, List<LedColor>> GenerateColors(List<PortIdentifier> ports, ICacheProvider cache)
            => ports.ToDictionary(p => p, p => _colorMap.ContainsKey(p) ? _colorMap[p].ToList() : null);

        public override List<LedColor> GenerateColors(int count, ICacheProvider cache)
            => throw new NotImplementedException();
    }
}
