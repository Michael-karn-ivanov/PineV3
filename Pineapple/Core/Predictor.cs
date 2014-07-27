using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Predictor
    {
        public Random random = new Random(DateTime.Now.Millisecond);

        public decimal FindBestPlaceForTriple(byte[] heroHand, byte[] deck, byte[] triple, out byte firstIndex, out byte secondIndex, out byte firstTriple, out byte secondTriple, int sampleSize)
        {
            firstIndex = 0;
            secondIndex = 0;
            firstTriple = 0;
            secondTriple = 0;
            var emptyIndexPack = determineEmptyIndexes(heroHand);
            decimal maxScore = -1000m;
            decimal maxScore2 = -1000m;

            var variation = new List<byte[]>();
            foreach (var pair in emptyIndexPack)
            {
                variation.Add(new byte[] { pair[0], pair[1], triple[0], triple[1] });
                variation.Add(new byte[] { pair[0], pair[1], triple[0], triple[2] });
                variation.Add(new byte[] { pair[0], pair[1], triple[1], triple[2] });
            }

            byte fiPar = 0;
            byte siPar = 0;
            byte ftPar = 0;
            byte stPar = 0;
            byte siPar2 = 0;
            byte fiPar2 = 0;
            byte ftPar2 = 0;
            byte stPar2 = 0;


            int heroEmptyCards = 0;
            foreach (var h in heroHand) heroEmptyCards += (h == 0 ? 1 : 0);

            foreach (var b in variation)
            {
                var heroHandCopy = new byte[13];
                var deckCopy = new byte[52];
                deck.CopyTo(deckCopy, 0);
                heroHand.CopyTo(heroHandCopy, 0);
                Predictor slave = new Predictor();
                var score = slave.placeCardsAndEval(heroHandCopy, b[0], b[1], b[2], b[3], deckCopy, heroEmptyCards, sampleSize);
                if (score > maxScore)
                {
                    lock ("maxscorelock")
                    {
                        if (score > maxScore)
                        {
                            fiPar2 = fiPar;
                            siPar2 = siPar;
                            ftPar2 = ftPar;
                            stPar2 = stPar;
                            maxScore2 = maxScore;
                            fiPar = b[0];
                            siPar = b[1];
                            ftPar = b[2];
                            stPar = b[3];
                            maxScore = score;
                        }
                    }
                }
                else if (score > maxScore2)
                {
                    lock ("maxscorelock")
                    {
                        if (score > maxScore2)
                        {
                            fiPar2 = b[0];
                            siPar2 = b[1];
                            ftPar2 = b[2];
                            stPar2 = b[3];
                            maxScore2 = score;
                        }
                    }
                }
            }

            Parallel.ForEach(variation, (b) =>
            {
                var heroHandCopy = new byte[13];
                var deckCopy = new byte[52];
                deck.CopyTo(deckCopy, 0);
                heroHand.CopyTo(heroHandCopy, 0);
                Predictor slave = new Predictor();
                var score = slave.placeCardsAndEval(heroHandCopy, b[0], b[1], b[2], b[3], deckCopy, heroEmptyCards, sampleSize);
                if (score > maxScore)
                {
                    lock ("maxscorelock")
                    {
                        if (score > maxScore)
                        {
                            fiPar2 = fiPar;
                            siPar2 = siPar;
                            ftPar2 = ftPar;
                            stPar2 = stPar;
                            maxScore2 = maxScore;
                            fiPar = b[0];
                            siPar = b[1];
                            ftPar = b[2];
                            stPar = b[3];
                            maxScore = score;
                        }
                    }
                }
                else if (score > maxScore2)
                {
                    lock ("maxscorelock")
                    {
                        if (score > maxScore2)
                        {
                            fiPar2 = b[0];
                            siPar2 = b[1];
                            ftPar2 = b[2];
                            stPar2 = b[3];
                            maxScore2 = score;
                        }
                    }
                }
            });

            variation.Clear();
            variation.Add(new[] { fiPar, siPar, ftPar, stPar });
            variation.Add(new[] { fiPar2, siPar2, ftPar2, stPar2 });
            maxScore = -1000;

            Parallel.ForEach(variation, (b) =>
            {
                var heroHandCopy = new byte[13];
                var deckCopy = new byte[52];
                deck.CopyTo(deckCopy, 0);
                heroHand.CopyTo(heroHandCopy, 0);
                Predictor slave = new Predictor();
                var score = slave.placeCardsAndEval(heroHandCopy, b[0], b[1], b[2], b[3], deckCopy, heroEmptyCards, sampleSize * 3);
                if (score > maxScore)
                {
                    lock ("maxscorelock")
                    {
                        if (score > maxScore)
                        {
                            fiPar = b[0];
                            siPar = b[1];
                            ftPar = b[2];
                            stPar = b[3];
                            maxScore = score;
                        }
                    }
                }
            });

            firstIndex = fiPar;
            secondIndex = siPar;
            firstTriple = ftPar;
            secondTriple = stPar;

            return maxScore;
        }

        #region Tree recursion
        public decimal placeCardsAndEval(byte[] heroHand, byte firstIndex, byte secondIndex, byte firstCard, byte secondCard, byte[] deck, int heroEmptyCards, int sampleSize)
        {
            heroHand[firstIndex] = firstCard;
            heroHand[secondIndex] = secondCard;

            var result = Evaluate(heroHand, deck, heroEmptyCards - 2, sampleSize);

            heroHand[firstIndex] = 0;
            heroHand[secondIndex] = 0;
            return result;
        }

        public decimal EvaluateTriple(byte[] heroHand, byte[] triple, List<byte[]> setOfIndexPairs, byte[] deck, int heroEmptyCards, int sampleSize)
        {
            decimal maxScore = -1000m;
            foreach (var pair in setOfIndexPairs)
            {
                var score = placeCardsAndEval(heroHand, pair[0], pair[1], triple[0], triple[1], deck, heroEmptyCards, sampleSize);
                if (score > maxScore) maxScore = score;
                score = placeCardsAndEval(heroHand, pair[0], pair[1], triple[0], triple[2], deck, heroEmptyCards, sampleSize);
                if (score > maxScore) maxScore = score;
                score = placeCardsAndEval(heroHand, pair[0], pair[1], triple[1], triple[2], deck, heroEmptyCards, sampleSize);
                if (score > maxScore) maxScore = score;
            }
            return maxScore;
        }

        public decimal Evaluate(byte[] heroHand, byte[] deck, int heroEmptyCards, int sampleSize)
        {
            LineValue shortValue;
            LineValue middleValue;
            LineValue topValue;
            bool isFantasyGained = false;

            byte[] shortCount;
            byte[] middleCount;
            byte[] topCount;

            decimal score;

            if (lookAndGet(heroHand, out score, heroEmptyCards))
                return score;

            decimal sum = 0m;
            var heroHandCopy = new byte[13];
            var emptyIndexPack = determineEmptyIndexes(heroHand);
            for (int i = 0; i < sampleSize; i++)
            {
                heroHand.CopyTo(heroHandCopy, 0);
                byte[] triple = pickRandomThree(deck);
                sum += EvaluateTriple(heroHandCopy, triple, emptyIndexPack, deck, heroEmptyCards, sampleSize);
                rePickTriple(deck, triple);

            }
            return sum / sampleSize;
        }
        #endregion

        #region count score methods
        public const decimal DeadHandEV = -6.0m;
        public const decimal FantazyEV = 11.5m;

        public bool lookAndGet(byte[] heroHand, out decimal score, int heroEmptyCards)
        {
            score = 0;
           // if (heroEmptyCards > 2) return false;
            LineValue shortValue;
            LineValue middleValue;
            bool isFantasyGained;
            byte[] shortCount;
            byte[] middleCount;
            byte[] countValue;

            countShortRange(heroHand, out countValue);
            shortCount = countValue;
            shortValue = getShortValue(countValue, out isFantasyGained);
            middleValue = getValueFromRange(heroHand, 3, 8, out middleCount);

            int shortInt = (int)shortValue;
            int middleInt = (int)middleValue;
            #region Highcard dead short vs middle
            if (middleValue == LineValue.Highcard && shortInt <= 2)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (middleCount[i] == 1 && shortCount[i] == 0) { break; }
                    else if (shortCount[i] == 0 && middleCount[i] == 1)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            #region Pair dead short vs middle
            if (middleValue == LineValue.Pair && shortInt >= 19 && shortInt <= 20)
            {
                bool topPairUp = false;
                for (int i = 13; i > 0; i--)
                {
                    if (middleCount[i] == 2 && shortCount[i] != 2) { topPairUp = true; break; }
                    else if (middleCount[i] != 2 && shortCount[i] == 2)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
                if (!topPairUp)
                {
                    for (int i = 13; i > 0; i--)
                    {
                        if (middleCount[i] == 1 && shortCount[i] == 0) { break; }
                        else if (middleCount[i] == 0 && middleCount[i] == 1)
                        {
                            score = DeadHandEV;
                            return true;
                        }
                    }
                }
            }
            #endregion
            #region Set dead short vs middle
            if (shortValue == LineValue.Set && middleValue == LineValue.Set)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (middleCount[i] == 3 && shortCount[i] != 3) break;
                    else if (middleCount[i] != 3 && shortCount[i] == 3)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion
            byte[] topCount;
            LineValue topValue = getValueFromRange(heroHand, 8, 13, out topCount);
            int topInt = (int)topValue;

            #region Highcard dead middle vs top
            if (topValue == LineValue.Highcard && middleInt <= 4)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 1 && middleCount[i] == 0) { break; }
                    else if (topCount[i] == 0 && middleCount[i] == 1)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region Pair dead middle vs top
            if (topValue == LineValue.Pair && middleInt >= 19 && middleInt <= 22)
            {
                bool topPairUp = false;
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 2 && middleCount[i] != 2) { topPairUp = true; break; }
                    else if (topCount[i] != 2 && middleCount[i] == 2)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
                if (!topPairUp)
                {
                    for (int i = 13; i > 0; i--)
                    {
                        if (topCount[i] == 1 && middleCount[i] == 0) { break; }
                        else if (topCount[i] == 0 && middleCount[i] == 1)
                        {
                            score = DeadHandEV;
                            return true;
                        }
                    }
                }
            }
            #endregion

            #region Two pairs dead middle vs top
            if (topValue == LineValue.TwoPairs && middleInt >= 23 && middleInt <= 24)
            {
                bool topPairUp = false;
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 2 && middleCount[i] != 2) { topPairUp = true; break; }
                    else if (topCount[i] != 2 && middleCount[i] == 2)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
                if (!topPairUp)
                {
                    for (int i = 13; i > 0; i--)
                    {
                        if (topCount[i] == 1 && middleCount[i] == 0) { break; }
                        else if (topCount[i] == 0 && middleCount[i] == 1)
                        {
                            score = DeadHandEV;
                            return true;
                        }
                    }
                }
            }
            #endregion

            #region Set dead middle vs top
            if (topValue == LineValue.Set && middleInt >= 25 && middleInt <= 27)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 3 && middleCount[i] != 3) break;
                    else if (topCount[i] != 3 && middleCount[i] == 3)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region Straight dead middle vs top
            if (topValue == LineValue.Straight && middleValue == LineValue.Straight)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] > 0 && middleCount[i] == 0) break;
                    else if (topCount[i] == 0 && middleCount[i] > 0)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region Flash dead middle vs top
            if (topValue == LineValue.Flash && middleValue == LineValue.Flash)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] > 0 && middleCount[i] == 0) break;
                    else if (topCount[i] == 0 && middleCount[i] > 0)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region Full house dead middle vs top
            if (topValue == LineValue.FullHouse && middleValue == LineValue.FullHouse)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 3 && middleCount[i] != 3) break;
                    else if (topCount[i] != 3 && middleCount[i] == 3)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region Care dead middle vs top
            if ((topValue == LineValue.Care || topValue == LineValue.Care1E) && (middleValue == LineValue.Care || middleValue == LineValue.Care1E))
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] == 4 && middleCount[i] != 4) break;
                    else if (topCount[i] != 4 && middleCount[i] == 4)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region Str flash dead middle vs top
            if (topValue == LineValue.StraightFlash && middleValue == LineValue.StraightFlash)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (topCount[i] > 0 && middleCount[i] == 0) break;
                    else if (topCount[i] == 0 && middleCount[i] > 0)
                    {
                        score = DeadHandEV;
                        return true;
                    }
                }
            }
            #endregion

            #region мертвые по комбинациям
            if ((topValue == LineValue.Highcard && middleInt >= 19) ||
                (middleInt >= 29 && (topValue == LineValue.Straight || topValue == LineValue.Straight1E || topValue == LineValue.Straight2E)) ||
                (topValue == LineValue.Set2E && middleInt >= 33) ||
                (topValue == LineValue.Set1E && middleInt >= 33) ||
                (topValue == LineValue.Set && middleInt >= 28) ||
                (topValue == LineValue.TwoPairs1E && middleInt >= 31) ||
                (topValue == LineValue.TwoPairs && middleInt >= 25) ||
                (topValue == LineValue.Pair3E && middleInt >= 33) ||
                (topValue == LineValue.Pair2E && middleInt >= 33) ||
                (topValue == LineValue.Pair1E && middleInt >= 28) ||
                (topValue == LineValue.Pair && middleInt >= 23) ||
                (topValue == LineValue.Highcard3E && middleInt >= 33) ||
                (topValue == LineValue.Highcard2E && middleInt >= 28) ||
                (topValue == LineValue.Highcard1E && middleInt >= 23) ||
                (topValue == LineValue.StraightFlash && middleValue == LineValue.Royal) ||
                (topValue == LineValue.Straight3E && middleValue == LineValue.Royal) ||
                (topValue == LineValue.Flash3E && middleValue == LineValue.Royal) ||
                (middleInt >= 33 && topInt >= 31 && topInt <= 32) ||
                (topValue == LineValue.FullHouse && middleInt >= 31) ||
                (middleInt >= 30 && (topValue == LineValue.Flash || topValue == LineValue.Flash1E || topValue == LineValue.Flash2E)))
            {
                score = DeadHandEV;
                return true;
            }
            if ((shortValue == LineValue.Set && (middleValue == LineValue.Pair || middleValue == LineValue.Highcard1E)) ||
                (middleValue == LineValue.Highcard && shortValue == LineValue.Pair))
            {
                score = DeadHandEV;
                return true;
            }
            #endregion

            if (heroEmptyCards != 0)
                return false;

            score += getTopBonus(topValue);
            if (middleValue == LineValue.Set)
                score += 2;
            else
                score += (2 * getTopBonus(middleValue));

            if (shortValue == LineValue.Set)
            {
                for (int i = 13; i > 0; i--)
                {
                    if (shortCount[i] == 3)
                    {
                        score += (9 + i);
                        break;
                    }
                }
            }
            if (shortValue == LineValue.Pair)
            {
                for (int i = 13; i > 4; i--)
                {
                    if (shortCount[i] == 2)
                    {
                        score += (i - 4);
                        break;
                    }
                }
            }
            if (isFantasyGained) score += FantazyEV;
            return true;
        }

        public decimal getTopBonus(LineValue value)
        {
            switch (value)
            {
                case LineValue.Royal:
                    return 25;
                case LineValue.StraightFlash:
                    return 15;
                case LineValue.Care:
                    return 10;
                case LineValue.FullHouse:
                    return 6;
                case LineValue.Flash:
                    return 4;
                case LineValue.Straight:
                    return 2;
                default:
                    return 0;
            }
        }

        public void countShortRange(byte[] heroHand, out byte[] countValue)
        {
            countValue = new byte[14];
            for (int i = 0; i < 3; i++)
            {
                if (heroHand[i] == 0)
                {
                    countValue[0]++;
                }
                else
                {
                    var b = (heroHand[i] - 1);
                    countValue[b % 13 + 1]++;
                }
            }
        }

        public LineValue getShortValue(byte[] countValue, out bool isFantasyGained)
        {
            isFantasyGained = false;
            if (countValue[0] == 3) return LineValue.E3;
            if (countValue[0] == 2) return LineValue.Highcard2E;
            if (countValue[0] == 1)
            {
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 2)
                    {
                        isFantasyGained = i >= 11;
                        return LineValue.Pair1E;
                    }
                    else if (countValue[i] == 1) return LineValue.Highcard1E;
                }
                return LineValue.Highcard1E;
            }
            for (int i = 1; i < countValue.Length; i++)
            {
                if (countValue[i] == 3)
                {
                    isFantasyGained = true;
                    return LineValue.Set;
                }
                else if (countValue[i] == 2)
                {
                    isFantasyGained = i >= 11;
                    return LineValue.Pair;
                }
            }
            return LineValue.Highcard;
        }

        public LineValue getValueFromRange(byte[] heroHand, int start, int end, out byte[] count)
        {
            byte[] countValue;
            bool colorValue;
            countRange(heroHand, start, end, out countValue, out colorValue);
            count = countValue;
            return getMiddleValue(countValue, colorValue);
        }

        public void countRange(byte[] heroHand, int startIndexInclusive, int endIndexExclusive, out byte[] countValue, out bool sameColor)
        {
            countValue = new byte[14];
            sameColor = true;
            int color = 0;
            for (int i = startIndexInclusive; i < endIndexExclusive; i++)
            {
                if (heroHand[i] == 0)
                {
                    countValue[0]++;
                }
                else
                {
                    var b = (heroHand[i] - 1);
                    countValue[b % 13 + 1]++;
                    if (sameColor)
                    {
                        int newColor = heroHand[i] < 14 ? 1 : heroHand[i] < 27 ? 2 : heroHand[i] < 40 ? 3 : 4;
                        if (color == 0) color = newColor;
                        else sameColor = (color == newColor);
                    }
                }
            }
        }

        public LineValue getMiddleValue(byte[] countValue, bool sameColorValue)
        {
            if (countValue[0] == 5) return LineValue.E5;
            if (countValue[0] == 4) return LineValue.E4;
            #region 3 empty
            if (countValue[0] == 3)
            {
                int firstValue = -1;
                int secondValue = -1;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 2) return LineValue.Pair3E;
                    else if (countValue[i] == 1)
                    {
                        if (firstValue > 0)
                        {
                            secondValue = i;
                            break;
                        }
                        else firstValue = i;
                    }
                }
                if (firstValue >= 9 && secondValue >= 9)
                {
                    if (sameColorValue) return LineValue.Royal3E;
                    else return LineValue.Straight3E;
                }
                if ((firstValue <= 4 && secondValue == 13) || (secondValue - firstValue <= 4))
                {
                    if (sameColorValue) return LineValue.StraightFlash3E;
                    else return LineValue.Straight3E;
                }
                if (sameColorValue) return LineValue.Flash3E;
                return LineValue.Highcard3E;
            }
            #endregion
            #region 2 empty
            if (countValue[0] == 2)
            {
                int firstValue = -1;
                int secondValue = -1;
                int thirdValue = -1;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 3) return LineValue.Set2E;
                    else if (countValue[i] == 2) return LineValue.Pair2E;
                    else if (countValue[i] == 1)
                    {
                        if (firstValue < 0) firstValue = i;
                        else if (secondValue < 0) secondValue = i;
                        else if (thirdValue < 0)
                        {
                            thirdValue = i;
                            break;
                        }
                    }
                }
                if (firstValue >= 9 && thirdValue >= 9)
                {
                    if (sameColorValue) return LineValue.Royal2E;
                    else return LineValue.Straight2E;
                }
                if ((secondValue <= 4 && thirdValue == 13) || (thirdValue - firstValue <= 4))
                {
                    if (sameColorValue) return LineValue.StraightFlash2E;
                    else return LineValue.Straight2E;
                }
                if (sameColorValue) return LineValue.Flash2E;
                return LineValue.Highcard2E;
            }
            #endregion
            #region 1 empty
            if (countValue[0] == 1)
            {
                int firstValue = -1;
                int secondValue = -1;
                int thirdValue = -1;
                int fourthValue = -1;
                int firstPairIndex = -1;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 4) return LineValue.Care1E;
                    else if (countValue[i] == 3) return LineValue.Set1E;
                    else if (countValue[i] == 2)
                    {
                        if (firstPairIndex >= 0) return LineValue.TwoPairs1E;
                        else
                        {
                            if (firstValue < 0) firstPairIndex = i;
                            else return LineValue.Pair1E;
                        }
                    }
                    else if (countValue[i] == 1)
                    {
                        if (firstPairIndex >= 0) return LineValue.Pair1E;
                        else
                        {
                            if (firstValue < 0) firstValue = i;
                            else if (secondValue < 0) secondValue = i;
                            else if (thirdValue < 0) thirdValue = i;
                            else fourthValue = i;
                        }
                    }
                }

                if (firstValue >= 9 && fourthValue >= 9)
                {
                    if (sameColorValue) return LineValue.Royal1E;
                    else return LineValue.Straight1E;
                }
                if ((thirdValue <= 4 && fourthValue == 13) || (fourthValue - firstValue <= 4))
                {
                    if (sameColorValue) return LineValue.StraightFlash1E;
                    else return LineValue.Straight1E;
                }
                if (sameColorValue) return LineValue.Flash1E;
                return LineValue.Highcard1E;
            }
            #endregion
            #region 0 empty
            if (countValue[0] == 0)
            {
                int firstValue = -1;
                int secondValue = -1;
                int thirdValue = -1;
                int fourthValue = -1;
                int fifthValue = -1;
                bool hasPair = false;
                bool hasTriple = false;
                for (int i = 1; i < countValue.Length; i++)
                {
                    if (countValue[i] == 4) return LineValue.Care;
                    else if (countValue[i] == 3)
                    {
                        if (hasPair) return LineValue.FullHouse;
                        hasTriple = true;
                    }
                    else if (countValue[i] == 2)
                    {
                        if (hasTriple) return LineValue.FullHouse;
                        else if (hasPair) return LineValue.TwoPairs;
                        else hasPair = true;
                    }
                    else if (countValue[i] == 1)
                    {
                        if (firstValue < 0) firstValue = i;
                        else if (secondValue < 0) secondValue = i;
                        else if (thirdValue < 0) thirdValue = i;
                        else if (fourthValue < 0) fourthValue = i;
                        else fifthValue = i;
                    }
                }
                if (hasTriple) return LineValue.Set;
                if (hasPair) return LineValue.Pair;

                if (firstValue == 9 && fifthValue == 13)
                {
                    if (sameColorValue) return LineValue.Royal;
                    else return LineValue.Straight;
                }
                if (fourthValue == 4 && fifthValue == 5 || (fifthValue - firstValue) == 4)
                {
                    if (sameColorValue) return LineValue.StraightFlash;
                    else return LineValue.Straight;
                }
                if (sameColorValue) return LineValue.Flash;
                return LineValue.Highcard;
            }
            #endregion

            throw new Exception();
        }
        #endregion

        #region Utils
        public const int DeckSize = 52;

        public byte[] pickRandomThree(byte[] deck)
        {
            byte[] result = new byte[3];

            var first = randomNext();
            while (deck[first] == 1) first = randomNext();
            deck[first] = 1;
            result[0] = (byte)(first + 1);

            var second = randomNext();
            while (deck[second] == 1) second = randomNext();
            deck[second] = 1;
            result[1] = (byte)(second + 1);

            var third = randomNext();
            while (deck[third] == 1) third = randomNext();
            deck[third] = 1;
            result[2] = (byte)(third + 1);

            return result;
        }

        public void rePickTriple(byte[] deck, byte[] triple)
        {
            deck[triple[0] - 1] = 0;
            deck[triple[1] - 1] = 0;
            deck[triple[2] - 1] = 0;
        }

        public int randomNext()
        {
            return random.Next(DeckSize);
        }

        public List<byte[]> determineEmptyIndexes(byte[] heroHand)
        {
            var result = new List<byte[]>();
            var topLineEmpty = new List<byte>();
            var middleLineEmpty = new List<byte>();
            var bottomLineEmpty = new List<byte>();

            for (byte i = 0; i < heroHand.Length; i++)
            {
                if (heroHand[i] == 0)
                {
                    if (i < 3)
                    {
                        topLineEmpty.Add(i);
                        continue;
                    }
                    if (i < 8)
                    {
                        middleLineEmpty.Add(i);
                        continue;
                    }
                    bottomLineEmpty.Add(i);
                }
            }

            if (topLineEmpty.Count > 1) result.Add(new byte[] { topLineEmpty[0], topLineEmpty[1] });
            if (middleLineEmpty.Count > 1) result.Add(new byte[] { middleLineEmpty[0], middleLineEmpty[1] });
            if (bottomLineEmpty.Count > 1) result.Add(new byte[] { bottomLineEmpty[0], bottomLineEmpty[1] });

            if (topLineEmpty.Count > 0)
            {
                if (middleLineEmpty.Count > 0)
                {
                    result.Add(new byte[] { topLineEmpty[0], middleLineEmpty[0] });
                    result.Add(new byte[] { middleLineEmpty[0], topLineEmpty[0] });
                }
                if (bottomLineEmpty.Count > 0)
                {
                    result.Add(new byte[] { topLineEmpty[0], bottomLineEmpty[0] });
                    result.Add(new byte[] { bottomLineEmpty[0], topLineEmpty[0] });
                }
            }

            if (middleLineEmpty.Count > 0)
                if (bottomLineEmpty.Count > 0)
                {
                    result.Add(new byte[] { middleLineEmpty[0], bottomLineEmpty[0] });
                    result.Add(new byte[] { bottomLineEmpty[0], middleLineEmpty[0] });
                }

            return result;
        }
        #endregion
    }
}
