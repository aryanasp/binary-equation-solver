using System;

namespace GeneticAlgorithm.Core.Evaluators
{
    public class Evaluator : IEvaluator
    {
        public Evaluator()
        {
            
        }
        
        //TODO: Implement The Fitness Function
        public int EvaluateChromosome(ChromosomeModel chromosomeModel)
        {
            var random = new Random();
            var score = random.Next(100);
            return score;
        }

        public int GetOptimumValueToStopSooner()
        {
            throw new NotImplementedException();
        }
    }
}