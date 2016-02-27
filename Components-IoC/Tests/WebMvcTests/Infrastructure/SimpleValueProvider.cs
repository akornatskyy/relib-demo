using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Public.WebMvcTests.Infrastructure
{
    public sealed class SimpleValueProvider : Dictionary<string, object>, IValueProvider
    {
        private readonly CultureInfo culture;

        public SimpleValueProvider()
            : this(null)
        {
        }

        public SimpleValueProvider(CultureInfo culture)
            : base(StringComparer.OrdinalIgnoreCase)
        {
            this.culture = culture ?? CultureInfo.InvariantCulture;
        }

        // copied from ValueProviderUtil
        public bool ContainsPrefix(string prefix)
        {
            foreach (string key in Keys)
            {
                if (key != null)
                {
                    if (prefix.Length == 0)
                    {
                        return true; // shortcut - non-null key matches empty prefix
                    }

                    if (key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        if (key.Length == prefix.Length)
                        {
                            return true; // exact match
                        }
                        
                        switch (key[prefix.Length])
                        {
                            case '.': // known separator characters
                            case '[':
                                return true;
                        }
                    }
                }
            }

            return false; // nothing found
        }

        public ValueProviderResult GetValue(string key)
        {
            object rawValue;
            if (!TryGetValue(key, out rawValue))
            {
                return null;
            }

            return new ValueProviderResult(rawValue, Convert.ToString(rawValue, culture), culture);
        }
    }
}