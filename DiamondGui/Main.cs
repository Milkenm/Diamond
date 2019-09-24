#region Usings
using System;
using System.Diagnostics;
using System.Windows.Forms;

using static DiamondGui.Controls;
using static DiamondGui.Core;
using static DiamondGui.Functions;
using static DiamondGui.Static;
#endregion Usings



namespace DiamondGui
{
    public partial class Main : Form
    {
        #region Main
        public Main()
        {
            try
            {
                InitializeComponent();
                MainForm = this;
            }
            catch (Exception _Exception)
            {
                ShowException(_Exception, "DiamondGui.Main.Main()");
            }
        }

        private void Main_Load(object sender, EventArgs e) => MainLoad();

        private void Main_FormClosing(object sender, FormClosingEventArgs e) => MainClosing();
        #endregion Main







        private void button_start_Click(object sender, EventArgs e) => ButtonStart(); // Button Start
        
        private void checkBox_revealToken_CheckedChanged(object sender, EventArgs e) => CheckBoxRevealToken(); // CheckBox Reveal Token

        private void button_refreshStatus_Click(object sender, EventArgs e) => ButtonRefreshStatus(); // Button Refresh Status

        private void button_setGame_Click(object sender, EventArgs e) => ButtonRefreshGame(); // Button Refresh Game
    }
}
