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
        public void Test1()
        {
            var neuronList = new List<Neuron>();
            neuronList.Add(new Neuron { weight = 1 });
            neuronList.Add(new Neuron { weight = 2 });
            neuronList.Add(new Neuron { weight = 3 });

            var neuron = new Neuron { weight = 5, inputNeurons = neuronList};

            neuron.CalculateAndSetWeight();

            Assert.GreaterOrEqual(neuron.weight, 0);
            Assert.LessOrEqual(neuron.weight, 1);

        }
    }
}