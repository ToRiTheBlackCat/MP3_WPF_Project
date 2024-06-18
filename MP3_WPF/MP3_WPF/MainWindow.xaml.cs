using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MP3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyCollectionChanged
    {
        private bool isCheckVN;
        private bool isCheckEU;
        private bool isCheckKO;
        public bool IsCheckVN
        {
            get => isCheckVN;
            set
            {
                if (isCheckVN != value)
                {
                    isCheckVN = value;
                    OnPropertyChanged("IsCheckVN");
                }
            }
        }
        public bool IsCheckEU
        {
            get => isCheckEU;
            set
            {
                if (isCheckEU != value)
                {
                    isCheckEU = value;
                    OnPropertyChanged("IsCheckEU");
                }
            }
        }
        public bool IsCheckKO
        {
            get => isCheckKO;
            set
            {
                if (isCheckKO != value)
                {
                    isCheckKO = value;
                    OnPropertyChanged("IsCheckKO");
                }
            }
        }

        HttpClient httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            lbxListSong.ItemsSource = new List<string>() { "", "", "", "", "", "", "", "", "", "" };
            this.DataContext = this;
            isCheckVN = true;
            httpClient.BaseAddress = new Uri(@"https://zingmp3.vn/zing-chart-tuan/bai-hat-Viet-Nam/IWZ9Z08I.html");
        }


        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("HELLO");
        }
        void CrawlBXH()
        {
            //try
            //{
            HttpRequest
                string htmlBXH = httpClient.GetStringAsync("").Result;
                string bxhPattern = @"<div class=""song-list"">(.*?)</div>";
                // Regex pattern for extracting song details
                string songPattern = @"<span class=""playlist-row-title__title-text"">(.*?)<\/span>";
                string artistPattern = @"<span class=""artists-albums"">([\w\s.,()&;]*)<\/span>";
                // Find all matches of song list
            //    Match bxhMatch = Regex.Match(htmlBXH, bxhPattern, RegexOptions.Singleline);

            //    if (bxhMatch.Success)
            //    {
            //        // Extract the song list HTML content
            //        string songListHtml = bxhMatch.Groups[1].Value;

            //        // Find all song items
            //        MatchCollection songMatches = Regex.Matches(songListHtml, @"<div class=""list-row"">(.*?)<\/div>", RegexOptions.Singleline);

            //        // Initialize a list to store song details
            //        List<Song> songs = new List<Song>();

            //        // Process each song item
            //        foreach (Match songMatch in songMatches.Take(10)) // Take only the first 10 songs
            //        {
            //            string songHtml = songMatch.Groups[1].Value;

            //            // Extract song details using regex
            //            Match titleMatch = Regex.Match(songHtml, songPattern);
            //            Match artistMatch = Regex.Match(songHtml, artistPattern);

            //            if (titleMatch.Success && artistMatch.Success && albumMatch.Success)
            //            {
            //                string title = titleMatch.Groups[1].Value;
            //                string[] artists = artistMatch.Groups[1].Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            //                string album = albumMatch.Groups[1].Value;

            //                // Add song to the list
            //                songs.Add(new Song { Title = title, Artists = artists, Album = album });
            //            }
            //        }

            //        // Display or process the extracted songs as needed
            //        foreach (Song song in songs)
            //        {
            //            Console.WriteLine($"Title: {song.Title}");
            //            Console.WriteLine($"Artists: {string.Join(", ", song.Artists)}");
            //            Console.WriteLine($"Album: {song.Album}");
            //            Console.WriteLine();
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Song list not found in HTML.");
            //    }
            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}



        }
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;

            var toggledButton = sender as ToggleButton;
            if (toggledButton == null) return;

            if (toggledButton == btnVN && IsCheckVN)
            {
                IsCheckEU = false;
                IsCheckKO = false;
            }
            else if (toggledButton == btnEU && IsCheckEU)
            {
                IsCheckVN = false;
                IsCheckKO = false;
            }
            else if (toggledButton == btnKO && IsCheckKO)
            {
                IsCheckVN = false;
                IsCheckEU = false;
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to quit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
    }
}