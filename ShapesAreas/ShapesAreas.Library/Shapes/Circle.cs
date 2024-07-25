using ShapesAreas.Library.Helpers;
using ShapesAreas.Library.Interfaces;

namespace ShapesAreas.Library.Shapes;

/// <summary>
/// Фигура: Окружность
/// </summary>
public readonly struct Circle : IShape
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="radius">Радиус</param>
    public Circle(double radius = default)
    {
        if (radius > 0)   
            Radius = radius;
    }
    
    /// <summary>
    /// Радиус
    /// </summary>
    public double Radius { get; init; }
    
    /// <inheritdoc cref="IShape"/>
    public double Area => MathHelper.Square(Radius) * Math.PI;
}