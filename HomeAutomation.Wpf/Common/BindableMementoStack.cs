using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;

namespace HomeAutomation.Wpf.Common
{
    /// <summary>
    /// Not thread safe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableMementoStack<T> : BindableBase
    {
        private readonly int _maxSize;
        private Stack<T> _undoStack;
        private readonly Stack<T> _redoStack;
        private T _current;
        private int _undoCount;
        private int _redoCount;

        public int UndoCount
        {
            get => _undoCount;
            set => SetProperty(ref _undoCount, value);
        }

        public int RedoCount
        {
            get => _redoCount;
            set => SetProperty(ref _redoCount, value);
        }

        public T Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public BindableMementoStack(int maxSize)
        {
            _maxSize = maxSize;
            _undoStack = new Stack<T>();
            _redoStack = new Stack<T>();
        }

        /// <summary>
        /// Assigns the given input to 'Current'. The input will be deep cloned, no references will be maintained.
        /// </summary>
        /// <param name="input"></param>
        public void Apply(T input)
        {
            if(Current != null)
                _undoStack.Push(Current);

            Current = input.Clone();

            UpdateStack(ref _undoStack);
        }

        public void Undo()
        {
            if (!_undoStack.Any())
                return;

            _redoStack.Push(Current);
            Current = _undoStack.Pop();

            UpdateStack(ref _undoStack);
        }

        public void Redo()
        {
            if (!_redoStack.Any())
                return;

            _undoStack.Push(Current);
            Current = _redoStack.Pop();

            UpdateStack(ref _undoStack);
        }

        private void UpdateStack(ref Stack<T> stack)
        {
            if (stack.Count > _maxSize)
            {
                var inverted = stack.Reverse().ToList();
                inverted.RemoveAt(0);
                stack = new Stack<T>(inverted);
            }

            RedoCount = _redoStack.Count;
            UndoCount = _undoStack.Count;
        }
    }
}
