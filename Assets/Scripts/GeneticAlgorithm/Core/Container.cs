using GeneticAlgorithm.Core.Evaluators;

namespace GeneticAlgorithm.Core
{
    public static class Container
    {
        public static EvaluatorController EvaluatorController;
        public static PopulationManager PopulationManager;

        public static void InjectPopulationManager(PopulationManager populationManager)
        {
            PopulationManager = populationManager;
        }

        public static void InjectEvaluatorController(EvaluatorController evaluatorController)
        {
            EvaluatorController = evaluatorController;
        }
    }
}