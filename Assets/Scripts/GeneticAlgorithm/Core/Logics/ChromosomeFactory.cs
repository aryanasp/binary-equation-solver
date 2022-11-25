using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using Random = System.Random;

namespace GeneticAlgorithm.Core
{
    public static class ChromosomeFactory
    {
        public static ChromosomeModel CreateRandomChromosome(int genesCount)
        {
            List<GenomeModel> genes = new List<GenomeModel>();
            // var seedRandom = new Random(2);
            var randomGenerator = new Random();
            for (int i = 0; i < genesCount; i++)
            {
                // var randomValue = Math.Abs(seedRandom.Next(0, 2) - randomGenerator.Next(0, 2));
                // Generate a gene with value 0 or 1 with passed tag
                genes.Add(new GenomeModel((short)randomGenerator.Next(0, 2)));
                Thread.Sleep(25);
            }
            return new ChromosomeModel(genes);
        }
        
        public static (ChromosomeModel offSpringA, ChromosomeModel offSpringB) CreateOffSpringChromosomes(
            ChromosomeModel parentA, ChromosomeModel parentB, float crossOverOffset = 0.5f)
        {
            var genomesOfParentA = parentA.Data;
            var genomesOfParentB = parentB.Data;
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
            foreach (var gene in chromosomeModel.Data)
            {
                var chanceToMutate = random.NextDouble();
                if (chanceToMutate > genomeMutationChance)
                {
                    gene.Value = (short)(1 - gene.Value);
                }
                Thread.Sleep(25);
            }
        }

        private static GenomeModel CloneGenome(GenomeModel genome)
        {
            return new GenomeModel(genome.Value);
        }
    }
}