using System.Collections.Generic;
using System.Linq;

namespace GeneticCore.Logics
{
    public static class ChromosomeUtils
    {
        public static List<short> ExportAsList(this ChromosomeModel chromosomeModel)
        {
            return chromosomeModel.Data.Select(pair => pair.Value.Value).ToList();
        }

        public static bool AreEqual(ChromosomeModel chromosomeA, ChromosomeModel chromosomeB)
        {
            var exportA = ExportAsList(chromosomeA);
            var exportB = ExportAsList(chromosomeB);
            if (exportA.Count != exportB.Count)
            {
                return false;
            }

            return !exportA.Where((t, i) => t != exportB[i]).Any();
        }

        public static bool EqualWith(this ChromosomeModel chromosomeA, ChromosomeModel chromosomeB)
        {
            return AreEqual(chromosomeA, chromosomeB);
        }
    }
}