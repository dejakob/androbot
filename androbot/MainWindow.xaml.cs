using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using androbot.Annotations;
using Microsoft.Win32;
using MessageBox = System.Windows.MessageBox;

namespace androbot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            Properties = new ObservableCollection<AndroProperty>();
            Tables = new ObservableCollection<AndroTable>();
            NewAndroProperty = new AndroProperty();
            NewAndroProperty.Getter = true;
            NewAndroProperty.Setter = true;
            NewAndroProperty.Column = true;
            DataContext = this;
            InitializeComponent();
        }

        public ObservableCollection<AndroProperty> Properties { get; set; }
        public String ProjectPath { get; set; }
        public AndroProperty NewAndroProperty { get; set; }
        public String ObjectName { get; set; }
        public ObservableCollection<AndroTable> Tables { get; set; }
        public String NewTable { get; set; }
        public AndroTable SelectedTable { get; set; }
        public ObservableCollection<AndroProperty> CurrentColumns { get; set; }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Properties.Add(NewAndroProperty);
            NewAndroProperty = new AndroProperty();
            NewAndroProperty.Getter = true;
            NewAndroProperty.Setter = true;
            NewAndroProperty.Column = true;
            OnPropertyChanged("NewAndroProperty");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                ProjectPath = fbd.SelectedPath;
                OnPropertyChanged("ProjectPath");
            }
        }

        private void btnSaveObject_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectPath != "" && Directory.Exists(ProjectPath + "\\src"))
            {
                ObjectSaver.Save(ObjectName, Properties, ProjectPath);
                Tables.Add(new AndroTable()
                {
                    Name = ObjectName,
                    Properties = Properties
                });
                Properties = new ObservableCollection<AndroProperty>();
                ObjectName = "";
                OnPropertyChanged("Properties");
                OnPropertyChanged("ObjectName");
                
            }
            else
            {
                MessageBox.Show("Please select a valid Eclipse project folder...");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tables.Add(new AndroTable()
            {
                Name = NewTable
            });
            NewTable = "";
            OnPropertyChanged("NewTable");
        }

        private void ListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (SelectedTable != null)
            {
                CurrentColumns = SelectedTable.Properties;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProjectPath != "" && Directory.Exists(ProjectPath + "\\src"))
            {
                DatabaseSaver ds = new DatabaseSaver();
                ds.CreateHelper(ProjectPath, Tables);
                foreach (AndroTable table in Tables)
                {
                    DAOSaver.CreateDAO(ProjectPath, table);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid Eclipse project folder...");
            }
        }


    }

    public class AndroProperty
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public Boolean AddToCtor { get; set; }
        public Boolean Getter { get; set; }
        public Boolean Setter { get; set; }
        public Boolean Column { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class AndroTable
    {
        public ObservableCollection<AndroProperty> Properties { get; set; }
        public String Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

}
