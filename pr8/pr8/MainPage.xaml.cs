using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

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
        private async void openImage2_Clicked(object sender, EventArgs e)
        {
            var options = new PickOptions()
            {
                PickerTitle = "dsd",
                FileTypes = FilePickerFileType.Images,
            };
            if (Device.RuntimePlatform == Device.Android)
            {
                var result = await FilePicker.PickAsync(options);   
                imageSection.Source = result.FullPath;
            }
            
        }

        private void save1_Clicked(object sender, EventArgs e)
        {
            try
            {
                Application.Current.Properties["fio"] = familia.Text;
                Application.Current.Properties["name"] = name.Text;
                Application.Current.Properties["othestvo"] = othestvo.Text;
                Application.Current.Properties["age"] = dateBirth.Date.Date;
                Application.Current.Properties["gg"] = raiting2.Text;
                Application.Current.Properties["ff"] = gender.SelectedItem.ToString();
                Application.Current.Properties["dd"] = isElder.SelectedItem.ToString();
                Application.Current.Properties["ss"] = needRoom.SelectedItem.ToString();
                Application.Current.Properties["image"] = imageSection.Source;
                Application.Current.SavePropertiesAsync();
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите корректные данные", "ок");
            }
        }

        private void save2_Clicked(object sender, EventArgs e)
        {
            try
            {
                Preferences.Set("fio", familia.Text);
                Preferences.Set("name", name.Text);
                Preferences.Set("othestvo", othestvo.Text);
                Preferences.Set("age", dateBirth.Date);
                Preferences.Set("gg", raiting2.Text);
                Preferences.Set("ff", gender.SelectedItem.ToString());
                Preferences.Set("dd", isElder.SelectedItem.ToString());
                Preferences.Set("ss", needRoom.SelectedItem.ToString());
                Preferences.Set("image", imageSection.Source.ToString().Substring(6));
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите корректные данные", "ок");
            }
            
        }
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private void save3_Clicked(object sender, EventArgs e)
        {
            try
            {
                StreamWriter outFile = new StreamWriter(Path.Combine(folderPath, "file.txt"));
                outFile.WriteLine(familia.Text);
                outFile.WriteLine(name.Text);
                outFile.WriteLine(othestvo.Text);
                outFile.WriteLine(dateBirth.Date);
                outFile.WriteLine(raiting2.Text);
                outFile.WriteLine(gender.SelectedItem.ToString());
                outFile.WriteLine(isElder.SelectedItem.ToString());
                outFile.WriteLine(needRoom.SelectedItem.ToString());
                outFile.WriteLine(imageSection.Source);
                outFile.Close();
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите корректные данные", "ок");
            }
        }

        private void open1_Clicked(object sender, EventArgs e)
        {
            try
            {
                object value;
                if (Application.Current.Properties.TryGetValue("fio", out value) == true)
                    familia.Text = (String)value;
                if (Application.Current.Properties.TryGetValue("name", out value) == true)
                    name.Text = (String)value;
                if (Application.Current.Properties.TryGetValue("othestvo", out value) == true)
                    othestvo.Text = (String)value;
                if (Application.Current.Properties.TryGetValue("age", out value) == true)
                    dateBirth.Date = (DateTime)value;
                if (Application.Current.Properties.TryGetValue("gg", out value) == true)
                    raiting2.Text = (String)value;
                if (Application.Current.Properties.TryGetValue("ff", out value) == true)
                    gender.SelectedItem = value;
                if (Application.Current.Properties.TryGetValue("dd", out value) == true)
                    isElder.SelectedItem = value;
                if (Application.Current.Properties.TryGetValue("ss", out value) == true)
                    needRoom.SelectedItem = value;
                if (Application.Current.Properties.TryGetValue("image", out value) == true)
                    imageSection.Source = value as ImageSource;
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите корректные данные", "ок");
            }
        }

        private void open2_Clicked(object sender, EventArgs e)
        {
            try
            {
                familia.Text = Preferences.Get("fio", "");
                name.Text = Preferences.Get("name", "");
                othestvo.Text = Preferences.Get("othestvo", "");
                dateBirth.Date = Preferences.Get("age", DateTime.Now);
                raiting2.Text = Preferences.Get("gg", "");
                gender.SelectedItem = Preferences.Get("ff", ToString());
                isElder.SelectedItem = Preferences.Get("dd", ToString());
                needRoom.SelectedItem = Preferences.Get("ss", ToString());
                imageSection.Source = Preferences.Get("image", "");
            }
            catch
            {
                DisplayAlert("Ошибка", "Введите корректные данные", "ок");
            }
        }

        private void open3_Clicked(object sender, EventArgs e)
        {
            try {
                if (File.Exists(Path.Combine(folderPath, "file.txt")) == true)
                {
                    StreamReader outFile = new StreamReader(Path.Combine(folderPath, "file.txt"));
                    familia.Text = outFile.ReadLine();
                    name.Text = outFile.ReadLine();
                    othestvo.Text = outFile.ReadLine();
                    dateBirth.Date = Convert.ToDateTime(outFile.ReadLine());
                    raiting2.Text = outFile.ReadLine();
                    gender.SelectedItem = outFile.ReadLine();
                    isElder.SelectedItem = outFile.ReadLine();
                    needRoom.SelectedItem = outFile.ReadLine();
                    imageSection.Source = outFile.ReadLine();
                    outFile.Close();
                }
            }
            catch {
                DisplayAlert("Ошибка", "Введите корректные данные", "ок");
            }
        }
    }
}
