using AdventOfCode.Solutions.Day3;
using FluentAssertions;

namespace AdventOfCode.Tests;

class Day3Tests
{
    [Test]
    public void TestPart1()
    {
        // Arrange
        var sut = new Day3();
        var filePath = $"./Solutions/Day{sut.DayNumber}/example.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");

        // Act
        var result = sut.RunPart1(inputLines);

        // Assert
        result.Should().Be(161);
    }

    [Test]
    public void TestPart2()
    {
        // Arrange
        var sut = new Day3();
        var filePath = $"./Solutions/Day{sut.DayNumber}/example_part2.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");

        // Act
        var result = sut.RunPart2(inputLines);

        // Assert
        result.Should().Be(48);
    }
}
