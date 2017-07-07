using System;
using System.Diagnostics;
using System.Linq;
using HomeAutomation.Wpf.Common;
using Xunit;

namespace HomeAutomation.Wpf.Tests.Common
{
    public class BindableMementoStackTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Apply_NewItem_ShowsAsCurrent(int repetitions)
        {
            // arrange
            var m = new BindableMementoStack<Guid>(3);

            // act
            foreach (var i in Enumerable.Range(1, repetitions))
            {
                var newValue = Guid.NewGuid();
                m.Apply(newValue);

                // assert
                Assert.Equal(newValue, m.Current);
            }
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Apply_NewItem_UndoStackCountCorrect(int repetitions)
        {
            // arrange
            var m = new BindableMementoStack<Guid>(10);

            // act
            // act
            foreach (var i in Enumerable.Range(1, repetitions))
            {
                var newValue = Guid.NewGuid();
                m.Apply(newValue);
            }

            Assert.Equal(repetitions, m.UndoCount);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(41)]
        [InlineData(202)]
        public void Apply_MaxCountExceeded_IsTrimmed(int maxSize)
        {
            // arrange
            var m = new BindableMementoStack<int>(maxSize);

            // act
            foreach (var i in Enumerable.Range(1, maxSize + 2))
                m.Apply(i);
            
            // assert 
            Assert.Equal(maxSize, m.UndoCount);
        }

        [Fact]
        public void Undo_NothingToUndo_NothingChanges()
        {
            // arrange
            var m = new BindableMementoStack<string>(2);
            m.Apply("foo");

            // act
            m.Undo();

            // assert
            Assert.Equal("foo", m.Current);
        }

        [Fact]
        public void Undo_OldItemIsRestored()
        {
            // arrange
            var m = new BindableMementoStack<string>(2);
            m.Apply("foo");
            m.Apply("bar");

            // act
            m.Undo();

            // assert
            Assert.Equal("foo", m.Current);
        }

        [Fact]
        public void Redo_ItemIsRestored()
        {
            // arrange
            var m = new BindableMementoStack<string>(2);
            m.Apply("foo");
            m.Apply("bar");
            m.Undo();

            // act
            m.Redo();

            // assert
            Assert.Equal("bar", m.Current);
        }

        [Fact]
        public void Redo_NothingToRedo_NothingChanges()
        {
            // arrange
            var m = new BindableMementoStack<string>(2);
            m.Apply("foo");

            // act
            m.Redo();

            // assert
            Assert.Equal("foo", m.Current);
        }
    }

    [Serializable]
    public class Test
    {
        public string Name { get; set; }
    }
}
