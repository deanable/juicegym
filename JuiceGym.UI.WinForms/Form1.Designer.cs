#nullable enable

namespace JuiceGym.UI.WinForms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.Container? components = null;

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
        this.ClientSize = new System.Drawing.Size(1200, 700);
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

        // Browse Button
        this.BrowseButton = new System.Windows.Forms.Button();
        this.BrowseButton.Location = new System.Drawing.Point(420, 35);
        this.BrowseButton.Name = "BrowseButton";
        this.BrowseButton.Size = new System.Drawing.Size(80, 23);
        this.BrowseButton.TabIndex = 2;
        this.BrowseButton.Text = "Browse...";
        this.BrowseButton.UseVisualStyleBackColor = true;
        this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);

        // Epochs Label
        this.EpochsLabel.AutoSize = true;
        this.EpochsLabel.Location = new System.Drawing.Point(12, 75);
        this.EpochsLabel.Name = "EpochsLabel";
        this.EpochsLabel.Size = new System.Drawing.Size(45, 15);
        this.EpochsLabel.TabIndex = 3;
        this.EpochsLabel.Text = "Epochs:";

        // Epochs TextBox
        this.EpochsTextBox.Location = new System.Drawing.Point(12, 95);
        this.EpochsTextBox.Name = "EpochsTextBox";
        this.EpochsTextBox.Size = new System.Drawing.Size(100, 23);
        this.EpochsTextBox.TabIndex = 4;
        this.EpochsTextBox.Text = "10";

        // File List View
        this.FileListView = new System.Windows.Forms.ListView();
        this.FileListView.Location = new System.Drawing.Point(12, 135);
        this.FileListView.Name = "FileListView";
        this.FileListView.Size = new System.Drawing.Size(350, 200);
        this.FileListView.TabIndex = 5;
        this.FileListView.View = System.Windows.Forms.View.Details;
        this.FileListView.FullRowSelect = true;
        this.FileListView.MultiSelect = true;
        this.FileListView.Columns.Add("File Name", 150);
        this.FileListView.Columns.Add("Size", 80);
        this.FileListView.Columns.Add("Dimensions", 100);
        this.FileListView.SelectedIndexChanged += new System.EventHandler(this.FileListView_SelectedIndexChanged);

        // Remove Button
        this.RemoveButton = new System.Windows.Forms.Button();
        this.RemoveButton.Location = new System.Drawing.Point(12, 345);
        this.RemoveButton.Name = "RemoveButton";
        this.RemoveButton.Size = new System.Drawing.Size(100, 30);
        this.RemoveButton.TabIndex = 6;
        this.RemoveButton.Text = "Remove Selected";
        this.RemoveButton.UseVisualStyleBackColor = true;
        this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);

        // Remove All Button
        this.RemoveAllButton = new System.Windows.Forms.Button();
        this.RemoveAllButton.Location = new System.Drawing.Point(120, 345);
        this.RemoveAllButton.Name = "RemoveAllButton";
        this.RemoveAllButton.Size = new System.Drawing.Size(100, 30);
        this.RemoveAllButton.TabIndex = 7;
        this.RemoveAllButton.Text = "Remove All";
        this.RemoveAllButton.UseVisualStyleBackColor = true;
        this.RemoveAllButton.Click += new System.EventHandler(this.RemoveAllButton_Click);

        // Preview Label
        this.PreviewLabel = new System.Windows.Forms.Label();
        this.PreviewLabel.AutoSize = true;
        this.PreviewLabel.Location = new System.Drawing.Point(400, 135);
        this.PreviewLabel.Name = "PreviewLabel";
        this.PreviewLabel.Size = new System.Drawing.Size(50, 15);
        this.PreviewLabel.TabIndex = 8;
        this.PreviewLabel.Text = "Image Preview:";

        // Preview PictureBox
        this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
        this.PreviewPictureBox.Location = new System.Drawing.Point(400, 155);
        this.PreviewPictureBox.Name = "PreviewPictureBox";
        this.PreviewPictureBox.Size = new System.Drawing.Size(300, 200);
        this.PreviewPictureBox.TabIndex = 9;
        this.PreviewPictureBox.TabStop = false;
        this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.PreviewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        // Metadata Label
        this.MetadataLabel = new System.Windows.Forms.Label();
        this.MetadataLabel.AutoSize = false;
        this.MetadataLabel.Location = new System.Drawing.Point(400, 365);
        this.MetadataLabel.Name = "MetadataLabel";
        this.MetadataLabel.Size = new System.Drawing.Size(300, 100);
        this.MetadataLabel.TabIndex = 10;
        this.MetadataLabel.Text = "Select an image to view metadata...";
        this.MetadataLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        // Train Button
        this.TrainButton.Location = new System.Drawing.Point(12, 385);
        this.TrainButton.Name = "TrainButton";
        this.TrainButton.Size = new System.Drawing.Size(120, 35);
        this.TrainButton.TabIndex = 11;
        this.TrainButton.Text = "Start Training";
        this.TrainButton.UseVisualStyleBackColor = true;
        this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);

        // Result Label
        this.ResultLabel.AutoSize = false;
        this.ResultLabel.Location = new System.Drawing.Point(12, 430);
        this.ResultLabel.Name = "ResultLabel";
        this.ResultLabel.Size = new System.Drawing.Size(1150, 250);
        this.ResultLabel.TabIndex = 12;
        this.ResultLabel.Text = "Ready to train...";

        // Add controls to form
        this.Controls.Add(this.PathLabel);
        this.Controls.Add(this.PathTextBox);
        this.Controls.Add(this.BrowseButton);
        this.Controls.Add(this.EpochsLabel);
        this.Controls.Add(this.EpochsTextBox);
        this.Controls.Add(this.FileListView);
        this.Controls.Add(this.RemoveButton);
        this.Controls.Add(this.RemoveAllButton);
        this.Controls.Add(this.PreviewLabel);
        this.Controls.Add(this.PreviewPictureBox);
        this.Controls.Add(this.MetadataLabel);
        this.Controls.Add(this.TrainButton);
        this.Controls.Add(this.ResultLabel);
    }

    #endregion
}
