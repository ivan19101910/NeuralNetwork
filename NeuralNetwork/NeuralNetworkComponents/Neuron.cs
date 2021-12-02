using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNetworkComponents
{
    public class Neuron
    {
        public List<Neuron> inputNeurons { get; set; }
        public double weight { get; set; }

        public double sigmoidFunction(double weights) => (double)1 / (1 + Math.Pow(Math.E, -weights));

        public void CalculateAndSetWeight()
        {
            double sum = 0;

            foreach(Neuron neuron in inputNeurons)
            {
                sum += neuron.weight;
            }

            weight = sigmoidFunction(sum);
        }
    }
}
