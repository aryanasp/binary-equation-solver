using GeneticAlgorithm.Core.Evaluators;

namespace GeneticAlgorithm.Core
{
    public static class Container
    {
        public static IEvaluator Evaluator;
        public static PopulationManager PopulationManager;

        public static void InjectPopulationManager(PopulationManager populationManager)
        {
            PopulationManager = populationManager;
        }

        public static void InjectEvaluator(IEvaluator evaluator)
        {
            Evaluator = evaluator;
        }
    }
}