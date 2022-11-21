

using System.IO;

namespace AdventOfCode._2021;

public class Day03
{
    [Test]
    public void One()
    {
        string[] lines = File.ReadAllLines("2021/Day03.txt");

        int gamma_rate = 0;
        int epsilon_rate = 0;

        string newstring_g = "";
        string newstring_e = "";

        for (int i = 0; i < 12; i++)
        {
            int count_0 = 0;
            int count_1 = 0;

            foreach (string reeks in lines)
            {
                if (reeks[i] == '0') { count_0++; } else { count_1++; }
            }

            if (count_0 > count_1) { newstring_e = newstring_e + "0"; newstring_g = newstring_g + "1"; }
            else { newstring_e = newstring_e + "1"; newstring_g = newstring_g + "0"; }

        }

        gamma_rate = Convert.ToInt32(newstring_g, 2);
        epsilon_rate = Convert.ToInt32(newstring_e, 2);

        Console.WriteLine(gamma_rate);
        Console.WriteLine(epsilon_rate);

        var answer = gamma_rate * epsilon_rate;

        answer.Should().Be(2954600);
    }

    [Test]
    public void Two()
    {
        var numbers = File.ReadAllLines("2021/Day03.txt").Select(line => new BinaryNumber(line)).ToArray();
        var answer = Process(numbers, 0, true) * Process(numbers, 0, false);
        answer.Should().Be(1662846);
    }

    [TestCase("1111", 15)]
    [TestCase("0001", 01)]
    public void Value_of_binary_number(string str, int value)
        => new BinaryNumber(str).Value.Should().Be(value);

    static int Process(BinaryNumber[] numbers, int position, bool preferOne)
    {
        if (numbers.Length == 1) { return numbers[0].Value; }

        var zeros = numbers.Where(n => n.IsZero(position)).ToArray();
        var ones = numbers.Where(n => n.IsOne(position)).ToArray();
        var useOnes = preferOne
            ? ones.Length >= zeros.Length
            : ones.Length < zeros.Length;
        return Process(useOnes ? ones : zeros, position + 1, preferOne);
    }

        sealed class BinaryNumber
    {
        private readonly string Val;

        public BinaryNumber(string val) => Val = val;

        public int Length => Val.Length;

        public int Value => Convert.ToInt32(Val, 2);

        public bool IsZero(int index) => Val[index] == '0';
        public bool IsOne(int index) => Val[index] == '1';

        public override string ToString() => Val;
    }
}
