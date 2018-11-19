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
    public class CreateForm : Form
    {
        Button m_ButtonNumberOfChances = new Button();
        Button m_ButtonStart = new Button();
        BullsAndCowsGame bcGame = new BullsAndCowsGame();
        List<Form> AllWindows = new List<Form>();
        List<bool> m_isYesClicked;
        public CreateForm(List<Form> AllWindows, List<bool> m_isYesClicked)
        {
            this.m_isYesClicked = m_isYesClicked;
            this.AllWindows = AllWindows;
            this.Size = new Size(292, 160);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Text = "Bulls And Cow";
            InitialControls();
        }
        private void InitialControls()
        {
            m_ButtonStart.Text = "Start";
            m_ButtonStart.Location = new Point(182, 85);
            m_ButtonStart.Size = new Size(76, 28);
            this.Controls.Add(this.m_ButtonStart);

            m_ButtonNumberOfChances.Text = "Number of chances: " + bcGame.CurrentChance;
            m_ButtonNumberOfChances.Location = new Point(16, 10);
            m_ButtonNumberOfChances.Size = new Size(246, 20);
            this.Controls.Add(this.m_ButtonNumberOfChances);
            m_ButtonNumberOfChances.Click += new EventHandler(ButtonNumberOfChances_Clicked);
            m_ButtonStart.Click += new EventHandler(ButtonStart_Clicked);
        }

        private void ButtonNumberOfChances_Clicked(object sender, EventArgs e)
        {
            if (bcGame.CurrentChance < bcGame.MaxGuesses)
            {
                m_ButtonNumberOfChances.Text = "Number of chances: " + ++bcGame.CurrentChance;
            }
            else
            {
                bcGame.CurrentChance = bcGame.MinGuesses;
                m_ButtonNumberOfChances.Text = "Number of chances: " + bcGame.CurrentChance;
            }
        }

        private void ButtonStart_Clicked(object sender, EventArgs e)
        {
            BullsAndCowGameBoard GameBoard = new BullsAndCowGameBoard(bcGame.CurrentChance, this.AllWindows, this.m_isYesClicked);
            this.AllWindows.Add(GameBoard);
            GameBoard.ShowDialog();
        }

    }
}
