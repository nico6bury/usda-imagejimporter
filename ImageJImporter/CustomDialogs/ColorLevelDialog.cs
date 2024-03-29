﻿using System;
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
        /// <summary>
        /// level information we're editing
        /// </summary>
        private LevelInformation currentLevelInformation;
        /// <summary>
        /// delegate to update level information in view
        /// </summary>
        private SendLevelInformation sendLevelsBack;
        /// <summary>
        /// the main layout panel we have in the dialog
        /// </summary>
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
            BuildLevelsShowing(currentLevelInformation, this);

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

        private void BuildLevelsShowing(LevelInformation levels, Form formToAddTo)
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
                ContextMenuStrip = uxMainPanelContextMenuStrip,
            };
            //just make sure the controls are reset
            mainPanel.Controls.Clear();

            //start trying to do panels
            for (int i = 0; i < levels.Count; i++)
            {
                LevelPanel subPanel = GetCloseComponents(out TextBox label1,
                    out Button button1, out Button button2,
                    out NumericUpDown numeric1, out NumericUpDown numeric2,
                    levels, i, uxSubPanelContextMenuStrip, LevelLabel_TextChanged,
                    ForeColorButton_Click, BackColorButton_Click, LevelMin_ValueChanged,
                    LevelMax_ValueChanged);
                subPanel.Location = new Point(12, subPanel.Height * i);
                mainPanel.Controls.Add(subPanel);
            }//end looping over each level

            formToAddTo.Controls.Clear();
            formToAddTo.Controls.Add(mainPanel);
        }//end BuildLevelsShowing()

        private static LevelPanel GetCloseComponents(out TextBox levelLabel, out Button foreColorButton,
            out Button backColorButton, out NumericUpDown levelMin,
            out NumericUpDown levelMax, LevelInformation levels, int index, ContextMenuStrip subPanelContextStrip,
            EventHandler nameChange, EventHandler foreColorChange, EventHandler backColorChange,
            EventHandler lvlStartChange, EventHandler lvlEndChange)
        {
            //set up Panel
            LevelPanel subPanel = new LevelPanel(levels[index], index)
            {
                BackColor = Color.GhostWhite,
                ForeColor = Color.Indigo,
                //AutoSize = true,
                //AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ContextMenuStrip = subPanelContextStrip,                
            };
            //forces subpanel to be double buffered
            subPanel.GetType()
                .GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic)
                .SetValue(subPanel, true, null);

            //set up TextBox
            levelLabel = new TextBox
            {
                AutoSize = false,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = subPanel.Level.LevelName,
            };
            levelLabel.Location = new Point(levelLabel.Location.X, 12);
            levelLabel.TextChanged += nameChange;

            //set up foreColorButton
            foreColorButton = new Button
            {
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Text = "Text Color",
                FlatStyle = FlatStyle.Flat,
            };
            foreColorButton.BackColor = subPanel.Level.BackColor;
            foreColorButton.ForeColor = subPanel.Level.ForeColor;
            foreColorButton.Location = new Point(foreColorButton.Location.X, levelLabel.Location.Y + levelLabel.Height + 10);
            foreColorButton.Click += foreColorChange;

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
            backColorButton.Click += backColorChange;

            //set up levelMin
            levelMin = new NumericUpDown
            {
                BackColor = Color.GhostWhite,
                ForeColor = Color.Indigo,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                Minimum = -10,
                Maximum = 99,
                Value = subPanel.Level.LevelStart,
                DecimalPlaces = 2,
            };
            levelMin.Location = new Point(foreColorButton.Location.X + foreColorButton.Width + 50, foreColorButton.Location.Y);
            levelMin.ValueChanged += lvlStartChange;

            //set up levelMax
            levelMax = new NumericUpDown
            {
                BackColor = Color.GhostWhite,
                ForeColor = Color.Indigo,
                AutoSize = true,
                Font = new Font(FontFamily.GenericSerif, 12),
                DecimalPlaces = 2,
                Minimum = -9,
                Maximum = 100,
                Value = subPanel.Level.LevelEnd,
            };
            levelMax.Location = new Point(backColorButton.Location.X + backColorButton.Width + 50, backColorButton.Location.Y);
            levelMax.ValueChanged += lvlEndChange;

            //add everything to the panel
            subPanel.Controls.Add(levelLabel);
            subPanel.Controls.Add(foreColorButton);
            subPanel.Controls.Add(backColorButton);
            subPanel.Controls.Add(levelMin);
            subPanel.Controls.Add(levelMax);

            //resize the control
            subPanel.Width = levelMax.Location.X + levelMax.Width;
            subPanel.Height = backColorButton.Location.Y + backColorButton.Height;

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
                            BuildLevelsShowing(currentLevelInformation, this);
                            ResizeMainPanelAndExitButton(null, null);
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
                            BuildLevelsShowing(currentLevelInformation, this);
                            ResizeMainPanelAndExitButton(null, null);
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
                    currentLevelInformation.ReSortLevels();
                    BuildLevelsShowing(currentLevelInformation, this);
                    ResizeMainPanelAndExitButton(null, null);
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
                    currentLevelInformation.ReSortLevels();
                    BuildLevelsShowing(currentLevelInformation, this);
                    ResizeMainPanelAndExitButton(null, null);
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

        /// <summary>
        /// event for when we want to remove a level
        /// </summary>
        private void uxRemoveLevel_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripMenuItem menuStrip)
            {
                if(menuStrip.Owner is ContextMenuStrip contextMenu)
                {
                    if(contextMenu.SourceControl is LevelPanel panel)
                    {
                        //force level information to remove a level
                        List<LevelInformation.Level> tempLevelList = currentLevelInformation.Levels;
                        tempLevelList.RemoveAt(panel.LevelInformationIndex);
                        currentLevelInformation.Levels = tempLevelList;
                        BuildLevelsShowing(currentLevelInformation, this);
                        ResizeMainPanelAndExitButton(null, null);
                    }//end if we're right clicking a level panel
                }//end if we're doing a right click action
            }//end if we came from the right place
        }//end Remove Level Click event handler

        private void uxAddNewLevel_Click(object sender, EventArgs e)
        {
            LevelEditor editor = new LevelEditor(AddNewLevelFromEditor);
            editor.ShowDialog();
        }//end Add Level Click event handler

        private void AddNewLevelFromEditor(LevelInformation.Level newLevel)
        {
            //add the level and resort everything
            currentLevelInformation.AddNewLevel(newLevel);
            currentLevelInformation.ReSortLevels();
            currentLevelInformation.RefreshLevelBounds();

            //rebuild our user interface
            BuildLevelsShowing(currentLevelInformation, this);
            ResizeMainPanelAndExitButton(null, null);
        }//end AddNewLevelFromEditor

        /// <summary>
        /// a simple form for the user to edit a new level
        /// </summary>
        private class LevelEditor : Form
        {
            private LevelPanel levelEditingPanel;
            private Button cancelButton;
            private Button acceptButton;
            private SendLevel returnLevel;

            internal LevelEditor(SendLevel callBackLevelReturner)
            {
                //save our delegate
                this.returnLevel = callBackLevelReturner;

                //initialize properties of this
                Text = "Level Editor";
                AutoSize = true;
                AutoSizeMode = AutoSizeMode.GrowAndShrink;
                SizeGripStyle = SizeGripStyle.Hide;
                ControlBox = false;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                ShowIcon = false;

                //initialize accept and exit buttons
                cancelButton = new Button
                {
                    Text = "Cancel",
                    AutoSize = true,
                };
                acceptButton = new Button
                {
                    Text = "Accept",
                    AutoSize = true,
                };
                CancelButton = cancelButton;
                AcceptButton = acceptButton;
                acceptButton.Click += AcceptButton_Click;

                //get the level panel initialized
                LevelInformation defaultLevel = new LevelInformation();
                defaultLevel.AddNewLevel(0, 0, "Level Name", cancelButton.BackColor, cancelButton.ForeColor);
                levelEditingPanel = GetCloseComponents(out TextBox label1,
                    out Button button1, out Button button2,
                    out NumericUpDown numeric1, out NumericUpDown numeric2,
                    defaultLevel, 0, null, LevelLabel_TextChanged, ForeColorButton_Click, 
                    BackColorButton_Click, LevelMin_ValueChanged, LevelMax_ValueChanged);

                //put the buttons where they belong
                RepositionButtons(levelEditingPanel);

                //add all the controls to this form
                Controls.Add(levelEditingPanel);
                Controls.Add(cancelButton);
                Controls.Add(acceptButton);
            }//end constructor

            private void AcceptButton_Click(object sender, EventArgs e)
            {
                //send level back to whoever called us
                returnLevel(levelEditingPanel.Level);
                this.Close();
            }//end click event handler for accepting new level

            private void ReBuildForm()
            {
                //hopefully reset things
                levelEditingPanel.Controls.Clear();
                Controls.Remove(levelEditingPanel);

                //just quickly make a temporary level thing
                LevelInformation tempLevel = new LevelInformation();
                tempLevel.AddNewLevel(levelEditingPanel.Level);

                //rebuild the editing panel
                levelEditingPanel = GetCloseComponents(out TextBox label1,
                    out Button button1, out Button button2,
                    out NumericUpDown numeric1, out NumericUpDown numeric2,
                    tempLevel, 0, null, LevelLabel_TextChanged, ForeColorButton_Click,
                    BackColorButton_Click, LevelMin_ValueChanged, LevelMax_ValueChanged);
                levelEditingPanel.AutoSize = true;

                //put buttons underneath level panel
                RepositionButtons(levelEditingPanel);

                //add the panel back to this control
                Controls.Add(levelEditingPanel);
            }//end ReBuildForm()

            private void RepositionButtons(LevelPanel panel)
            {
                //put the buttons back where they belong
                int buttonMargin = 5;
                cancelButton.Location = new Point(panel.Location.X, panel.Location.Y + panel.Height + buttonMargin);
                acceptButton.Location = new Point(panel.Location.X + panel.Width - acceptButton.Width, cancelButton.Location.Y);
            }//end RepositionButtons

            private void LevelLabel_TextChanged(object sender, EventArgs e)
            {
                if (sender is TextBox textBox)
                {
                    if (textBox.Parent is LevelPanel lvlPanel)
                    {
                        lvlPanel.Level.LevelName = textBox.Text;
                    }//end if sender parent is LevelPanel, cast it
                }//end sender is a textbox, cast it
            }//end event handler for changing level name

            private void ForeColorButton_Click(object sender, EventArgs e)
            {
                if (sender is Button button)
                {
                    if (button.Parent is LevelPanel lvlPanel)
                    {
                        using (ColorDialog colorPicker = new ColorDialog())
                        {
                            if (colorPicker.ShowDialog() == DialogResult.OK)
                            {
                                lvlPanel.Level.ForeColor = colorPicker.Color;
                                ReBuildForm();
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
                                ReBuildForm();
                            }//end if the use selected ok on picking a new dialog
                        }//end use of Color Picking Dialog
                    }//end if sender parent is a LevelPanel, cast it
                }//end if sender is a button, cast it
            }//end event handler for changing level backcolor

            private void LevelMax_ValueChanged(object sender, EventArgs e)
            {
                if (sender is NumericUpDown nud)
                {
                    if (nud.Parent is LevelPanel lvlPanel)
                    {
                        lvlPanel.Level.LevelEnd = nud.Value;
                        ReBuildForm();
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
                        ReBuildForm();
                    }//end if sender parent is LevelPanel, cast it
                }//end if sender is a numericUpDown
            }//end event handler for changing level exclusive min
        }//end LevelEditor
    }//end class
}//end namespace