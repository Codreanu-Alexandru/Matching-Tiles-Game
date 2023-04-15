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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Maching_Tiles
{
    public partial class EnterPassword : Window
    {
        public String Password { get; set; }
        public EnterPassword()
        {
            InitializeComponent();
        }

        private void OkButton(object sender, RoutedEventArgs e)
        {
            if (PasswordControl.Password.Length != 0)
            {
                Password = PasswordControl.Password;
                this.Close();
            }
        }
    }
}
