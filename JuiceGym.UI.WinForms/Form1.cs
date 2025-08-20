using JuiceGym.Core.Interfaces;
using JuiceGym.Core.Models;
using JuiceGym.Data;
using JuiceGym.Training.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuiceGym.UI.WinForms
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Button? TrainButton;
        private System.Windows.Forms.Button? BrowseButton;
        private System.Windows.Forms.TextBox? PathTextBox;
        private System.Windows.Forms.TextBox? EpochsTextBox;
        private System.Windows.Forms.Label? ResultLabel;
        private System.Windows.Forms.Label? PathLabel;
        private System.Windows.Forms.Label? EpochsLabel;
        private System.Windows.Forms.ListView? FileListView;
        private System.Windows.Forms.PictureBox? PreviewPictureBox;
        private System.Windows.Forms.Label? PreviewLabel;
        private System.Windows.Forms.Label? MetadataLabel;
        private System.Windows.Forms.Button? RemoveButton;
        private System.Windows.Forms.Button? RemoveAllButton;

        public Form1()
        {
            InitializeComponent();
        }

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Input validation
                string? path = PathTextBox?.Text?.Trim() ?? string.Empty;
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

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Select the directory containing training images";
                folderBrowser.SelectedPath = PathTextBox?.Text ?? string.Empty;

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowser.SelectedPath;
                    if (!string.IsNullOrEmpty(selectedPath))
                    {
                        PathTextBox!.Text = selectedPath;
                        LoadDirectoryContents(selectedPath);
                    }
                }
            }
        }

        private void LoadDirectoryContents(string path)
        {
            if (FileListView == null) return;

            FileListView.Items.Clear();

            try
            {
                var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                var imageFiles = Directory.GetFiles(path)
                    .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                    .OrderBy(file => Path.GetFileName(file));

                foreach (var file in imageFiles)
                {
                    var fileInfo = new FileInfo(file);
                    var item = new ListViewItem(Path.GetFileName(file));
                    item.SubItems.Add(FormatFileSize(fileInfo.Length));
                    item.Tag = file;

                    // Get image dimensions
                    try
                    {
                        using (var img = Image.FromFile(file))
                        {
                            item.SubItems.Add($"{img.Width} x {img.Height}");
                        }
                    }
                    catch
                    {
                        item.SubItems.Add("Unknown");
                    }

                    FileListView.Items.Add(item);
                }

                if (ResultLabel != null)
                {
                    ResultLabel.Text = $"Found {FileListView.Items.Count} image files in the directory.";
                }
            }
            catch (Exception ex)
            {
                if (ResultLabel != null)
                {
                    ResultLabel.Text = $"Error loading directory: {ex.Message}";
                }
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        private void FileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FileListView?.SelectedItems.Count > 0 && PreviewPictureBox != null && MetadataLabel != null)
            {
                var selectedItem = FileListView.SelectedItems[0];
                if (selectedItem != null)
                {
                    var filePath = selectedItem.Tag as string;

                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        try
                        {
                            // Load and display image
                            using (var originalImage = Image.FromFile(filePath))
                            {
                                var previewImage = new Bitmap(originalImage);
                                PreviewPictureBox.Image = previewImage;
                            }

                            // Get file info
                            var fileInfo = new FileInfo(filePath);
                            var metadata = $"File: {Path.GetFileName(filePath)}\n" +
                                         $"Size: {FormatFileSize(fileInfo.Length)}\n" +
                                         $"Created: {fileInfo.CreationTime}\n" +
                                         $"Modified: {fileInfo.LastWriteTime}\n" +
                                         $"Path: {filePath}";

                            MetadataLabel.Text = metadata;
                        }
                        catch (Exception ex)
                        {
                            MetadataLabel.Text = $"Error loading image: {ex.Message}";
                            PreviewPictureBox.Image = null;
                        }
                    }
                }
            }
            else if (PreviewPictureBox != null && MetadataLabel != null)
            {
                PreviewPictureBox.Image = null;
                MetadataLabel.Text = "Select an image to view metadata...";
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (FileListView == null) return;

            var selectedItems = FileListView.SelectedItems;
            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please select items to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Remove {selectedItems.Count} selected file(s) from the training list?",
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    FileListView.Items.Remove(selectedItems[i]);
                }

                if (ResultLabel != null)
                {
                    ResultLabel.Text = $"Removed {selectedItems.Count} files. {FileListView.Items.Count} files remaining.";
                }
            }
        }

        private void RemoveAllButton_Click(object sender, EventArgs e)
        {
            if (FileListView == null) return;

            if (FileListView.Items.Count == 0)
            {
                MessageBox.Show("No files to remove.", "Empty List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Remove all {FileListView.Items.Count} files from the training list?",
                "Confirm Remove All",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var count = FileListView.Items.Count;
                FileListView.Items.Clear();

                if (PreviewPictureBox != null)
                {
                    PreviewPictureBox.Image = null;
                }

                if (MetadataLabel != null)
                {
                    MetadataLabel.Text = "Select an image to view metadata...";
                }

                if (ResultLabel != null)
                {
                    ResultLabel.Text = $"Removed all {count} files. Training list is now empty.";
                }
            }
        }
    }
}
