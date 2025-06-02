using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

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

        private readonly string _cheminFichier;

        public Manager(string cheminFichier = null)
        {
            _cheminFichier = cheminFichier ?? Path.Combine(AppContext.BaseDirectory, "scores.xml");
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

            var serializer = new DataContractSerializer(typeof(ScoreData));

            var settings = new XmlWriterSettings { Indent = true };

            using (TextWriter tw = File.CreateText(_cheminFichier))
            using (XmlWriter writer = XmlWriter.Create(tw, settings))
            {
                serializer.WriteObject(writer, data);
            }

            Console.WriteLine(">> Scores sauvegardés en XML dans : " + _cheminFichier);
        }

        public void Charger()
        {
            if (!File.Exists(_cheminFichier))
            {
                Console.WriteLine(">> Fichier XML introuvable : " + _cheminFichier);
                return;
            }

            var serializer = new DataContractSerializer(typeof(ScoreData));

            using (Stream stream = File.OpenRead(_cheminFichier))
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

                    Console.WriteLine(">> Scores chargés depuis XML : " + _cheminFichier);
                }
                else
                {
                    Console.WriteLine(">> Erreur lors de la désérialisation XML.");
                }
            }
        }
    }
}
