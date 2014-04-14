using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using InetRadioReceiver.Source.AppCore;
using log4net;

namespace InetRadioReceiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(App).ToString());
        private AppCore core = null;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
             log4net.Config.XmlConfigurator.Configure();
             logger.Info("app start");
             core = new AppCore(); 
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
             logger.Error(e.Exception);
             core.Dispose();
             System.Environment.Exit(0);
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
             logger.Info("app exit");
             logger.Info("shutdown");
        }
    }
}
