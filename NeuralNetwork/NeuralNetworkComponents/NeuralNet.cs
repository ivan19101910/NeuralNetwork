using System;
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
        public List <Layer> Layers { get; set; }

        //List<Neuron> InputLayer { get; set; }
        //List<Layer> HiddenLayers { get; set; }
        //List<Neuron> OutputLayer { get; set; }

        public int InputCount { get; }

        public int OutputCount { get; }

        public List<int> HiddenLayersCount { get; }

        public List<double> FeedForward(List<double> inputs)
        {
            //feeding signals to input layer
            for(int i = 0; i < inputs.Count; ++i)
            {
                var signal = new List<double>() { inputs[i] };
                var neuron = Layers[0].Neurons[i];

                neuron.FeedForward(signal);
            }


            //feeding to next layers
            
            for(int i = 1; i < Layers.Count; ++i)
            {
                var prevLayerSignals = Layers[i - 1].GetAllSignals();

                foreach(Neuron neuron in Layers[i].Neurons)
                {
                    neuron.FeedForward(prevLayerSignals);
                }
            }

            return Layers[Layers.Count].GetAllSignals();//TODO return max value?
        }

        public void Learn(double convergenceStep, List<double> testData)
        {
            
            double localGradient = 0;
            for (int i = Layers.Count - 1; i >= 0; ++i)
            {
                for(int j = 0; j < Layers[i].Neurons.Count; ++j)
                {                   
                    if(i == Layers.Count - 1)//rework if last layer have more then 1 neuron(need to add weighted sum in this case)
                    {
                        var eps = Layers[i].Neurons[j].OutputSignal - testData.Last();// calculate epsilon
                        localGradient = Layers[i].Neurons[j].FindLocalGradientLast(eps);//gradient for last

                        for (int wn = 0; wn < Layers[i].Neurons[j].InputNeurons.Count; ++wn)//wn is weight number
                        {
                            Layers[i].Neurons[j].Weights[wn] -= convergenceStep * eps * Layers[i].Neurons[j].InputNeurons[wn].OutputSignal;
                        }
                    }                  
                    else
                    {
                        //maybe rework for prev 
                        var prevLocalGradient = Layers[i + 1].Neurons[j].LocalGradient;
                        var prevWeight = Layers[i + 1].Neurons[j].Weights[j];
                        localGradient = Layers[i].Neurons[j].FindLocalGradient(prevLocalGradient, prevWeight);
                        //TODO for weighted sums
                        double wSum = 0;

                        for (int n = 0; n < Layers[i+1].Neurons[j].Weights.Count; ++n)//maybe need to rework, rework this method, move to Neuron class impossible?
                        {
                            wSum += Layers[i+1].Neurons[n].LocalGradient * Layers[i + 1].Neurons[n].Weights[j];
                        }

                        for (int wn = 0; wn < Layers[i].Neurons[j].InputNeurons.Count; ++wn)//wn is weight number
                        {
                            

                            Layers[i].Neurons[j].Weights[wn] -= convergenceStep * Layers[i].Neurons[j].LocalGradient * Layers[i].Neurons[j].InputNeurons[wn].OutputSignal;
                        }
                    }

                    
                }
            }

            
        }

        public void ConfigureNeuralNetwork()
        {
            Layers = new List<Layer>();
            Layers.Add(new Layer());
            Layers[0].Neurons = new List<Neuron>();
            for (int i = 0; i < InputCount; ++i)
            {
                Layers[0].Neurons.Add(new Neuron());
            }

            //HiddenLayers = new List<Layer>(HiddenLayersCount.Count);
            for(int i = 0; i < HiddenLayersCount.Count; ++i)
            {
                var prevLayer = Layers.Last();
                Layers.Add(new Layer());
                Layers[i + 1].Neurons = new List<Neuron>();
                for (int j = 0; j < HiddenLayersCount[i]; ++j)
                {
                    Layers[i + 1].Neurons.Add(new Neuron(prevLayer.Neurons));
                }
            }

            var lastLayer = Layers.Last();
            Layers.Add(new Layer());
            Layers[Layers.Count - 1].Neurons = new List<Neuron>();
            for (int i = 0; i < OutputCount; ++i)
            {
                Layers[Layers.Count - 1].Neurons.Add(new Neuron(lastLayer.Neurons));
                //Layer.Add(new Neuron(lastLayer.Neurons));
            }
        }

        
    }
}
