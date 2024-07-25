using ShapesAreas.Library.Shapes;

namespace ShaperAreas.Tests.ShapesTests;

public class TriangleTests
{
    [Theory]
    [InlineData(3,4, 5, 6)]
    [InlineData(15, 14, 10, 67.712166558159993)]
    [InlineData(6, 8, 10, 24)]
    public void TriangleArea_Success(double firstSide, double secondSide, double thirdSide, double expected)
    {
        // Arrange
        var triangle = new Triangle(firstSide, secondSide, thirdSide);
        
        // Act
        var area = triangle.Area;
        
        // Assert
        Assert.Equal(expected, area);
    }
    
    [Theory]
    [InlineData(3,4, 5, true)]
    [InlineData(15, 14, 10, false)]
    [InlineData(6, 8, 10, true)]
    public void TriangleIsRectangular_Success(double firstSide, double secondSide, double thirdSide, bool expected)
    {
        // Arrange
        var triangle = new Triangle(firstSide, secondSide, thirdSide);
        
        // Act
        var isRectangular = triangle.IsRectangular;
        
        // Assert
        Assert.Equal(expected, isRectangular);
    }
    
    [Theory]
    [InlineData(128,4, 5, false)]
    [InlineData(15, 14, 10, true)]
    [InlineData(19, 3, 5, false)]
    public void TriangleCanExist_Success(double firstSide, double secondSide, double thirdSide, bool expected)
    {
        // Arrange
        // Act
        var canExist = Triangle.CanExist(firstSide, secondSide, thirdSide);
        
        // Assert
        Assert.Equal(expected, canExist);
    }

}