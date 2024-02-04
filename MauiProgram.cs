﻿using CommunityToolkit.Mvvm.Messaging;
using MauiCommunityToolkitMvvmSample.Notes;
using Microsoft.Extensions.Logging;

namespace MauiCommunityToolkitMvvmSample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AutoRegister();
            builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
