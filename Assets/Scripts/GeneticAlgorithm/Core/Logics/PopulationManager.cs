using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace GeneticAlgorithm.Core
{
    public class PopulationManager
    {
        private SortedSet<ChromosomeModel> _population;
        private HashSet<ChromosomeModel> _deadChromosomesPool;
        private int _societySize;

        private int _pairFactor = 1;
        private float _crossOverOffset = 0.5f;
        private float _mutationChance = 0.1f;
        private float _genomeMutateChance = 0.5f;

        public PopulationManager(int chromosomesGeneCount, int peopleCount)
        {
            if (Container.PopulationManager != null)
            {
                throw new Exception("There Is Two Population Manager Please Destroy One Of Them!");
            }

            Container.InjectPopulationManager(this);
            _societySize = peopleCount;
            _population = new SortedSet<ChromosomeModel>(new ChromosomeComparer());
            _deadChromosomesPool = new HashSet<ChromosomeModel>();
            Debug.Log("Society: ");
            while (_population.Count < _societySize)
            {
                var newChromosome = ChromosomeFactory.CreateRandomChromosome(chromosomesGeneCount);
                //This part should replace with fitness function
                Container.EvaluatorController.EvaluateChromosome(newChromosome);
                newChromosome.PrintData();
                _population.Add(newChromosome);
            }

            Debug.Log("Max: ");
            _population.Max.PrintData();
            Debug.Log("Min: ");
            _population.Min.PrintData();
            Debug.Log($"Population: {_population.Count}");
        }

        public PopulationManager SetPairFactor(int pairFactor)
        {
            if (pairFactor > (int)(_societySize / 2))
            {
                throw new Exception("[PopulationManager]: Count Of Pairs Cant Be More Than Half Size Of Society.");
            }

            _pairFactor = pairFactor;
            return this;
        }

        public PopulationManager SetMutationChance(float mutationChance)
        {
            if (mutationChance >= 1f)
            {
                throw new Exception("[PopulationManager]: Chance Cant Be More Than 100%.");
            }

            _mutationChance = mutationChance;
            return this;
        }

        public PopulationManager SetGenomeMutationChance(float genomeMutationChance)
        {
            if (genomeMutationChance >= 1f)
            {
                throw new Exception("[PopulationManager]: Chance Cant Be More Than 100%.");
            }

            _genomeMutateChance = genomeMutationChance;
            return this;
        }

        public PopulationManager SetCrossOverOffset(float crossOverOffset)
        {
            if (crossOverOffset >= 1f)
            {
                throw new Exception("[PopulationManager]: Offset Cant Be More Than 100%.");
            }

            _crossOverOffset = crossOverOffset;
            return this;
        }

        public ChromosomeModel Build()
        {
            while (true)
            {
                Debug.Log($"Population: {_population.Count}");
                var selects = Select();
                var children = CrossOver(selects);
                var result = Mutate(children);
                var evaluatedChildren = EvaluateFitnessFunction(result);
                if (AllNewChildrenWereInDeadPool(evaluatedChildren))
                {
                    break;
                }

                var newAddedChromosomes = AddToPopulation(evaluatedChildren);
                for (int i = 0; i < newAddedChromosomes; i++)
                {
                    KillWeakerChromosome();
                }
            }

            return _population.Max;
        }

        private bool AllNewChildrenWereInDeadPool(List<ChromosomeModel> newGeneration)
        {
            if (_deadChromosomesPool.Count == 0)
            {
                return false;
            }

            var allWereInDeadPool = true;
            foreach (var child in newGeneration)
            {
                var isChildRepeat = false;
                foreach (var deadAncestors in _deadChromosomesPool)
                {
                    if (child.EqualWith(deadAncestors))
                    {
                        isChildRepeat = true;
                    }
                }

                allWereInDeadPool = allWereInDeadPool && isChildRepeat;
            }

            return allWereInDeadPool;
        }

        private int AddToPopulation(List<ChromosomeModel> evaluatedChildren)
        {
            var populationCount = _population.Count;
            foreach (var child in evaluatedChildren)
            {
                _population.Add(child);
            }

            return _population.Count - populationCount;
        }

        private void KillWeakerChromosome()
        {
            var min = _population.Min;
            _deadChromosomesPool.Add(min);
            var performed = _population.Remove(min);
            if (!performed)
            {
                throw new Exception("Can't Delete Item From Sorted List!");
            }
        }

        private List<ChromosomeModel> EvaluateFitnessFunction(List<ChromosomeModel> result)
        {
            return result.Select(Container.EvaluatorController.EvaluateChromosome).ToList();
        }

        private List<ChromosomeModel> Mutate(List<ChromosomeModel> children)
        {
            var random = new Random();
            foreach (var child in children.Where(child => random.NextDouble() > _mutationChance))
            {
                child.MutateChromosome(_genomeMutateChance);
            }

            return children;
        }

        private List<ChromosomeModel> CrossOver(List<Tuple<ChromosomeModel, ChromosomeModel>> selects)
        {
            var children = new List<ChromosomeModel>();
            foreach (var tuple in selects)
            {
                var (childA, childB) =
                    ChromosomeFactory.CreateOffSpringChromosomes(tuple.Item1, tuple.Item2, _crossOverOffset);
                children.Add(childA);
                children.Add(childB);
            }

            return children;
        }

        private List<Tuple<ChromosomeModel, ChromosomeModel>> Select()
        {
            var newSortedList = ShallowClonePopulation();
            var outputList = new List<Tuple<ChromosomeModel, ChromosomeModel>>();
            for (int i = 0; i < _pairFactor; i++)
            {
                var parent1 = newSortedList.Max;
                newSortedList.Remove(newSortedList.Max);
                var parent2 = newSortedList.Max;
                newSortedList.Remove(newSortedList.Max);
                var tuple = new Tuple<ChromosomeModel, ChromosomeModel>(parent1, parent2);
                outputList.Add(tuple);
            }

            return outputList;
        }

        private SortedSet<ChromosomeModel> ShallowClonePopulation()
        {
            var newSortedList = new SortedSet<ChromosomeModel>();
            foreach (var chromosomeModel in _population)
            {
                newSortedList.Add(chromosomeModel);
            }

            return newSortedList;
        }
    }
}