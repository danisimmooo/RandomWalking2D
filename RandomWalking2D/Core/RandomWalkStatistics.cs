
using System;

namespace RandomWalking2D.Core
{
    public class RandomWalkStatistics
    {
        private readonly RandomWalkSimulator simulator;

        public RandomWalkStatistics()
        {
            simulator = new RandomWalkSimulator();
        }

        public RandomWalkStatistics(int seed)
        {
            simulator = new RandomWalkSimulator(seed);
        }

        // Выполнение серии из M реализаций
        public RandomWalkResult RunExperiments(
            int steps,
            double stepLength,
            int simulations)
        {
            if (steps <= 0)
                throw new ArgumentException(
                    "Количество шагов должно быть положительным.");

            if (simulations <= 0)
                throw new ArgumentException(
                    "Количество реализаций должно быть положительным.");

            if (stepLength <= 0 ||
                double.IsNaN(stepLength) ||
                double.IsInfinity(stepLength))
            {
                throw new ArgumentException(
                    "Длина шага должна быть положительной.");
            }

            double[] distances = new double[simulations];

            for (int i = 0; i < simulations; i++)
            {
                double x;
                double y;

                simulator.Simulate(
                    steps,
                    stepLength,
                    out x,
                    out y);

                distances[i] =
                    RandomWalkSimulator.CalculateDistance(x, y);
            }

            return new RandomWalkResult(
                distances,
                steps,
                simulations,
                stepLength);
        }
    }
}
