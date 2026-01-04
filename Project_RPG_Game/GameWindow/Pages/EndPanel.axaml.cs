using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Tmds.DBus.Protocol;

namespace Project_RPG_Game.GameWindow.Pages;

public partial class EndPanel : UserControl {
    private TextBlock _titleText;
    public TextBlock messageText;
    private Button _leaveButton;

    public EndPanel() {
        InitializeComponent();
        
        // Récupération des contrôles depuis le XAML
        _titleText = this.FindControl<TextBlock>("TitleText");
        messageText = this.FindControl<TextBlock>("MessageText");
        _leaveButton = this.FindControl<Button>("LeaveButton");
        
    }

    public void Win() {
        _titleText.Text = "YOU WIN !";
        _titleText.Foreground = Brushes.Gold;
        messageText.Text = "You have reach the day 31 !";
    }

    public void Lose(string msg) {
        _titleText.Text = "GAME OVER";
        _titleText.Foreground = Brushes.DarkRed;
        messageText.Text = msg;
    }

    private void OnLeaveButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e) {
        var window = this.VisualRoot as Window;
        window?.Close();
    }
}