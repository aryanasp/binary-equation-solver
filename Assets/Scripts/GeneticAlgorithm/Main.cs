using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Core;
using GeneticAlgorithm.Core.Evaluators;

namespace GeneticAlgorithm
{
    [System.Serializable]
    public class GeneticAlgorithmInfo
    {
        public int pairFactor = 1;
        public float crossOverOffset = 0.5f;
        public float mutationChance = 0.1f;
        public float genomeMutateChance = 0.5f;
    }
    public class GeneticAlgorithm
    {
        public GeneticAlgorithm(int chromosomesGeneCount, int initialPeopleCount, IEvaluator evaluator)
        {
            ResetContainer();
            var evaluatorController = new EvaluatorController(evaluator);
            var populationManager = new PopulationManager(chromosomesGeneCount, initialPeopleCount);
        }

        private void ResetContainer()
        {
            Container.EvaluatorController = null;
            Container.PopulationManager = null;
        }

        public GeneticAlgorithm SetGeneticAlgorithmStructuralParameters(GeneticAlgorithmInfo info)
        {
            Container.PopulationManager.SetPairFactor(info.pairFactor)
                .SetCrossOverOffset(info.crossOverOffset)
                .SetMutationChance(info.mutationChance)
                .SetGenomeMutationChance(info.genomeMutateChance);
            return this;
        }

        public List<short> RunAlgorithm()
        {
            var winnerChromosome = Container.PopulationManager.Build();
            winnerChromosome.PrintData();
            return winnerChromosome.Data.Select(item => item.Value).ToList();
        }
    }
}