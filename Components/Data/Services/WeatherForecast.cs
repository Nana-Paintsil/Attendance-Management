namespace AttendanceManagement.Data
{
    public interface ILogoutService
    {
        bool IsLogoutFormVisible { get; }
        event Action<bool> OnLogoutFormVisibilityChanged;

        void ToggleLogoutForm();
        Task Logout();
    }

    public class LogoutService : ILogoutService
    {
        private bool isLogoutFormVisible = false;

        public bool IsLogoutFormVisible => isLogoutFormVisible;

        public event Action<bool> OnLogoutFormVisibilityChanged;

        public void ToggleLogoutForm()
        {
            isLogoutFormVisible = !isLogoutFormVisible;
            OnLogoutFormVisibilityChanged?.Invoke(isLogoutFormVisible);
        }

        public async Task Logout()
        {
            // Perform logout actions
            isLogoutFormVisible = false;
            OnLogoutFormVisibilityChanged?.Invoke(isLogoutFormVisible);
        }
    }

}