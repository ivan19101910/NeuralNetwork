using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNetworkComponents
{
    class Neuron
    {
        List<Neuron> inputNeurons { get; set; }
        double weight { get; set; }

        double sigmoidFunction(double weights) => 1 / 1 + Math.Pow(Math.E, -weights);

        void CalculateAndSetWeight()
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
