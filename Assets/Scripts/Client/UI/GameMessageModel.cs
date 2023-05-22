
using System;

namespace ChargeGame
{
    public class GameMessageModel : ModelBase
    {
        private ClientContext _context;

        public string Message { get; private set; }
        public Action<string> MessageChanged;

        public GameMessageModel(ClientContext context)
        {
            _context = context;
        }


        protected override void Subscribe()
        {
            _context.NewMessageRecieved += UpdateText;
        }
        protected override void Unsubscribe()
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