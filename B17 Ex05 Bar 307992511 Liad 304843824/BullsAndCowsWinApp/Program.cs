using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BullsAndCowsGameLogic;


namespace BullsAndCowsWinApp
{
    public class Program
    {
        internal List<bool> m_isYesClicked = new List<bool>();
        internal List<Form> AllWindows = new List<Form>();

        [STAThread]
        public static void Main()
        {
            Program BullsAndCows = new Program();          
            BullsAndCows.RunGame();
        }

        public void RunGame()
        {
            this.m_isYesClicked.Add(false);
            do
            {
                m_isYesClicked[0] = false;
                CreateForm form = new CreateForm(this.AllWindows, m_isYesClicked);
                AllWindows.Add(form);
                form.ShowDialog();
            } while (m_isYesClicked[0]);
        }
    }
}

