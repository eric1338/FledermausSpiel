using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class Konfiguration
    {
        public static readonly int ROUND_VALUE = 5;
    

        public static float Round(float value)
        {
            return (float)Math.Round(value, Konfiguration.ROUND_VALUE);
        }
    }
}
