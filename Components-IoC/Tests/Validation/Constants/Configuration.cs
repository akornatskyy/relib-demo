using System;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

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