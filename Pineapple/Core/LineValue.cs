using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public enum LineValue
    {
        // Highcard dead short vs middle, Highcard dead middle vs top
        Highcard = 0,
        Highcard1E = 1,
        Highcard2E = 2,
        // Highcard dead middle vs top
        Highcard3E = 3,
        E4 = 4,
        Straight3E = 5,
        Flash3E = 6,
        StraightFlash3E = 7,
        Royal3E = 8,
        E3 = 9,
        E5 = 10,
        Straight1E = 11,
        Flash1E = 12,
        StraightFlash1E = 13,
        Royal1E = 14,
        Straight2E = 15,
        Flash2E = 16,
        StraightFlash2E = 17,
        Royal2E = 18,
        // Pair dead short vs middle, Pair dead middle vs top
        Pair = 19,
        Pair1E = 20,
        // Pair dead middle vs top
        Pair2E = 21,
        Pair3E = 22,
        // Two pairs dead middle vs top
        TwoPairs1E = 23,
        TwoPairs = 24,     
        // Set dead middle vs top
        Set1E = 25,
        Set2E = 26,
        Set = 27,
        Straight = 28,
        Flash = 29,
        FullHouse = 30,
        Care = 31,
        Care1E = 32,
        StraightFlash = 33,
        Royal = 34,
    }

    public enum LineValue2
    {
        Highcard = 0,
        Pair = 1,
        TwoPairs = 2,
        Set = 3,
        Straight = 4,
        Flash = 5,
        FullHouse = 6,
        Care = 7,
        StraightFlash = 8,
        Royal = 9,
        Highcard1E = 10,
        Pair1E = 11,
        TwoPairs1E = 12,
        Set1E = 13,
        Straight1E = 14,
        Flash1E = 15,
        Care1E = 16,
        StraightFlash1E = 17,
        Royal1E = 18,
        Highcard2E = 19,
        Pair2E = 20,
        Set2E = 21,
        Straight2E = 22,
        Flash2E = 23,
        StraightFlash2E = 24,
        Royal2E = 25,
        Highcard3E = 26,
        Pair3E = 27,
        Straight3E = 28,
        Flash3E = 29,
        StraightFlash3E = 30,
        Royal3E = 31,
        E4 = 32,
        E5 = 33,
        E3 = 34
    }


}
