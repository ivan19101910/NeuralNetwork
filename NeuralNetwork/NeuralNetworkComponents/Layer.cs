using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.NeuralNetworkComponents
{
    public class Layer
    {
        public List<Neuron> Neurons { get; set; }

        public Layer(List<Neuron> neurons)
        {
            Neurons = neurons;
        }
        public Layer()
        {

        }
    }
}
