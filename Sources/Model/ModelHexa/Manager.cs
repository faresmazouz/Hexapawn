using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ModelHexa
{
    public class Manager
    {
        public Score Victoires { get; private set; } = new Score();
        public Score Defaites { get; private set; } = new Score();
        public Score VictoiresVsBot { get; private set; } = new Score();
        public Score DefaitesVsBot { get; private set; } = new Score();
        public Score PartiesTotales { get; private set; } = new Score();
        public Score PartiesVsBot { get; private set; } = new Score();

        private static string FichierScores =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "scores.json");

        // Le constructeur appelle Charger()
        public Manager()
        {
            Charger();
        }

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
                memoryStream.Position = 0;

                using (FileStream fileStream = File.Create(FichierScores))
                {
                    memoryStream.WriteTo(fileStream);
                }
            }
        }

        public void Charger()
        {
            if (!File.Exists(FichierScores))
            {
                Console.WriteLine(">> Fichier scores.json introuvable.");
                return;
            }

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

                    Console.WriteLine(">> Scores chargés depuis le fichier JSON.");
                }
                else
                {
                    Console.WriteLine(">> Le fichier JSON est vide ou mal formé.");
                }
            }
        }
    }
}
