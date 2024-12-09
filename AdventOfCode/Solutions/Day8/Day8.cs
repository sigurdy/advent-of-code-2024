using AdventOfCode.Solutions.Day8.Utils;

namespace AdventOfCode.Solutions.Day8;

public class Day8 : Solutions
{
    public Day8() : base(8)
    {
    }

    public (int x, int y) GetDistanceBetweenAntennas((int x, int y) firstPoint, (int x, int y) secondPoint)
    {
        return (secondPoint.x - firstPoint.x, secondPoint.y - firstPoint.y);
    }

    public void CalculateAntinodesPart1(AntennaMap map)
    {
        foreach (var antennaType in map.AntennaPosition.Keys)
        {
            var antennas = map.AntennaPosition[antennaType];

            foreach (var antenna in antennas)
            {
                foreach (var otherAntennas in antennas)
                {
                    if (otherAntennas == antenna) continue;
                    var distance = GetDistanceBetweenAntennas(antenna, otherAntennas);
                    map.UpdateAntinode((otherAntennas.Item1 + distance.x, otherAntennas.Item2 + distance.y));
                }
            }
        }
    }

    public void CalculateAntinodesPart2(AntennaMap map)
    {
        foreach (var antennaType in map.AntennaPosition.Keys)
        {
            var antennas = map.AntennaPosition[antennaType];

            foreach (var antenna in antennas)
            {
                foreach (var otherAntenna in antennas)
                {
                    if (otherAntenna == antenna) continue;
                    
                    map.UpdateAntinode(otherAntenna); // Update this antenna if there is at least two antennas
                    bool proceed = true;

                    var target = (otherAntenna.Item1, otherAntenna.Item2);
                    while (proceed)
                    {
                        var distance = GetDistanceBetweenAntennas(antenna, otherAntenna);
                        
                        proceed = map.UpdateAntinode((target.Item1 + distance.x, target.Item2 + distance.y));

                        target = (target.Item1 + distance.x, target.Item2 + distance.y);
                    }
                }
            }
        }
    }

    public override long RunPart1(string[] inputLines)
    {
        AntennaMap antennaMap = new AntennaMap(inputLines);
        CalculateAntinodesPart1(antennaMap);
        return antennaMap.AntinodePositions.Count;
    }

    public override long RunPart2(string[] inputLines)
    {
        AntennaMap antennaMap = new AntennaMap(inputLines);
        CalculateAntinodesPart2(antennaMap);
        return antennaMap.AntinodePositions.Count;
    }
}