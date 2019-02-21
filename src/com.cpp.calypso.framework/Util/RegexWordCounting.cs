using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.cpp.calypso.framework
{
    public class RegexWordCounting : IWordCounting
    {

        public int CountWords(string text)
        {
            var regex = new Regex(@"\w+", RegexOptions.Compiled);
            return regex.Matches(text).Count;
        }
    }
}
