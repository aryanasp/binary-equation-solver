using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm.Core
{
    public static class ChromosomeFactory
    {
        public static ChromosomeModel CreateRandomChromosome(List<string> tags)
        {
            List<GenomeModel> genes = new List<GenomeModel>();
            var randomGenerator = new Random();
            for (int i = 0; i < tags.Count; i++)
            {
                // Generate a gene with value 0 or 1 with passed tag
                genes.Add(new GenomeModel(tags[i], (short)randomGenerator.Next(0, 2)));
            }
            return new ChromosomeModel(genes);
        }
        
        public static (ChromosomeModel offSpringA, ChromosomeModel offSpringB) CreateOffSpringChromosomes(
            ChromosomeModel parentA, ChromosomeModel parentB, float crossOverOffset = 0.5f)
        {
            var genomesOfParentA = parentA.Data.Values.ToList();
            var genomesOfParentB = parentB.Data.Values.ToList();
            if (genomesOfParentA.Count != genomesOfParentB.Count)
            {
                throw new Exception("[ChromosomeFactory]: Size Of Chromosomes Are Not Equal.");
            }

            var sizeOfChromosomes = genomesOfParentA.Count;
            var crossOverPoint = (int)(crossOverOffset * sizeOfChromosomes);

            var genomesOfChildA = new List<GenomeModel>();
            var genomesOfChildB = new List<GenomeModel>();

            for (int i = 0; i < crossOverPoint; i++)
            {
                genomesOfChildA.Add(CloneGenome(genomesOfParentB[i]));
                genomesOfChildB.Add(CloneGenome(genomesOfParentA[i]));
            }

            for (int i = crossOverPoint; i < sizeOfChromosomes; i++)
            {
                genomesOfChildA.Add(CloneGenome(genomesOfParentA[i]));
                genomesOfChildB.Add(CloneGenome(genomesOfParentB[i]));
            }

            var childA = new ChromosomeModel(genomesOfChildA);
            var childB = new ChromosomeModel(genomesOfChildB);
            // Children are called offSprings too
            return (childA, childB);
        }
        
        ///<summary>
        ///<para> both startIndex and endIndex are inclusive. </para>
        ///</summary>
        public static void MutateChromosome(this ChromosomeModel chromosomeModel,
            float genomeMutationChance)
        {
            var random = new Random();
            foreach (var keyPairValues in chromosomeModel.Data)
            {
                var chanceToMutate = random.NextDouble();
                if (chanceToMutate > genomeMutationChance)
                {
                    keyPairValues.Value.Value = (short)(1 - keyPairValues.Value.Value);
                }
            }
        }

        private static GenomeModel CloneGenome(GenomeModel genome)
        {
            return new GenomeModel(genome.Tag, genome.Value);
        }
    }
}