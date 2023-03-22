using System.Collections.Generic;
using NUnit.Framework;

namespace Max.Business.Components.Tests.Utils
{
    public class PartitionerTests
    {
        [Test]
        public void PartitionBy_ValuePredicates_IfPredicatesCoverAllValues_ThenNoRemainingPartition()
        {
            // Arrange
            var source = new Dictionary<string, int>
            {
                {"apple", 1},
                {"banana", 2},
                {"orange", 3},
                {"grape", 4},
                {"watermelon", 5}
            };

            // Act
            var partitions = source.PartitionBy(
                value => value % 2 == 0,
                value => value % 2 != 0
            );

            // Assert
            Assert.AreEqual(2, partitions.Count);
            Assert.AreEqual(new Dictionary<string, int> {{"banana", 2}, {"grape", 4}}, partitions[0]);
            Assert.AreEqual(new Dictionary<string, int> {{"apple", 1}, {"orange", 3}, {"watermelon", 5}},
                partitions[1]);
        }

        [Test]
        public void PartitionBy_KeyPredicates_IfPredicatesCreateTwoGroups_ThenTwoPartitionsAndNoRemaining()
        {
            // Arrange
            var source = new Dictionary<int, string>
            {
                {1, "apple"},
                {2, "banana"},
                {3, "orange"},
                {4, "grape"},
                {5, "watermelon"}
            };

            // Act
            var partitions = source.PartitionBy(
                key => key % 2 == 0
            );

            // Assert
            Assert.AreEqual(2, partitions.Count);
            Assert.AreEqual(new Dictionary<int, string> {{2, "banana"}, {4, "grape"}}, partitions[0]);
            Assert.AreEqual(new Dictionary<int, string> {{1, "apple"}, {3, "orange"}, {5, "watermelon"}},
                partitions[1]);
        }

        [Test]
        public void PartitionBy_EnumerablePredicates_IfPredicatesCreateTwoGroups_ThenTwoPartitionsAndNoRemaining()
        {
            // Arrange
            var source = new List<int> {1, 2, 3, 4, 5};

            // Act
            var partitions = source.PartitionBy(
                value => value % 2 == 0
            );

            // Assert
            Assert.AreEqual(2, partitions.Count);
            Assert.AreEqual(new List<int> {2, 4}, partitions[0]);
            Assert.AreEqual(new List<int> {1, 3, 5}, partitions[1]);
        }

        [Test]
        public void PartitionByValues_IfPredicatesCreateTwoGroups_ThenTwoPartitionsAndNoRemaining()
        {
            // Arrange
            var source = new Dictionary<string, int>
            {
                {"apple", 1},
                {"banana", 2},
                {"orange", 3},
                {"grape", 4},
                {"watermelon", 5}
            };

            // Act
            var partitions = source.PartitionByValues(
                value => value % 2 == 0
            );

            // Assert
            Assert.AreEqual(2, partitions.Count);
            Assert.AreEqual(new Dictionary<string, int> {{"banana", 2}, {"grape", 4}}, partitions[0]);
            Assert.AreEqual(new Dictionary<string, int> {{"apple", 1}, {"orange", 3}, {"watermelon", 5}},
                partitions[1]);
        }

        [Test]
        public void PartitionByKeys_IfPredicatesCoverAllKeys_ThenNoRemainingPartition()
        {
            // Arrange
            var source = new Dictionary<int, string>
            {
                {1, "apple"},
                {2, "banana"},
                {3, "orange"},
                {4, "grape"},
                {5, "watermelon"}
            };

            // Act
            var partitions = source.PartitionByKeys(
                key => key % 2 == 0,
                key => key % 2 != 0
            );

            // Assert
            Assert.AreEqual(2, partitions.Count);
            Assert.AreEqual(new Dictionary<int, string> { {2, "banana"}, {4, "grape"} }, partitions[0]);
            Assert.AreEqual(new Dictionary<int, string> { {1, "apple"}, {3, "orange"}, {5, "watermelon"} }, partitions[1]);
        }
    }
}