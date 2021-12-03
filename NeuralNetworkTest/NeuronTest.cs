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

            var neuron = new Neuron { Weight = 5};

            var result = neuron.SigmoidFunction(1 + 2 + 0.5);

            Assert.GreaterOrEqual(result, 0);
            Assert.LessOrEqual(result, 1);

        }
        [Test]
        public void TestBoundedNeuronCreate()
        {
            var neuronList = new List<Neuron>();
            neuronList.Add(new Neuron { Weight = 1 });

            var NeuronWithBoundedNeurons = new Neuron(neuronList, 2);
            Assert.AreSame(neuronList[0], NeuronWithBoundedNeurons.InputNeurons[0]);
            NeuronWithBoundedNeurons.InputNeurons[0].Weight = 1;
            Assert.AreEqual(1, neuronList[0].Weight);
        }

        [Test]
        public void TestNeuralNetCreation()
        {
            var net = new NeuralNet(3, 2, 4, 5, 6);
            net.ConfigureNeuralNetwork(new List<double> { 0.5, 0.6, 0.7, 0.8, 0.9});
        }
    }
}