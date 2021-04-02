using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageJImporter
{
    public partial class ColorLevelDialog : Form
    {
        private LevelInformation currentLevelInformation;
        private SendLevelInformation sendLevelsBack;
        private FlowLayoutPanel mainPanel;
        public ColorLevelDialog(LevelInformation defaultLevelInformation,
            SendLevelInformation levelCallBackDelegate)
        {
            //set up all the controls
            InitializeComponent();
            
            //set private fields to parameter value
            this.currentLevelInformation = defaultLevelInformation;
            this.sendLevelsBack = levelCallBackDelegate;

            //make sure we don't have null pointers
            if (currentLevelInformation == null) currentLevelInformation = LevelInformation.DefaultLevels;
            if (sendLevelsBack == null) sendLevelsBack = DummyDelegateHandler;

            //make all the levels show up
            BuildLevelsShowing(currentLevelInformation);

            //resize stuff
            ResizeMainPanelAndExitButton(null, null);
        }//end constructor

        private void ResizeMainPanelAndExitButton(object sender, EventArgs e)
        {
            mainPanel.Size = new Size(this.Width - 100, this.Height - 100);
            mainPanel.Location = new Point(12, 12);
        }//end event handler for the form being resized

        private void BuildLevelsShowing(LevelInformation levels)
        {
            //set up the panel we'll show
            mainPanel = new FlowLayoutPanel
            {
                BackColor = Color.Honeydew,
                ForeColor = Color.Navy,
                AutoSize = false,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Font = new Font(FontFamily.GenericSerif, 12),
                Location = new Point(12, 12),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Margin = new Padding(50),
            };

            //start trying to do panels
            for(int i = 0; i < levels.Count; i++)
            {
                LevelPanel subPanel = GetCloseComponents(out TextBox label1,
                    out Button button1, out Button button2,
                    out NumericUpDown numeric1, out NumericUpDown numeric2,
                    levels[i]);
                subPanel.Location = new Point(12, subPanel.Height * i);
                mainPanel.Controls.Add(subPanel);
            }//end looping over each level
            
            Controls.Add(mainPanel);
        }//end BuildLevelsShowing()

        private LevelPanel GetCloseComponents(out TextBox levelLabel, out Button foreColorButton,
            out Button backColorButton, out NumericUpDown levelMin,
            out NumericUpDown levelMax, LevelInformation.Level level)
        {
            //set up Panel
            LevelPanel subPanel = new LevelPanel(level)
            {
                BackColor = Color.GhostWhite,
                ForeColor = Color.Indigo,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };

            //set up TextBox
            levelLabel = new TextBox
            {
                AutoSize = false,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = subPanel.Level.LevelName,
            };
            levelLabel.Location = new Point(levelLabel.Location.X, 12);

            //set up foreColorButton
            foreColorButton = new Button
            {
                BackColor = subPanel.Level.ForeColor,
                ForeColor = subPanel.Level.BackColor,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = "Text Color",
            };
            foreColorButton.Location = new Point(foreColorButton.Location.X, levelLabel.Location.Y + levelLabel.Height + 10);

            //set up backColorButton
            backColorButton = new Button
            {
                BackColor = subPanel.Level.BackColor,
                ForeColor = subPanel.Level.ForeColor,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = "Button Color",
            };
            backColorButton.Location = new Point(backColorButton.Location.X, foreColorButton.Location.Y + foreColorButton.Height + 10);

            //set up levelMin
            levelMin = new NumericUpDown
            {
                BackColor = Color.GhostWhite,
                ForeColor = Color.Indigo,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Minimum = -10,
                Value = subPanel.Level.LevelStart,
                DecimalPlaces = 2,
            };
            levelMin.Location = new Point(foreColorButton.Location.X + foreColorButton.Width + 50, foreColorButton.Location.Y);

            //set up levelMax
            levelMax = new NumericUpDown
            {
                BackColor = Color.GhostWhite,
                ForeColor = Color.Indigo,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Value = subPanel.Level.LevelEnd,
                DecimalPlaces = 2,
                Maximum = 100,
            };
            levelMax.Location = new Point(backColorButton.Location.X + backColorButton.Width + 50, backColorButton.Location.Y);

            //add everything to the panel
            subPanel.Controls.Add(levelLabel);
            subPanel.Controls.Add(foreColorButton);
            subPanel.Controls.Add(backColorButton);
            subPanel.Controls.Add(levelMin);
            subPanel.Controls.Add(levelMax);

            return subPanel;
        }//end GetCloseComponents(params...)

        /// <summary>
        /// default delegate pointer so we don't get null pointer
        /// </summary>
        /// <param name="level">doesn't really matter</param>
        protected void DummyDelegateHandler(LevelInformation level)
        {
            MessageBox.Show("Delegate Error", "Something Went Wrong",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }//end DummyDelegateHandler

        private void ColorLevelDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendLevelsBack(currentLevelInformation);
        }//end ColorLevelDialog FormClosing Event Handler
    }//end class
}//end namespace