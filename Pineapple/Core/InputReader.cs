using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class InputReader
    {
        public static byte[] ReadInput(string input)
        {
            String[] cards = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var result = new byte[cards.Length];
            for (int i = 0; i < cards.Length; i++)
            {
                result[i] = ReadSingle(cards[i]);
            }
            return result;
        }

        public static byte[] ReadDeck(string input)
        {
            var deck = new byte[52];
            for (int i = 0; i < deck.Length; i++) deck[i] = 1;
            foreach (var b in ReadInput(input))
            {
                if (deck[b - 1] == 1) deck[b - 1] = 0;
            }
            return deck;
        }

        public static byte ReadSingle(string single)
        {
            if (single.Length != 2) return 0;
            var low = single.ToLower();
            char color = low[1];
            byte multiplicator = 0;
            if (color == 's')
                multiplicator = 0;
            else if (color == 'c')
                multiplicator = 1;
            else if (color == 'd')
                multiplicator = 2;
            else if (color == 'h')
                multiplicator = 3;
            else return 0;
            char value = low[0];
            if (value == '2')
                return (byte)(1 + multiplicator * 13);
            else if (value == '3')
                return (byte)(2 + multiplicator * 13);
            else if (value == '4')
                return (byte)(3 + multiplicator * 13);
            else if (value == '5')
                return (byte)(4 + multiplicator * 13);
            else if (value == '6')
                return (byte)(5 + multiplicator * 13);
            else if (value == '7')
                return (byte)(6 + multiplicator * 13);
            else if (value == '8')
                return (byte)(7 + multiplicator * 13);
            else if (value == '9')
                return (byte)(8 + multiplicator * 13);
            else if (value == 't')
                return (byte)(9 + multiplicator * 13);
            else if (value == 'j')
                return (byte)(10 + multiplicator * 13);
            else if (value == 'q')
                return (byte)(11 + multiplicator * 13);
            else if (value == 'k')
                return (byte)(12 + multiplicator * 13);
            else if (value == 'a')
                return (byte)(13 + multiplicator * 13);
            else return 0;
        }

        public static String WriteSingle(byte single)
        {
            var rem = (single - 1) % 13;
            var color = (byte)((single - 1 - rem) / 13);
            StringBuilder result = new StringBuilder();
            switch (rem)
            {
                case 0:
                    result.Append("2");
                    break;
                case 1:
                    result.Append("3");
                    break;
                case 2:
                    result.Append("4");
                    break;
                case 3:
                    result.Append("5");
                    break;
                case 4:
                    result.Append("6");
                    break;
                case 5:
                    result.Append("7");
                    break;
                case 6:
                    result.Append("8");
                    break;
                case 7:
                    result.Append("9");
                    break;
                case 8:
                    result.Append("T");
                    break;
                case 9:
                    result.Append("J");
                    break;
                case 10:
                    result.Append("Q");
                    break;
                case 11:
                    result.Append("K");
                    break;
                case 12:
                    result.Append("A");
                    break;
                default: break;
            }
            switch (color)
            {
                case 0:
                    result.Append("s");
                    break;
                case 1:
                    result.Append("c");
                    break;
                case 2:
                    result.Append("d");
                    break;
                case 3:
                    result.Append("h");
                    break;
                default: break;
            }
            return result.ToString();
        }

        public static String WriteLine(byte index)
        {
            if (index < 3) return "Short";
            if (index < 8) return "Middle";
            if (index < 13) return "Top";
            return "Undefined";
        }

        public static HeroHandBuilder HeroHand()
        {
            return new HeroHandBuilder();
        }

        public static DeckBuilder Deck()
        {
            return new DeckBuilder();
        }
    }

    public class DeckBuilder
    {
        public byte[] Deck = new byte[52];

        public DeckBuilder Remove(String line)
        {
            byte[] s = InputReader.ReadInput(line);
            foreach (byte idx in s)
                Deck[idx - 1] = 1;
            return this;
        }
    }

    public class HeroHandBuilder
    {
        public byte[] Hand = new byte[13];

        public HeroHandBuilder Short(String shortLine)
        {
            byte[] s = InputReader.ReadInput(shortLine);
            for (int i = 0; i < 3; i++)
            {
                if (s.Length - 1 < i)
                    Hand[i] = 0;
                else
                    Hand[i] = s[i];
            }
            return this;
        }

        public HeroHandBuilder Middle(String middleLine)
        {
            byte[] s = InputReader.ReadInput(middleLine);
            for (int i = 0; i < 5; i++)
            {
                if (s.Length - 1 < i)
                    Hand[i + 3] = 0;
                else
                    Hand[i + 3] = s[i];
            }
            return this;
        }

        public HeroHandBuilder Top(String topLine)
        {
            byte[] s = InputReader.ReadInput(topLine);
            for (int i = 0; i < 5; i++)
            {
                if (s.Length - 1 < i)
                    Hand[i + 8] = 0;
                else
                    Hand[i + 8] = s[i];
            }
            return this;
        }
    }
}
