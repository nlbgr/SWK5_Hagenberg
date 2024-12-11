using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swack.Logic;
using Swack.Logic.Models;

namespace Swack.UI.ViewModels;

public class MainViewModel
{
    private IMessagingLogic messagingLogic;

    public ObservableCollection<ChannelViewModel> Channels { get; private set; } = [];

    private ChannelViewModel? currentChannel;

    public ChannelViewModel? CurrentChannel
    {
        get => currentChannel;
        set
        {
            currentChannel = value;
            if (currentChannel is not null)
            {
                currentChannel.UnreadMessages = 0;
            }
        }
    }

    public MainViewModel(IMessagingLogic messagingLogic)
    {
        // Channels = new List<Channel>
        // {
        //     new Channel("#swk5"),
        //     new Channel("#wea5"),
        //     new Channel("#kurztests")
        // };

        this.messagingLogic = messagingLogic ?? throw new ArgumentException(nameof(messagingLogic));
    }

    

    public async Task InitializeAsync()
    {
        foreach( var channel in await messagingLogic.GetChannelsAsync())
        {
            Channels.Add(new ChannelViewModel(channel, messagingLogic));
        }

        this.messagingLogic.MessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(Message message)
    {
        var channel = this.Channels.FirstOrDefault(c => c.Channel.Name == message.Channel.Name);
        channel?.Messages.Add(message);

        if (channel is not null && channel != CurrentChannel)
        {
            channel.UnreadMessages++;
        }
    }
}
