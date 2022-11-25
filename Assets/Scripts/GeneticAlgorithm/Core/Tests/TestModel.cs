using System.Collections.Generic;
using GeneticAlgorithm;
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
        }
        
        private void Test1ChangingOneGeneValuesShouldNotChangeOthers()
        {
            var chrmsmA = new ChromosomeModel(new List<GenomeModel>
            {
                new GenomeModel("X", 5),
                new GenomeModel("Y", 10),
                new GenomeModel("Z", 15),
                new GenomeModel("W", 20),
            });
            var chrmsmB = new ChromosomeModel(new List<GenomeModel>
            {
                new GenomeModel("X", -5),
                new GenomeModel("Y", -10),
                new GenomeModel("Z", -15),
                new GenomeModel("W", -20),
            });
            var (chrmsmC, chrmsmD) = ChromosomeFactory.CreateOffSpringChromosomes(chrmsmA, chrmsmB, 0.5f);
            chrmsmC.Data["X"].AddValue();
            var isChangesToo = chrmsmC.Data["X"].Value == chrmsmB.Data["X"].Value;
            if (isChangesToo)
            {
                Debug.Log($"<color=#FF0000> Test1 Failed. </color>");
                return;
            }
            Debug.Log($"<color=#00FF00> Test1 Successfully Passed. </color>");
        }
    }
}