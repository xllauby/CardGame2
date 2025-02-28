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

namespace CardGame
{
    public partial class Window1 : Window
    {
        public int PlayersCount { get; set; }

        public Window1(int playersCount)
        {
            InitializeComponent();
            PlayersCount = playersCount;
            DataContext = this;
        }
    }
}
