using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data;
using JuiceGym.Training.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuiceGym.UI.WinForms
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Button? TrainButton;
        private System.Windows.Forms.TextBox? PathTextBox;
        private System.Windows.Forms.TextBox? EpochsTextBox;
        private System.Windows.Forms.Label? ResultLabel;
        private System.Windows.Forms.Label? PathLabel;
        private System.Windows.Forms.Label? EpochsLabel;

        public Form1()
        {
            InitializeComponent();
        }

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation
                string path = PathTextBox?.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(path))
                {
                    if (ResultLabel != null)
                    {
                        ResultLabel.Text = "Please enter a directory path";
                    }
                    return;
                }

                if (!int.TryParse(EpochsTextBox?.Text, out int epochs) || epochs < 1 || epochs > 1000)
                {
                    if (ResultLabel != null)
                    {
                        ResultLabel.Text = "Please enter a valid number of epochs (1-1000)";
                    }
                    return;
                }

                if (!Directory.Exists(path))
                {
                    if (ResultLabel != null)
                    {
                        ResultLabel.Text = $"Directory not found: {path}";
                    }
                    return;
                }

                if (ResultLabel != null)
                {
                    ResultLabel.Text = "Loading images...";
                }
                var dataLoader = new DataLoader();
                var imageData = await dataLoader.LoadImagesAsync(path);

                if (ResultLabel != null)
                {
                    ResultLabel.Text = "Training model...";
                }
                var trainer = new Trainer();
                var trainingConfig = new TrainingConfig(epochs, 32);
                var modelPath = await trainer.TrainModelAsync(imageData, trainingConfig);

                if (ResultLabel != null)
                {
                    ResultLabel.Text = $"Model saved to: {modelPath}";
                }
            }
            catch (Exception ex)
            {
                if (ResultLabel != null)
                {
                    ResultLabel.Text = $"Error: {ex.Message}";
                }
            }
        }
    }
}
