﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppKit;
using Foundation;

namespace Xamarin.Essentials
{
    public static partial class Sms
    {
        internal static bool IsComposeSupported
        {
            get
            {
                var can = false;
                var url = NSUrl.FromString("sms:");
                NSRunLoop.Main.InvokeOnMainThread(() => can = NSWorkspace.SharedWorkspace.UrlForApplication(url) != null);
                return can;
            }
        }

        static Task PlatformComposeAsync(SmsMessage message)
        {
            var firstNumber = message.Recipients?.FirstOrDefault();
            var uri = $"sms:{firstNumber}";
            if (!string.IsNullOrEmpty(message?.Body))
                uri += "&body=" + Uri.EscapeDataString(message.Body);

            var nsurl = NSUrl.FromString(uri);
            NSWorkspace.SharedWorkspace.OpenUrl(nsurl);
            return Task.CompletedTask;
        }
    }
}
