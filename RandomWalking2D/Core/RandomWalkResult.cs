
namespace RandomWalking2D.Core
{
    public class RandomWalkResult
    {
        public double[] Distances { get; private set; }
        public double[] FinalX { get; private set; }
        public double[] FinalY { get; private set; }

        public int Steps { get; private set; }
        public int Simulations { get; private set; }
        public double StepLength { get; private set; }

        public double MeanSquareDistance { get; private set; }
        public double TheoreticalMeanSquare { get; private set; }
        public double Difference { get; private set; }

        public RandomWalkResult(
            double[] distances,
            double[] finalX,
            double[] finalY,
            int steps,
            int simulations,
            double stepLength)
        {
            Distances = distances;
            FinalX = finalX;
            FinalY = finalY;

            Steps = steps;
            Simulations = simulations;
            StepLength = stepLength;

            double sum = 0.0;

            for (int i = 0; i < distances.Length; i++)
            {
                sum += distances[i] * distances[i];
            }

            MeanSquareDistance = sum / distances.Length;

            TheoreticalMeanSquare = steps * stepLength * stepLength;

            Difference = System.Math.Abs(
                MeanSquareDistance - TheoreticalMeanSquare);
        }
    }
}
