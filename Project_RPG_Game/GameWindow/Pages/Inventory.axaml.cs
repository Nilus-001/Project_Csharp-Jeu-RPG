using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace Project_RPG_Game.GameWindow.Pages;

public partial class Inventory : UserControl {
    public UserControl GamePage;
    private GameWindow Main;


    public Inventory() {
        InitializeComponent();
    }
    public Inventory(UserControl game) {
        InitializeComponent();
        GamePage = game;



        AttachedToVisualTree += (sender, visualTreeAttachmentEventArgs) => {
            Main = VisualRoot as GameWindow;
            if (Main != null) {
                Main.KeyDown += OnKeyDown;
            }

            
        };
        DetachedFromVisualTree += (sender, visualTreeAttachmentEventArgs) => {
            if (Main != null) {
                Main.KeyDown -= OnKeyDown;
            }
        };

    }
    private void OnKeyDown(object sender, KeyEventArgs e) {
        
        if (e.Key == Key.Escape ||  e.Key == Key.E) {
            Main.CloseOverlay();
        }
       

       
    }
}