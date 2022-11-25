using System;
using GeneticAlgorithm.Core;
using GeneticAlgorithm.Core.Evaluators;

namespace DefaultNamespace
{
    public class CustomEvaluator : IEvaluator
    {
        public int EvaluateChromosome(ChromosomeModel chromosomeModel)
        {
            var data = chromosomeModel.Data;
            var x = data[0].Value;
            var y = data[1].Value;
            var z = data[2].Value;
            var w = data[3].Value;
            return 0 - (int)Math.Abs((17) + (Math.Pow(x, 3)) + (Math.Pow(x, 3) * Math.Pow(y, 2)) - (2 * Math.Pow(y, 3) * Math.Pow(z, 2)) - (19 * w * Math.Pow(x, 2)));
        }

        public int GetOptimumValueToStopSooner()
        {
            return 0;
        }
    }
}