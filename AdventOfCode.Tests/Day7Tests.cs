using AdventOfCode.Solutions.Day7;
using FluentAssertions;

namespace AdventOfCode.Tests;

public class Day7Tests
{
    [Test]
    public void TestPart1()
    {
        // Arrange
        var sut = new Day7();
        var filePath = $"./Solutions/Day{sut.DayNumber}/example.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");

        // Act
        var result = sut.RunPart1(inputLines);

        // Assert
        result.Should().Be(3749);
    }

    [Test]
    public void TestPart2()
    {
        // Arrange
        var sut = new Day7();
        var filePath = $"./Solutions/Day{sut.DayNumber}/example.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");

        // Act
        var result = sut.RunPart2(inputLines);

        // Assert
        result.Should().Be(11387);
    }
}