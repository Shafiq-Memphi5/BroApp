using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BroApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int count = 1;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void newTab(object sender, RoutedEventArgs e)
        {
            count++;
            PivotItem newTab = new PivotItem
            {
                Header = $"Tab {count} - Shafiq",
                
            };
            WebView newWeb = new WebView
            {
                Source = new Uri("https://bing.com"),
                Height = 800
            };
            StackPanel newPanel = new StackPanel();
            TextBlock newText = new TextBlock
            {
                Text = "Shafiq",
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10)
            };
            newPanel.Children.Add(newText);
            newPanel.Children.Add(newWeb);
            newTab.Content = newPanel;
            Tab.Items.Add(newTab);
            Tab.SelectedItem = newTab;

            UpdatePreview();
            UpdateTabHeaders();
        }

        private async void UpdatePreview()
        {
            MainThumbnail.Children.Clear();

            foreach (PivotItem newTab in Tab.Items)
            {
                if (newTab.Content is WebView newWeb)
                {
                    var stream = new InMemoryRandomAccessStream();

                    await newWeb.CapturePreviewToStreamAsync(stream);

                    BitmapImage bitmapImage = new BitmapImage();

                    stream.Seek(0);

                    await bitmapImage.SetSourceAsync(stream);

                    Image capturedImage = new Image
                    {
                        Source = bitmapImage,
                        Width = 100,
                        Height = 60,
                        Margin = new Thickness(7)
                    };

                    MainThumbnail.Children.Add(capturedImage);
                }
            }
        }

        private void backBtn(object sender, RoutedEventArgs e)
        {
            if(Tab.SelectedIndex > 0)
            {
                Tab.SelectedIndex--;
            }
        }

        private void nextBtn(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex >= 0)
            {
                Tab.SelectedIndex++;
            }
        }

        private void closeBtn(object sender, RoutedEventArgs e)
        {
            if(Tab.SelectedIndex >= 0)
            {
                Tab.Items.RemoveAt(Tab.SelectedIndex);
                UpdateTabHeaders();
            }
        }

        private void UpdateTabHeaders()
        {
            for (int i = 0; i < Tab.Items.Count; i++)
            {
                var pivotItem = (PivotItem)Tab.Items[i];
                pivotItem.Header = $"Tab {i + 1} - Shafiq";
            }
        }
    }
}
