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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using Microsoft.Win32;
using WinForm = System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System;
using TagLib;
using System.Reflection;

namespace Cursova_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //PlayList playList;
        private int VisibilityPlayList = 1;
        private int VisibilityInformation = 1;
        private int On_Off_volume = 1;
        private int VisibilityEqualizer = 1;
        private int Play_Stop = 1;
        private int index = 0;

        OpenFileDialog openFile = new OpenFileDialog();
        SaveFileDialog saveFile = new SaveFileDialog();

        private int counters = 1;
       // [Serializable]
        public ObservableCollection<object> list { get; private set; }
        OpenFileDialog open = new OpenFileDialog();
        private string path;
        WaveStream waveStream;
        WaveOut media;
        TagLib.File tagFile;
        FileInfo info;
        private System.Timers.Timer timer = new System.Timers.Timer(1000);

        public MainWindow()
        {
            path = "../../";
            list = new ObservableCollection<object>();
            Load(path);
            InitializeComponent();
            //media.Visibility = Visibility.Hidden;
          this.MouseDown += MainWindow_MouseDown;
          //WaveStream waveStream = new Mp3FileReader(@"C:\Users\Ultimate\Desktop\Oleg\Cursova_WPF_NewVersion\Cursova_WPF\Ace_of_base_-_Experience_Pearls_(get-tune.net).mp3");
          Init(index);
          //if (list[index] != null)
          //{
          //    waveStream = new Mp3FileReader(list[index].ToString());
          //    media = new WaveOut();
          //    media.Init(waveStream);
          //}
          //tim.Interval = 1000;
          //tim.Tick += new EventHandler(tim_Tick);
          timer.Interval = 1000;
          this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);

          volumeSlider.Maximum = 1;
          volumeSlider.Value = 0.5;
          
          gridPlayList.Visibility = System.Windows.Visibility.Collapsed;
          Information.Visibility = System.Windows.Visibility.Collapsed;
          Equalizer.Visibility = System.Windows.Visibility.Collapsed;
  
          InitializeToolTip();
          
        }
        private void InitializeToolTip()
        {
            play_Stop_Image.ToolTip = "Play/Stop";
            prev_Image.ToolTip = "Previous song";
            next_Image.ToolTip = "Next song";
            playlist_Image.ToolTip = "Play list";
            equalizer_Image.ToolTip = "Equalizer";
            serialize_Image.ToolTip = "Save play list";
            deserialize_Image.ToolTip = "Open play list";
            option_Image.ToolTip = "Options";
            on_Off_Image.ToolTip = "Volume";
            exit_Image.ToolTip = "Exit";
            turn_Image.ToolTip = "Turn";
            add_Image.ToolTip = "Add a song";
            dell_Image.ToolTip = "To delete a song";
            AddFolder_Image.ToolTip = "Add a folder song";
            information_Image.ToolTip = "Information about song";
            exitPlayList_Image.ToolTip = "Exit";
            serch_Image.ToolTip = "Search song";
            exitInf_Image.ToolTip = "Exit";
        }
        void timer_Tick(object sender, EventArgs e)
        {
            //durationSlider.Maximum = tagFile.Properties.Duration.TotalSeconds;//media.NaturalDuration.TimeSpan.TotalSeconds;
            //durationSlider.Value = tagFile.Properties.Duration.TotalSeconds;//media.Position.TotalSeconds;
            durationSlider.Value = media.GetPosition();
            //txtTime.Text = Convert.ToInt32(tagFile.Properties.Duration.Minutes).ToString() + ":" + Convert.ToInt32(tagFile.Properties.Duration.Seconds).ToString();
            
        }
        //void tim_Tick(object sender, EventArgs e)
        //{
        //    if (media.NaturalDuration.HasTimeSpan == true)
        //    {
        //        durationSlider.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
        //        durationSlider.Value = media.Position.TotalSeconds;
        //        txtTime.Text = Convert.ToInt32(media.Position.Minutes).ToString() + ":" + Convert.ToInt32(media.Position.Seconds).ToString();
        //    }
        //}
#region Button
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
                list.Add(path);
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
            gridPlayList.Visibility = System.Windows.Visibility.Collapsed;
            Information.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            if (playList != null)
            {
                Close();

            }
        }
        private void Turn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ShowInTaskbar = false;
            this.WindowState = WindowState.Normal;
            this.ShowInTaskbar = !this.ShowInTaskbar;
            if (this.ShowInTaskbar)
            {
                this.WindowState = WindowState.Minimized;
            }
            else
            {
                this.WindowState = WindowState.Normal;

            }
        }
        private void PlayList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //playList.Window1.Left;
            if (VisibilityPlayList == 1)
            {
                gridPlayList.Visibility = System.Windows.Visibility.Visible;
                VisibilityPlayList++;
            }
            else
            {
                gridPlayList.Visibility = System.Windows.Visibility.Collapsed;
                Information.Visibility = System.Windows.Visibility.Collapsed;
                VisibilityPlayList--;
            }


        }

        private void Play_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //media.Play();
            //WaveStream waveStream = new Mp3FileReader(list[index].ToString());
            //var x = new WaveOut();
            //x.Init(waveStream);
            if (Play_Stop == 1)
            {
                media.Play();
                ImageSourceConverter converter = new ImageSourceConverter();
                string path = string.Format(@"{0}\{1}", (System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), "../../Icon/Pause.png");
                ImageSource imageSource = (ImageSource)converter.ConvertFromString(path);
                play_Stop_Image.Source = imageSource;
                Play_Stop++;
            }
            else
            {
                media.Pause();
                ImageSourceConverter converter = new ImageSourceConverter();
                string path = string.Format(@"{0}\{1}", (System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), "../../Icon/Play.png");
                ImageSource imageSource = (ImageSource)converter.ConvertFromString(path);
                play_Stop_Image.Source = imageSource;
                Play_Stop--;
            }

        }

        private void PlayNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            index++;
            if (index != list.Count)
            //index++;
            // if (index != playList.Items.Count)
            {
                media.Stop();
                //index++;
                Init(index);
                //WaveStream waveStream = new Mp3FileReader(list[index].ToString());
                //var x = new WaveOut();
                //x.Init(waveStream);
                media.Play();
            }
            else
            {
                index--;
            }

        }

        private void playList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            media.Stop();
            this.index = playList.SelectedIndex;
            Init(index);
            media.Play();
        }

        private void PlayPrev_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (index != 0)
            {
                media.Stop();
                index++;
                Init(index);
                //WaveStream waveStream = new Mp3FileReader(list[index].ToString());
                //var x = new WaveOut();
                //x.Init(waveStream);
                media.Play();
            }
        }

        private void Information_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object x = playList.SelectedItem;
            if (VisibilityInformation == 1)
            {
                Information.Visibility = System.Windows.Visibility.Visible;
                VisibilityInformation++;
            }
            else
            {
                Information.Visibility = System.Windows.Visibility.Collapsed;
                VisibilityInformation--;
            }

        }

        private void InformationExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Information.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void volumeOn_Off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (On_Off_volume == 1)
            {
                volumeSlider.Value = 0;
                ImageSourceConverter converter = new ImageSourceConverter();
                string path = string.Format(@"{0}\{1}", (System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), "../../Icon/SoundOff.png");
                ImageSource imageSource = (ImageSource)converter.ConvertFromString(path);
                on_Off_Image.Source = imageSource;
                On_Off_volume++;
            }
            else
            {
                volumeSlider.Value = 0.5;
                ImageSourceConverter converter = new ImageSourceConverter();
                string path = string.Format(@"{0}\{1}", (System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)), "../../Icon/SoundOn.png");
                ImageSource imageSource = (ImageSource)converter.ConvertFromString(path);
                on_Off_Image.Source = imageSource;
                On_Off_volume--;
            }
        }
#endregion
        private void Init(int index)
        {
              this.index = index;
              waveStream = new Mp3FileReader(list[index].ToString());
              media = new WaveOut();
              media.Init(waveStream);

              tagFile = TagLib.File.Create(list[index].ToString());
              txtAlbum.Text = tagFile.Tag.Album;
              txtArtists.Text = String.Join(", ", tagFile.Tag.Performers);
              txtYear.Text = tagFile.Tag.Title;

              infTitleSound.Text = tagFile.Tag.Title;
              infAlbumSound.Text = tagFile.Tag.Album;
              infArtistSound.Text = String.Join(", ", tagFile.Tag.Performers);
              infYearSound.Text = (tagFile.Tag.Year).ToString();
              infLenghtSound.Text = tagFile.Properties.Duration.ToString("mm\\:ss");
              info = new FileInfo(list[index].ToString());
              infSizeSound.Text = (info.Length / 1024 / 1024).ToString() + " MB";
              infBitSound.Text = tagFile.Properties.AudioBitrate.ToString() + " kbps";

              txtTime.Text = tagFile.Properties.Duration.ToString("mm\\:ss");
              //soundImage.Source = @"Icon/1380032928_CD-Music.png.png";
              //string x = tagFile.Tag.Pictures;
              

            //TagLib.File tagFile = TagLib.File.Create(System.IO.Path.GetFileName((list[index].ToString())));
            //string x = tagFile.Tag.FirstAlbumArtist;
        }
        private void Load(string path)
        {
            FileInfo f;
            double size;
            //System.IO.Path.GetFileName((image[ind]));
            //var installPath = @"../../";//Settings.Default.TargetDirectory;


            var packages = System.IO.Directory.GetFiles(path, "*.mp3");

            
            
           // MessageBox.Show(tagFile.Tag.Album);
           // AuthorSound.Text = tagFile.Tag.Album;
            //WaveStream waveStream = new Mp3FileReader(packages.ToString());
            //var x = new WaveOut();
            //x.Init(waveStream);
            //x.Play();
            //list.Clear();
            foreach (string filename in packages)
            {
                f = new FileInfo(filename);
                size = f.Length / 1024 / 1024;
                //list.Add(new { Filename = System.IO.Path.GetFileName((filename)) });
                list.Add(filename);
                counters++;
            }

        }

        
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }




        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float test = float.Parse(volumeSlider.Value.ToString());
           
            media.Volume = test;
        }

        private void SavePlayList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FileStream fs = new FileStream("../../SaveFile.xml", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(fs, list);
            fs.Close();
        }

        private void OpenPlayList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //List<string> words;
            ObservableCollection<object> tmpList;
            FileStream fs = new FileStream("../../SaveFile.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter bf = new BinaryFormatter();
            tmpList = (ObservableCollection<object>)bf.Deserialize(fs);
            for (int i = 0; i < tmpList.Count; i++)
            {
                list.Add(tmpList[i]);
            }

            fs.Close();
        }

        private void Equalizer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (VisibilityEqualizer == 1)
            {
                Equalizer.Visibility = System.Windows.Visibility.Visible;
                VisibilityEqualizer++;
            }
            else
            {
                Equalizer.Visibility = System.Windows.Visibility.Collapsed;
                VisibilityEqualizer--;
            }
        }

     
    }
}
