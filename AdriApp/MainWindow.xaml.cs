
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WinForms = System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Resources;

namespace AdriApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string finalDirectory;

        private void Janela_iniciou(object sender, RoutedEventArgs e)
        {
            this.bt_ant.Content = "Voltar";
            this.bt_pro.Content = "Vamo Instala";

            this.bt_ant.Visibility = Visibility.Collapsed;
            this.tab_inicio.Visibility = Visibility.Visible;
            this.tab_avanco.Visibility = Visibility.Collapsed;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            this.End_Program();
        }
        private void End_Program()
        {
            Environment.Exit(0);

        }

        private void set_tabs(object sender, SelectionChangedEventArgs e)
        {
            if (tab_ctrl.SelectedIndex == 0)
            {
                tab_avanco.Visibility = Visibility.Collapsed;
                tab_inicio.Visibility = Visibility.Visible;
                bt_ant.Visibility = Visibility.Collapsed;
                bt_pro.Visibility = Visibility.Visible;
                tab_fim.Visibility = Visibility.Collapsed;
            }
            else if (tab_ctrl.SelectedIndex == 1)
            {
                tab_avanco.Visibility = Visibility.Visible;
                tab_inicio.Visibility = Visibility.Visible;
                bt_ant.Visibility = Visibility.Visible;
                bt_pro.Visibility = Visibility.Visible;
                tab_fim.Visibility = Visibility.Collapsed;
            }
            else if (tab_ctrl.SelectedIndex > 1)
            {
                tab_avanco.Visibility = Visibility.Collapsed;
                tab_inicio.Visibility = Visibility.Collapsed;
                bt_ant.Visibility = Visibility.Collapsed;
                bt_pro.Visibility = Visibility.Collapsed;
                tab_fim.Visibility = Visibility.Visible;
                bt_cancel.Visibility = Visibility.Collapsed;
            }
        }

        private async void click_instalar(object sender, RoutedEventArgs e)
        {
            string selectedDirectory = txtDirectory.Text;

            if (string.IsNullOrEmpty(selectedDirectory) || !Directory.Exists(selectedDirectory))
            {
                MessageBox.Show("Por favor insira um diretório válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            tab_ctrl.SelectedIndex = tab_ctrl.SelectedIndex + 1;

            // Define the source file path
            string sourceFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Images", "MonkeyPuppet.jpg");

            // Define the target file path
            string targetFilePath = System.IO.Path.Combine(selectedDirectory, "MonkeyPuppet.jpg");


            try
            {
                // Check if the source file exists
                if (File.Exists(sourceFilePath))
                {
                    // Copy the file to the target directory
                    File.Copy(sourceFilePath, targetFilePath, true); // true to overwrite if exists
                    prg_1.Minimum = 0;
                    prg_1.Maximum = 100;

                    for (int i = 0; i <= 100; i++)
                    {
                        await Task.Delay(10);

                        prg_1.Value = i;

                        textBlockStatus.Text = $"Instalando... {i}%";
                    }

                    textBlockStatus.Text = "Feito";
                    MessageBox.Show($"Instalação concluída", "Successo", MessageBoxButton.OK, MessageBoxImage.Information);

                    tab_ctrl.SelectedIndex = tab_ctrl.SelectedIndex + 1;
                    this.finalDirectory = targetFilePath;
                }
                else
                {
                    MessageBox.Show($"Erro inesperado no source code da Matrix: {sourceFilePath}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.End_Program();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors during the file copy
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ir_anterior(object sender, RoutedEventArgs e)
        {
            if (tab_ctrl.SelectedIndex >= 1)
            {
                tab_ctrl.SelectedIndex = tab_ctrl.SelectedIndex - 1;
                prg_1.Value = 0;
            }
        }

        private void ir_cancelar(object sender, RoutedEventArgs e)
        {

            var teste = MessageBox.Show("Você tem certeza?", "Vaga-bond", MessageBoxButton.YesNo);

            switch (teste)
            {
                case MessageBoxResult.Yes:
                    this.End_Program();
                    break;
                default: break;
            }
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {

            using (var dialog = new WinForms.FolderBrowserDialog())
            {
                dialog.Description = "Select Installation Directory";
                dialog.ShowNewFolderButton = true;

                // Show the dialog and get the result
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Set the selected path to the TextBox
                    this.txtDirectory.Text = dialog.SelectedPath;
                }
            }

        }

        private async void Fim_Sucesso(object sender, RoutedEventArgs e)
        {
            await Task.Delay(150);
            Process.Start(finalDirectory);
            this.End_Program();
        }
    }
}