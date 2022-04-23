using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            int row = RandomRow;
            int column = RandomColumn;
            do
            {
                RandomRow = random.Next(0, MaxRows);
                RandomColumn = random.Next(0, MaxColumns);
            } while (row == RandomRow && column == RandomColumn);
            
        }

        private void SquareButton_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Score += IncreaseScoreBy;
            UpdateScoreData();
            UpdateSquareRowAndColumn();
        }

        private void UpdateScoreData(string textFilePath= @"C:\Users\infin\source\repos\ClickIt\ScoresData.txt")
        {
            // Line 1(Array[0]) = Score
            // Line 2(Array[1]) = High Score
            string[] ScoresFile = File.ReadAllLines(textFilePath);
            ScoresFile[0] = Score.ToString();
            if (Score > Convert.ToInt32(ScoresFile[1])) { ScoresFile[1] = Score.ToString(); }

            File.WriteAllLines(textFilePath, ScoresFile);
        }


        // PROPERY CHANGED STUFF
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            var MousePosition = Mouse.GetPosition(MainGrid);

            int row = 0;
            int column = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in MainGrid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= MousePosition.Y)
                    break;
                row++;
            }

            // calc column mouse was over
            foreach (var columnDefinition in MainGrid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= MousePosition.X)
                    break;
                column++;
            }


            // If the user clicks something other then the square, end the game
            if (row != RandomRow || column != RandomColumn) { EndGame(); }
            
        }

        private void EndGame(bool failedGame = true)
        {
            if (failedGame)
            {
                MainGrid.Background = Brushes.Red;
                ClickingSquare.IsHitTestVisible = false;
            }
        }
    }
}
