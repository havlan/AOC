﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public static class Utils
    {
        public static int IntPow(int x, uint pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        public static long IntPow(long x, uint pow)
        {
            long ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        /// <summary>
        /// Compare two strings and return the index of the first difference.  Return -1 if the strings are equal.
        /// </summary>
        public static int DiffersAtIndex(string s1, string s2)
        {
            int index = 0;
            int min = Math.Min(s1.Length, s2.Length);
            while (index < min && s1[index] == s2[index])
                index++;

            return (index == min && s1.Length == s2.Length) ? -1 : index;
        }
    }
}
