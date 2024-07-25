using ShapesAreas.Library.Shapes;

namespace ShaperAreas.Tests.ShapesTests;

public class CircleTests
{
    [Theory]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(15)]
    public void CircleArea_Success(double radius)
    {
        // Arrange
        var circle = new Circle(radius);
        var expectedArea = radius * radius * Math.PI;
        
        // Act
        var area = circle.Area;
        
        // Assert
        Assert.Equal(expectedArea, area);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(0, 0)]
    [InlineData(-10, 0)]
    public void CircleCreation_Success(double radius, double expected)
    {
        // Arrange
        var circle = new Circle(radius);
        
        // Act
        var actual = circle.Radius;
        
        // Assert
        Assert.Equal(expected, actual);
    }
}