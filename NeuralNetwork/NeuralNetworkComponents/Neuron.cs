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
        //public double Weight { get; set; }
        public List<double> Weights { get; set; }

        public double SigmoidFunction(double x) => 1 / (1 + Math.Pow(Math.E, -x));
        public double SigmoidFunctionDerivative(double x) => (1 / (1 + Math.Pow(Math.E, -x))) * (1 - (1 / (1 + Math.Pow(Math.E, -x))));
        public double OutputSignal { get; set; }
        public double LocalGradient { get; set; }

        public double FindLocalGradientLast(double eps)
        {
            //var localGradient = eps * SigmoidFunctionDerivative(InputsSum());//may cause perfomance problem?
            var localGradient = eps * OutputSignal * (1 - OutputSignal);
            LocalGradient = localGradient;
            return localGradient;
        }

        public double FindLocalGradient(double prevLocalGradient, double prevWeight)
        {
            //var localGradient = eps * SigmoidFunctionDerivative(InputsSum());//may cause perfomance problem?
            var localGradient = prevLocalGradient * prevWeight * OutputSignal;
            LocalGradient = localGradient;
            return localGradient;
        }
        public Neuron(List<Neuron> inputNeurons)
        {
            InputNeurons = inputNeurons;
            Weights = new List<double>();
        }
        public Neuron()
        {
            Weights = new List<double>();
        }

        public void SetAllWeights(List<double> weights)
        {
            for (int i = 0; i < weights.Count; ++i)
            {
               Weights.Add(weights[i]);
            }
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



        /// <summary>
        /// Use after at least one step
        /// ONLY for learning
        /// </summary>

        public double InputsSum()
        {
            var sum = 0.0;

            for (int i = 0; i < InputNeurons.Count; ++i)
            {
                sum += InputNeurons[i].OutputSignal;
            }

            return sum;
        }

        public double FeedForward(List<double> inputs)
        {
            var sum = 0.0;

            for(int i =0; i < inputs.Count; ++i)
            {
                sum += inputs[i] * Weights[i];
            }
            OutputSignal = SigmoidFunction(sum);
            return OutputSignal;
        }
    }
}
