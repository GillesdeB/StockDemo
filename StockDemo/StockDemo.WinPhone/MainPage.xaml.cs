using StockDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace StockDemo.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StockViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            viewModel = new StockViewModel();
            ButtonGetQuote.Click += ButtonGetQuote_Click;
        }

        private async void ButtonGetQuote_Click(object sender, RoutedEventArgs e)
        {
            ButtonGetQuote.IsEnabled = false;

            var result = await viewModel.GetQuote(TextTicker.Text);
            if (result)
            {
                TextBlockCompany.Text = viewModel.Data.Company;
                TextBlockQuote.Text = viewModel.Data.CurrentQuote;
                TextErrorMessage.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextErrorMessage.Text = viewModel.ErrorMessage;
                TextErrorMessage.Visibility = Visibility.Visible;
            }

            ButtonGetQuote.IsEnabled = true;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
            //Button.Click += delegate {
            //    var title = string.Format("{0} clicks!", count++);
            //    Button.Content = title;
            //};
        }
    }
}
