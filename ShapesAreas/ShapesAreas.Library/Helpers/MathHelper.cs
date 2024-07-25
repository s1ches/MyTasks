namespace ShapesAreas.Library.Helpers;

/// <summary>
/// Набор математических функций
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Погрешность
    /// </summary>
    public static readonly double MeasurementError = Math.Pow(10, -6);
    
    /// <summary>
    /// Степень квадрата
    /// </summary>
    private const int SquarePower = 2;

    /// <summary>
    /// Возведение в квадрат
    /// </summary>
    /// <param name="argument">Число</param>
    /// <returns>Возведённое в степень <see cref="SquarePower"/> число</returns>
    public static double Square(double argument)
        => Math.Pow(argument, SquarePower);
    
    /// <summary>
    /// Макксимум из последовательности
    /// </summary>
    /// <param name="arguments">Последовательность чисел</param>
    /// <returns>Максимальный элемент последовательности</returns>
    public static double Max(params double[] arguments) => arguments.Max();
    
    /// <summary>
    /// Минимум из последовательности
    /// </summary>
    /// <param name="arguments">Последовательность чисел</param>
    /// <returns>Минимальный элемент последовательности</returns>
    public static double Min(params double[] arguments) => arguments.Min();

    /// <summary>
    /// Медианное значение последовательности
    /// </summary>
    /// <param name="arguments">Последовательность чисел</param>
    /// <returns>Медианный элемент последовательности</returns>
    public static double Median(params double[] arguments)
    {
        if (arguments.Length % 2 == 0)
            return (arguments[(arguments.Length - 1) / 2] + arguments[(arguments.Length - 1) / 2 + 1]) / 2;

        return arguments[arguments.Length / 2];
    }
    
}