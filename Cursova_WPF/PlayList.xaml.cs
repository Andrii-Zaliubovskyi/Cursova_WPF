using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using WinForm = System.Windows.Forms;

namespace Cursova_WPF
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class PlayList : Window
    {
        //public static ObservableCollection<PlayList> list;
        public ObservableCollection<object> list { get; private set; }
        OpenFileDialog open = new OpenFileDialog();
        private string path;
        private int counters = 1;
        public PlayList()
        {
          //  list = new ObservableCollection<PlayList>
          //{
          //    new PlayList {Name = "Olya"},
          //    new PlayList {Name = "Kolya"},
          //    new PlayList {Name = "Sveta"},
          //    new PlayList {Name = "Oleg"}
          //};

            path = "../../txt/";
            list = new ObservableCollection<object>();
            Load(path);
            InitializeComponent();
            toolBar.MouseDown += PlayList_MouseDown;
           
        }

        private void PlayList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Load(string path)
        {
            FileInfo f;
            double size;
            //System.IO.Path.GetFileName((image[ind]));
            //var installPath = @"../../";//Settings.Default.TargetDirectory;

            
               var packages = System.IO.Directory.GetFiles(path, "*.txt");
            

            //list.Clear();
            foreach (string filename in packages)
            {
                f = new FileInfo(filename);
                size = f.Length / 1024 / 1024;
                //list.Add(new { Filename = System.IO.Path.GetFileName((filename)) });
                list.Add(counters + ". " + System.IO.Path.GetFileName((filename)));
                counters++;
            }

        }

        private void PlayListAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var dialog = new WinForm.FolderBrowserDialog();

            //if (dialog.ShowDialog() == WinForm.DialogResult.OK)
            //{
            //    path = dialog.SelectedPath;

            //    Load(path);

            //}
            var dialog = new WinForm.OpenFileDialog();
            if (dialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                path = dialog.FileName;
                list.Add(counters + ". " + System.IO.Path.GetFileName(path));
                counters++;
                //Load(dialog.SafeFileName);
            }

        }
        private void PlayListAddFolder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new WinForm.FolderBrowserDialog();
            //dialog.ShowDialog();
            if (dialog.ShowDialog() == WinForm.DialogResult.OK)
            {
                path = dialog.SelectedPath;
                //string test = Directory.GetFiles(path, "*.txt").ToString();
                //list.Add(Directory.GetFiles(path, "*.txt"));
                Load(path);
                //list.Add(System.IO.Path.GetFileName((path)));
                // dialog.ShowDialog();
            }
            //var dialog = new System.Windows.Forms.FolderBrowserDialog();
            //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
        }
        private void PlayListDell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (playList.SelectedItems.Count > 1)
            {
                for (int i = playList.SelectedItems.Count; i >= 0; i--)
                {
                    list.Remove(playList.SelectedItem);
                }
            }
            else
            {
                list.Remove(playList.SelectedItem);
            }
        }

        private void ExitPlayList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
