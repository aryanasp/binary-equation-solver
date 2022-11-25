using System;

namespace GeneticAlgorithm.Core.Evaluators
{
    public class Evaluator : IEvaluator
    {
        public Evaluator()
        {
            if (Container.Evaluator != null)
            {
                throw new Exception("There Is Two Evaluator Please Destroy One Of Them!");
            }
            Container.InjectEvaluator(this);
        }
        
        //TODO: Implement The Fitness Function
        public ChromosomeModel EvaluateChromosome(ChromosomeModel chromosomeModel)
        {
            var random = new Random();
            var score = random.Next(100);
            chromosomeModel.Score = score;
            return chromosomeModel;
        }
    }
}