using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BullsAndCowsGameLogic;




namespace BullsAndCowsWinApp
{
    public class BullsAndCowGameBoard : Form
    {
        public int boardCurrentNumberOfChances = 0;
        private const int k_rectangleSize = 40;
        private const int k_spaceSize = 10;
        private const int k_numberOfGuessesPerChance = 4;
        private const int k_approveButtonHeight = k_rectangleSize / 2;
        private const int k_approveButtonHeightOffset = k_approveButtonHeight / 2;
        private const int k_guessAnswerSize = 17;
        private const int k_guessAnswerSpace = 6;
        private const int k_spaceToGuessAnswer = 30;
        private const int k_spaceFromTop = 20;
        private const int k_spaceFromBlackButton = 40;
        private const int k_spaceFromLeft = 40;
        private int m_chancesLeft;

        private int numberOfCurrentRowGuesses = 0;
        private bool[] currentRowGuessed = new bool[k_numberOfGuessesPerChance];
        private List<Button> guessAnswerButtons = new List<Button>();
        private List<Button> buttons = new List<Button>();
        private List<string> UserGuess = new List<string>();
        private List<Button> BlackButtons = new List<Button>();
        BullsAndCowsGame BCGame = new BullsAndCowsGame();
        private List<string> m_codeToGuess;
        private bool flag;
        //Program restartTheGame = new Program();
        private StringBuilder codeToGuessString = new StringBuilder();

        List<Form> AllWindows;
        List<bool> m_isYesClicked;

        public BullsAndCowGameBoard(int numberOfChances, List<Form> AllWindows, List<bool> m_isYesClicked)
        {
            this.m_isYesClicked = m_isYesClicked;
            this.AllWindows = AllWindows;
            boardCurrentNumberOfChances = numberOfChances;
            this.Size = new Size(380, 660);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Bulls And Cow Game Board";
            m_codeToGuess = BCGame.randomCodeFromComputer();
            m_chancesLeft = numberOfChances;

            for (int i = 0; i < k_numberOfGuessesPerChance; i++)
            {
                Button blackButton = new Button();

                blackButton.Enabled = false;
                blackButton.Size = new Size(k_rectangleSize, k_rectangleSize);
                blackButton.Location = new Point(k_spaceFromLeft + (k_rectangleSize + k_spaceSize) * i, k_spaceFromTop);
                blackButton.BackColor = Color.Black;
                BlackButtons.Add(blackButton);
                this.Controls.Add(blackButton);
            }
            int totalSpacekFromBlackButton = k_spaceFromTop + k_rectangleSize + k_spaceFromBlackButton;

            for (int j = 0; j < boardCurrentNumberOfChances; j++)
            {
                for (int i = 0; i < k_numberOfGuessesPerChance; i++)
                {
                    Button guessButton = new Button();
                    if (j != 0)
                    {
                        guessButton.Enabled = false;
                    }
                    guessButton.Size = new Size(k_rectangleSize, k_rectangleSize);
                    guessButton.Location = new Point(k_spaceFromLeft + (k_rectangleSize + k_spaceSize) * i, (k_rectangleSize + k_spaceSize) * j + totalSpacekFromBlackButton);
                    guessButton.Click += new EventHandler(guessButton_Clicked);
                    guessButton.Name = String.Format("R{0}C{1}", j, i);
                    buttons.Add(guessButton);
                    this.Controls.Add(guessButton);
                }
                Button approveButton = new Button();
                approveButton.Enabled = false;
                approveButton.Size = new Size(k_rectangleSize, k_approveButtonHeight);
                approveButton.Text = "-->>";
                approveButton.Location = new Point(k_spaceFromLeft + (k_rectangleSize + k_spaceSize) * k_numberOfGuessesPerChance, (k_rectangleSize + k_spaceSize) * j + k_approveButtonHeightOffset + totalSpacekFromBlackButton);
                approveButton.Click += new EventHandler(approveButton_Clicked);
                approveButton.Name = String.Format("R{0}", j);
                buttons.Add(approveButton);
                this.Controls.Add(approveButton);

                for (int i = 0; i < k_numberOfGuessesPerChance; i++)
                {
                    Button guessAnswerButton = new Button();
                    guessAnswerButton.Size = new Size(k_guessAnswerSize, k_guessAnswerSize);
                    guessAnswerButton.Location = new Point(k_spaceFromLeft + (k_rectangleSize + k_spaceSize) * k_numberOfGuessesPerChance + k_rectangleSize + k_spaceToGuessAnswer + (k_guessAnswerSize + k_guessAnswerSpace) * (i / 2), (k_rectangleSize + k_spaceSize) * j + (k_guessAnswerSize + k_guessAnswerSpace) * (i % 2) + totalSpacekFromBlackButton);
                    guessAnswerButton.Enabled = false;
                    guessAnswerButtons.Add(guessAnswerButton);
                    this.Controls.Add(guessAnswerButton);
                }
            }

        }
        private void CloseAllWindows(List<Form> AllWindows)
        {
            for(int i = 0; i < AllWindows.Count; i++)
            {
                AllWindows[i].Close();
            }
        }
        private void ShowResult()
        {
            for (int i = 0; i < k_numberOfGuessesPerChance; i++)
            {
                Color CurrentCodeColor = Color.FromName(m_codeToGuess[i]);

                BlackButtons[i].BackColor = CurrentCodeColor;
            }
        }
        private void guessButton_Clicked(object sender, EventArgs e)
        {           
            Button currentButton = ((Button)sender);
            int indexOfC = currentButton.Name.IndexOf("C");
            int buttonRow = -1;
            int buttonCol = -1;
            int.TryParse(currentButton.Name.Substring(1, indexOfC - 1), out buttonRow);
            int.TryParse(currentButton.Name.Substring(indexOfC + 1, currentButton.Name.Length - indexOfC - 1), out buttonCol);
            if (currentRowGuessed[buttonCol] == false)
            {
                currentRowGuessed[buttonCol] = true;
                numberOfCurrentRowGuesses++;
            }
            if (numberOfCurrentRowGuesses == k_numberOfGuessesPerChance)
            {
                buttons[buttonRow * (k_numberOfGuessesPerChance + 1) + k_numberOfGuessesPerChance].Enabled = true;
            }
            FanColors ColorToPick = new FanColors(currentButton);
            ColorToPick.ShowDialog();


        }
        private void approveButton_Clicked(object sender, EventArgs e)
        {
            Button currentButton = ((Button)sender);
            int buttonRow = -1;
            int.TryParse(currentButton.Name.Substring(1, currentButton.Name.Length - 1), out buttonRow);
            for (int i = 0; i < k_numberOfGuessesPerChance; i++)
            {

                buttons[i + (k_numberOfGuessesPerChance + 1) * buttonRow].Enabled = false;
                currentRowGuessed[i] = false;
                if (buttonRow != (boardCurrentNumberOfChances - 1))
                {
                    buttons[i + (k_numberOfGuessesPerChance + 1) * (buttonRow + 1)].Enabled = true;
                }
            }

            for (int i = 0; i < k_numberOfGuessesPerChance; i++)
            {
                UserGuess.Add(buttons[i + ((k_numberOfGuessesPerChance + 1) * buttonRow)].BackColor.Name);
            }

            numberOfCurrentRowGuesses = 0;
            currentButton.Enabled = false;
            BCGame.checkTheGuess(m_chancesLeft, UserGuess, m_codeToGuess);
            m_chancesLeft--;

            for (int i = 0; i < BCGame.Bulls; i++)
            {
                guessAnswerButtons[i + k_numberOfGuessesPerChance * buttonRow].BackColor = Color.Black;
            }

            for (int i = BCGame.Bulls; i < k_numberOfGuessesPerChance; i++)
            {
                if (BCGame.Cows > 0)
                {
                    guessAnswerButtons[i + k_numberOfGuessesPerChance * buttonRow].BackColor = Color.Yellow;
                    BCGame.Cows--;
                }
            }

            flag = BCGame.isGameWon(BCGame.CurrentChance);
            if (flag)
            {
                DialogResult restartGame = MessageBox.Show("Congruts, You won the game!" + Environment.NewLine + "Do you want to play again?", "Game Won", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (restartGame == DialogResult.Yes)
                {
                    this.m_isYesClicked[0] = true;
                    CloseAllWindows(this.AllWindows);
                    //restartTheGame.RunGame();
                }
                else if (restartGame == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }
            if ((m_chancesLeft == 0) && (!flag))
            {
                codeToGuessString = BCGame.ConvertTheCodeToString(m_codeToGuess);
                ShowResult();
                DialogResult restartGame = MessageBox.Show("Bad luck, Try again next time" + Environment.NewLine + "The code is:" + codeToGuessString + Environment.NewLine + "Do you want to play again?", "Game Lose", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (restartGame == DialogResult.Yes)
                {
                    this.m_isYesClicked[0] = true;
                    CloseAllWindows(this.AllWindows);
                    //restartTheGame.RunGame();
                }
                else if (restartGame == DialogResult.No)
                {
                    Environment.Exit(0);
                }
            }
            BCGame.ClearResult(UserGuess);
        }
    }
}

