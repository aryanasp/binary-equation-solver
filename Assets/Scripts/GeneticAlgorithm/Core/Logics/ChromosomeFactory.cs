using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace GeneticAlgorithm.Core
{
    public static class ChromosomeFactory
    {
        public static ChromosomeModel CreateRandomChromosome(int genesCount)
        {
            List<GenomeModel> genes = new List<GenomeModel>();
            for (int i = 0; i < genesCount; i++)
            {
                genes.Add(new GenomeModel((short)UnityEngine.Random.Range(0, 2)));
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
            foreach (var gene in chromosomeModel.Data)
            {
                var chanceToMutate = UnityEngine.Random.Range(0f, 1f);
                if (chanceToMutate > genomeMutationChance)
                {
                    gene.Value = (short)(1 - gene.Value);
                }
            }
        }

        private static GenomeModel CloneGenome(GenomeModel genome)
        {
            return new GenomeModel(genome.Value);
        }
    }
}