﻿using System.Collections.Generic;
using System.ComponentModel;
using TTController.Common.Plugin;

namespace TTController.Common
{
    public class PortConfig
    {
        public List<IPortModifier> ColorModifiers { get; internal set; } = new List<IPortModifier>();

        [DefaultValue("Unknown")] public string Name { get; internal set; } = "Unknown";
        [DefaultValue("Default")] public string DeviceType { get; internal set; } = "Default";
        [DefaultValue(false)] public bool IgnoreColorCache { get; internal set; } = false;
        [DefaultValue(false)] public bool IgnoreSpeedCache { get; internal set; } = false;

        public static readonly PortConfig Default = new PortConfig();
    }
}
