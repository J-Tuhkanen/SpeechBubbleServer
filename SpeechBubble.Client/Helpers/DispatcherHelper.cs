﻿using System;
using System.Windows.Threading;

namespace SpeechBubble.Client.Helpers
{
    public static class DispatcherHelper
    {
        public static Dispatcher? UIDispatcher { get; private set; }

        public static void Initialize()
        {
            if (UIDispatcher?.Thread.IsAlive ?? false)
                return;

            UIDispatcher = Dispatcher.CurrentDispatcher;
        }

        public static DispatcherOperation RunAsync(Action action)
        {
            CheckDispatcher();

            return UIDispatcher.BeginInvoke(action);
        }

        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (action == null)
                return;

            CheckDispatcher();

            if (UIDispatcher.CheckAccess())
                action();
            else
                UIDispatcher.BeginInvoke(action);
        }

        private static void CheckDispatcher()
        {
            if (UIDispatcher == null)
            {
                throw new InvalidOperationException("The DispatcherHelper is not initialized.");
            }
        }
    }
}
