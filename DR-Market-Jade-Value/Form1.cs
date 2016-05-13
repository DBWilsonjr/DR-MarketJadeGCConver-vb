using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DR_Market_Jade_Value
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void frmLoad(object sender, EventArgs e)
        {
            cmbJLevel.Items.Add(4);
            cmbJLevel.Items.Add(5);
            cmbJLevel.Items.Add(6);
            lblJPrice.Text = "";
            lblGJade.Text = "";
            lblGJHigh.Text = "";
            lblGJLow.Text = "";
            lblGJAvg.Text = "";
            lblHighRat.Text = "";
            lblLowRat.Text = "";
            lblAvgRat.Text = "";
        }

        /// <summary>
        /// This is basically the program. Upon choosing one of the combobox options, the program figures out how many guild coins are saved by spending gems on the
        /// level of jade selected and below, finds how much value you're getting per Gem by spending gems instead of GCs. The premise of the program is that Gems
        /// are more valuable and that a certain value must be met before it's worth spending gems on lv4+ jades instead of just waiting for daily/weekly GCs.
        /// The price of each Jade differs per type, so we do the math for the highest jade (True Damage at 100GC), the lowest Jade (Armor at 30GC), and the average of all 
        /// Jades (53.7, rounded to 54).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbJLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int jadeLvl = 0;
            int JH = 0, JL = 0, JA = 0;
            double HRat = 0, LRat = 0, ARat = 0;

            int.TryParse(cmbJLevel.SelectedItem.ToString(), out jadeLvl);

            //Determine base price of market jade.
            switch (jadeLvl)
            {
                case 4: lblJPrice.Text = "68 Gems"; break;
                case 5: lblJPrice.Text = "165 Gems"; break;
                case 6: lblJPrice.Text = "396 Gems"; break;
                default: lblJPrice.Text = "Read Error"; break;
            }

            //Get math for guild costs of equal leveled jades
            JadeMath(jadeLvl - 3, out JH, out JL, out JA);

            //Output guild costs
            lblGJade.Text = (Math.Pow(3,(jadeLvl - 3))).ToString() + " lvl 3 Jades";
            lblGJHigh.Text = JH.ToString() + " guild coins";
            lblGJLow.Text = JL.ToString() + " guild coins";
            lblGJAvg.Text = JA.ToString() + " guild coins";

            //Do Gem:GC Ratio Math
            GemMath(jadeLvl, JH, JL, JA, out HRat, out LRat, out ARat);

            //Output Ratios
            lblHighRat.Text = HRat.ToString("N2") + " GCs per Gem";
            lblLowRat.Text = LRat.ToString("N2") + " GCs per Gem";
            lblAvgRat.Text = ARat.ToString("N2") + " GCs per Gem";
        }

        /// <summary>
        /// This function determines how many guild coins are required to get the same jade level you can buy from the market.
        /// </summary>
        /// <param name="JLvl"></param>
        /// <param name="Jhigh"></param>
        /// <param name="JLow"></param>
        /// <param name="JAvg"></param>
        public void JadeMath(int JLvl, out int Jhigh, out int JLow, out int JAvg)
        {
            Jhigh = 0;
            JLow = 0;
            JAvg = 0;
            int guildJadeNum = (int)Math.Pow(3,JLvl);
            Jhigh = guildJadeNum * 100;
            JLow = guildJadeNum * 30;
            JAvg = guildJadeNum * 54;
        }

        /// <summary>
        /// This function find the ratios for guild coins per gem. It's basic af.
        /// </summary>
        /// <param name="JLv"></param>
        /// <param name="JHigh"></param>
        /// <param name="JLow"></param>
        /// <param name="JAvg"></param>
        /// <param name="HighRate"></param>
        /// <param name="LowRate"></param>
        /// <param name="AvgRate"></param>
        public void GemMath(int JLv, int JHigh, int JLow, int JAvg, out double HighRate, out double LowRate, out double AvgRate)
        {
            HighRate = 0;
            LowRate = 0;
            AvgRate = 0;
            double jadeVal = 0;

            switch (JLv)
            {
                case 4: jadeVal = 68; break;
                case 5: jadeVal = 165; break;
                case 6: jadeVal = 396; break;
                default: MessageBox.Show("Something went pretty wrong here. You should close this thing out", "Something borked!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
            }

            HighRate = (JHigh / jadeVal);
            LowRate = (JLow / jadeVal);
            AvgRate = (JAvg / jadeVal);
        }

    }
}
