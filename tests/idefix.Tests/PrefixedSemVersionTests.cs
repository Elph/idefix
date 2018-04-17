using Semver;
using Xunit;

namespace idefix.Tests
{
    public class PrefixedSemVersionTests
    {
        [Fact]
        public void ShouldReturnCorrectValues_WhenCreatedFromString()
        {
            // Arrange
            // Act
            var version = new PrefixedSemVersion("prefix/v0.1.2");

            //Assert
            Assert.Equal("prefix", version.Prefix);
            Assert.Equal(0, version.Major);
            Assert.Equal(1, version.Minor);
            Assert.Equal(2, version.Patch);
        }

        [Fact]
        public void ShouldReturnCorrectValues_WhenCreatedFromValues()
        {
            // Arrange
            // Act
            var version = new PrefixedSemVersion("prefix", new SemVersion(0, 1, 2));

            //Assert
            Assert.Equal("prefix", version.Prefix);
            Assert.Equal(0, version.Major);
            Assert.Equal(1, version.Minor);
            Assert.Equal(2, version.Patch);
        }


        [Fact]
        public void ToString_ShouldReturnCorrectStringRepresentation_WhenPrefix()
        {
            // Arrange
            // Act
            var version = new PrefixedSemVersion("prefix", new SemVersion(0, 1, 2));

            //Assert
            Assert.Equal("prefix/v0.1.2", version.ToString());
        }

        [Fact]
        public void ToString_ShouldReturnCorrectStringRepresentation_WhenNoPrefix()
        {
            // Arrange
            // Act
            var version = new PrefixedSemVersion(null, new SemVersion(0, 1, 2));

            //Assert
            Assert.Equal("v0.1.2", version.ToString());
        }


        [Fact]
        public void IncrementMajor_ShouldIncrementInOneTheMajor()
        {
            // Arrange
            var version = new PrefixedSemVersion(null, new SemVersion(0, 1, 2));

            // Act
            var newVersion = version.IncrementMajor();

            //Assert
            Assert.Equal(1, newVersion.Major);
        }

        [Fact]
        public void IncrementMinor_ShouldIncrementInOneTheMinor()
        {
            // Arrange
            var version = new PrefixedSemVersion(null, new SemVersion(0, 1, 2));

            // Act
            var newVersion = version.IncrementMinor();

            //Assert
            Assert.Equal(2, newVersion.Minor);
        }

        [Fact]
        public void IncrementPatch_ShouldIncrementInOneThePatch()
        {
            // Arrange
            var version = new PrefixedSemVersion(null, new SemVersion(0, 1, 2));

            // Act
            var newVersion = version.IncrementPatch();

            //Assert
            Assert.Equal(3, newVersion.Patch);
        }

    }
}
