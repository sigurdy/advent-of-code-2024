using FluentAssertions;
using src.Solutions.Day1;
using src.Solutions.Day2;

namespace tests;

internal class Day2Tests
{
    [Test]
    public void TestNumberIncreas()
    {
        // Arrange
        var sut = new Day2();
        var filePath = $"./Solutions/Day{sut.DayNumber}/Example/part1.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");

        // Act
        

        // Assert
    }

    [Test]
    public void TestPart1()
    {
        // Arrange
        var sut = new Day2();
        var filePath = $"./Solutions/Day{sut.DayNumber}/Example/part1.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");

        // Act
        var result = sut.RunPart1(inputLines);

        // Assert
        result.Should().Be(2);
    }
}
