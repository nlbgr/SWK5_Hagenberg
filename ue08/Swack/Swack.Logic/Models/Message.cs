using System;
using System.Collections.Generic;
using System.Text;

namespace Swack.Logic.Models
{
    public class Message
    {
        public Message(Channel channel, User user, DateTime timestamp, string text)
        {
            Channel = channel;
            User = user;
            Text = text;
            Timestamp = timestamp;
        }

        public Channel Channel { get; set; }

        public User User { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
