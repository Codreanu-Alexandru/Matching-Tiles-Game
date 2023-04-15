using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Maching_Tiles
{
    [Serializable]
    public class User
    {
        [XmlAttribute]
        public int Id { get; set; }

        [XmlAttribute]
        public string Username { get; set; }

        [XmlAttribute]
        public string Password { get; set; }

        [XmlAttribute]
        public int GamesPlayed { get; set; }

        [XmlAttribute]
        public int GamesWon { get; set; }

        [XmlAttribute]
        public string ProfilePicture { get; set; }

    }
}
