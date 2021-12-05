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

            return Layers[Layers.Count-1].GetAllSignals();//TODO return max value?
        }

        public void SetWeights()
        {
            Layers[0].Neurons[0].Weights.Add(1);
            Layers[0].Neurons[1].Weights.Add(1);
            //
            Layers[1].Neurons[0].Weights.Add(0.5);
            Layers[1].Neurons[0].Weights.Add(0.9);
                                                
            Layers[1].Neurons[1].Weights.Add(0.6);
            Layers[1].Neurons[1].Weights.Add(0.2);
                                                
            Layers[1].Neurons[2].Weights.Add(0.8);
            Layers[1].Neurons[2].Weights.Add(0.1);
            //                                  
            Layers[2].Neurons[0].Weights.Add(0.1);
            Layers[2].Neurons[0].Weights.Add(0.2);
            Layers[2].Neurons[0].Weights.Add(0.3);
                                                
            Layers[2].Neurons[1].Weights.Add(0.5);
            Layers[2].Neurons[1].Weights.Add(0.4);
            Layers[2].Neurons[1].Weights.Add(0.1);
            //                                  
            Layers[3].Neurons[0].Weights.Add(0.8);
            Layers[3].Neurons[0].Weights.Add(0.9);
        }

        public void SetRandomWeights()
        {
            Random rnd = new Random();
            //set input layers weights
            for(int i = 0; i < Layers[0].Neurons.Count; ++i)
            {
                Layers[0].Neurons[i].Weights.Add(1);
            }

            //set other layers weights
            for(int layer = 1; layer < Layers.Count; ++layer)
            {
                for(int neuron = 0; neuron < Layers[layer].Neurons.Count; ++neuron)
                {
                    for(int weight = 0; weight < Layers[layer].Neurons[neuron].InputNeurons.Count; ++weight)
                    {
                        Layers[layer].Neurons[neuron].Weights.Add(rnd.NextDouble());
                    }
                }
            }
        }

        public void LearnNetwork(double convergenceStep, List<List<double>> testData)
        {
            for(int i = 0; i < testData.Count; ++i)
            {
                Learn(convergenceStep, testData[i]);
            }
        }

        public void Learn(double convergenceStep, List<double> testData)
        {
            SetRandomWeights();
            var eps = 0.0;

            List<double> inputList = new List<double>();
            for (int i = 0; i < testData.Count - 1; ++i)
            {
                inputList.Add(testData[i]);
            }

            FeedForward(inputList);
            

            double localGradient = 0;
            do
            {
                for (int i = Layers.Count - 1; i >= 0; --i)
                {
                    for (int j = 0; j < Layers[i].Neurons.Count; ++j)
                    {
                        if (i == Layers.Count - 1)//rework if last layer have more then 1 neuron(need to add weighted sum in this case)
                        {
                            eps = Layers[i].Neurons[j].OutputSignal - testData.Last();// calculate epsilon
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

                            for (int n = 0; n < Layers[i + 1].Neurons[j].Weights.Count; ++n)//maybe need to rework, rework this method, move to Neuron class impossible?
                            {
                                wSum += Layers[i + 1].Neurons[n].LocalGradient * Layers[i + 1].Neurons[n].Weights[j];
                            }

                            for (int wn = 0; wn < Layers[i].Neurons[j].InputNeurons.Count; ++wn)//wn is weight number
                            {
                                Layers[i].Neurons[j].Weights[wn] -= convergenceStep * Layers[i].Neurons[j].LocalGradient * Layers[i].Neurons[j].InputNeurons[wn].OutputSignal;
                            }
                        }


                    }
                }
            } while (eps > 0.05);



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
