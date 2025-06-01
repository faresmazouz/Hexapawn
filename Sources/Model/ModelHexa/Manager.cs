using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace ModelHexa
{
    public class Manager
    {
        public Score Victoires { get; private set; } = new Score(0);
        public Score Defaites { get; private set; } = new Score(0);
        public Score VictoiresVsBot { get; private set; } = new Score(0);
        public Score DefaitesVsBot { get; private set; } = new Score(0);
        public Score PartiesTotales { get; private set; } = new Score(0);
        public Score PartiesVsBot { get; private set; } = new Score(0);

        private const string FichierScores = "scores.json";

        public void Sauvegarder()
        {
            var data = new ScoreData
            {
                Victoires = Victoires.Value,
                Defaites = Defaites.Value,
                VictoiresVsBot = VictoiresVsBot.Value,
                DefaitesVsBot = DefaitesVsBot.Value,
                PartiesTotales = PartiesTotales.Value,
                PartiesVsBot = PartiesVsBot.Value
            };

            var serializer = new DataContractJsonSerializer(typeof(ScoreData));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.WriteObject(memoryStream, data);
                memoryStream.Position = 0; // reset pour être sûr

                using (FileStream fileStream = File.Create(FichierScores))
                {
                    memoryStream.WriteTo(fileStream);
                }
            }
        }

        public void Charger()
        {
            if (!File.Exists(FichierScores)) return;

            var serializer = new DataContractJsonSerializer(typeof(ScoreData));
            using (FileStream stream = File.OpenRead(FichierScores))
            {
                var data = serializer.ReadObject(stream) as ScoreData;
                if (data != null)
                {
                    Victoires.Value = data.Victoires;
                    Defaites.Value = data.Defaites;
                    VictoiresVsBot.Value = data.VictoiresVsBot;
                    DefaitesVsBot.Value = data.DefaitesVsBot;
                    PartiesTotales.Value = data.PartiesTotales;
                    PartiesVsBot.Value = data.PartiesVsBot;
                }
            }
        }
    }
}
