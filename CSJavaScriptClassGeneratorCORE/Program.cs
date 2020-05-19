using Ac4yClassModule.Class;
using Ac4yClassModule.Service;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace CSJavaScriptClassGeneratorCORE
{
    class Program
    {

        #region members

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string APPSETTINGS_OUTPUTPATH = "OUTPUTPATH";

        private const string APPSETTINGS_JAVASCRIPTOUTPUTPATH = "JAVASCRIPTOUTPUTPATH";
        private const string APPSETTINGS_TEMPLATEPATH = "TEMPLATEPATH";

        private const string APPSETTINGS_TEMPLATESUBPATH = "TEMPLATESUBPATH";

        private const string APPSETTINGS_JAVASCRIPTTEMPLATESUBPATH = "JAVASCRIPTTEMPLATESUBPATH";
        
        public IConfiguration Config { get; set; }

        #endregion members


        public Program(IConfiguration config)
        {

            Config = config;

        } // Program

        public void Run()
        {

            Ac4yClass ac4yClass = new Ac4yClassHandler().GetAc4yClassFromType(typeof(CSEFTPC4Core3Objects.Ac4yObjects.Ac4yPersistentChild));

            new JavaScriptClassGenerator()
            {
                TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                ,
                TemplateSubPath = Config[APPSETTINGS_JAVASCRIPTTEMPLATESUBPATH]
                ,
                OutputPath = Config[APPSETTINGS_JAVASCRIPTOUTPUTPATH]
            }
                .Generate(ac4yClass);


        } // run

        static void Main(string[] args)
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                IConfiguration config = null;

                config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();
                
                new Program(config).Run();

            }
            catch (Exception exception)
            {

                log.Error(exception.Message);
                log.Error(exception.StackTrace);

                Console.ReadLine();

            }

        } // Main
    }
}
