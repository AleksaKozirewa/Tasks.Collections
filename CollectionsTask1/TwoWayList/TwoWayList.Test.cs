using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CollectionsTask1
{
    public class TwoWayListTest
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[0])]
        [InlineData(new[] { 1 })]
        public void ListShouldBeConvertedToArray(int[] input)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            var array = list.ConvertToArray();
            Assert.Equal(input, array);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, 1, new[] { 2, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 2, new[] { 1, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 3, new[] { 1, 2 })]
        [InlineData(new int[0], 0, new int[0])]
        [InlineData(new[] { 1, 2, 3 }, 4, new[] { 1, 2, 3 })]
        public void ShouldRemoveElementAtList(int[] input, int value, int[] expected)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list.Remove(value);

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, 0, 0, new[] { 0, 1, 2, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 1, 1, new[] { 1, 1, 2, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 2, 3, new[] { 1, 2, 3, 3 })]
        [InlineData(new int[0], 0, 0, new int[] { 0 })]
        [InlineData(new[] { 1, 2, 3 }, 3, 4, new[] { 1, 2, 3, 4 })]
        [InlineData(new[] { 1 }, 0, 2, new[] { 2, 1 })]
        public void ShouldAddElementAtListAtSomePosition(int[] input, int index, int value, int[] expected)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list.AddAt(value, index);

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Fact]
        public void ShouldAddElementAtListAtSomePosition1()
        {
            var list = new TwoWayList();

            list.Add(1);

            Assert.Throws<ArgumentOutOfRangeException>(() => list.AddAt(2, 3));
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 0, 2, new[] { 2, 2, 3, 4, 5 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 4, 6, new[] { 1, 2, 3, 4, 6 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 8, new[] { 1, 2, 8, 4, 5 })]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 5, 8, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new[] { 1 }, 0, 3, new[] { 3 })]
        [InlineData(new int[0], 0, 3, new int[0])]
        public void ShouldSetElementByUsingIndexing(int[] input, int index, int value, int[] expected)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list[index] = value;

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 0, 1)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 4, 5)]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, 2, 3)]
        [InlineData(new[] { 1 }, 0, 1)]
        public void ShouldGetElementByUsingIndexing(int[] input, int index, int value)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            var expectedValue = list[index];
            Assert.Equal(expectedValue, value);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, 0, new[] { 2, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 1, new[] { 1, 3 })]
        [InlineData(new[] { 1, 2, 3 }, 2, new[] { 1, 2 })]
        [InlineData(new int[0], 0, new int[0])]
        [InlineData(new[] { 1, 2, 3 }, 4, new[] { 1, 2, 3 })]
        public void ShouldRemoveElementAtListAtSomePosition(int[] input, int index, int[] expected)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list.RemoveAt(index);

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Fact]
        public void ShouldAddElementAtList()
        {
            var list = new TwoWayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            var array = list.ToArray();

            Assert.Equal(new int[] { 1, 2, 3, 4, 5 }, array);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, new[] { 5, 4, 3, 2, 1 })]
        [InlineData(new[] { 1, 2, 3}, new[] { 3, 2, 1 })]
        [InlineData(new[] { 1 }, new[] { 1 })]
        [InlineData(new int[0], new int[0])]
        public void ListShouldBeReversed(int[] input, int[] expected)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list.Reverse();

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Theory]
        [InlineData(new[] { 5, 3, 2, 4, 1 }, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new[] { 5, 3 }, new[] { 3, 5 })]
        [InlineData(new[] { 5 }, new[] { 5 })]
        [InlineData(new int[0], new int[0])]
        public void ListShouldBeSorted(int[] input, int[] expected)
        {
            var list = new TwoWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list.Sort();

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, 1, 2)]
        [InlineData(new[] { 1, 2, 3 }, 2, 3)]
        [InlineData(new[] { 1, 2, 3 }, 0, 1)]
        [InlineData(new[] { 1 }, 0, 1)]
        public void ShouldGetElementByValidIndex(int[] arr, int index, int value)
        {
            var list = new TwoWayList();
            for (var i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i]);
            }

            var expectedValue = list.GetElementByIndex(index);
            Assert.Equal(expectedValue, value);
        }
    }
}
