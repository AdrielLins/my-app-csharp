
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

        private void Janela_iniciou(object sender, RoutedEventArgs e)
        {
            this.bt_ant.Content = "< Anterior";
            this.bt_pro.Content = "Próximo >";

            this.bt_ant.Visibility = Visibility.Collapsed;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void set_tabs(object sender, SelectionChangedEventArgs e)
        {
           /* if (tab_ctrl.SelectedIndex == 0)
            {
                tab_avanco.Visibility = Visibility.Collapsed;
                tab_inicio.Visibility = Visibility.Visible;
                bt_ant.Visibility = Visibility.Collapsed;
                bt_pro.Visibility = Visibility.Visible;
            }
            else if (tab_ctrl.SelectedIndex == 1)
            {*/
                tab_avanco.Visibility = Visibility.Visible;
                tab_inicio.Visibility = Visibility.Visible;
                bt_ant.Visibility = Visibility.Visible;
                bt_pro.Visibility = Visibility.Visible;
           // }
        }

        private async void click_instalar(object sender, RoutedEventArgs e)
        {
            tab_ctrl.SelectedIndex = tab_ctrl.SelectedIndex + 1;

            prg_1.Minimum = 0;
            prg_1.Maximum = 100;

            for (int i = 0; i <= 100; i++)
            {
                await Task.Delay(50);

                prg_1.Value = i;

                textBlockStatus.Text = $"Instalando... {i}%";
            }

            textBlockStatus.Text = "Instalação concluída";
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
                    Environment.Exit(0);
                    break;
                default: break;
            }
        }
    }
}