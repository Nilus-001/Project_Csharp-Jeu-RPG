using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Project_RPG_Game.characters;
using Project_RPG_Game.items;
using Project_RPG_Game.status.@interface;


namespace Project_RPG_Game.GameWindow.Pages;

public partial class Inventory : UserControl {
    public Game GamePage;
    private GameWindow Main;
    public Guild Guild;

    private Hero HeroSelectedForItem;
    private Item ItemSelected;


    public Inventory() {
        InitializeComponent();
    }
    public Inventory(Guild guild,Game game) {
        InitializeComponent();
        GamePage = game;
        Guild = guild;
        UpdateCharacterInfo();
        UpdateInventoryInfo();

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
    
    
    public void UpdateInventoryInfo() {
        for (int i = 0; i < 12; i++) {
            int row = i / 4;
            int col = i % 4; 
            
            var itemBlock = InventoryZone.Children
                            .OfType<Border>()
                            .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);

            itemBlock.Child = null;
            if (i < Guild.GuildInventory.Count) {
                itemBlock.Child = CreateItemCard(Guild.GuildInventory[i]);
            }
        }
    }
    
    public void UpdateCharacterInfo() {
        for (int i = 0; i < 8; i++) {
            int row = i / 2;
            int col = i % 2;
            
            var heroBlock = HeroInfo.Children
                .OfType<Border>()
                .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);
            
            heroBlock.Child = null;
            if (i < Guild.GuildHeroes.Count) {
                heroBlock.Child = CreateHeroCard(Guild.GuildHeroes[i]);
            }
        }
        
    }

    
    private Border CreateItemCard(Item item) {
  
        var border = new Border {
            Margin = new Thickness(4),
            CornerRadius = new CornerRadius(8),
            BorderThickness = new Thickness(2),
            
        };
        
        var btn = new Border {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Stretch,
            CornerRadius = new CornerRadius(6),
            Cursor = new Cursor(StandardCursorType.Hand),
            Background = Brushes.Transparent, 
            BorderBrush = new SolidColorBrush(Colors.Green),
            
            Child = new Image {
               
                Source = new Bitmap(AssetLoader.Open(new Uri($"avares://{item.Img}"))), 
                Stretch = Stretch.Uniform,
                Margin = new Thickness(4) 
            
            }
            
        };
        ToolTip.SetTip(btn, $"{item.Name}\nNiveau: {item.Rarity}\nDesc: {item.Description}");
        
        btn.PointerReleased += (s, e) => {
            if (e.InitialPressMouseButton == MouseButton.Left) {
                OnItemClick(item,btn);
            } 
        };
        border.Child = btn;
        return border;
    }


    private Border _selectedItemBorder = new Border();
    private void OnItemClick(Item item, Border btn) {
        ItemSelected = item;
        
        _selectedItemBorder.BorderThickness = new Thickness(0);
        btn.BorderThickness = new Thickness(2);
        _selectedItemBorder = btn;
        
        for (int i = 0; i < 8; i++) {
            int row = i / 4;
            int col = i % 4;
            
            var heroSelect = HeroItemSelection.Children
                .OfType<Border>()
                .FirstOrDefault(b => Grid.GetRow(b) == row && Grid.GetColumn(b) == col);
            heroSelect.Child = null;

            if (i < Guild.GuildHeroes.Count) {
                heroSelect.Child = CreateHeroSelectCube(Guild.GuildHeroes[i],item);
            }
        }

        if (item is Usable) {
            EquipUse.Content = "Use";
        }else{
            EquipUse.Content = "Equip";
        }

        EquipUse.IsVisible = true;

    }

    private Button _heroSelectButton = new Button();

    private Border CreateHeroSelectCube(Hero hero, Item item) {
        bool hasSpace = hero.EquipmentSlot > hero.EquipmentList.Count;
        if (item is Usable) {
            hasSpace = true;
        }
        
        var button = new Button {
            Width = 70,
            Height = 70,
            Padding = new Thickness(0),
            Background = Brushes.Transparent,
            BorderBrush = new SolidColorBrush(0xFF444444),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(4),
            Cursor = hasSpace ? new Cursor(StandardCursorType.Hand) : new Cursor(StandardCursorType.No),
            IsEnabled = hasSpace
        };
    
        var image = new Image {
            Source = new Bitmap(AssetLoader.Open(new Uri($"avares://{hero.Race.Img}"))),
            Stretch = Stretch.Uniform,
            Opacity = hasSpace ? 1.0 : 0.3
        };
    
        button.Content = image;
        ToolTip.SetTip(button, hero.Name);
    
        if (hasSpace) {
            button.Click += (sender, e) => {
                HeroSelectedForItem = hero;
            
                _heroSelectButton.BorderBrush = new SolidColorBrush(0xFF444444);
                _heroSelectButton.BorderThickness = new Thickness(2);
                _heroSelectButton = button;
            
                button.BorderBrush = Brushes.Green;
                button.BorderThickness = new Thickness(2);
            };
        }
        var border = new Border {
            Child = button,
            Background = hasSpace ? Brushes.Transparent : new SolidColorBrush(0x66000000)
        };
    
        return border;
    }
    
     private void EquipUse_OnClick(object? sender, RoutedEventArgs e) {
         if (ItemSelected is not null && HeroSelectedForItem is not null) {
             if (ItemSelected is Usable usable) {
                 Guild.UseUsable(usable, HeroSelectedForItem);
             }else if (ItemSelected is Equipment equip) {
                 Guild.EquipEquipment(equip,HeroSelectedForItem);
             }

             ItemSelected = null;
             HeroSelectedForItem = null;
         }
         
         EquipUse.IsVisible = false;
         UpdateInventoryInfo(); 
         UpdateCharacterInfo();

         foreach (var border in HeroItemSelection.Children.OfType<Border>()) {
             border.Child = null;
         }
     }




    private StackPanel CreateHeroCard(Hero hero) {
        var panel = new StackPanel {
            Orientation = Orientation.Horizontal,
            Background = new SolidColorBrush(0xFF2b2b2b),
            Margin = new Thickness(5)
        };
        
        var imageBorder = new Border {
            Width = 80,
            Height = 120,
            CornerRadius = new CornerRadius(4),
            Margin = new Thickness(0, 0, 10, 0)
        };
        
        var image = new Image {
            Source = new Bitmap(AssetLoader.Open(new Uri($"avares://{hero.Race.Img}"))),
            Stretch = Stretch.Uniform
        };
        imageBorder.Child = image;
        panel.Children.Add(imageBorder);
        
        var infoPanel = new StackPanel {
            Orientation = Orientation.Vertical,
            Spacing = 4,
            Width = 200
        };
        
        var nameText = new TextBlock {
            Text = hero.Name,
            FontSize = 16,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        };
        infoPanel.Children.Add(nameText);
        
        var classRaceText = new TextBlock {
            FontSize = 12,
            Foreground = new SolidColorBrush(0xFFaaaaaa)
        };
        classRaceText.Inlines.Add(new Run(hero.GameClass.Name) { 
            Foreground = new SolidColorBrush(0xFF4a9eff) 
        });
        classRaceText.Inlines.Add(new Run(" - "));
        classRaceText.Inlines.Add(new Run(hero.Race.Name) { 
            Foreground = new SolidColorBrush(0xFF90ee90) 
        });
        infoPanel.Children.Add(classRaceText);
        
        var levelXpText = new TextBlock {
            FontSize = 11,
            Foreground = new SolidColorBrush(0xFFffd700)
        };
        levelXpText.Inlines.Add(new Run("LVL "));
        levelXpText.Inlines.Add(new Run(hero.Level.ToString()) { FontWeight = FontWeight.Bold });
        levelXpText.Inlines.Add(new Run(" - XP: "));
        levelXpText.Inlines.Add(new Run($"{hero.Xp}/{hero.XpMax}"));
        infoPanel.Children.Add(levelXpText);
        
        var hpPanel = CreateStatBar("HP", hero.Hp, hero.HpMax, 0xFFff6b6b);
        infoPanel.Children.Add(hpPanel);
        
        var foodPanel = CreateStatBar("Food", hero.Food, hero.FoodMax, 0xFFffa500);
        infoPanel.Children.Add(foodPanel);
        
        var salaryText = new TextBlock {
            FontSize = 11,
            Foreground = new SolidColorBrush(0xFFffeb3b)
        };
        salaryText.Inlines.Add(new Run("💰 Salary: "));
        salaryText.Inlines.Add(new Run(hero.Salary.ToString()) { FontWeight = FontWeight.Bold });
        salaryText.Inlines.Add(new Run(" gold/day"));
        infoPanel.Children.Add(salaryText);
        
        
            var effectsContainer = new StackPanel { Spacing = 2 , Orientation = Orientation.Horizontal , VerticalAlignment = VerticalAlignment.Center};
            effectsContainer.Children.Add(new TextBlock {
                Text = "Effets:",
                FontSize = 10,
                Foreground = new SolidColorBrush(0xFFbbbbbb)
            });
            if (hero.StatusList.Count > 0) {
                var effectsPanel = new StackPanel {
                    Orientation = Orientation.Horizontal,
                    Spacing = 3
                };
                
                foreach (var status in hero.StatusList) {
                    SolidColorBrush colorBrush = new SolidColorBrush(0xFF4caf50);
                    if (status is INegativeStatus) {
                        colorBrush = new SolidColorBrush(0xFF800000);
                    }
                    var effectBadge = new Border {
                        Background = colorBrush,
                        Padding = new Thickness(4, 2),
                        CornerRadius = new CornerRadius(3)
                    };
                    effectBadge.Child = new TextBlock {
                        Text = status.Name,
                        FontSize = 9,
                        Foreground = Brushes.White
                    };
                    effectsPanel.Children.Add(effectBadge);
                    
                    ToolTip.SetTip(effectBadge, $"Expire in: {status.ExpirationIn} tours");
                }
            effectsContainer.Children.Add(effectsPanel);
           
            }
            infoPanel.Children.Add(effectsContainer);
        
        
        panel.Children.Add(infoPanel);
        var deleteButton = new Button {
            Width = 30,
            Height = 30,
            CornerRadius = new CornerRadius(15),
            Background = new SolidColorBrush(0xFFdc3545),
            Foreground = Brushes.White,
            Content = "x",
            FontSize = 18,
            FontWeight = FontWeight.Bold,   
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 0, 5, 0),
            Cursor = new Cursor(StandardCursorType.Hand)
        };

        deleteButton.Click += (sender, e) => {
            Guild.GuildHeroes.Remove(hero);
            UpdateCharacterInfo();
        };
        deleteButton.PointerEntered += (sender, e) => {
            deleteButton.Background = new SolidColorBrush(0xFFff4444);
        };
        
        deleteButton.PointerExited += (sender, e) => {
            deleteButton.Background = new SolidColorBrush(0xFFdc3545);
        };
        panel.Children.Add(deleteButton);


        string heroEquipementsTip = "";
        foreach (var equip in hero.EquipmentList) {
            heroEquipementsTip += $"\n[{equip.Name} ; Durability : {equip.Durability}/{equip.DurabilityMax} ; Description : {equip.Description[Range.EndAt(25)]}...]";
        }

        string contentTip = $"Slot : {hero.EquipmentList.Count}/{hero.EquipmentSlot}\n" +
                            $"Equipment : {heroEquipementsTip}";
        
        
        ToolTip.SetTip(imageBorder,contentTip);
        return panel;
    }

    private StackPanel CreateStatBar(string label, int current, int max, uint color) {
        var container = new StackPanel { Spacing = 2 };
        
        var labelText = new TextBlock {
            Text = $"{label}: {current}/{max}",
            FontSize = 11,
            Foreground = new SolidColorBrush(color)
        };
        container.Children.Add(labelText);
        
        var barBackground = new Border {
            Height = 6,
            Background = new SolidColorBrush(0xFF333333),
            CornerRadius = new CornerRadius(3)
        };
        
        double percentage = max > 0 ? (double)current / max : 0;
        var barFill = new Border {
            Width = 200 * percentage,
            HorizontalAlignment = HorizontalAlignment.Left,
            Background = new SolidColorBrush(color),
            CornerRadius = new CornerRadius(3)
        };
        
        barBackground.Child = barFill;
        container.Children.Add(barBackground);
        
        return container;
}

   
}