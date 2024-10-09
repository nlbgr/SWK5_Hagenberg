using HashDictionary.Impl;

namespace HashDictionary.Tests {
    public class HashDictionaryTest {
        [Fact]
        public void AddAndIndexerGetterAreConsistent()
        {
            IDictionary<int, int> dict = new HashDictinary<int, int>();
            dict.Add(1, 10);
            Assert.Equal(10, dict[1]);

            dict.Add(3, 30);
            Assert.Equal(10, dict[1]);
            Assert.Equal(30, dict[3]);

            dict.Add(2, 20);
            Assert.Equal(10, dict[1]);
            Assert.Equal(20, dict[2]);
            Assert.Equal(30, dict[3]);
        }

        [Fact]
        public void ArgumentException_WhenItemAddedTwice()
        {
            IDictionary<int, int> dict = new HashDictinary<int, int>();
            dict.Add(1, 10);
            dict.Add(2, 20);

            Assert.Throws<ArgumentException>(() => dict.Add(1, 100));
        }

        [Theory]
        [InlineData(new[] {10}, 1)]
        [InlineData(new[] {10, 20}, 2)]
        [InlineData(new[] {10, 20, 30}, 3)]
        public void Theory_CountPropertyIsOK_WhenAddingItems(IEnumerable<int> list, int expected)
        {
            IDictionary<int, int> dict = new HashDictinary<int, int>();
            int i = 0;
            foreach (var item in list)
            {
                dict.Add(i++, item);
            }

            Assert.Equal(expected, dict.Count);
        }
    }

}