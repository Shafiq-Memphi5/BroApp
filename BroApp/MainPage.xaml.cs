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
            // Clear existing children from MainThumbnail (assumed to be a container for thumbnails)
            MainThumbnail.Children.Clear();

            // Iterate through each PivotItem in the Tab
            foreach (PivotItem newTab in Tab.Items)
            {
                // Check if the content of the PivotItem is a WebView
                if (newTab.Content is WebView newWeb)
                {
                    // Create a stream to save the captured image
                    var stream = new InMemoryRandomAccessStream();

                    // Capture the current preview of the WebView to the stream
                    await newWeb.CapturePreviewToStreamAsync(stream);

                    // Create a BitmapImage to hold the captured image
                    BitmapImage bitmapImage = new BitmapImage();

                    // Reset the stream position to the beginning
                    stream.Seek(0);

                    // Set the source of the BitmapImage to the stream
                    await bitmapImage.SetSourceAsync(stream);

                    // Create an Image control to display the captured image
                    Image capturedImage = new Image
                    {
                        Source = bitmapImage, // Set the image source
                        Width = 100,          // Set the desired width
                        Height = 60,         // Set the desired height
                        Margin = new Thickness(7) // Set margin around the image
                    };

                    // Add the captured image to MainThumbnail
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
