using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Serilog;
using Serilog.Core;

namespace FridgeLynxie.Android.Utility
{
    public static class LocalLogger
    {
        public static void Init()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.AndroidLog()
            .Enrich.WithProperty(Constants.SourceContextPropertyName, "FridgeLynxieLog") //Sets the Tag field.
            .CreateLogger();
        }

        public static void Dispose()
        {
            Log.CloseAndFlush();
            //Log.Dispose();
        }

        public static void WriteInfo (string message)
        {
            Log.Information(message);
        }

        public static void WriteError(string message, Exception e)
        {
            Log.Error(e, message);
        }
    }
}