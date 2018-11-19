using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BullsAndCowsWinApp
{
    public class FanColors : Form
    {
        private const int k_amountOfColorsToPick = 8;
        private const int k_buttonSize = 40;
        private const int k_spaceSize = 10;
        private const int k_spaceFromLeft = 40;
        private const int k_spaceFromTop = 20;
        private const int k_spaceToGuessAnswer = 30;
        private Color[] k_arrayOfColor = new Color[] { Color.Purple, Color.Red, Color.Green, Color.LightBlue, Color.Blue, Color.Yellow, Color.Brown, Color.White};
  
        private Button parentButton;
        public FanColors(Button parentButton)
        {
            this.Size = new Size(300, 170);
            this.Text = "Pick a color";

            this.parentButton = parentButton;

            for (int i = 0; i < k_amountOfColorsToPick; i++)
            {
                Button ChosenColor = new Button();
                ChosenColor.Size = new Size(k_buttonSize, k_buttonSize);
                ChosenColor.BackColor = k_arrayOfColor[i];
                ChosenColor.Click += new EventHandler(ChosenColor_Clicked);
                ChosenColor.Location = new Point(k_spaceFromLeft + (k_buttonSize + k_spaceSize) * (i / 2) , (k_buttonSize + k_spaceSize) * (i % 2) + k_spaceFromTop);
                this.Controls.Add(ChosenColor);
            }
        }

        private void ChosenColor_Clicked(object sender, EventArgs e)
        {
            Button currentButton = ((Button)sender);
            this.parentButton.BackColor = currentButton.BackColor;
        }

    }
}
