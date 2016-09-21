using DotNetBrowser;
using DotNetBrowser.WPF;
using DotNetBrowser.DOM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using DotNetBrowser.Events;
using System.Threading;
using System.Windows.Input;

namespace Open_file_from_WPFBrowser
{
    public partial class MainWindow : Window
    {
        public BrowserView webView;

        DOMDocument objDocument;
        DOMElement flag;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LightBrowser_Click(object sender, RoutedEventArgs e)
        {
            webView = new WPFBrowserView(BrowserFactory.Create(BrowserType.LIGHTWEIGHT));

            controlsVisibility();

            this.Title = "LightBrowser";
        }

        private void HeavyBrowser_Click(object sender, RoutedEventArgs e)
        {
            webView = new WPFBrowserView(BrowserFactory.Create(BrowserType.HEAVYWEIGHT));

            controlsVisibility();

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
                Status.Visibility = Visibility.Visible;
                Status.Content = "Page loaded successfully! Wait for upload libraries.";

            }
            else
            {
                Status.Visibility = Visibility.Visible;
                Status.Content = "Something gone wrong!";
            }
        }

        
        private void clickOnPage_Click(object sender, RoutedEventArgs e)
        {
            objDocument = webView.Browser.GetDocument();

            try
            {
                flag = objDocument.GetElementById(textBox.Text.ToString());
            }
            catch (NullReferenceException e1)
            {
                Console.WriteLine("{0}", e1);
                Status.Visibility = Visibility.Visible;
                Status.Content = "No button elements!";

            }
            catch (ArgumentException e2)
            {
                Console.WriteLine("{0}", e2);
            }

            if (flag != null)
            {
                
                flag.Click();
                Status.Content = "";
            }

            else
            {
                Status.Visibility = Visibility.Visible;
                Status.Content = "No element with this ID";
            }
        }

        
        void controlsVisibility()
        {
            WPFWeb.Children.Add((UIElement)webView.GetComponent());

            webView.Browser.LoadURL("teamdev.com");

            LightBrowser.Visibility = Visibility.Hidden;
            HeavyBrowser.Visibility = Visibility.Hidden;
            OpenFile.Visibility = Visibility.Visible;
            clickOnPage.Visibility = Visibility.Visible;
            checkBoxOn.Visibility = Visibility.Visible;
            textBox.Visibility = Visibility.Visible;
            textBox.Text = "button";
            tagId.Visibility = Visibility.Visible;
            Status.Visibility = Visibility.Visible;
            comboBox.Visibility = Visibility.Visible;
        }

       
        private void checkBoxOn_Click(object sender, RoutedEventArgs e)
        {
            objDocument = webView.Browser.GetDocument();
            List<DOMNode> flags = null;

            try
            {
                flags = objDocument.GetElementsByTagName("input");
            }
            catch (NullReferenceException e1)
            {
                Console.WriteLine("{0}", e1);
                Status.Visibility = Visibility.Visible;
                Status.Content = "No checkbox elements!";

            }
            catch (ArgumentException e2)
            {
                Console.WriteLine("{0}", e2);
            }

            if (flags != null)
            {
                foreach (var item in flags)
                {

                    DOMElement tmp = (DOMElement)item;

                    if (tmp.GetAttribute("type").ToString() == "checkbox")
                    {
                        item.Click();

                    }

                }

                Status.Content = "";
            }

            else
            {
                Status.Visibility = Visibility.Visible;
                Status.Content = "No checkbox for flag";
            }
        }

        private void comboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            objDocument = webView.Browser.GetDocument();

            DOMNode list = null;

            try
            {
                list = objDocument.GetElementById("effectTypes");
            }
            catch (NullReferenceException e1)
            {
                Console.WriteLine("{0}", e1);
                Status.Visibility = Visibility.Visible;
                Status.Content = "No checkbox elements!";

            }
            catch (ArgumentException e2)
            {
                Console.WriteLine("{0}", e2);
            }

            List<DOMNode> optns = list.GetElementsByTagName("option");
            if (comboBox.Items.Count == optns.Count)
            {
                for (int i = 0; i < optns.Count; i++)
                {
                    Status.Content = "";
                    if (comboBox.SelectedItem.ToString() == optns[i].TextContent)
                    {
                        DOMElement tmp = (DOMElement)optns[i];
                        tmp.SetAttribute("selected", "selected");
                        tmp.Click();
                        break;
                    }
                    else
                    {
                        DOMElement tmp = (DOMElement)optns[i];
                        if (tmp.SetAttribute("selected", "selected"))
                        {
                            tmp.RemoveAttribute("selected");
                        }
                    }

                }
            }
        }

        private void comboBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (comboBox.Items.Count == 0)
            {

                objDocument = webView.Browser.GetDocument();

                DOMNode list = null;
                List<DOMNode> optns = null;

                try
                {
                    list = objDocument.GetElementById("effectTypes");
                    optns = list.GetElementsByTagName("option");
                }
                catch (NullReferenceException e1)
                {
                    Console.WriteLine("{0}", e1);
                    Status.Visibility = Visibility.Visible;
                    Status.Content = "No combobox elements!";

                }
                catch (ArgumentException e2)
                {
                    Console.WriteLine("{0}", e2);
                }
                if (optns != null)
                {
                    foreach (var item in optns)
                    {
                        comboBox.Items.Add(item.TextContent);
                        Status.Visibility = Visibility.Visible;
                        Status.Content = "Elements loaded!";
                    }
                }
            }
        }
    }
}
