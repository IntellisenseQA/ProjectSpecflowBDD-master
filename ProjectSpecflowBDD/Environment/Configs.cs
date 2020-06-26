using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ProjectBDDRegsitrationProcess.Environment
{
    internal static class ConfigurationHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();
            return config;
        }
    }
}
