using L6_7_Nikolaev.Models;
using L6_7_Nikolaev.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace L6_7_Nikolaev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            user = new User
            {
                Name = "Слава",
                SurName = "Николаев",
                Role = "Админ",
                Login = "SlavaNik",
                Password = "12345"
            };
        }

        private void myWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
       

       


        private void butClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void butReg_Click(object sender, RoutedEventArgs e)
        {
            RegistForm registForm = new RegistForm();
            if (registForm.ShowDialog() == true)
            {
                if (registForm.passwordBox.Text == user.Password) {
                    MessageBox.Show("Авторизация пройдена");
                    ListUserForm listUserForm = new ListUserForm();
                    this.DataContext = listUserForm;
                    listUserForm.ShowDialog();
                }
                else {
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                MessageBox.Show("Авторизация не пройдена");
            }
        }
    }
}
