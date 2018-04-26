using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using log4net.Config;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using TestbedUtils.Memory;

namespace TestbedUtils.Training.Neat
{
    public class SMBNeatNetworkTrainer : AbstractNetworkTrainer
    {
        private static String experimentName = "Super Mario Bros";
        private NeatEvolutionAlgorithm<NeatGenome> evolutionAlgorithm;
        private String championFile;

        public SMBNeatNetworkTrainer(String stateFile, String xmlConfigFile, String championFile)
        {
            this.championFile = championFile;

            XmlConfigurator.Configure(new FileInfo("log4net.properties"));

            UpdateMemoryMapperRef handler = DelegateUpdateMemoryMapperRef;
            SMBNeatExperiment experiment = new SMBNeatExperiment(stateFile, handler);

            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load(xmlConfigFile);
            experiment.Initialize(experimentName, xmlConfig.DocumentElement);

            memoryMapperArray = new IMemoryMapper[experiment.GetMaxDegreeOfParallelism()];

            evolutionAlgorithm = experiment.CreateEvolutionAlgorithm();
            evolutionAlgorithm.UpdateEvent += new EventHandler(UpdateEvent);
        }

        public override void StartTraining()
        {
            evolutionAlgorithm.StartContinue();
        }

        public override void StopTraining()
        {
            evolutionAlgorithm.Stop();
        }

        private void UpdateEvent(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("gen={0:N0} bestFitness={1:N6}", evolutionAlgorithm.CurrentGeneration, evolutionAlgorithm.Statistics._maxFitness));

            var doc = NeatGenomeXmlIO.SaveComplete(
                new List<NeatGenome>() {
                    evolutionAlgorithm.CurrentChampGenome
                }, false
            );

            doc.Save(championFile);
        }
    }
}
