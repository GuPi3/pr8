using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace pr8
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void dateBirth_DateSelected(object sender, DateChangedEventArgs e)
        {
            int ag = DateTime.Now.Year - dateBirth.Date.Year;
            if (DateTime.Now.Month < dateBirth.Date.Month ||
                (DateTime.Now.Month == dateBirth.Date.Month &&
                DateTime.Now.Day < dateBirth.Date.Day)) ag--;
            age.Text = "Возраст" + ag.ToString();
        }

        private void slider1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            raiting2.Text = e.NewValue.ToString();
        }

        async private void openImage_Clicked(object sender, EventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                var result = await FilePicker.PickAsync();
                imageSection.Source = result.FullPath;
            }
            if (Device.RuntimePlatform == Device.UWP)
            {
                var result = await FilePicker.PickAsync();
                var stream = await result.OpenReadAsync();
                imageSection.Source = ImageSource.FromStream(() => stream);
            }
        }
    }
}
