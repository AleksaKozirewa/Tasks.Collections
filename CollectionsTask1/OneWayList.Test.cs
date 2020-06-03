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

            var array = list.ToArray();

            Assert.Equal(new int[] { 1, 2, 3 }, array);

        }

    }
}
