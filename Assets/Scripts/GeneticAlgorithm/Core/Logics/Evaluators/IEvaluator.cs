namespace GeneticAlgorithm.Core.Evaluators
{
    public interface IEvaluator
    {
        ChromosomeModel EvaluateChromosome(ChromosomeModel chromosomeModel);
    }
}