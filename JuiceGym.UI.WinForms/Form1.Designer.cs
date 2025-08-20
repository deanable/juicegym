namespace JuiceGym.UI.WinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.TrainButton = new System.Windows.Forms.Button();
        this.PathTextBox = new System.Windows.Forms.TextBox();
        this.EpochsTextBox = new System.Windows.Forms.TextBox();
        this.ResultLabel = new System.Windows.Forms.Label();
        this.PathLabel = new System.Windows.Forms.Label();
        this.EpochsLabel = new System.Windows.Forms.Label();
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "JuiceGym - Image Classification Trainer";

        // Path Label
        this.PathLabel.AutoSize = true;
        this.PathLabel.Location = new System.Drawing.Point(12, 15);
        this.PathLabel.Name = "PathLabel";
        this.PathLabel.Size = new System.Drawing.Size(100, 15);
        this.PathLabel.TabIndex = 0;
        this.PathLabel.Text = "Image Directory Path:";

        // Path TextBox
        this.PathTextBox.Location = new System.Drawing.Point(12, 35);
        this.PathTextBox.Name = "PathTextBox";
        this.PathTextBox.Size = new System.Drawing.Size(400, 23);
        this.PathTextBox.TabIndex = 1;
        this.PathTextBox.Text = "C:\\path\\to\\images";

        // Epochs Label
        this.EpochsLabel.AutoSize = true;
        this.EpochsLabel.Location = new System.Drawing.Point(12, 75);
        this.EpochsLabel.Name = "EpochsLabel";
        this.EpochsLabel.Size = new System.Drawing.Size(45, 15);
        this.EpochsLabel.TabIndex = 2;
        this.EpochsLabel.Text = "Epochs:";

        // Epochs TextBox
        this.EpochsTextBox.Location = new System.Drawing.Point(12, 95);
        this.EpochsTextBox.Name = "EpochsTextBox";
        this.EpochsTextBox.Size = new System.Drawing.Size(100, 23);
        this.EpochsTextBox.TabIndex = 3;
        this.EpochsTextBox.Text = "10";

        // Train Button
        this.TrainButton.Location = new System.Drawing.Point(12, 135);
        this.TrainButton.Name = "TrainButton";
        this.TrainButton.Size = new System.Drawing.Size(100, 30);
        this.TrainButton.TabIndex = 4;
        this.TrainButton.Text = "Start Training";
        this.TrainButton.UseVisualStyleBackColor = true;
        this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);

        // Result Label
        this.ResultLabel.AutoSize = true;
        this.ResultLabel.Location = new System.Drawing.Point(12, 180);
        this.ResultLabel.Name = "ResultLabel";
        this.ResultLabel.Size = new System.Drawing.Size(50, 15);
        this.ResultLabel.TabIndex = 5;
        this.ResultLabel.Text = "Ready to train...";

        // Add controls to form
        this.Controls.Add(this.PathLabel);
        this.Controls.Add(this.PathTextBox);
        this.Controls.Add(this.EpochsLabel);
        this.Controls.Add(this.EpochsTextBox);
        this.Controls.Add(this.TrainButton);
        this.Controls.Add(this.ResultLabel);
    }

    #endregion
}
