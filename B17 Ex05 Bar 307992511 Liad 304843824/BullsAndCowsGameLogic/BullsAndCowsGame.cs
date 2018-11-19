using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsGameLogic
{
    public class BullsAndCowsGame
    {
        private static int m_bulls;
        private static int m_cows;
        private List<string> lotteryRange = new List<string> { "Purple", "Red", "Green", "LightBlue", "Blue", "Yellow", "Brown", "White" };
        private const int k_sizeOfCodeToGuess = 4;
        private int m_CurrentNumberOfChances = k_MinGuesses;

        private const int k_MaxGuesses = 10;
        private const int k_MinGuesses = 4;

        public int CurrentChance
        {
            get
            {
                return m_CurrentNumberOfChances;
            }
            set
            {
                m_CurrentNumberOfChances = value;
            }
        }
        public int MaxGuesses
        {
            get
            {
                return k_MaxGuesses;
            }
        }
        public int MinGuesses
        {
            get
            {
                return k_MinGuesses;
            }
        }
        public BullsAndCowsGame()
        {

            m_bulls = 0;
            m_cows = 0;
        }

        public int Bulls
        {
            get { return m_bulls; }
            set { m_bulls = value; }
        }

        public int Cows
        {
            get { return m_cows; }
            set { m_cows = value; }
        }

        public void checkTheGuess(int NumOfTries, List<string> UserGuess, List<string> codeToGuess)
        {
            for (int i = 0; i < k_sizeOfCodeToGuess; i++)
            {
                for (int j = 0; j < k_sizeOfCodeToGuess; j++)
                {
                    if (codeToGuess[i] == UserGuess[j])
                    {
                        if (i == j)
                        {
                            Bulls++;
                        }
                        else
                        {
                            Cows++;
                        }
                    }

                }
            }
        }

        public bool isGameWon(int NumOfTries)
        {
            if (NumOfTries > 0)
            {

                if (Bulls == k_sizeOfCodeToGuess)
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> randomCodeFromComputer()
        {
            Random color = new Random();
            int drawedNumber;
            List<string> codeToGuess = new List<string>();

            for (int i = 0; i < k_sizeOfCodeToGuess; i++)
            {
                drawedNumber = color.Next(lotteryRange.Count - 1);
                codeToGuess.Add(lotteryRange[drawedNumber]);
                lotteryRange.Remove(lotteryRange[drawedNumber]);
            }

            return codeToGuess;
        }

        public void ClearResult(List<string> UserGuess)
        {
            Bulls = 0;
            Cows = 0;
            for (int i = UserGuess.Count - 1; i >= 0; i--)
            {
                UserGuess.Remove(UserGuess[i]);
            }
        }
        public StringBuilder ConvertTheCodeToString(List<string> codeToGuess)
        {
            StringBuilder codeToGuessString = new StringBuilder();
            for (int i = 0; i < codeToGuess.Count; i++)
            {
                codeToGuessString.Append(codeToGuess[i]);
                codeToGuessString.Append(",");
            }
            return codeToGuessString;
        }
    }
}

