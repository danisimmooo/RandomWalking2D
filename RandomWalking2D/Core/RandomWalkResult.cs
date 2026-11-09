
namespace RandomWalking2D.Core
{
    public class RandomWalkResult
    {
        // Конечные смещения всех реализаций
        public double[] Distances { get; private set; }

        // Количество шагов
        public int Steps { get; private set; }

        // Количество реализаций
        public int Simulations { get; private set; }

        // Длина одного шага
        public double StepLength { get; private set; }

        // Средний квадрат конечного смещения
        public double MeanSquareDistance { get; private set; }

        // Теоретическое значение N * l^2
        public double TheoreticalMeanSquare { get; private set; }

        // Абсолютная разница с теорией
        public double Difference { get; private set; }

        public RandomWalkResult(
            double[] distances,
            int steps,
            int simulations,
            double stepLength)
        {
            Distances = distances;
            Steps = steps;
            Simulations = simulations;
            StepLength = stepLength;

            double sum = 0.0;

            for (int i = 0; i < distances.Length; i++)
            {
                sum += distances[i] * distances[i];
            }

            MeanSquareDistance = sum / distances.Length;

            TheoreticalMeanSquare =
                steps * stepLength * stepLength;

            Difference = System.Math.Abs(
                MeanSquareDistance - TheoreticalMeanSquare);
        }
    }
}
