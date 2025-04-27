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
    public partial class UserInfoControl : UserControl
    {
        public UserInfoControl()
        {
            InitializeComponent();
            GenderComboBox.SelectedIndex = 0;
        }

        public string GetGender()
        {
            return GenderComboBox.SelectedItem?.ToString();
        }

        public string GetAge()
        {
            return AgeTextBox.Text;
        }
    }
}
