﻿using SimpleNeuralNetwork.EventArguments;
using SimpleNeuralNetwork.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNeuralNetwork
{
    class Program
    {
        private static string trainedNetworksPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar;

        static void Main(string[] args)
        {
            Run(NeuralNetworkFactoryHelper.NetworkFor.Addition, new double[] { .2, .1 });

            Console.ReadKey(true);

        }

        private static void Run(NeuralNetworkFactoryHelper.NetworkFor networkFor, double[] tests)
        {
            var factory = new NeuralNetworkFactoryHelper(trainedNetworksPath);
            factory.OnUpdateStatus += Factory_OnUpdateStatus;

            //TRAIN THE NUERAL NETWORK
            var neuralNetwork = factory.Train(networkFor);
            Console.WriteLine("");
            Console.WriteLine("Computation with newly trained network:");
            var result = neuralNetwork.Run(tests);
            WriteMatrix(result);

            //LOAD TRAINED NEURAL NETWORK
            neuralNetwork = factory.Load(networkFor);
            Console.WriteLine("");
            Console.WriteLine("Computation with old trained network:");
            result = neuralNetwork.Run(tests);
            WriteMatrix(result);
        }

        private static void WriteMatrix(double[] result)
        {
            for (var i = 0; i < result.Length; i++)
                Console.WriteLine("Output Neuron " + (i + 1) + ": " + Math.Round(result[i], 1).ToString("0.0", CultureInfo.InvariantCulture));

        }

        private static void Factory_OnUpdateStatus(object sender, ProgressEventArgs e)
        {
            Console.WriteLine(e.Status);
        }

    }
}