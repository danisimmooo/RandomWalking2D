
using System;

namespace RandomWalking2D.Core
{
    public class RandomWalkSimulator
    {
        private readonly Random random;

        public RandomWalkSimulator()
        {
            random = new Random();
        }

        public RandomWalkSimulator(int seed)
        {
            random = new Random(seed);
        }

        // Генерация равномерного случайного угла [0; 2π)
        public double GenerateAngle()
        {
            return random.NextDouble() * 2.0 * Math.PI;
        }

        // Вычисление приращений координат
        public static void CalculateDisplacement(
            double angle,
            double stepLength,
            out double deltaX,
            out double deltaY)
        {
            ValidateStepLength(stepLength);

            if (double.IsNaN(angle) || double.IsInfinity(angle))
                throw new ArgumentException("Некорректный угол.");

            deltaX = stepLength * Math.Cos(angle);
            deltaY = stepLength * Math.Sin(angle);
        }

        // Выполнение шага с заданным углом
        public static void ApplyStep(
            ref double x,
            ref double y,
            double angle,
            double stepLength)
        {
            double deltaX;
            double deltaY;

            CalculateDisplacement(
                angle, stepLength, out deltaX, out deltaY);

            x += deltaX;
            y += deltaY;
        }

        // Выполнение случайного шага
        public void MakeStep(
            ref double x,
            ref double y,
            double stepLength)
        {
            ApplyStep(
                ref x,
                ref y,
                GenerateAngle(),
                stepLength);
        }

        // Выполнение N шагов
        public void Simulate(
            int steps,
            double stepLength,
            out double x,
            out double y)
        {
            if (steps <= 0)
                throw new ArgumentException(
                    "Количество шагов должно быть положительным.");

            ValidateStepLength(stepLength);

            x = 0.0;
            y = 0.0;

            for (int i = 0; i < steps; i++)
            {
                MakeStep(ref x, ref y, stepLength);
            }
        }

        // Расчёт конечного евклидова смещения
        public static double CalculateDistance(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        private static void ValidateStepLength(double stepLength)
        {
            if (stepLength <= 0 ||
                double.IsNaN(stepLength) ||
                double.IsInfinity(stepLength))
            {
                throw new ArgumentException(
                    "Длина шага должна быть положительной.");
            }
        }
    }
}
