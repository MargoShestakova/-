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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Controls;

namespace SimptomChek
{
    public partial class ResultsControl : UserControl
    {
        public ResultsControl()
        {
            InitializeComponent();
        }

        public string ResultsText
        {
            get { return ResultsTextBox.Text; }
            set { ResultsTextBox.Text = value; }
        }
    }
}
