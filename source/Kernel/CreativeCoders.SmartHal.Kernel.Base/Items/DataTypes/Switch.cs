﻿using System;

namespace CreativeCoders.SmartHal.Kernel.Base.Items.DataTypes
{
    public static class Switch
    {
        public static SwitchValue On { get; } = new SwitchValue(1, x => x > 0.0001);

        public static SwitchValue Off { get; } = new SwitchValue(0, x => Math.Abs(x) < 0.0001);
    }
}