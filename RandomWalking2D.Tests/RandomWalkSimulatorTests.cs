
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomWalking2D.Core;

namespace RandomWalking2D.Tests
{
    [TestClass]
    public class RandomWalkSimulatorTests
    {
        // 1. Проверяем, что случайный угол находится в диапазоне [0; 2π)
        [TestMethod]
        public void GenerateAngle_ShouldBeWithinRange()
        {
            RandomWalkSimulator simulator = new RandomWalkSimulator(12345);

            for (int i = 0; i < 10000; i++)
            {
                double angle = simulator.GenerateAngle();

                Assert.IsTrue(angle >= 0.0);
                Assert.IsTrue(angle < 2.0 * Math.PI);
            }
        }

        // 2. Проверяем перемещение вправо при угле 0°
        [TestMethod]
        public void CalculateDisplacement_ZeroAngle_ShouldMoveRight()
        {
            double dx, dy;

            RandomWalkSimulator.CalculateDisplacement(
                0.0, 2.0, out dx, out dy);

            Assert.AreEqual(2.0, dx, 0.000001);
            Assert.AreEqual(0.0, dy, 0.000001);
        }

        // 3. Проверяем перемещение вверх при угле 90°
        [TestMethod]
        public void CalculateDisplacement_RightAngle_ShouldMoveUp()
        {
            double dx, dy;

            RandomWalkSimulator.CalculateDisplacement(
                Math.PI / 2.0, 2.0, out dx, out dy);

            Assert.AreEqual(0.0, dx, 0.000001);
            Assert.AreEqual(2.0, dy, 0.000001);
        }

        // 4. Проверяем длину перемещения
        [TestMethod]
        public void CalculateDisplacement_ShouldHaveFixedLength()
        {
            double dx, dy;
            double stepLength = 3.5;
            double angle = 1.234;

            RandomWalkSimulator.CalculateDisplacement(
                angle, stepLength, out dx, out dy);

            double distance = Math.Sqrt(dx * dx + dy * dy);

            Assert.AreEqual(stepLength, distance, 0.000001);
        }

        // 5. Проверяем расстояние от начала координат
        [TestMethod]
        public void CalculateDistance_ShouldReturnEuclideanDistance()
        {
            double distance = RandomWalkSimulator.CalculateDistance(3.0, 4.0);

            Assert.AreEqual(5.0, distance, 0.000001);
        }

        // 6. Проверяем воспроизводимость при одинаковом seed
        [TestMethod]
        public void Simulate_SameSeed_ShouldReturnSameCoordinates()
        {
            RandomWalkSimulator simulator1 = new RandomWalkSimulator(123);
            RandomWalkSimulator simulator2 = new RandomWalkSimulator(123);

            double x1, y1, x2, y2;

            simulator1.Simulate(100, 1.0, out x1, out y1);
            simulator2.Simulate(100, 1.0, out x2, out y2);

            Assert.AreEqual(x1, x2, 0.000001);
            Assert.AreEqual(y1, y2, 0.000001);
        }



        // 7. Проверяем соответствие теоретическому закону диффузии
        [TestMethod]
        public void RunExperiments_MeanSquareDistance_ShouldMatchTheory()
        {
            RandomWalkStatistics statistics =
                new RandomWalkStatistics(12345);

            int steps = 100;
            int simulations = 30000;
            double stepLength = 1.0;

            RandomWalkResult result = statistics.RunExperiments(
                steps, stepLength, simulations);

            double relativeError = Math.Abs(
                result.MeanSquareDistance - result.TheoreticalMeanSquare)
                / result.TheoreticalMeanSquare;

            Assert.IsTrue(relativeError < 0.05,
                "Средний квадрат расстояния слишком сильно отличается от теории.");
        }

        // 8. Проверяем рост среднего квадрата расстояния при увеличении N
        [TestMethod]
        public void RunExperiments_MeanSquareDistance_ShouldGrowWithSteps()
        {
            RandomWalkStatistics statistics =
                new RandomWalkStatistics(54321);

            double stepLength = 1.0;
            int simulations = 30000;

            RandomWalkResult result100 =
                statistics.RunExperiments(100, stepLength, simulations);

            RandomWalkResult result400 =
                statistics.RunExperiments(400, stepLength, simulations);

            double ratio = result400.MeanSquareDistance
                         / result100.MeanSquareDistance;

            Assert.IsTrue(ratio > 3.6 && ratio < 4.4,
                "При увеличении числа шагов в 4 раза средний квадрат расстояния должен увеличиться примерно в 4 раза.");
        }

    }
}
