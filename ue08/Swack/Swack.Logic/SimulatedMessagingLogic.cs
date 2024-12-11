using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;
using Swack.Logic.Models;

namespace Swack.Logic
{
    public class SimulatedMessagingLogic : IMessagingLogic
    {
        private const double NEW_MESSAGE_PROBABILITY = 0.4;
        private const double IMAGE_MESSAGE_PROBABILITY = 0.2;
        
        private IList<Channel> channels = new List<Channel>();
        private IDictionary<Channel, IList<Message>> messages = new Dictionary<Channel, IList<Message>>();
        private IList<User> simulatedUsers = new List<User>();
        private IList<Message> messagesToSend = new List<Message>();
        private Random random = new Random();
        private DispatcherTimer timer;
        private readonly User currentUser;

        public SimulatedMessagingLogic(User currentUser)
        {
            this.currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            InitializeSimulationTimer();
            InitializeUsers();
            InitializeChannels();
        }
        
        public event MessageReceivedHandler? MessageReceived;
        public event ChannelCreatedHandler? ChannelCreated;

        public Task CreateChannelAsync(Channel channel)
        {
            if (this.channels.Any(c => c.Name == channel.Name))
            {
                throw new ArgumentException($"Channel {channel.Name} already exists, name must be unique.");
            }

            this.channels.Add(channel);
            ChannelCreated?.Invoke(channel);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Channel>> GetChannelsAsync()
        {
            return Task.FromResult<IEnumerable<Channel>>(this.channels);
        }

        public Task SendMessageAsync(Channel channel, string text)
        {
            var message = new Message(channel, this.currentUser, DateTime.Now, text);

            SendMessage(message);

            SimulateMessage(message.Channel);

            return Task.CompletedTask;
        }

        private void SendMessage(Message message)
        {
            if (!this.messages.TryGetValue(message.Channel, out var channelMessages))
            {
                channelMessages = new List<Message>();
            }

            channelMessages.Add(message);
            MessageReceived?.Invoke(message);
        }

        private void SimulateMessage(Channel? channel = null)
        {
            Message simulatedMessage;

            var user = GetRandomUser();
            var timestamp = DateTime.Now.AddMilliseconds(random.Next(3000));
            channel ??= this.channels[random.Next(this.channels.Count)]; // choose a channel randomly if not set


            // randomly send image or text message
            if (random.NextDouble() < IMAGE_MESSAGE_PROBABILITY)
            {
                // add ticks to generate unique urls (otherwise WPF would cache the image)
                // var imageUrl = $"http://lorempixel.com/400/300?{DateTime.Now.Ticks}" // does not seem to work anymore
                var imageUrl = $"https://picsum.photos/400/300?{random.Next()}";
                simulatedMessage = new ImageMessage(channel, user, timestamp, imageUrl);
            }
            else
            {
                var text = GetRandomMessageText();
                simulatedMessage = new Message(channel, user, timestamp, text);
            }

            this.messagesToSend.Add(simulatedMessage);
        }

        private User GetRandomUser()
        {
            return this.simulatedUsers[random.Next(this.simulatedUsers.Count)];
        }

        private string GetRandomMessageText()
        {
            var answers = new[]
            {
                "Interessant! 😏",
                "Eher nein. 😢",
                "Ok. 😊",
                "Hahahaha. 😂",
                "Echt?",
                "Hi! ✌",
            };

            return answers[random.Next(answers.Length)];
        }
        
        private void InitializeChannels()
        {
            this.channels.Add(new Channel("#swk"));
            this.channels.Add(new Channel("#kurztests"));
            this.channels.Add(new Channel("#fun"));
            this.channels.Add(new Channel("#pub"));
        }

        private void InitializeUsers()
        {
            this.simulatedUsers.Add(new User("Johann H.", "https://robohash.org/1.png?&size=150x150"));
            this.simulatedUsers.Add(new User("Sebastian P.", "https://robohash.org/2.png?&size=150x150"));
            this.simulatedUsers.Add(new User("Daniel S.", "https://robohash.org/3.png?&size=150x150"));
            this.simulatedUsers.Add(new User("Christian S.", "https://robohash.org/4.png?&size=150x150"));
            this.simulatedUsers.Add(new User("Roman S.", "https://robohash.org/5.png?s&size=150x150"));
        }

        [MemberNotNull(nameof(timer))]
        private void InitializeSimulationTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(1000);
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Simulate();
        }

        private void Simulate()
        {
            if (this.messagesToSend.Any())
            {
                var messages = this.messagesToSend.Where(m => m.Timestamp < DateTime.Now).ToList();

                foreach (var message in messages)
                {
                    this.messagesToSend.Remove(message);

                    SendMessage(message);
                }
            }

            if (this.random.NextDouble() < NEW_MESSAGE_PROBABILITY)
            {
                SimulateMessage();
            }
        }
    }
}
