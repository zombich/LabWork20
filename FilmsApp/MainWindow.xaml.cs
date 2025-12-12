using FilmsLibrary.Contexts;
using FilmsLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace FilmsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilmsDbContext _context = new FilmsDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AddFrameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedFilm = FilmListBox.SelectedItem as Film;

            if (selectedFilm is null)
                return;

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = " Файл .PNG(*.png)|*.png|Файл .JPG (*.jpg)|*.jpg";

            if (openFileDialog.ShowDialog() == false)
                return;

            var fileInfo = new FileInfo(openFileDialog.FileName);

            if (fileInfo.Length >> 20 > 2)
            {
                MessageBox.Show("Файл слишком большой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var directory = Path.Combine(Environment.CurrentDirectory, "images");

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var fileDirectory = Path.Combine(directory, openFileDialog.SafeFileName);

                File.Copy(openFileDialog.FileName, fileDirectory, true);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var frame = new Frame 
            { 
                FilmId = selectedFilm.FilmId,
                FileName = openFileDialog.SafeFileName
            };

            _context.Frames.Add(frame);
            await _context.SaveChangesAsync();

            MessageBox.Show("Файл сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var films = await _context.Films.ToListAsync();
            FilmListBox.ItemsSource = films;
            FilmListBox.SelectedItem = films.FirstOrDefault();
        }
    }
}