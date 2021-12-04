using NUnit.Framework;
using NeuralNetwork.NeuralNetworkComponents;
using System.Collections.Generic;

namespace NeuralNetworkTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            network = new NeuralNet();
        }
        NeuralNet network;
        [Test]
        public void TestActivationFunction()
        {
            //var neuronList = new List<Neuron>();
            //neuronList.Add(new Neuron { Weight = 1 });
            //neuronList.Add(new Neuron { Weight = 2 });
            //neuronList.Add(new Neuron { Weight = 3 });

            var neuron = new Neuron {};

            var result = neuron.SigmoidFunction(1 + 2 + 0.5);

            Assert.GreaterOrEqual(result, 0);
            Assert.LessOrEqual(result, 1);

        }

        [Test]
        public void TestNeuralNetCreation()
        {
            var net = new NeuralNet(3, 2, 4, 5, 6);
            net.ConfigureNeuralNetwork();
        }
        [Test]
        public void SimpleNeuralNetworkTest() 
        {
            var net = new NeuralNet(3, 3, 3);
            net.ConfigureNeuralNetwork();
            net.Layers[0].Neurons[0].Weights = new List<double> { 1 };
            net.Layers[0].Neurons[1].Weights = new List<double> { 1 };
            net.Layers[0].Neurons[2].Weights = new List<double> { 1 };

            net.Layers[1].Neurons[0].Weights = new List<double> { 1.1, 2.1, 3.1 };
            net.Layers[1].Neurons[1].Weights = new List<double> { 1.2, 2.2, 3.2 };
            net.Layers[1].Neurons[2].Weights = new List<double> { 1.3, 2.3, 3.3 };

            net.Layers[2].Neurons[0].Weights = new List<double> { 3.1, 3.2, 3.3 };
            net.Layers[2].Neurons[1].Weights = new List<double> { 3.11, 3.22, 3.33 };
            net.Layers[2].Neurons[2].Weights = new List<double> { 3.111, 3.222, 3.333 };
            //for (int i = 0; i < 3; ++i)
            //{
            //    for(int j = 0; j < 3; ++j)
            //    {
            //        net.Layers[0].Neurons[j].SetAllWeights(new List<double> { i + 0.1 });
            //        net.Layers[1].Neurons[j].SetAllWeights(new List<double> { i + 1.1, i + 1.2, i + 1.3 });
            //        net.Layers[2].Neurons[j].SetAllWeights(new List<double> { i + 3.1, i + 3.2, i + 3.3 });
            //    }

            //}
            var f = 5;

        }
    }
}