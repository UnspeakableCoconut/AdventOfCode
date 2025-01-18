using AdventOfCode;

namespace AoC2015
{
    public class Day14
    {
        private static List<RacingReindeer> reindeer = [];

        public static void Run()
        {
            int yr = 2015,
                day = 14;
            string[] input = Setup.GetInput(yr, day);
            Console.WriteLine($"{yr} Day {day}:");
            PartOne(input).Message(1);
            PartTwo(input).Message(2);
        }

        static int PartOne(string[] input)
        {
            reindeer = LoadReindeer(input);
            int maxDist = 0;
            foreach (RacingReindeer r in reindeer)
            {
                int distance = GetDistance(r.Speed, r.FlightTime, r.RestTime);
                if (maxDist > distance) continue;
                maxDist = distance;
            }
            return maxDist;
        }

        static List<RacingReindeer> LoadReindeer(string[] input)
        {
            List<RacingReindeer> reindeer = new List<RacingReindeer>();
            int[] speeds = input.Extract(@"\d+ km\/s").Extract(@"\d+").ToInt();
            int[] flightTimes = input.Extract(@"km\/s for \d+").Extract(@"\d+").ToInt();
            int[] restTimes = input.Extract(@"rest for \d+").Extract(@"\d+").ToInt();

            for (int i = 0; i < input.Length; i++)
                reindeer.Add(new RacingReindeer(speeds[i], flightTimes[i], restTimes[i]));

            return reindeer;
        }

        static int GetDistance(int speed, int flightTime, int restTime)
        {
            int fullTime = flightTime + restTime,
                fullTurns = 2503 / fullTime,
                secondsLeft = 2503 % fullTime;

            if (secondsLeft > flightTime) secondsLeft = flightTime;

            int fullTurnDist = (speed * flightTime * fullTurns),
                partialTurnDist = (speed * secondsLeft);

            return fullTurnDist + partialTurnDist;
        }

        static int PartTwo(string[] input)
        {
            for (int i = 1; i <= 2503; i++)
            {
                UpdateDistances(i);
                UpdatePoints();
            }
            int maxPoints = 0;
            foreach (RacingReindeer r in reindeer) 
                maxPoints = maxPoints < r.Points ? r.Points : maxPoints;
                
            return maxPoints;
        }

        static void UpdateDistances(int time)
        {
            int reindeerTime;
            RacingReindeer r;
            for (int i = 0; i < reindeer.Count; i++)
            {
                reindeerTime = time; // see, the trick is to NOT overwrite the time/how many seconds have passed
                r = reindeer[i];
                while (reindeerTime > r.FullTime) reindeerTime = reindeerTime - r.FullTime;
                if (reindeerTime > r.FlightTime) continue;
                r.Distance += r.Speed;
                reindeer[i] = r;
            }
        }

        static void UpdatePoints()
        {
            int max = GetMaxDist();
            RacingReindeer r;
            for (int i = 0; i < reindeer.Count; i++)
            {
                r = reindeer[i];
                if (r.Distance < max) continue;
                r.Points++;
                reindeer[i] = r;
            }
        }

        static int GetMaxDist()
        {
            return reindeer.Select(r => r.Distance).ToArray().Max();
        }

        public struct RacingReindeer
        {
            public RacingReindeer(int speed, int flightTime, int restTime)
            {
                Speed = speed;
                FlightTime = flightTime;
                RestTime = restTime;
                FullTime = FlightTime + RestTime;
                Distance = 0;
                Points = 0;
            }

            public int Speed { get; }

            public int FlightTime { get; }

            public int RestTime { get; }

            public int FullTime { get; }

            public int Distance { get; set; }

            public int Points { get; set; }
        }
    }
}
