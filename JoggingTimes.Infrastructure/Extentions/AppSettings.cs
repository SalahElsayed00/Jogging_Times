//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JoggingTimes.Infrastructure.Extentions
//{
//    public class AppSettings
//    {
//        private static AppSettings _appSettings;
//        public ConnectionStrings ConnectionStrings { get; set; }

//        public static AppSettings Current
//        {
//            get
//            {
//                if (_appSettings == null)
//                {
//                    _appSettings = GetCurrentSettings();
//                }

//                return _appSettings;
//            }
//        }

//        public AppSettings(IConfiguration config)
//        {
//            this.ConnectionStrings = config.GetSection("ConnectionStrings").Get<ConnectionStrings>();
//        }

//        public static AppSettings GetCurrentSettings()
//        {
//            var builder = new ConfigurationBuilder()
//                            .SetBasePath(Directory.GetCurrentDirectory())
//                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//            IConfigurationRoot configuration = builder.Build();

//            var settings = new AppSettings(configuration);

//            return settings;
//        }
//    }

//    public class ConnectionStrings
//    {
//        public string DBConection { get; set; }
//    }
//}
