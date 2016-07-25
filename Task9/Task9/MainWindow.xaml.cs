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
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Task9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AssemblyHelper helper;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSelectDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.MyDocuments;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                tbSelectedDir.Text = fbd.SelectedPath;
        }

        private void tbSelectedDir_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbDllList.Items.Clear();
            DirectoryHelper helper = new DirectoryHelper(tbSelectedDir.Text);
            var dllList = helper.GetAllDllFromDir();
            dllList.Select(d => lbDllList.Items.Add(d.Name)).ToList();
        }

        private void lbDllList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbTypeList.Items.Clear();
            if (lbDllList.SelectedIndex != -1)
            {
                try
                {
                    helper = new AssemblyHelper($@"{tbSelectedDir.Text}/{lbDllList.SelectedItem.ToString()}");
                    var types = helper.GetTypes();
                    types.Select(t => lbTypeList.Items.Add(t)).ToList();
                }
                catch (ReflectionTypeLoadException)
                {
                    //string message = $"{ex.Message}\n";
                    //ex.LoaderExceptions.Select(exc => message += $"{exc.Message}\n").ToList();
                    System.Windows.MessageBox.Show("Could not load one or more requested types because failed to load one of dependecies.");
                }
            }
        }

        private void cbVariant_Loaded(object sender, RoutedEventArgs e)
        {
            cbVariant.Items.Add("Fields");
            cbVariant.Items.Add("Properties");
            cbVariant.Items.Add("Methods");
            cbVariant.Items.Add("All");
            cbVariant.SelectedIndex = 3;
        }

        private void lbTypeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbMemberList.Items.Clear();
            List<MemberInfo> list;
            int typeIndex = lbTypeList.SelectedIndex;
            if (typeIndex != -1)
            {
                try
                {
                    switch (cbVariant.SelectedIndex)
                    {
                        case 0:
                            list = helper.GetFieldsOfType(typeIndex);
                            break;
                        case 1:
                            list = helper.GetPropertiesOfType(typeIndex);
                            break;
                        case 2:
                            list = helper.GetMethodsOfType(typeIndex);
                            break;
                        default:
                            list = helper.GetAllMembersOfType(typeIndex);
                            break;
                    }
                    list.Select(m => lbMemberList.Items.Add($"{m.MemberType}: {m.ToString()}")).ToList();
                }
                catch (FileNotFoundException)
                {
                    System.Windows.MessageBox.Show("Could not get requested info about the type because failed to load one of dependencies.");
                }
            }
        }

        private void cbVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbTypeList_SelectionChanged(sender, e);
        }
    }
}
