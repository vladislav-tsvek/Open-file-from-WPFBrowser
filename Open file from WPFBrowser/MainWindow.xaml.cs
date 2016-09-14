using DotNetBrowser;
using DotNetBrowser.WPF;
using Microsoft.Win32;
using System;
using System.Windows;

namespace Open_file_from_WPFBrowser
{
    public partial class MainWindow : Window
    {
        BrowserView webView;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LightBrowser_Click(object sender, RoutedEventArgs e)
        {
            webView = new WPFBrowserView(BrowserFactory.Create(BrowserType.LIGHTWEIGHT));
            WPFWeb.Children.Add((UIElement)webView.GetComponent());

            webView.Browser.LoadURL("http://www.google.com");

            LightBrowser.Visibility = Visibility.Hidden;
            HeavyBrowser.Visibility = Visibility.Hidden;
            OpenFile.Visibility = Visibility.Visible;
            Features.Visibility = Visibility.Visible;
            Status.Visibility = Visibility.Visible;
            this.Title = "LightBrowser";
        }

        private void HeavyBrowser_Click(object sender, RoutedEventArgs e)
        {
            webView = new WPFBrowserView(BrowserFactory.Create(BrowserType.HEAVYWEIGHT));
            WPFWeb.Children.Add((UIElement)webView.GetComponent());
            
            webView.Browser.LoadURL("http://www.google.com");
            
            LightBrowser.Visibility = Visibility.Hidden;
            HeavyBrowser.Visibility = Visibility.Hidden;
            OpenFile.Visibility = Visibility.Visible;
            Features.Visibility = Visibility.Visible;
            Status.Visibility = Visibility.Visible;
            this.Title = "HeavyBrowser";            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!webView.Browser.IsDisposed())
            {
                webView.Dispose();
                webView.Browser.Dispose();
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".html";
            openFileDialog.Filter = "HTML pages (.html)|*.html";

            // Show the Dialog.
            Nullable<bool> result = openFileDialog.ShowDialog();

           


            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = openFileDialog.FileName;
                //Load Web page
                webView.Browser.LoadURL(filename);
                //Status.Content = webView.Browser.URL.ToString();
                Status.Content = "Page loaded successfully! Wait for upload libraries.";

            }
            else
            {
                Status.Content = "Something gone wrong!";
            }
        }

        private void Features_Click(object sender, RoutedEventArgs e)
        {
            Window1 feature = new Window1();
            feature.Show();
        }
    }
}
