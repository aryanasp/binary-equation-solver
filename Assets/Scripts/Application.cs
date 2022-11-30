using System;
using System.Collections.Generic;
using DefaultNamespace.EquationParser;
using GeneticAlgorithm.Core;
using GeneticAlgorithm.Core.Evaluators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Application : MonoBehaviour, IEvaluator
    {

        public TMP_InputField inputField;
        public TextMeshProUGUI resultText;

        private EquationCompiler _equationCompiler;
        
        public void FindButton()
        {
            ResetApplication();
            InitializeCompiler();
            var tags = _equationCompiler.FindParameters();
            var geneticAlgorithm = new GeneticAlgorithm.GeneticAlgorithm(tags.Count, tags.Count, this);
            var results = geneticAlgorithm.RunAlgorithm();
            DisplayOutputs(results);
        }

        private void DisplayOutputs(List<short> results)
        {
            var index = 0;
            resultText.text = "";
            foreach (var elements in ParserMemory.Parameters)
            {
                elements.Value.Value = results[index];
                resultText.text += $"{elements.Value.Tag}: {elements.Value.Value}, ";
            }
        }

        private void InitializeCompiler()
        {
            var input = inputField.text;
            _equationCompiler = new EquationCompiler(input);
            _equationCompiler.Lex();
        }

        private void ResetApplication()
        {
            ParserMemory.ResetMemory();
            _equationCompiler = null;
        }

        public int EvaluateChromosome(ChromosomeModel chromosomeModel)
        {
            var data = chromosomeModel.Data;
            int index = 0;
            foreach (var elements in ParserMemory.Parameters)
            {
                elements.Value.Value = data[index].Value;
                index++;
            }
            _equationCompiler.ResetCompileMemory();
            return 0 - Math.Abs(_equationCompiler.Compile());
        }

        public int GetOptimumValueToStopSooner()
        {
            return 0;
        }
    }
}