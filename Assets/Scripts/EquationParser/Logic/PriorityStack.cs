using System;
using System.Collections.Generic;

namespace DefaultNamespace.EquationParser
{
    public class PriorityStack<T>
    {
        private T[] _stack;
        private int _size;
        public int Count;
        public PriorityStack(int priorityCount)
        {
            _stack = new T[priorityCount];
            Count = 0;
            _size = priorityCount;
        }

        public List<T> Add(T newElement, int priority)
        {
            var popElements = new List<T>();
            if (priority >= _size)
            {
                throw new Exception("Priority Out Of Index!");
            }
            
            if (Count == 0)
            {
                _stack[priority] = newElement;
                Count++;
                return popElements;
            }
            
            for (var i = _size - 1; i >= priority; i--)
            {
                if (!Equals(_stack[i], default(T)))
                {
                    popElements.Add(_stack[i]);
                    _stack[i] = default(T);
                    Count--;
                }
                if (i != priority) continue;
                _stack[i] = newElement;
                Count++;
                break;
            }

            return popElements;
        }

        public List<T> PopAllStack()
        {
            return Add(default(T), 0);
        }
    }
}