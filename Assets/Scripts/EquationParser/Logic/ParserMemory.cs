using System;
using System.Collections.Generic;

namespace DefaultNamespace.EquationParser
{
    public static class ParserMemory
    {
        private static readonly Dictionary<string, VariableModel> Parameters;
        private static readonly Dictionary<string, int> CompilersId;
        private static readonly List<EquationCompiler> Compilers;
        private static int _size;

        static ParserMemory()
        {
            _size = 0;
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
            CompilersId[subEquation] = _size;
            Compilers.Add(new EquationCompiler(subEquation));
            Compilers[_size].Lex();
            return _size++;
        }

        public static EquationCompiler GetCompilerWithId(int id)
        {
            if (id >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Compilers[id];
        }

        public static void ResetCompilers()
        {
            for (int i = 0; i < _size; i++)
            {
                Compilers[i].ResetCompileMemory();
            }
        }
    }
}