using FluentAssertions;
using src.Solutions.Day1;

namespace tests;

public class Day1Tests
{
    [Test]
    public void TestPart1()
    {
        // Arrange
        var sut = new Day1();
        var filePath = $"./Solutions/Day{sut.DayNumber}/example.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");
        
        // Act
        var result = sut.RunPart1(inputLines);
        
        // Assert
        result.Should().Be(11);
    }
    
    [Test]
    public void TestPart2()
    {
        // Arrange
        var sut = new Day1();
        var filePath = $"./Solutions/Day{sut.DayNumber}/example.txt";
        var inputLines = (File.Exists(filePath)) ? File.ReadAllLines(filePath) : throw new InvalidOperationException("File not found");
        
        // Act
        var result = sut.RunPart2(inputLines);
        
        // Assert
        result.Should().Be(31);
    }
}