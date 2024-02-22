using ExcelDataReader;
using L6_7_Nikolaev.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace L6_7_Nikolaev.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListUserForm.xaml
    /// </summary>
    public partial class ListUserForm : Window
    {
        private IExcelDataReader edr;
        public ListUserForm()
        {
            InitializeComponent();
            list = new ObservableCollection<User>
            {
                new User()
                {
                    Name = "Вася",
                    SurName = "Василий",
                    Role = "Пользователь",
                    Login = "12345@123",
                    Password= "Password",
                },
                new User()
                {
                    Name = "Петя",
                    SurName = "Петров",
                    Role = "Пользователь",
                    Login = "vas@mail.ru",
                    Password= "12345",
                },
                 new User()
                {
                    Name = "123",
                    SurName = "1234567",
                    Role = "Admin",
                    Login = "admin",
                    Password= "admin",
                },
            };
            gridData.ItemsSource = List;
            
        }
        private ObservableCollection<User> list;
        public ObservableCollection<User> List 
        { get 
            { 
                return list; 
            } 
            set
            {
                list = value;
            } 
        }
 

        private void butOpenExel_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EXCEL Files (*.xlsx)|.xlsx|EXCEL Files 2003 (*.xls)|.xls|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() != true)
                return;
            dataGridExel.ItemsSource = readFileExcel(openFileDialog.FileName);
        }

        private DataView readFileExcel(string fileNames)
        {
            var extension = fileNames.Substring(fileNames.LastIndexOf("."));
            FileStream fs = File.Open(fileNames, FileMode.Open, FileAccess.Read);

            if (extension == ".xlsx")
                edr = ExcelReaderFactory.CreateOpenXmlReader(fs);
            if(extension == ".lxs")
                edr = ExcelReaderFactory.CreateBinaryReader(fs);

            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true,
                }
            };
            DataSet dataSet = edr.AsDataSet(conf);
            DataView dgGrid = dataSet.Tables[0].AsDataView();

            edr.Close();
            return dgGrid;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Export();
        }
        void Export()
        {
            string path = "export.txt";
            var sw = new StreamWriter(path);
            DataTable users = new DataTable("SELECT * FROM [dbo].[user]");
            for (int i =0; i < users.Rows.Count; i++)
            {
                sw.WriteLine($"Name: {users.Rows[i][0]}");
                sw.WriteLine($"SurName: {users.Rows[i][1]}");
                sw.WriteLine($"Role: {users.Rows[i][2]}");
                sw.WriteLine($"Login: {users.Rows[i][3]}");
                sw.WriteLine($"Password: {users.Rows[i][4]}");

            }
            sw.Close();
            Process.Start("notepad.exe",path);
        }
    }
}