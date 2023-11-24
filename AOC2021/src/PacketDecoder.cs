using System.Text.Encodings;
using System.Text;
using AOC;
namespace AOC_2021
{
    public class PacketDecoder : ISolver
    {
        private string filename;

        private string data;
        public PacketDecoder(string filename)
        {
            this.filename = filename;
        }

        public void Init()
        {
            this.data = File.ReadAllText(this.filename);
        }

        public void PartOne()
        {
            var binaryString = String.Join(String.Empty, this.data.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            Console.WriteLine("Aint nobody got time fo dat.");
        }

        public void PartTwo()
        {
            //throw new NotImplementedException();
        }
    }
}