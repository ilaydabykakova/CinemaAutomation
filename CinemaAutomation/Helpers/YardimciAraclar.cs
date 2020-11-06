using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CinemaAutomation.Helpers
{
    public class YardimciAraclar
    {
        public static string RastgeleKarakter(string PATTERN)
        {
            Random R = new Random();
            string Karakterler = "ABCDEFGHIJKLMNOPRSTUVYZWQX0123456789";
            MatchEvaluator Rastgele = delegate (Match m) { return Karakterler[R.Next(Karakterler.Length)].ToString(); };
            return Regex.Replace(PATTERN, "X", Rastgele);
        }
    }
}