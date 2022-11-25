using System.Collections.Generic;
using System.Linq;
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
            Test1ChangingOneGeneValuesShouldNotChangeOthers();
            Test2LinqSelect();
        }
        
        private void Test1ChangingOneGeneValuesShouldNotChangeOthers()
        {
        }
        
        private void Test2LinqSelect()
        {
        }
    }
}