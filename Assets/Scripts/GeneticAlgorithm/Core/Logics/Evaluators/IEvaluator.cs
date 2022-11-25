namespace GeneticAlgorithm.Core.Evaluators
{
    public interface IEvaluator
    {
        int EvaluateChromosome(ChromosomeModel chromosomeModel);
        int GetOptimumValueToStopSooner();
    }
}