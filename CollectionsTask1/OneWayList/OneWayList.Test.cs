using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CollectionsTask1
{
    public class OneWayListTest
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new int[0])]
        [InlineData(new[] { 1 })]
        public void ListShouldBeConvertedToArray(int[] input)
        {
            var list = new OneWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            var array = list.ConvertToArray();
            Assert.Equal(input, array);
        }

        [Fact]
        public void ShouldRemoveElementAtList()
        {
            var list = new OneWayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Remove(3);

            var array = list.ToArray();
            Assert.Equal(new int[]
            {
                1, 2

            }, array);
        }

        [Fact]
        public void ShouldAddElementAtListAtSomePosition()
        {
            var list = new OneWayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.AddAt(4, 0);

            var array = list.ToArray();

            Assert.Equal(new int[] {4, 1, 2, 3}, array);
        }

        [Theory]
        [InlineData(new[] {5, 3}, 0, 2, new[] {2, 3})]
        public void ShouldSetElementByUsingIndexing(int [] input, int index, int value, int [] expected)
        {
            var list = new OneWayList();

            for (var i = 0; i < input.Length; i++)
            {
                list.Add(input[i]);
            }

            list[index] = value;

            var array = list.ToArray();
            Assert.Equal(expected, array);
        }

        [Fact]
        public void ShouldRemoveElementAtListAtSomePosition()
        {
            var list = new OneWayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.RemoveAt(1);

            var array = list.ToArray();

            Assert.Equal(new int[] { 1, 3, 4, 5}, array);
        }

        [Fact]
        public void ListShouldBeReversed()
        {
            var list = new OneWayList();
            //list.Add(1);
            // list.Add(2);
            // list.Add(3);

            list.Reverse();

            var array = list.ToArray();
            Assert.Equal(new int[] { }, array);
        }
        
        [Theory]
        [InlineData(new[] { 5, 3, 2, 4, 1 }, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new[] { 5, 3 }, new[] { 3, 5 })]
        [InlineData(new[] { 5 }, new[] { 5 })]
        [InlineData(new int[0], new int[0])]
        public void ListShouldBeSorted(int[] input, int[] expected)
        {
            var list = new OneWayList();

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
            var list = new OneWayList();
            for (var i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i]);
            }

            var expectedValue = list.GetElementByIndex(index);
            Assert.Equal(expectedValue, value);
        }
    }
}
