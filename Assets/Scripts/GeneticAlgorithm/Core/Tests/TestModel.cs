using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using GeneticAlgorithm.Core.Evaluators;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GeneticAlgorithm.Core
{
    [CreateAssetMenu(fileName = "GeneticCoreTests", menuName = "Tests/StructureTest", order = 0)]
    public class TestModel : ScriptableObject
    {
        [Button]
        public void DoTests()
        {
            var geneticAlgorithm = new GeneticAlgorithm(4, 4, new CustomEvaluator());
            var result = geneticAlgorithm.RunAlgorithm();
            
        }

        [Button]
        public void ResetContainer()
        {
            Container.EvaluatorController = null;
            Container.PopulationManager = null;
        }

        private void Test1ChangingOneGeneValuesShouldNotChangeOthers()
        {
        }
        
        private void Test2LinqSelect()
        {
        }
    }
}