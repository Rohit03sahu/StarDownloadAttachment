using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadAttachmentConsole
{
    public class ConfigManager
    {
        static ConfigManager _instance = null;
               
        static IConfiguration _configuration { get; set; }

        public static ConfigManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConfigManager();

                return _instance;
            }
        }

        private ConfigManager()
        {
        }

        public static void Init(IConfiguration config)
        {
            _configuration = config;
        }


        public string GetStringValue(string key)
        {
            return _configuration["AppSettings:" + key];
        }

        public int GetIntValue(string key)
        {
            bool isParsable = int.TryParse(GetStringValue(key), out int returnValue);

            return returnValue;
        }

        public List<IConfigurationSection> GetSection(string key)
        {
            return _configuration.GetSection(key)
                                           .GetChildren()
                                           .ToList();
        }

        
    }
}
