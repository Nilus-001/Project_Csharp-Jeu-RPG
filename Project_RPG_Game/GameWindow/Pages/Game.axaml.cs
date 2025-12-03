using System;
using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;


namespace Project_RPG_Game.GameWindow.Pages;

public partial class Game : UserControl {
    private GameWindow Main;
    private UserControl Inventory;
    
    
    public Game() {
        InitializeComponent();
        Inventory = new Inventory(this);
       
        
        
        
        
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
        if (e.Key == Key.E) {
            OpenInventory_OnClick(null, null);
        }
    }



    private void OpenInventory_OnClick(object? sender, RoutedEventArgs e) {
        Main.OpenOverlay(Inventory);
        
        

    }

   
    

    
    
}