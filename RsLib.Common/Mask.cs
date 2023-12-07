using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsLib.Common
{
    public static class Mask
    {
        public static double[,] Mask3x3 =
        {
            { 1.0,1.0,1.0},
            { 1.0,1.0,1.0},
            { 1.0,1.0,1.0}
        };
        public static double[,] Mask5x5 =
        {
            { 1.0,1.0,1.0,1.0,1.0},
            {1.0, 1.0, 1.0, 1.0, 1.0},
            { 1.0,1.0,1.0,1.0,1.0},
            { 1.0,1.0,1.0,1.0,1.0},
            { 1.0,1.0,1.0,1.0,1.0}
        };

    }
}
