using System;
using System.Diagnostics;
using System.Media;
using System.Windows.Forms;

namespace GoodNight
{
    public partial class MainForm : Form
    {
        private readonly Timer timer;
        private int minutesLeft;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        public MainForm()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 60000; // 1 minute
            timer.Tick += Timer_Tick;

            CreateTrayIcon();
        }

        private void CreateTrayIcon()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Показать", null, (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; });
            trayMenu.Items.Add("Выход", null, (s, e) => Application.Exit());

            trayIcon = new NotifyIcon();
            trayIcon.Icon = System.Drawing.SystemIcons.Application;
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick += (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; };
        }

        private RadioButton shutdownRadioButton;
        private RadioButton rebootRadioButton;
        private TextBox minutesTextBox;
        private Button startButton;
        private Button resetButton;
        private Button exitButton;
        private Label countdownLabel;

        private void InitializeComponent()
        {
            this.shutdownRadioButton = new RadioButton();
            this.rebootRadioButton = new RadioButton();
            this.minutesTextBox = new TextBox();
            this.startButton = new Button();
            this.resetButton = new Button();
            this.exitButton = new Button();
            this.countdownLabel = new Label();

            this.SuspendLayout();

            // shutdownRadioButton
            this.shutdownRadioButton.AutoSize = true;
            this.shutdownRadioButton.Location = new System.Drawing.Point(12, 12);
            this.shutdownRadioButton.Name = "shutdownRadioButton";
            this.shutdownRadioButton.Size = new System.Drawing.Size(82, 17);
            this.shutdownRadioButton.TabIndex = 0;
            this.shutdownRadioButton.TabStop = true;
            this.shutdownRadioButton.Text = "Выключить";
            this.shutdownRadioButton.UseVisualStyleBackColor = true;

            // rebootRadioButton
            this.rebootRadioButton.AutoSize = true;
            this.rebootRadioButton.Location = new System.Drawing.Point(12, 35);
            this.rebootRadioButton.Name = "rebootRadioButton";
            this.rebootRadioButton.Size = new System.Drawing.Size(156, 17);
            this.rebootRadioButton.TabIndex = 1;
            this.rebootRadioButton.TabStop = true;
            this.rebootRadioButton.Text = "Перезагрузить компьютер";
            this.rebootRadioButton.UseVisualStyleBackColor = true;

            // minutesTextBox
            this.minutesTextBox.Location = new System.Drawing.Point(65, 65);
            this.minutesTextBox.Name = "minutesTextBox";
            this.minutesTextBox.Size = new System.Drawing.Size(50, 20);
            this.minutesTextBox.TabIndex = 2;

            // startButton
            this.startButton.Location = new System.Drawing.Point(12, 100);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Начали";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += StartButton_Click;

            // resetButton
            this.resetButton.Location = new System.Drawing.Point(93, 100);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Сброс";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += ResetButton_Click;

            // exitButton
            this.exitButton.Location = new System.Drawing.Point(174, 100);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Выход";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += (s, e) => Application.Exit();

            // countdownLabel
            this.countdownLabel.AutoSize = true;
            this.countdownLabel.Location = new System.Drawing.Point(12, 75);
            this.countdownLabel.Name = "countdownLabel";
            this.countdownLabel.Size = new System.Drawing.Size(47, 13);
            this.countdownLabel.TabIndex = 6;
            this.countdownLabel.Text = "через:";

            Label minutesLabel = new Label();
            minutesLabel.AutoSize = true;
            minutesLabel.Location = new System.Drawing.Point(121, 68);
            minutesLabel.Text = "мин.";

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 135);
            this.Controls.Add(this.shutdownRadioButton);
            this.Controls.Add(this.rebootRadioButton);
            this.Controls.Add(this.minutesTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.countdownLabel);
            this.Controls.Add(minutesLabel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Good Night v1.0";
            this.Icon = System.Drawing.SystemIcons.Application;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(minutesTextBox.Text, out int minutes) && minutes > 0)
            {
                minutesLeft = minutes;
                timer.Start();
                startButton.Enabled = false;
                minutesTextBox.Enabled = false;
                countdownLabel.Text = $"Осталось: {minutesLeft} мин.";
            }
            else
            {
                MessageBox.Show("Введите количество минут", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            minutesLeft = 0;
            startButton.Enabled = true;
            minutesTextBox.Enabled = true;
            minutesTextBox.Text = string.Empty;
            countdownLabel.Text = "через:";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            minutesLeft--;
            if (minutesLeft <= 0)
            {
                timer.Stop();
                SystemSounds.Beep.Play();
                string args = shutdownRadioButton.Checked ? "/s /t 0" : "/r /t 0";
                try
                {
                    Process.Start(new ProcessStartInfo("shutdown", args) { CreateNoWindow = true, UseShellExecute = false });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                countdownLabel.Text = $"Осталось: {minutesLeft} мин.";
            }
        }
    }
}
