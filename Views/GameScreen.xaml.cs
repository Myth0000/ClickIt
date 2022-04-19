using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for GameScreen.xaml
    /// </summary>
    public partial class GameScreen : UserControl, INotifyPropertyChanged
    {

        private int _score;
        public int Score
        {
            get { return this._score; }
            set
            {
                if (this._score != value)
                {
                    this._score = value;
                    this.NotifyPropertyChanged("Score");
                }
            }
        }

        private int _randomRow;
        public int RandomRow
        {
            get { return this._randomRow; }
            set
            {
                if (this._randomRow != value)
                {
                    this._randomRow = value;
                    this.NotifyPropertyChanged("RandomRow");
                }
            }
        }

        private int _randomColumn;
        public int RandomColumn
        {
            get { return this._randomColumn; }
            set
            {
                if (this._randomColumn != value)
                {
                    this._randomColumn = value;
                    this.NotifyPropertyChanged("RandomColumn");
                }
            }
        }

        public int SquareHeight { get; } = 50;
        public int SquareWidth { get; } = 50;
        


        int MaxRows = 4;
        int MaxColumns = 4;
        int IncreaseScoreBy = 3;


        public GameScreen()
        {
            this.DataContext = this;
            UpdateSquareRowAndColumn();

            InitializeComponent();
        }


        public void UpdateSquareRowAndColumn()
        {
            Random random = new();

            RandomRow = random.Next(0, MaxRows);
            RandomColumn = random.Next(0, MaxColumns);
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            Score += IncreaseScoreBy;
            UpdateSquareRowAndColumn();
        }

        // PROPERY CHANGED STUFF
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
