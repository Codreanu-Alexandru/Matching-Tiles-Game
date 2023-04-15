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
    public class Users
    {
        public List<User> AllUsers { get; set; }

        public Users()
        {
            AllUsers = XMLHelpers.DeserialiseUsers().OrderByDescending(x =>
            {
                if (x.GamesPlayed == 0)
                {
                    return -1;
                }
                else if (x.GamesWon == 0)
                {
                    return x.GamesPlayed * (-1);
                }
                else
                {
                    return x.GamesWon / x.GamesPlayed;
                }
            }).ToList();
            for (int i = 0; i < AllUsers.Count; i++)
            {
                AllUsers[i].ProfilePicture = "Images\\" + AllUsers[i].ProfilePicture;
            }
        }
    }
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();
        }
    }
}
