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
                string path = PathTextBox.Text;
                int epochs = int.Parse(EpochsTextBox.Text);

                if (!Directory.Exists(path))
                {
                    ResultLabel.Text = "Invalid path";
                    return;
                }

                ResultLabel.Text = "Loading images...";
                var dataLoader = new DataLoader();
                var imageData = await dataLoader.LoadImagesAsync(path);

                ResultLabel.Text = "Training model...";
                var trainer = new Trainer();
                var trainingConfig = new TrainingConfig { Epochs = epochs };
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
