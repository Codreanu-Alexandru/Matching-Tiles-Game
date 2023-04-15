using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Maching_Tiles
{
    public static class XMLHelpers
    {
        public static List<User> SampleUsers()
        {
            List<User> users = new List<User>()
            {
                new User()
                {
                    Username = "Alex",
                    Password = "test",
                    Id = ("Alex" + "test").GetHashCode(),
                    GamesPlayed = 0,
                    GamesWon = 0,
                    ProfilePicture = "11.jpg"
                },
                new User()
                {
                    Username = "Alexandru",
                    Password = "test",
                    Id = ("Alexandru" + "test").GetHashCode(),
                    GamesPlayed = 0,
                    GamesWon = 0,
                    ProfilePicture = "1.jpg"
                },
                new User()
                {
                    Username = "Codrea",
                    Password = "test",
                    Id = ("Codrea" + "test").GetHashCode(),
                    GamesPlayed = 0,
                    GamesWon = 0,
                    ProfilePicture = "21.jpg"
                },
            };
            return users;
        }

        public static void SerialiseUsers(List<User> users, String file = "Users.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
            using (var writer = new StreamWriter(file))
            {
                xmlSerializer.Serialize(writer, users);
            }
        }

        public static List<User> DeserialiseUsers(String file = "Users.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<User>));
            List<User> users = new List<User>();
            using (var reader = new StreamReader(file))
            {
                users = (List<User>)xmlSerializer.Deserialize(reader);
            }

            return users;
        }

        public static List<GameDetails> SampleGames()
        {
            List<GameDetails> games = new List<GameDetails>()
            { 
                /*
                 * "🖤","❤","🔰","🎈","🎆","🎁","🎄",
                 * "🍔","🧂","🥗","🍙","☕","🍵","🐱",
                 * "🦁","🐯","🦒","🦊","🐻","🐰","🐹",
                 * "🐭","🐗","🌿","🌾","🌵","🌴","🌳",
                 * "🍃","🍁","🍂","🥀","🌸","🏵","🍆",
                 * "🌽","🧄","🥑","🍄","🌶","🍏","🍉",
                 * "🚒","🚓","🚀","💀","🎮","🎶","🧸"
                 */
                new GameDetails()
                {
                    PlayerId = -1669389377,
                    Round = 0,
                    Score = 0,
                    Tries = 35,
                    Board = new List<List<string>>()
                    {
                        new List<string>()
                        {
                            "🎁","💀","💀","🌳","🧸"
                        },
                        new List<string>()
                        {
                            "🎶","🌿","🌾","🎁","🖤"
                        },
                        new List<string>()
                        {
                            "🐻","🌳","","🖤","🌿"
                        },
                        new List<string>()
                        {
                            "🍄","🧸","🌾","🌶","🌿"
                        },
                        new List<string>()
                        {
                            "🌿","🌶","🐻","🍄","🎶"
                        },
                    }

                },
                new GameDetails()
                {
                    PlayerId = 161036264,
                    Round = 0,
                    Score = 0,
                    Tries = 35,
                    Board = new List<List<string>>()
                    {
                        new List<string>()
                        {
                           "🎁","💀","💀","🌳","🧸"
                        },
                        new List<string>()
                        {
                            "🎶","🌿","🌾","🎁","🖤"
                        },
                        new List<string>()
                        {
                            "🐻","🌳","","🖤","🌿"
                        },
                        new List<string>()
                        {
                            "🍄","🧸","🌾","🌶","🌿"
                        },
                        new List<string>()
                        {
                            "🌿","🌶","🐻","🍄","🎶"
                        },
                    }
                },
                new GameDetails()
                {
                   PlayerId = 545159776,
                   Round = 0,
                   Score = 0,
                   Tries = 35,
                   Board = new List<List<string>>()
                    {
                        new List<string>()
                        {
                            "🎁","💀","💀","🌳","🧸"
                        },
                        new List<string>()
                        {
                            "🎶","🌿","🌾","🎁","🖤"
                        },
                        new List<string>()
                        {
                            "🐻","🌳","","🖤","🌿"
                        },
                        new List<string>()
                        {
                            "🍄","🧸","🌾","🌶","🌿"
                        },
                        new List<string>()
                        {
                            "🌿","🌶","🐻","🍄","🎶"
                        },
                    }
                }
            };

            return games;
        }

        public static void SerialiseGames(List<GameDetails> games, String file = "Games.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GameDetails>));
            using (var writer = new StreamWriter(file))
            {
                xmlSerializer.Serialize(writer, games);
            }
        }

        public static List<GameDetails> DeserialiseGames(String file = "Games.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<GameDetails>));
            List<GameDetails> games = new List<GameDetails>();
            using (var reader = new StreamReader(file))
            {
                games = (List<GameDetails>)xmlSerializer.Deserialize(reader);
            }

            return games;
        }
    }
}
