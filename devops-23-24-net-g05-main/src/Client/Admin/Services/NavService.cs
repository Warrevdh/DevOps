namespace Client.Admin.Services
{
    public class NavService
    {
        public event Action OnActiveChanged;

        private string _active;
        public string Active
        {
            get => _active;
            set
            {
                if (_active != value)
                {
                    _active = value;
                    OnActiveChanged?.Invoke();
                }
            }
        }
    }
}
