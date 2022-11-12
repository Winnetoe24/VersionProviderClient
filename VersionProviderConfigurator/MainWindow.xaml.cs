// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Search;
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace VersionProviderConfigurator
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {

            this.InitializeComponent();
            GetClientConfigurationsAsync();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        public ObservableCollection<ClientConfigurationInfo> ClientConfigurations { get; private set; } = new ObservableCollection<ClientConfigurationInfo>(); 


        private async Task GetClientConfigurationsAsync()
        {
            System.Diagnostics.Debug.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().Location);
           // File.Create("./data.txt").Close();
            StorageFolder appInstalledFolder = Package.Current.InstalledLocation;
            System.Diagnostics.Debug.WriteLine($"{appInstalledFolder}");
            appInstalledFolder.CreateFileAsync("./data.txt").Close();
            StorageFolder configurationFolder = await appInstalledFolder.GetFolderAsync("data\\client");

            var result = configurationFolder.CreateFileQueryWithOptions(new QueryOptions());

            IReadOnlyList<StorageFile> imageFiles = await result.GetFilesAsync();
            foreach (StorageFile file in imageFiles)
            {
                ClientConfigurations.Add(await LoadClientConfigurationInfo(file));
            }

        }

        public async Task<ClientConfigurationInfo> LoadClientConfigurationInfo(StorageFile file) { 
        
            ClientConfigurationInfo obj = JsonSerializer.Deserialize<ClientConfigurationInfo>(File.ReadAllText(file.Path));

            return obj;
        }
    }
}
