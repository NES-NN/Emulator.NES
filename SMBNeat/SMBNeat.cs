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
    class SMBNeat
    {
        static void Main(string[] args)
        {
            //Simple Neat 
            ISMBNeatExperiment SMBEvolution = new SMBEvolution();
            SMBEvolution.StartExperment();
        }
    }
}
