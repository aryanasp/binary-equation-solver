using System.Collections.Generic;
using UnityEngine;

namespace GeneticCore
{
    public class ChromosomeModel
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
    }
}