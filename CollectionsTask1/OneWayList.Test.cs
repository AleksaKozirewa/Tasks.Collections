using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CollectionsTask1
{
    public class OneWayListTest
    {
        [Fact]
        public void ListShouldBeConvertedToArray()
        {
            var list = new OneWayList();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            //  list.AddAt(4,3);
            //   list.GetElementByIndex(2);

            var array = list.ToArray();

            Assert.Equal(new int[] { 1, 2, 3 }, array);

        }

        [Theory]
        [InlineData(new[] { 1, 2, 3 }, 1, 2)]
        [InlineData(new[] { 1, 2, 3 }, 2, 3)]
        [InlineData(new[] { 1, 2, 3 }, 0, 1)]
        [InlineData(new[] { 1 }, 0, 1)]
        public void ShouldGetElementByValidIndex(Array arr, int index, int value)
        {
            var list = new OneWayList();
            for (var i = 0; i <= arr.Length; i++)
            {
                list.Add(i);
            }

            var expectedValue = list.GetElementByIndex(index);
            Assert.Equal(expectedValue, value);
            //Assert.Throws<int>(typeof(ArgumentOutOfRangeException));
        }

        /* [Theory]
         [InlineData(new[] { 1, 2, 3 }, 1, 3)]
         public void ShouldGetElementByInvalidIndex(Array arr, int invalidIndex, int value)
         {
             var list = new OneWayList();
             for (var i = 0; i < arr.Length; i++)
             {
                 list.Add(i);
             }

             list.GetElementByIndex(invalidIndex);
             Assert.Throws<int>(typeof(ArgumentOutOfRangeException));
         }*/
    }
}
