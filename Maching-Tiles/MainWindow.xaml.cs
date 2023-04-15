using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Maching_Tiles
{
    public partial class MainWindow : Window
    {
        public bool IsUserSelected
        {
            get { return UsersControl.SelectedIndex != -1; }
        }

        private User selectedUser;
        private List<User> users;
        private List<String> imagePaths;
        private static int ImageCount;
        public MainWindow()
        {
            InitializeComponent();
            ImageCount = 21;
            GetImagePaths();
            DeserialiseUsers();
        }

        private void DeserialiseUsers()
        {
            users = XMLHelpers.DeserialiseUsers();

            foreach (var user in users)
            {
                UsersControl.Items.Add(user.Username);
            }
        }

        private void SerialisedUsers()
        {
            XMLHelpers.SerialiseUsers(users);
        }

        private void GetImagePaths()
        {
            imagePaths = new List<String>();
            String pathAux = "Images\\";
            String imageExtension = ".jpg";
            for (int i = 1; i <= ImageCount; i++)
            {
                imagePaths.Add((pathAux + i.ToString() + imageExtension));
            }
        }

        private void UpdateUserImage(User user)
        {
            string pathAux = "Images\\";
            UserImage.Source = new BitmapImage(new Uri((pathAux + user.ProfilePicture), UriKind.Relative));
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void NewUserButton(object sender, RoutedEventArgs e)
        {
            Window window = new NewUserMenuWindow();
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

        private void DeleteUserButton(object sender, RoutedEventArgs e)
        {
            if (UsersControl.SelectedIndex != -1)
            {
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show("Delete " + users[UsersControl.SelectedIndex].Username + " ?", "User deletion", button);
                if (result.CompareTo(MessageBoxResult.Yes) == 0)
                {
                    users.Remove(users[UsersControl.SelectedIndex]);
                    SerialisedUsers();
                    UsersControl.Items.Clear();
                    DeserialiseUsers();
                }
            }
            else
            {
                MessageBox.Show("No user selected 💀", "🤨 Skill issue");
            }
        }

        private async void PlayButton(object sender, RoutedEventArgs e)
        {
            if (UsersControl.SelectedIndex == -1)
            {
                MessageBox.Show("No user selected 💀", "Bro dead arse forgot 💀");
            }
            else
            {
                EnterPassword enterPassword = new EnterPassword();
                enterPassword.Left = this.Left;
                enterPassword.Top = this.Top;
                string result = string.Empty;
                if (enterPassword.ShowDialog() == false)
                {
                    result = enterPassword.Password;
                    enterPassword.Close();
                }


                if (result == users[UsersControl.SelectedIndex].Password)
                {
                    Window window = new GameMenuWindow(users[UsersControl.SelectedIndex].Id);
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
                else
                {
                    MessageBox.Show("💀💀💀", "Did you forget ?");
                }
            }
        }

        private void UsersControlSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UsersControl.SelectedIndex == -1)
            {
                DeleteUser.IsEnabled = false;
                Play.IsEnabled = false;
            }
            else
            {
                Play.IsEnabled = true;
                DeleteUser.IsEnabled = true;
                UpdateUserImage(users[UsersControl.SelectedIndex]);
            }
        }
    }
}
