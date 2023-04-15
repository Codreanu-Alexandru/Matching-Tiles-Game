using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Maching_Tiles
{
    public partial class NewUserMenuWindow : Window
    {
        private List<String> imagePaths;
        private int currentImageIndex;
        private static int ImageCount;
        public NewUserMenuWindow()
        {
            ImageCount = 21;
            currentImageIndex = 0;
            UploadImages();
            InitializeComponent();
            UpdateImageControl();
        }

        private void UploadImages()
        {
            imagePaths = new List<String>();
            String pathAux = "Images\\";
            String imageExtension = ".jpg";
            for (int i = 1; i <= ImageCount; i++)
            {
                imagePaths.Add((pathAux + i.ToString() + imageExtension));
            }
        }

        private void UpdateImageControl()
        {
            ImageControl.Source = new BitmapImage(new Uri(imagePaths[currentImageIndex], UriKind.Relative));
        }

        private void NextButton(object sender, RoutedEventArgs e)
        {
            currentImageIndex++;
            if (currentImageIndex >= imagePaths.Count())
            {
                currentImageIndex = 0;
            }
            UpdateImageControl();
        }

        private void PreviousButton(object sender, RoutedEventArgs e)
        {
            currentImageIndex--;
            if (currentImageIndex < 0)
            {
                currentImageIndex = imagePaths.Count() - 1;
            }
            UpdateImageControl();
        }

        private void ConfirmButton(object sender, RoutedEventArgs e)
        {
            if (UsernameInput.Text != string.Empty && PasswordInput.Password != string.Empty)
            {
                User newUser = new User()
                {
                    Id = (UsernameInput.Text + PasswordInput.Password).GetHashCode(),
                    Username = UsernameInput.Text,
                    Password = PasswordInput.Password,
                    ProfilePicture = imagePaths[currentImageIndex].Substring(7),
                    GamesPlayed = 0,
                    GamesWon = 0,
                };

                List<User> users = XMLHelpers.DeserialiseUsers();

                foreach (var user in users)
                {
                    if (user.Id == newUser.Id)
                    {
                        MessageBox.Show("User already exists with the same credentials");
                        return;
                    }
                }

                users.Add(newUser);

                XMLHelpers.SerialiseUsers(users);
                MessageBox.Show("Your account has been added to the list.", "Success!");
                ExitButton(sender, e);
            }
        }

        private async void ExitButton(object sender, RoutedEventArgs e)
        {
            Window window = new MainWindow();
            window.Opacity = 0;

            DoubleAnimation fadeOutAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.0001));
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);

            await Task.Delay(TimeSpan.FromSeconds(0.0001));

            window.Left = this.Left;
            window.Top = this.Top;
            window.Width = this.Width;
            window.Height = this.Height;


            DoubleAnimation fadeInAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.0001));
            window.BeginAnimation(Window.OpacityProperty, fadeInAnimation);

            window.Show();
            this.Close();
        }
    }
}
