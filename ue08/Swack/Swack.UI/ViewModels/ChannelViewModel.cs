using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Swack.Logic;
using Swack.Logic.Models;

namespace Swack.UI.ViewModels;

public class ChannelViewModel : NotifyPropertyChanged
{
    private IMessagingLogic messagingLogic;
    private int unreadMessages;
    private string? currentMessage;
    // public event PropertyChangedEventHandler? PropertyChanged; // wegen INotifyPropertyChanged

    public Channel Channel { get; }
    public ObservableCollection<Message> Messages { get; } = [];
    
    
    public int UnreadMessages
    {
        get => unreadMessages;
        set 
        {
            // if (unreadMessages != value)
            // {
            //     unreadMessages = value;
            //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnreadMessages)));
            // }
            this.Set(ref unreadMessages, value);
        }
    }
    
    public string? CurrentMessage
    {
        get => currentMessage;
        set
        {
            // if (currentMessage != value)
            // {
            //     currentMessage = value;
            //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMessage)));
            // }
            this.Set(ref currentMessage, value);
        }
    }

    public ICommand SendMessageCommand { get; private set; }


    public ChannelViewModel(Channel channel, IMessagingLogic messagingLogic)
    {
        this.Channel = channel;
        this.messagingLogic = messagingLogic;
        this.SendMessageCommand = new AsyncDelegateCommand(this.SendMessageAsync, _ => !string.IsNullOrWhiteSpace(CurrentMessage));
    }

    public async Task SendMessageAsync(object? _)
    {
        if (!string.IsNullOrWhiteSpace(CurrentMessage))
        {
            await messagingLogic.SendMessageAsync(Channel, CurrentMessage);
            CurrentMessage = null;
        }
    }
}
