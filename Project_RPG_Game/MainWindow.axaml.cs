using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Project_RPG_Game.GameWindow;

namespace Project_RPG_Game;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    

    private void BStart_OnClick(object? sender, RoutedEventArgs e) {
        Window NewGameWindow = new GameWindow.GameWindow();
        NewGameWindow.Show();
        Close();
    }

    private void Launcher_OnOpened(object? sender, EventArgs e) {
        
    }

    private void BLeave_OnClick(object? sender, RoutedEventArgs e) {
        Close();
    }
}