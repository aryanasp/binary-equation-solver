using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm.Core
{
    public class ChromosomeModel : IComparer<ChromosomeModel>
    {
        public readonly Dictionary<string, GenomeModel> Data;
        public int Score;

        public ChromosomeModel(List<GenomeModel> genomeModels)
        {
            Data = new Dictionary<string, GenomeModel>();
            foreach (var genome in genomeModels)
            {
                Data[genome.Tag] = genome;
            }
        }

        public void SetScore(int score)
        {
            Score = score;
        }

        public void PrintData()
        {
            var line = "";
            foreach (var pair in Data)
            {
                line += $"{pair.Key}: {pair.Value.Value}, ";
            }
            Debug.Log(line);
        }

        public int Compare(ChromosomeModel x, ChromosomeModel y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.Score.CompareTo(y.Score);
        }
    }
}