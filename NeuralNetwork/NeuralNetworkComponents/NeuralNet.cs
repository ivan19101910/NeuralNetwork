﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNetworkComponents
{
    public class NeuralNet
    {
        public NeuralNet()
        {

        }

        public NeuralNet(int inputCount, int outputCount, params int[] layers)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            HiddenLayersCount = new List<int>();
            HiddenLayersCount.AddRange(layers);

        }
        List <Layer> Layers { get; set; }

        //List<Neuron> InputLayer { get; set; }
        //List<Layer> HiddenLayers { get; set; }
        //List<Neuron> OutputLayer { get; set; }

        public int InputCount { get; }

        public int OutputCount { get; }

        public List<int> HiddenLayersCount { get; }

        public double FeedForward(List<double> inputs)
        {

        }

        public void ConfigureNeuralNetwork(List<double> inputWeights)
        {
            Layers = new List<Layer>();
            Layers.Add(new Layer());
            Layers[0].Neurons = new List<Neuron>();
            for (int i = 0; i < InputCount; ++i)
            {
                Layers[0].Neurons.Add(new Neuron(0));
            }

            //HiddenLayers = new List<Layer>(HiddenLayersCount.Count);
            for(int i = 0; i < HiddenLayersCount.Count; ++i)
            {
                var prevLayer = Layers.Last();
                Layers.Add(new Layer());
                Layers[i + 1].Neurons = new List<Neuron>();
                for (int j = 0; j < HiddenLayersCount[i]; ++j)
                {
                    Layers[i + 1].Neurons.Add(new Neuron(prevLayer.Neurons, i+1));
                }
            }

            var lastLayer = Layers.Last();
            Layers.Add(new Layer());
            Layers[Layers.Count - 1].Neurons = new List<Neuron>();
            for (int i = 0; i < OutputCount; ++i)
            {
                Layers[Layers.Count - 1].Neurons.Add(new Neuron(lastLayer.Neurons, Layers.Count - 1));
                //Layer.Add(new Neuron(lastLayer.Neurons));
            }
        }

        
    }
}
