using ShapesAreas.Library.Helpers;
using ShapesAreas.Library.Interfaces;

namespace ShapesAreas.Library.Shapes;

/// <summary>
/// Фигура: Треугольник
/// </summary>
public readonly struct Triangle : IShape
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="firstSide">Длина первой стороны</param>
    /// <param name="secondSide">Длина второй стороны</param>
    /// <param name="thirdSide">Длина третьей стороны</param>
    public Triangle(double firstSide = default, double secondSide = default, double thirdSide = default)
    {
        if (!CanExist(firstSide, secondSide, thirdSide))
            return;

        FirstSideLength = firstSide; 
        SecondSideLength = secondSide;
        ThirdSideLength = thirdSide;
    }
    
    /// <summary>
    /// Длина первой стороны
    /// </summary>
    public double FirstSideLength { get; init; }
    
    /// <summary>
    /// Длина второй стороны
    /// </summary>
    public double SecondSideLength { get; init; }
    
    /// <summary>
    /// Длина третьей стороны
    /// </summary>
    public double ThirdSideLength { get; init; }

    /// <summary>
    /// Периметр
    /// </summary>
    private double Perimeter => (FirstSideLength + SecondSideLength + ThirdSideLength) / 2;

    /// <inheritdoc cref="IShape"/> 
    public double Area
    {
        get
        {
            var perimeter = Perimeter;

            return Math.Sqrt(perimeter *
                             (perimeter - FirstSideLength) *
                             (perimeter - SecondSideLength) *
                             (perimeter - ThirdSideLength));
        }
    }

    /// <summary>
    /// Является ли треугольник прямоугольным
    /// </summary>
    public bool IsRectangular
    {
        get
        {
            var hypotenuse = MathHelper.Max(FirstSideLength, SecondSideLength, ThirdSideLength);
            var firstLeg = MathHelper.Min(FirstSideLength, SecondSideLength, ThirdSideLength);
            var secondLeg = MathHelper.Median(FirstSideLength, SecondSideLength, ThirdSideLength);
            
            return Math.Abs(MathHelper.Square(firstLeg) + MathHelper.Square(secondLeg) -
                            MathHelper.Square(hypotenuse)) < MathHelper.MeasurementError;
        }
    }
    
    /// <summary>
    /// Метод для проверки, может ли треугольник с такими сторонами существовать
    /// </summary>
    /// <param name="firstSide">Длина первой стороны</param>
    /// <param name="secondSide">Длина второй стороны</param>
    /// <param name="thirdSide">Длина третьей стороны</param>
    /// <returns>True, если может существовать, False иначе</returns>
    public static bool CanExist(double firstSide, double secondSide, double thirdSide)
    {
        var maxSideLength = MathHelper.Max(firstSide, secondSide, thirdSide);
        var minSideLength = MathHelper.Min(firstSide, secondSide, thirdSide);
        var medianSideLength = MathHelper.Median(firstSide, secondSide, thirdSide);

        return minSideLength + medianSideLength > maxSideLength;
    }
}