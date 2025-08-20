using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data;
using JuiceGym.Training.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuiceGym.GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation
                string path = PathTextBox.Text?.Trim();
                if (string.IsNullOrEmpty(path))
                {
                    ResultLabel.Text = "Please enter a directory path";
                    return;
                }

                if (!int.TryParse(EpochsTextBox.Text, out int epochs) || epochs < 1 || epochs > 1000)
                {
                    ResultLabel.Text = "Please enter a valid number of epochs (1-1000)";
                    return;
                }

                if (!Directory.Exists(path))
                {
                    ResultLabel.Text = $"Directory not found: {path}";
                    return;
                }

                ResultLabel.Text = "Loading images...";
                var dataLoader = new DataLoader();
                var imageData = await dataLoader.LoadImagesAsync(path);

                ResultLabel.Text = "Training model...";
                var trainer = new Trainer();
                var trainingConfig = new TrainingConfig(epochs, 32);
                var modelPath = await trainer.TrainModelAsync(imageData, trainingConfig);

                ResultLabel.Text = $"Model saved to: {modelPath}";
            }
            catch (Exception ex)
            {
                ResultLabel.Text = $"Error: {ex.Message}";
            }
        }
    }
}
