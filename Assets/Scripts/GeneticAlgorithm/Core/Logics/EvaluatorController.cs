using System;
using GeneticAlgorithm.Core.Evaluators;

namespace GeneticAlgorithm.Core
{
    public class EvaluatorController
    {
        public readonly IEvaluator Evaluator;
        public EvaluatorController(IEvaluator evaluator)
        {
            if (Container.EvaluatorController != null)
            {
                throw new Exception("There Is Two EvaluatorController Please Destroy One Of Them!");
            }
            Container.InjectEvaluatorController(this);
            Evaluator = evaluator;
        }

        public ChromosomeModel EvaluateChromosome(ChromosomeModel chromosomeModel)
        {
            chromosomeModel.Score = Evaluator.EvaluateChromosome(chromosomeModel);
            return chromosomeModel;
        }
    }
}