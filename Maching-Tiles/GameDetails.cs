using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Maching_Tiles
{
    [Serializable]
    public class GameDetails
    {
        [XmlAttribute]
        public int PlayerId { get; set; }
        [XmlAttribute]
        public int Round { get; set; }
        [XmlAttribute]
        public int Score { get; set; }
        [XmlAttribute]
        public int Tries { get; set; }
        [XmlArray]
        public List<List<String>> Board { get; set; }
    }
}
