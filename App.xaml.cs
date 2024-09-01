namespace AttendanceManagement
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            const int minWidth = 1280;
            const int minHeight = 700;
            window.MinimumHeight = minHeight;
            window.MinimumWidth = minWidth;

            return window;
        }

    }
}
