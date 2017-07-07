using LocalizationDemo.Resx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalizationDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //NameLabel.Text = AppResources.NameText;
            NameEntry.Placeholder = AppResources.NamePlaceholder;
        }
    }
}
