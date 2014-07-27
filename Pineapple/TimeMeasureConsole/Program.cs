using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace TimeMeasureConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Go!");
            var start = DateTime.Now;
            for (int i = 0; i < 1; i++)
            {
                var objectUnderTest = new Predictor();
                methodAgainstAlmostEmpty(objectUnderTest);
                methodAgainstEmpty(objectUnderTest);
            }
            var stop = DateTime.Now;
            Console.WriteLine((stop.Ticks - start.Ticks) / 10000);
            Console.ReadKey();
        }

        public const string Deck1 = "Ac Js Th Td Tc Jh 2d 2h 3h 3s "
                + "4h 4c 4s 4d 5c 5s 5h 5d 6s 6h "
                + "9c 9s 9d 9h Ts Js Jd Jc Qd Qh Kd Kc";

        public const string Deck2 = "Qd 3d 5d 3s 4s 7s 2h 2s 2c 3h "
            + "4s 4c 4d 5c 5s 5h 6h 6s 8s 8c " +
            "9c 9d Td Th Tc Ts Jh Jc Js Kc";

        static void methodAgainstAlmostEmpty(Predictor objectUnderTest)
        {
            byte[] heroHand = InputReader.ReadInput("Qs ? 6c ? ? 6d Ad ? 7d 7c ? 8c ?");
            byte[] deck = InputReader.ReadDeck(Deck1);
            byte[] triple = InputReader.ReadInput("2c 3c Jd");
            byte firstIndex;
            byte secondIndex;
            byte firstValue;
            byte secondValue;
            objectUnderTest.FindBestPlaceForTriple(heroHand, deck, triple, out firstIndex, out secondIndex, out firstValue, out secondValue, 125);
        }

        static void methodAgainstEmpty(Predictor objectUnderTest)
        {
            byte[] heroHand = InputReader.ReadInput("6d Qs ? 7h ? ? 4d ? ? Jd 8d 2d ?");
            byte[] deck = InputReader.ReadDeck(Deck2);
            byte[] triple = InputReader.ReadInput("Kh Ks 6d");
            byte firstIndex;
            byte secondIndex;
            byte firstValue;
            byte secondValue;
            objectUnderTest.FindBestPlaceForTriple(heroHand, deck, triple, out firstIndex, out secondIndex, out firstValue, out secondValue, 125);
        }
    }
}
