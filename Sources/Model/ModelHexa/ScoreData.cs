using System.Runtime.Serialization;

namespace ModelHexa
{
    [DataContract]
    public class ScoreData
    {
        [DataMember]
        public int Victoires { get; set; }

        [DataMember]
        public int Defaites { get; set; }

        [DataMember]
        public int VictoiresVsBot { get; set; }

        [DataMember]
        public int DefaitesVsBot { get; set; }

        [DataMember]
        public int PartiesTotales { get; set; }

        [DataMember]
        public int PartiesVsBot { get; set; }
    }
}
