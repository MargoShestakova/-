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

using System;
using System.Windows;
using System.Windows.Controls;

namespace SimptomChek
{
    public partial class QuestionsControl : UserControl
    {
        public QuestionsControl()
        {
            InitializeComponent();
        }

        public string QuestionText
        {
            get { return QuestionTextBlock.Text; }
            set { QuestionTextBlock.Text = value; }
        }

        public event RoutedEventHandler YesClick
        {
            add { YesButton.Click += value; }
            remove { YesButton.Click -= value; }
        }

        public event RoutedEventHandler NoClick
        {
            add { NoButton.Click += value; }
            remove { NoButton.Click -= value; }
        }
    }
}
