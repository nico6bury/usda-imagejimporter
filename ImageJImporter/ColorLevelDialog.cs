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
            if(mainPanel != null)
            {
                mainPanel.Size = new Size(this.Width - 100, this.Height - 100);
                mainPanel.Location = new Point(12, 12);
            }//end if the main panel is not null
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
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true,
                Margin = new Padding(50),
            };
            //just make sure the controls are reset
            mainPanel.Controls.Clear();

            //start trying to do panels
            for (int i = 0; i < levels.Count; i++)
            {
                LevelPanel subPanel = GetCloseComponents(out TextBox label1,
                    out Button button1, out Button button2,
                    out NumericUpDown numeric1, out NumericUpDown numeric2,
                    levels, i);
                subPanel.Location = new Point(12, subPanel.Height * i);
                mainPanel.Controls.Add(subPanel);
            }//end looping over each level

            Controls.Clear();
            Controls.Add(mainPanel);
        }//end BuildLevelsShowing()

        private LevelPanel GetCloseComponents(out TextBox levelLabel, out Button foreColorButton,
            out Button backColorButton, out NumericUpDown levelMin,
            out NumericUpDown levelMax, LevelInformation levels, int index)
        {
            //set up Panel
            LevelPanel subPanel = new LevelPanel(levels[index], index)
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
            levelLabel.TextChanged += LevelLabel_TextChanged;

            //set up foreColorButton
            foreColorButton = new Button
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = "Text Color",
                FlatStyle = FlatStyle.Flat,
            };
            foreColorButton.BackColor = subPanel.Level.ForeColor;
            foreColorButton.ForeColor = subPanel.Level.BackColor;
            foreColorButton.Location = new Point(foreColorButton.Location.X, levelLabel.Location.Y + levelLabel.Height + 10);
            foreColorButton.Click += ForeColorButton_Click;

            //set up backColorButton
            backColorButton = new Button
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = "Button Color",
                FlatStyle = FlatStyle.Flat,
            };
            backColorButton.BackColor = subPanel.Level.BackColor;
            backColorButton.ForeColor = subPanel.Level.ForeColor;
            backColorButton.Location = new Point(backColorButton.Location.X, foreColorButton.Location.Y + foreColorButton.Height + 10);
            backColorButton.Click += BackColorButton_Click;

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
            levelMin.ValueChanged += LevelMin_ValueChanged;

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
            levelMax.ValueChanged += LevelMax_ValueChanged;

            //add everything to the panel
            subPanel.Controls.Add(levelLabel);
            subPanel.Controls.Add(foreColorButton);
            subPanel.Controls.Add(backColorButton);
            subPanel.Controls.Add(levelMin);
            subPanel.Controls.Add(levelMax);

            return subPanel;
        }//end GetCloseComponents(params...)

        private void LevelLabel_TextChanged(object sender, EventArgs e)
        {
            if(sender is TextBox textBox)
            {
                if(textBox.Parent is LevelPanel lvlPanel)
                {
                    lvlPanel.Level.LevelName = textBox.Text;
                    currentLevelInformation[lvlPanel.LevelInformationIndex] = lvlPanel.Level;
                    BuildLevelsShowing(currentLevelInformation);
                }//end if sender parent is LevelPanel, cast it
            }//end sender is a textbox, cast it
        }//end event handler for changing level name

        private void ForeColorButton_Click(object sender, EventArgs e)
        {
            if(sender is Button button)
            {
                if(button.Parent is LevelPanel lvlPanel)
                {
                    using (ColorDialog colorPicker = new ColorDialog())
                    {
                        if(colorPicker.ShowDialog() == DialogResult.OK)
                        {
                            lvlPanel.Level.ForeColor = colorPicker.Color;
                            currentLevelInformation[lvlPanel.LevelInformationIndex] = lvlPanel.Level;
                            BuildLevelsShowing(currentLevelInformation);
                        }//end if the use selected ok on picking a new dialog
                    }//end use of Color Picking Dialog
                }//end if sender parent is a LevelPanel, cast it
            }//end if sender is a button, cast it
        }//end event handler for changing level forecolor

        private void BackColorButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Parent is LevelPanel lvlPanel)
                {
                    using (ColorDialog colorPicker = new ColorDialog())
                    {
                        if (colorPicker.ShowDialog() == DialogResult.OK)
                        {
                            lvlPanel.Level.BackColor = colorPicker.Color;
                            currentLevelInformation[lvlPanel.LevelInformationIndex] = lvlPanel.Level;
                            BuildLevelsShowing(currentLevelInformation);
                        }//end if the use selected ok on picking a new dialog
                    }//end use of Color Picking Dialog
                }//end if sender parent is a LevelPanel, cast it
            }//end if sender is a button, cast it
        }//end event handler for changing level backcolor

        private void LevelMax_ValueChanged(object sender, EventArgs e)
        {
            if(sender is NumericUpDown nud)
            {
                if(nud.Parent is LevelPanel lvlPanel)
                {
                    lvlPanel.Level.LevelEnd = nud.Value;
                    currentLevelInformation[lvlPanel.LevelInformationIndex] = lvlPanel.Level;
                    BuildLevelsShowing(currentLevelInformation);
                }//end if sender parent is LevelPanel, cast it
            }//end if sender is a numericUpDown
        }//end event handler for changing level inclusive max

        private void LevelMin_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown nud)
            {
                if (nud.Parent is LevelPanel lvlPanel)
                {
                    lvlPanel.Level.LevelStart = nud.Value;
                    currentLevelInformation[lvlPanel.LevelInformationIndex] = lvlPanel.Level;
                    BuildLevelsShowing(currentLevelInformation);
                }//end if sender parent is LevelPanel, cast it
            }//end if sender is a numericUpDown
        }//end event handler for changing level exclusive min

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