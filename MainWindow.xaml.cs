using System;
using System.Windows;
using System.Windows.Threading;
namespace _21PR_Kolbazov_RPM
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private DateTime startTime;
        private TimeSpan elapsedTime;
        private bool isPaused = false;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }
        // -------------------------------------------------------------------
        // Метод InitializeTimer(): создан для инициализации таймера, чтобы уменьшить кол-во кода в конструкторе
        private void InitializeTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1) // Свойство Interval устанавливает интервал таймера в виде объекта TimeSpan
            };
            timer.Tick += Timer_Tick;
        }
        // -------------------------------------------------------------------
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                elapsedTime = DateTime.Now - startTime;
                UpdateTimerText();
            }
        }
        // -------------------------------------------------------------------
        // Метод UpdateTimerText(): Вынесен в отдельный метод для улучшения читаемости.
        private void UpdateTimerText()
        {
            TimerTextBlock.Text = $"{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}:{elapsedTime.Milliseconds / 10:D2}";
        }
        // -------------------------------------------------------------------
        private void StartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                ResumeTimer(); // Возобновляем таймер
            }
            else
            {
                StartNewTimer(); // Запускаем новый таймер
            }
            timer.Start(); // Запускаем таймер
        }
        // -------------------------------------------------------------------
        // Методы StartNewTimer() и ResumeTimer(): Разделены действия на запуск нового таймера и возобновление при нажатии на кнопку "Старт". Это улучшает структурирование кода.
        private void StartNewTimer()
        {
            elapsedTime = TimeSpan.Zero; // Сбрасываем время при новом старте
            startTime = DateTime.Now; // Запоминаем время старта
        }
        private void ResumeTimer()
        {
            startTime = DateTime.Now - elapsedTime; // Изменяем начало так, чтобы учитывать уже прошедшее время
            isPaused = false; // Снимаем паузу
        }
        // -------------------------------------------------------------------
        private void PauseTimerButton_Click(object sender, RoutedEventArgs e)
        {
            PauseTimer(); // Останавливаем таймер
        }
        // -------------------------------------------------------------------
        // Методы PauseTimer() и StopTimer(): Вынесены действия в отдельные методы для лучшего управления логикой.
        private void PauseTimer()
        {
            timer.Stop();
            isPaused = true; // Устанавливаем флаг паузы
        }
        private void StopTimer()
        {
            timer.Stop(); // Останавливаем таймер
            isPaused = false; // Сброс флага паузы
            elapsedTime = TimeSpan.Zero; // Сбрасываем время
            TimerTextBlock.Text = "00:00:00"; // Сбрасываем текстовый блок
        }
        // -------------------------------------------------------------------
        private void StopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            StopTimer(); // Останавливаем таймер и сбрасываем
        }
    }
}
