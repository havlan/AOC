namespace AOC
{
    class TreacheryOfWhales : ISolver
    {
        string filename;
        List<int> data;

        public TreacheryOfWhales(string filename)
        {
            this.filename = filename;
            this.data = ReadData();
        }

        private List<int> ReadData()
        {
            var data = new List<int>();
            using (var sr = new StreamReader(this.filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    foreach (var l in line.Split(","))
                    {
                        data.Add(int.Parse(l));
                    }
                }
            }
            return data;
        }

        private int SumN(int n)
        {
            return (n * (1 + n)) / 2;
        }

        public void PartOne()
        {
            var lower = data.Min();
            var upper = data.Max();

            var minFuel = Int32.MaxValue;
            for (var i = lower; i < upper; i++)
            {
                var currentFuel = 0;
                foreach (var c in this.data)
                {
                    var dist = Math.Abs(i - c);
                    currentFuel += dist;
                }

                if (currentFuel < minFuel)
                {
                    minFuel = currentFuel;
                }
            }

            Console.WriteLine("{0}", minFuel);
        }

        public void PartTwo()
        {

            var lower = data.Min();
            var upper = data.Max();

            var minFuel = Int32.MaxValue;
            for (var i = lower; i < upper; i++)
            {
                var currentFuel = 0;
                foreach (var c in this.data)
                {
                    var dist = SumN(Math.Abs(i - c));
                    currentFuel += dist;
                }

                if (currentFuel < minFuel)
                {
                    minFuel = currentFuel;
                }
            }

            Console.WriteLine("{0}", minFuel);
        }
    }
}