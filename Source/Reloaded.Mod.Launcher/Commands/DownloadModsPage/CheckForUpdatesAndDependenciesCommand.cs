﻿using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Reloaded.Mod.Launcher.Commands.Templates;
using Reloaded.WPF.Utilities;
using MessageBox = Reloaded.Mod.Launcher.Pages.Dialogs.MessageBox;

namespace Reloaded.Mod.Launcher.Commands.DownloadModsPage
{
    public class CheckForUpdatesAndDependenciesCommand : WithCanExecuteChanged, ICommand
    {
        private XamlResource<string> _noUpdateDialogTitle   = new XamlResource<string>("NoUpdateDialogTitle");
        private XamlResource<string> _noUpdateDialogMessage = new XamlResource<string>("NoUpdateDialogMessage");
        private bool _canExecute = true;

        /* ICommand. */

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            _canExecute = false;
            RaiseCanExecute(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            var updates      = await Task.Run(Update.CheckForModUpdatesAsync);
            var dependencies = Update.CheckMissingDependencies(out var missingDependencies);

            if ( (!updates) && (!dependencies) )
            {
                var box = new MessageBox(_noUpdateDialogTitle.Get(), _noUpdateDialogMessage.Get());
                box.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                box.ShowDialog();
            }
            else if (dependencies)
            {
                try
                {
                    await Update.DownloadPackagesAsync(missingDependencies, false, false);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            _canExecute = true;
            RaiseCanExecute(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
