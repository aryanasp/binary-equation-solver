using System;
using System.Collections.Generic;

namespace DefaultNamespace.EquationParser
{
    public static class ParserMemory
    {
        private static Dictionary<string, VariableModel> Parameters;
        private static Dictionary<string, int> CompilersId;
        private static List<EquationCompiler> Compilers;

        static ParserMemory()
        {
            Parameters = new Dictionary<string, VariableModel>();
            CompilersId = new Dictionary<string, int>();
            Compilers = new List<EquationCompiler>();
        }

        public static void AddParameter(string tag)
        {
            Parameters[tag] = new VariableModel
            {
                Tag = tag,
                Value = 0
            };
        }

        public static void SetValueToParameter(string tag, int value)
        {
            Parameters[tag].Value = value;
        }
        
        public static int GetParameter(string parameterTag)
        {
            return Parameters[parameterTag].Value;
        }

        public static int AssignId(string subEquation)
        {
            if (CompilersId.ContainsKey(subEquation))
            {
                return CompilersId[subEquation];
            }
            Compilers.Add(new EquationCompiler(subEquation));
            CompilersId[subEquation] = Compilers.Count - 1;
            var index = Compilers.Count - 1;
            Compilers[Compilers.Count - 1].Lex();
            return index;
        }

        public static EquationCompiler GetCompilerWithId(int id)
        {
            if (id >= Compilers.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Compilers[id];
        }
        
        
        public static void ResetMemory()
        {
            ResetLexers();
            ResetCompilers();
            Parameters = new Dictionary<string, VariableModel>();
            CompilersId = new Dictionary<string, int>();
            Compilers = new List<EquationCompiler>();
        }
        
        public static void ResetLexers()
        {
            for (int i = 0; i < Compilers.Count; i++)
            {
                Compilers[i].ResetCompileMemory();
            }
        }
        
        public static void ResetCompilers()
        {
            for (int i = 0; i < Compilers.Count; i++)
            {
                Compilers[i].ResetCompileMemory();
            }
        }
    }
}