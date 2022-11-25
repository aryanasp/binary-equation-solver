using System;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm.Core
{
    public class ChromosomeComparer : IComparer<ChromosomeModel>
    {
        public ChromosomeComparer()
        {
            
        }
        public int Compare(ChromosomeModel x, ChromosomeModel y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.Score.CompareTo(y.Score);
        }
    }
    public class ChromosomeModel : IComparable<ChromosomeModel>
    {
        public readonly List<GenomeModel> Data;
        public int Score;
        
        public ChromosomeModel(List<GenomeModel> genomeModels)
        {
            Data = new List<GenomeModel>();
            foreach (var genome in genomeModels)
            {
                Data.Add(genome);
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
                line += $"{pair.Value}, ";
            }
            Debug.Log(line);
        }

        public int CompareTo(ChromosomeModel other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Score.CompareTo(other.Score);
        }
    }
}