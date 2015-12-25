using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.IO;

namespace Public.ValidationTests.Constants
{
    public static class Configuration
    {
        public static IConfigurationSource Source { get; private set; }

        static Configuration()
        {
            Source = new FileConfigurationSource(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Source\Public\WebMvc\Web.config"));
        }
    }
}
