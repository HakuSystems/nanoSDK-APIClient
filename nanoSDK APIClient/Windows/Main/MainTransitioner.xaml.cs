using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace nanoSDK_APIClient.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainTransitioner.xaml
    /// </summary>
    public partial class MainTransitioner : Window
    {
        public MainTransitioner()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VersionNumberText.Text = $"V{Application.ResourceAssembly.ManifestModule.Assembly.GetName().Version}";
            DragMove();
        }
    }
}
