using Microsoft.Win32;
using MuzioPlayer.SqlLite;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace MuzioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Song currentSong = new Song();

        WaveOutEvent song = new WaveOutEvent();

        string pathToSong;

        bool playStop = false;

        public Song CurrentSong
        {
            get { return currentSong; }
            set
            {
                currentSong = value;
                OnPropertyChanged(nameof(currentSong));
            }
        }

        public BindingList<Song> songs = new BindingList<Song>();


        public MainWindow()
        {
            InitializeComponent();

            songs = new BindingList<Song>();///  инициализация с бдшки

            currentSong = songs.First(i => i.Selected == true);

            dataGrid.ItemsSource = songs;

            DataContext = this;
        }

        private void OpenPopup_Click(object sender, RoutedEventArgs e)
        {
            //popup.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pivo");
        }

        private void CurrentSong_panel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pivo");

        }

        private void PlayPause_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var audioFile = new AudioFileReader(currentSong.Path);


                if (playStop)
                {
                    song.Stop();

                    playStop = false;

                    return;
                }

                song.Init(audioFile);

                song.Play();

                playStop = true;
            }
            catch { }
        }

        private void LoadSongs_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Audio Files (*.mp3, *.wav)|*.mp3;*.wav|All Files (*.*)|*.*";

            openFileDialog.Multiselect = false;


            if (openFileDialog.ShowDialog() == false) { return; }


            pathToSong = openFileDialog.FileName;


            using (var audioFile = new AudioFileReader(pathToSong))
            {
                var tagLibFile = TagLib.File.Create(pathToSong);

                songs.Add(new Song(tagLibFile.Tag.Title, tagLibFile.Tag.FirstPerformer, (double)tagLibFile.Properties.Duration.TotalSeconds, pathToSong));

            }

        }
        private void dataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectionChanged();
        }







        void SelectionChanged()
        {
            foreach (var item in songs)
            {
                item.Selected = false;
            }

            songs.First(i => i.Equals(currentSong)).Selected = true;

        }












        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}

