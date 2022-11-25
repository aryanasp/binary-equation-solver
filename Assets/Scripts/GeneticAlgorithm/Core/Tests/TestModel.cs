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
        
        private void Test2LinqSelect()
        {
            var chrmsmA = new ChromosomeModel(new List<GenomeModel>
            {
                new GenomeModel("X", 5),
                new GenomeModel("Y", 10),
                new GenomeModel("Z", 15),
                new GenomeModel("W", 20),
            });
            chrmsmA.SetScore(0);
            var chrmsmB = new ChromosomeModel(new List<GenomeModel>
            {
                new GenomeModel("X", -5),
                new GenomeModel("Y", -10),
                new GenomeModel("Z", -15),
                new GenomeModel("W", -20),
            });
            chrmsmB.SetScore(0);
            var i = new List<ChromosomeModel>();
            i.Add(chrmsmA);
            i.Add(chrmsmB);
            var x = new Evaluator();
            var score1 = i[0].Score;
            i.Select(Container.Evaluator.EvaluateChromosome).ToList();
            var score2 = i[0].Score;
            if (score1 == score2)
            {
                Debug.Log($"<color=#FF0000> Test2 Failed. </color>");
                return;
            }
            Debug.Log($"<color=#00FF00> Test2 Successfully Passed. </color>");
        }
    }
}