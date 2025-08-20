using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data;
using JuiceGym.Training.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JuiceGym.GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void TrainButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse input
                string? path = PathTextBox.Text;
                int epochs = int.Parse(EpochsTextBox.Text);

                // Validate directory
                if (!Directory.Exists(path))
                    throw new DirectoryNotFoundException($"Invalid path: {path}");

                // Load data
                ResultLabel.Content = $"Loading images from {path}";
                var dataLoader = new DataLoader();
                var data = await dataLoader.LoadImagesAsync(path);

                // Train model
                var trainer = new Trainer();
                var config = new TrainingConfig(epochs, 32);
                ResultLabel.Content = "Starting training...";
                var modelPath = await trainer.TrainModelAsync(data, config);

                ResultLabel.Content = $"Model saved to: {modelPath}";
            }
            catch (Exception ex)
            {
                ResultLabel.Content = $"Error: {ex.Message}";
            }
        }
    }
}
