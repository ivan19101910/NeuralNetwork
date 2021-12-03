using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNetworkComponents
{
    public class Neuron
    {
        public List<Neuron> InputNeurons { get; set; }
        public double Weight { get; set; }

        public double SigmoidFunction(double weightsSum) => 1 / (1 + Math.Pow(Math.E, -weightsSum));

        public Neuron(List<Neuron> inputNeurons, double weight)
        {
            InputNeurons = inputNeurons;
            Weight = weight;
        }
        public Neuron(List<Neuron> inputNeurons)
        {
            InputNeurons = inputNeurons;
        }
        public Neuron()
        {

        }

        public Neuron(double weight)
        {
            Weight = weight;
        }

        //public void CalculateAndSetWeight()
        //{
        //    double sum = 0;

        //    foreach(Neuron neuron in InputNeurons)
        //    {
        //        sum += neuron.Weight;
        //    }

        //    Weight = SigmoidFunction(sum);
        //}
        public double FeedForward(List<double> inputs)
        {
            var sum = 0.0;

            for(int i =0; i < inputs.Count; ++i)
            {
                sum += inputs[i] * InputNeurons[i].Weight;
            }

            return SigmoidFunction(sum);
        }
    }
}
