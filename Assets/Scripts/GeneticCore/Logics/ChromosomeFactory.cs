using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticCore.Logics
{
    public static class ChromosomeFactory
    {
        public static (ChromosomeModel offSpringA, ChromosomeModel offSpringB) CreateOffSpringChromosomes(ChromosomeModel parentA, ChromosomeModel parentB, float crossOverOffset = 0.5f)
        {
            var genomesOfParentA = parentA.Data.Values.ToList();
            var genomesOfParentB = parentB.Data.Values.ToList();
            if (genomesOfParentA.Count != genomesOfParentB.Count)
            {
                throw new Exception("[ChromosomeFactory]: Size Of Chromosomes Are Not Equal.");
            }

            var sizeOfChromosomes = genomesOfParentA.Count;
            var crossOverPoint = (int) (crossOverOffset * sizeOfChromosomes);

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

        private static GenomeModel CloneGenome(GenomeModel genome)
        {
            return new GenomeModel(genome.Tag, genome.Value);
        }
    }
}