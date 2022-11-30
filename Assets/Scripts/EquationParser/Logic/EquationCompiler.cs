using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultNamespace.EquationParser
{
    public class EquationCompiler
    {
        public static Dictionary<char, int> OperationsPriority = new Dictionary<char, int>
        {
            ['='] = 0,
            ['+'] = 1,
            ['-'] = 1,
            ['*'] = 2,
            ['/'] = 2,
            ['^'] = 3,
        };

        public static bool AnalyzeLegalExpressions(string input)
        {
            //TODO: input should not contain "$" or "#" or double operation characters like "**", etc.
            return true;
        }

        public string Input { get; private set; }

        public bool IsCompiledOnce = false;
        public int CompileAnswer = 0;

        private List<string> _lexedStringTokens;

        public EquationCompiler(string input)
        {
            Input = StandardizeInput(input);
            _lexedStringTokens = new List<string>();
        }

        private string StandardizeInput(string input)
        {
            var replaceStartToken = "";
            if ((input[0] == ' ' && input[1] == '-'))
            {
                replaceStartToken = "0";
            }

            if (input[0] == '-')
            {
                replaceStartToken = "0 ";
            }

            var replaceEndToken = "";
            var lastIndex = input.Length - 1;
            var thereIsEqualSignInTheEnd = (input[lastIndex] == ' ' && input[lastIndex - 1] == '=')
                 || (input[lastIndex] == '=');
            if (!thereIsEqualSignInTheEnd)
            {
                replaceEndToken = "=";
            }
            
            return $"{replaceStartToken}{input}{replaceEndToken}";
        }

        public void ResetCompileMemory()
        {
            IsCompiledOnce = false;
            CompileAnswer = 0;
        }
        
        public void ResetLexMemory()
        {
            _lexedStringTokens = new List<string>();
        }

        public void Lex()
        {
            var i = 0;
            var operationStacks = new PriorityStack<char>(OperationsPriority.Values.Max() + 1);
            var currentToken = "";
            while (i < Input.Length)
            {
                if (Input[i] == ' ')
                {
                    if (currentToken != "")
                    {
                        _lexedStringTokens.Add(currentToken);
                        currentToken = "";
                    }

                    i++;
                    continue;
                }

                if (Input[i] == '(')
                {
                    var depth = 1;
                    var parentheseToken = "";
                    while (depth > 0)
                    {
                        i++;
                        if (Input[i] == '(')
                        {
                            depth++;
                        }

                        if (Input[i] == ')')
                        {
                            depth--;
                        }

                        if (depth != 0)
                        {
                            parentheseToken += Input[i];
                        }
                    }

                    var id = ParserMemory.AssignId(parentheseToken);
                    _lexedStringTokens.Add($"${id}");
                    i++;
                    continue;
                }

                if (OperationsPriority.ContainsKey(Input[i]))
                {
                    if (currentToken != "")
                    {
                        _lexedStringTokens.Add(currentToken);
                    }
                    currentToken = "";
                    var pops = operationStacks.Add(Input[i], OperationsPriority[Input[i]]);
                    for (int j = 0; j < pops.Count; j++)
                    {
                        _lexedStringTokens.Add(pops[j].ToString());
                    }

                    if (Input[i] == '=')
                    {
                        break;
                    }
                }
                else
                {
                    if (currentToken == "")
                    {
                        if (int.TryParse(Input[i].ToString(), out int answer))
                        {
                            currentToken += "#";
                        }
                        else
                        {
                            currentToken += "@";
                        }
                    }

                    currentToken += Input[i];
                }
                i++;
            }
        }

        public int Compile()
        {
            if (IsCompiledOnce)
            {
                return CompileAnswer;
            }

            CompileAnswer = 0;
            Stack<int> calculationStack = new Stack<int>();
            foreach (var token in _lexedStringTokens)
            {
                if (token[0] == '#')
                {
                    var theValue = int.Parse(token.Substring(1));
                    calculationStack.Push(theValue);
                }
                else if (token[0] == '$')
                {
                    var id = int.Parse(token.Substring(1));
                    var compiler = ParserMemory.GetCompilerWithId(id);
                    var theValue = compiler.Compile();
                    calculationStack.Push(theValue);
                }
                else if (token[0] == '@')
                {
                    var tag = token.Substring(1);
                    var theValue = ParserMemory.GetParameter(tag);
                    calculationStack.Push(theValue);
                }
                else
                {
                    if (token.Length != 1)
                    {
                        throw new Exception("There Is Operation With Two Sign");
                    }
                    else if (!OperationsPriority.ContainsKey(token[0]))
                    {
                        throw new Exception("There Is No Such Operation Builtin");
                    }
                    else
                    {
                        var operationToken = token[0];
                        // It Is Stack So First Second Operand Will Be Pop Out
                        var operand2 = calculationStack.Pop();
                        var operand1 = calculationStack.Pop();
                        var result = 0;
                        switch (operationToken)
                        {
                            case '+':
                                result = operand1 + operand2;
                                break;
                            case '-':
                                result = operand1 - operand2;
                                break;
                            case '*':
                                result = operand1 * operand2;
                                break;
                            case '/':
                                if (operand2 == 0)
                                {
                                    result = operand1 > 0 ? int.MaxValue : int.MinValue;
                                    break;
                                }

                                result = (int)(operand1 / operand2);
                                break;
                            case '^':
                                if (operand2 == 0 && operand1 == 0)
                                {
                                    result = 0;
                                    break;
                                }

                                result = (int)Math.Pow(operand1, operand2);
                                break;
                            default:
                                throw new Exception($"Operand {operationToken} Is Not Defined!");
                        }

                        calculationStack.Push(result);
                    }
                }
            }

            if (calculationStack.Count != 1)
            {
                throw new Exception("Stack Should Not Have More Than 1 Or Less Than 1 Elements!");
            }

            CompileAnswer = calculationStack.Pop();
            IsCompiledOnce = true;
            return CompileAnswer;
        }
    }
}