
using System;

namespace ChargeGame
{
    public class GameMessageModel
    {
        private ClientContext _context;

        public string Message { get; private set; }
        public Action<string> MessageChanged;

        public GameMessageModel(ClientContext context)
        {
            _context = context;
        }

        public void Enable()
        {
            Subscribe();
        }
        public void Disable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _context.NewMessageRecieved += UpdateText;
        }
        private void Unsubscribe()
        {
            _context.NewMessageRecieved -= UpdateText;
        }

        private void UpdateText(string message)
        {
            Message = message;
            MessageChanged?.Invoke(message);
        }
    }
}