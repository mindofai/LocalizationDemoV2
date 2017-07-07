using LocalizationDemo.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using LocalizationDemo.iOS.Services;
using Foundation;
using LocalizationDemo.Helpers;
using System.Threading;

[assembly: Dependency(typeof(Localize))]
namespace LocalizationDemo.iOS.Services
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];

                netLanguage = iOSToDotnetLanguage(pref);
            }

            CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    Console.WriteLine(netLanguage + " failed, trying " + fallback + " (" + e1.Message + ")");
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    Console.WriteLine(netLanguage + " couldn't be set, using 'en' (" + e2.Message + ")");
                    ci = new System.Globalization.CultureInfo("en");
                }
            }

            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        string iOSToDotnetLanguage(string iOSLanguage)
        {
            Console.WriteLine("iOS Language:" + iOSLanguage);
            var netLanguage = iOSLanguage;

            switch (iOSLanguage)
            {
                case "ms-MY":  
                case "ms-SG":
                    netLanguage = "ms"; 
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;
            }

            Console.WriteLine(".NET Language/Locale:" + netLanguage);
            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            Console.WriteLine(".NET Fallback Language:" + platCulture.LanguageCode);
            var netLanguage = platCulture.LanguageCode;

            switch (platCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
                    break;
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;
            }

            Console.WriteLine(".NET Fallback Language/Locale:" + netLanguage + " (application-specific)");
            return netLanguage;
        }
    }
}
