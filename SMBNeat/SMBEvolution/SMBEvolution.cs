using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net.Config;
using SharpNeat.Core;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using dotNES;

namespace SMBNeat
{
    class SMBEvolution : ISMBNeatExperiment
    {
        static NeatEvolutionAlgorithm<NeatGenome> _ea;
        const string CHAMPION_FILE = "SMB_champion.xml";
        public static readonly string[] STATES = { "SMBWorld1Level1.bin", "SMBWorld1Level2.bin" };

        public void StartExperment()
        {
            // Initialise log4net (log to console).
            XmlConfigurator.Configure(new FileInfo("log4net.properties"));

            SMBExperiment experiment = new SMBExperiment(STATES);

            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("smb.config.xml");
            experiment.Initialize("Super Mario Bros", xmlConfig.DocumentElement);

            _ea = experiment.CreateEvolutionAlgorithm();
            _ea.UpdateEvent += new EventHandler(ea_UpdateEvent);
            _ea.StartContinue();

            Console.ReadLine();
        }

        public void ea_UpdateEvent(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("gen={0:N0} bestFitness={1:N6}", _ea.CurrentGeneration, _ea.Statistics._maxFitness));

            // Save the best genome to file
            var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
            doc.Save(CHAMPION_FILE);
        }
    }
}
