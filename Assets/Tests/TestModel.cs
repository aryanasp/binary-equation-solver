using DefaultNamespace.EquationParser;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GeneticAlgorithm.Core
{
    [CreateAssetMenu(fileName = "GeneticCoreTests", menuName = "Tests/StructureTest", order = 0)]
    public class TestModel : ScriptableObject
    {
        public string input = "3*4+ 2*5=";
        [Button] 
        public void DoTests()
        {
            ParserMemory.ResetMemory();
            var compiler = new EquationCompiler(input);
            compiler.Lex();
            var res = compiler.Compile();
            Debug.Log(res);
            ParserMemory.ResetMemory();
            compiler = null;
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