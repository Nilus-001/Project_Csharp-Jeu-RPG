using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Project_RPG_Game.GameWindow.Pages;


namespace Project_RPG_Game.GameWindow;

public partial class GameWindow : Window {
    
    public GameWindow() {
        InitializeComponent();

        WindowState = WindowState.FullScreen;
        MainContent.Content = new Game();
        KeyDown += OnKeyDown;
    }
    

    public void NavigateTo(UserControl page) {
        MainContent.Content = page;
    }

    public void OpenOverlay(UserControl page) {
        Overlay.Content = page;
        OverlayContainer.IsVisible = true;
    }

    public void CloseOverlay() {
        OverlayContainer.IsVisible = false;
        Overlay.Content = null;
    }
    
    private void OnKeyDown(object sender, KeyEventArgs e) {
        
        if (e.Key == Key.Escape) {
            
        }

        if (e.Key == Key.F11) {
            if (WindowState == WindowState.FullScreen) {
                WindowState = WindowState.Maximized;
            }
            else {
                WindowState = WindowState.FullScreen;
            }
        }
    }
    
   
}