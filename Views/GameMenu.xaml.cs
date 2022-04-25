using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClickIt.Views
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        public string HighScore { get; set; }
        public GameMenu()
        {

            this.DataContext = this;
            HighScore = File.ReadAllLines(@"C:\Users\infin\source\repos\ClickIt\ScoresData.txt")[1];

            InitializeComponent();
            
        }
    }
}
