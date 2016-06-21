using ProgParty.SiliconAngle.ApiUniversal.Parameter;
using ProgParty.SiliconAngle.ApiUniversal.Result;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProgParty.SiliconAngle.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BuyBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Core.Pages.Shop));
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Core.Pages.Contact), null);
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //Register.RegisterOnLoaded();
        }

        private ArticleCategory GetByName(string name)
        {
            switch (name)
            {
                case "All": return ArticleCategory.All;
                case "Cloud": return ArticleCategory.Cloud;
                case "Mobile": return ArticleCategory.Mobile;
                case "Social": return ArticleCategory.Social;
                case "Big-data": return ArticleCategory.BigData;
                case "Bleeding-edge": return ArticleCategory.BleedingEdge;
                default:
                    throw new System.NotImplementedException("This type is not implemented " + name);
            }
        }

        private void ComboBoxMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadPreviousGallery_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
