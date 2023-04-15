using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

namespace Maching_Tiles
{
    public class Data
    {
        public static string HiddenToken { get; set; }

        private static int columns;
        private static int rows;
        public static int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                if (value != columns && value > 0)
                {
                    columns = value;
                }
            }
        }
        public static int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                if (value != rows && value > 0)
                {
                    rows = value;
                }

            }
        }
        public List<String> uniqueTiles;
        public List<GameDetails> SavedGames { get; set; }
        public void UploadTokens()
        {
            uniqueTiles = new List<String>()
            {
                "🖤","❤","🔰","🎈","🎆","🎁","🎄",
                "🍔","🧂","🥗","🍙","☕","🍵","🐱",
                "🦁","🐯","🦒","🦊","🐻","🐰","🐹",
                "🐭","🐗","🌿","🌾","🌵","🌴","🌳",
                "🍃","🍁","🍂","🥀","🌸","🏵","🍆",
                "🌽","🧄","🥑","🍄","🌶","🍏","🍉",
                "🚒","🚓","🚀","💀","🎮","🎶","🧸"
            };
        }

        static Data()
        {
            Columns = 5;
            Rows = 5;
            HiddenToken = "❔";
        }

        public Data()
        {
            UploadTokens();
            SavedGames = XMLHelpers.SampleGames();
        }
    }

    public partial class GameMenuWindow : Window
    {
        public List<User> users;
        public GameDetails gameDetails { get; set; }

        Button PreviousButton;
        public List<List<Button>> buttons;
        public User LoggedUser { get; set; }
        Data data;
        public bool IsSavedGame { get; set; }
        public GameMenuWindow(int userId)
        {
            InitializeComponent();

            data = new Data();
            gameDetails = new GameDetails()
            {
                Round = 0,
                Score = 0,
                Tries = 0,
            };

            IsSavedGame = false;

            users = XMLHelpers.DeserialiseUsers();
            LoggedUser = users.Find(x => x.Id == userId);

            ScoreBlock.Text = gameDetails.Score.ToString();
            MissesBlock.Text = gameDetails.Tries.ToString();
            UsernameBlock.Text = LoggedUser.Username;

            SetButtons();
            GameGrid.Visibility = Visibility.Hidden;
            PlayerProfile.Source = new BitmapImage(new Uri("Images\\" + LoggedUser.ProfilePicture, UriKind.Relative));
        }
        public void SetTiles()
        {
            gameDetails.Board = new List<List<String>>();
            for (int i = 0; i < Data.Rows; i++)
            {
                gameDetails.Board.Add(new List<String>());
                for (int j = 0; j < Data.Columns; j++)
                {
                    gameDetails.Board[i].Add(string.Empty);
                }
            }

            Random random = new Random();
            List<String> tilePairs = new List<String>();
            int pairsNeeded = (Data.Rows * Data.Columns) / 2;

            data.uniqueTiles = data.uniqueTiles.OrderBy(_ => random.Next()).ToList();

            while (tilePairs.Count < pairsNeeded * 2)
            {
                foreach (String tile in data.uniqueTiles)
                {
                    if (tilePairs.Count < pairsNeeded * 2)
                    {
                        tilePairs.Add(tile);
                        tilePairs.Add(tile);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < Data.Rows; i++)
            {
                for (int j = 0; j < Data.Columns; j++)
                {
                    if (i != 2 || j != 2)
                    {
                        int index = random.Next(tilePairs.Count);
                        gameDetails.Board[i][j] = tilePairs[index];
                        tilePairs.RemoveAt(index);
                    }
                }
            }
        }

        private void SetButtons()
        {
            buttons = new List<List<Button>>()
            {
                new List<Button>(){A0,A1,A2,A3,A4 },
                new List<Button>(){B0,B1,B2,B3,B4 },
                new List<Button>(){C0,C1,new Button(),C3,C4 },
                new List<Button>(){D0,D1,D2,D3,D4 },
                new List<Button>(){E0,E1,E2,E3,E4 },
            };

        }

        private void HelpAboutClick(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private async void ExitButtonClick(object sender, RoutedEventArgs e)
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

        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            IsSavedGame = false;
            ResetButtons();
            GameGrid.Visibility = Visibility.Visible;
            gameDetails = new GameDetails()
            {
                PlayerId = LoggedUser.Id,
                Score = 0,
                Round = 0,
                Tries = 35,
            };
            ScoreBlock.Text = gameDetails.Score.ToString();
            MissesBlock.Text = gameDetails.Tries.ToString();
            RoundBlock.Text = gameDetails.Round.ToString();
            SetTiles();
        }

        private void ResetButtons()
        {
            SetTiles();
            for (int i = 0; i < buttons.Count; i++)
            {
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    buttons[i][j].Content = Data.HiddenToken;
                    buttons[i][j].Visibility = Visibility.Visible;
                    buttons[i][j].IsEnabled = true;
                }
            }
        }

        private async void TokenClick(object sender, RoutedEventArgs e)
        {
            if (PreviousButton == null)
            {
                var a = (sender as Button);
                var i = (int)a.GetValue(Grid.RowProperty);
                var j = (int)a.GetValue(Grid.ColumnProperty);

                (sender as Button).Content = gameDetails.Board[i][j];

                PreviousButton = (sender as Button);
                PreviousButton.IsEnabled = false;
            }
            else
            {
                var a = (sender as Button);
                var i = (int)a.GetValue(Grid.RowProperty);
                var j = (int)a.GetValue(Grid.ColumnProperty);

                a.IsEnabled = false;
                a.Content = gameDetails.Board[i][j];

                await Dispatcher.BeginInvoke(new Action(() =>
                {
                    a.Content = gameDetails.Board[i][j];
                }), System.Windows.Threading.DispatcherPriority.Background);


                await Task.Delay(250);

                var k = (int)PreviousButton.GetValue(Grid.RowProperty);
                var l = (int)PreviousButton.GetValue(Grid.ColumnProperty);

                if (gameDetails.Board[i][j] == gameDetails.Board[k][l])
                {

                    gameDetails.Board[k][l] = Data.HiddenToken;
                    gameDetails.Board[i][j] = Data.HiddenToken;

                    a.Visibility = Visibility.Hidden;
                    PreviousButton.Visibility = Visibility.Hidden;

                    a.Content = Data.HiddenToken;
                    PreviousButton.Content = Data.HiddenToken;

                    PreviousButton = null;

                    gameDetails.Score++;
                    ScoreBlock.Text = gameDetails.Score.ToString();



                    if (gameDetails.Score == 12)
                    {
                        gameDetails.Round++;
                        RoundBlock.Text = gameDetails.Round.ToString();

                        gameDetails.Score = 0;
                        ScoreBlock.Text = gameDetails.Score.ToString();

                        gameDetails.Tries = 35;
                        MissesBlock.Text = gameDetails.Tries.ToString();

                        if (gameDetails.Round == 3)
                        {
                            MessageBox.Show("Congratulations " + LoggedUser.Username + "!", "YIPPEE");
                            users.Find(x => x.Id == LoggedUser.Id).GamesPlayed++;
                            users.Find(x => x.Id == LoggedUser.Id).GamesWon++;
                            XMLHelpers.SerialiseUsers(users);

                            GameGrid.Visibility = Visibility.Hidden;

                            gameDetails.Round = 0;
                            RoundBlock.Text = gameDetails.Round.ToString();

                            gameDetails.Score = 0;
                            ScoreBlock.Text = gameDetails.Score.ToString();

                            gameDetails.Tries = 35;
                            MissesBlock.Text = gameDetails.Tries.ToString();
                        }
                        else
                        {
                            ResetButtons();
                            GameGrid.Visibility = Visibility.Visible;
                        }
                    }
                }
                else
                {
                    gameDetails.Tries--;
                    MissesBlock.Text = gameDetails.Tries.ToString();
                    if (gameDetails.Tries == 0)
                    {
                        MessageBox.Show("💀💀💀", "You lost!");
                        users.Find(x => x.Id == LoggedUser.Id).GamesPlayed++;
                        XMLHelpers.SerialiseUsers(users);
                        GameGrid.Visibility = Visibility.Hidden;
                        return;
                    }
                    (sender as Button).IsEnabled = true;
                    a.Content = Data.HiddenToken;
                    PreviousButton.Content = Data.HiddenToken;
                    PreviousButton.IsEnabled = true;
                    PreviousButton = null;
                }
            }

            if (IsSavedGame)
            {
                var games = XMLHelpers.DeserialiseGames();
                var newGames = new List<GameDetails>();

                foreach (var game in games)
                {
                    if (game.PlayerId != LoggedUser.Id)
                    {
                        newGames.Add(game);
                    }
                }

                XMLHelpers.SerialiseGames(newGames);
                IsSavedGame = false;
            }

            return;

        }
        private void SaveGameClick(object sender, RoutedEventArgs e)
        {
            var games = XMLHelpers.DeserialiseGames();
            var newGames = new List<GameDetails>();

            foreach (var game in games)
            {
                if (game.PlayerId != LoggedUser.Id)
                {
                    newGames.Add(game);
                }
            }

            XMLHelpers.SerialiseGames(newGames);
            IsSavedGame = false;

            data.SavedGames.Add(gameDetails);
            XMLHelpers.SerialiseGames(data.SavedGames);
            gameDetails = new GameDetails()
            {
                PlayerId = LoggedUser.Id,
                Score = 0,
                Round = 0,
                Tries = 35,
            };
            ScoreBlock.Text = gameDetails.Score.ToString();
            MissesBlock.Text = gameDetails.Tries.ToString();
            RoundBlock.Text = gameDetails.Round.ToString();
            GameGrid.Visibility = Visibility.Hidden;
        }

        private void ContinueFromGame(GameDetails game)
        {
            if (game.PlayerId != LoggedUser.Id)
            {
                return;
            }

            gameDetails = new GameDetails()
            {
                Board = new List<List<String>>()
                {
                    new List<string>()
                    {
                        "", "", "", "", "",
                    },
                    new List<string>()
                    {
                        "", "", "", "", "",
                    },
                    new List<string>()
                    {
                        "", "", "", "", "",
                    },
                    new List<string>()
                    {
                        "", "", "", "", "",
                    },
                    new List<string>()
                    {
                        "", "", "", "", "",
                    }
                }
            };

            for (int i = 0; i < Data.Rows; i++)
            {
                for (int j = 0; j < Data.Columns; j++)
                {
                    if (game.Board[i][j] == Data.HiddenToken)
                    {
                        if (i != 2 || j != 2)
                        {
                            gameDetails.Board[i][j] = Data.HiddenToken;
                            buttons[i][j].Content = Data.HiddenToken;
                            buttons[i][j].Visibility = Visibility.Hidden;
                            buttons[i][j].IsEnabled = false;
                        }
                    }
                    else
                    {
                        gameDetails.Board[i][j] = game.Board[i][j];
                        buttons[i][j].Content = Data.HiddenToken;
                    }
                }
            }

            gameDetails.Score = game.Score;
            gameDetails.Round = game.Round;
            gameDetails.Tries = game.Tries;

            ScoreBlock.Text = gameDetails.Score.ToString();
            RoundBlock.Text = gameDetails.Round.ToString();
            MissesBlock.Text = gameDetails.Tries.ToString();
        }

        private void StatisticsClick(object sender, RoutedEventArgs e)
        {
            Window window = new StatisticsWindow();
            window.Left = this.Left;
            window.Top = this.Top;
            window.Width = this.Width;
            window.Height = this.Height;
            window.Show();
        }

        private void SavedGameClick(object sender, RoutedEventArgs e)
        {
            GameDetails game = XMLHelpers.DeserialiseGames().FindLast(x => x.PlayerId == LoggedUser.Id);

            if (game != null)
            {
                ContinueFromGame(game);
                GameGrid.Visibility = Visibility.Visible;
            }

            IsSavedGame = true;
        }
    }
}
