using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Layout;
using Project_RPG_Game.characters;
using Project_RPG_Game.generator;
using Project_RPG_Game.items;
using System.Collections.Generic;
using System;
using Avalonia.Input;
using Project_RPG_Game.classes;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.GameWindow.Pages;

public partial class Shop : UserControl {
    private List<Hero> _heroShop = new List<Hero>();
    private List<Item> _itemShop = new List<Item>();
    private List<Mission> _missionShop = new List<Mission>();
    public Guild Guild;
    public Game GamePage;
    
    public Shop() {
        InitializeComponent();
    }

    public Shop(Guild guild, Game gamePage) {
        GamePage = gamePage;
        InitializeComponent();
        Guild = guild;
    }

    public Object GeneratebyRarity(bool isItemShop = false) {
        int random = Global.Random(0, 101);
        RarityGenerator generator = new RarityGenerator(Rarity.Classic);
        switch (random) { 
            case < 50 :
                generator.Rarity = Rarity.Common;
                break;
            case < 70:
                generator.Rarity = Rarity.Rare;
                break;
            case < 85:
                generator.Rarity = Rarity.Epic;
                break;
            case < 95:
                generator.Rarity = Rarity.Legendary;
                break;
            default:
                generator.Rarity = Rarity.Mythic;
                break;
        }

        if (isItemShop) {
            return generator.ItemGenerator();
        }
        return generator.HeroGenerator();
    }

    public Mission GenerateMission() {
        int random = Global.Random(0, 101);
        MissionGenerator generator = new MissionGenerator(Difficulty.Win);
        switch (random) { 
            case < 40 :
                generator.Difficulty = Difficulty.Easy;
                break;
            case < 70:
                generator.Difficulty = Difficulty.Normal;
                break;
            case < 90:
                generator.Difficulty = Difficulty.Hard;
                break;
            case < 97:
                generator.Difficulty = Difficulty.Gamble;
                break;
            default:
                generator.Difficulty = Difficulty.Impossible;
                break;
        }
        return generator.MissioGenerator();
    }

    public void RefreshShop(int shopType = 0) { // 0 = hero | 1 = items | 2 = missions
        ShopList.Children.Clear();
        
        if (shopType == 1) {
            _itemShop.Clear();
            for (int i = 0; i < 3; i++) {
                _itemShop.Add((Item)GeneratebyRarity(true));
            }
            PrintShop(shopType);
        } else if (shopType == 0) {
            _heroShop.Clear();
            for (int i = 0; i < 3; i++) {
                _heroShop.Add((Hero)GeneratebyRarity());
            }
            PrintShop(shopType);
        } else {
            _missionShop.Clear();
            for (int i = 0; i < 3; i++) {
                _missionShop.Add(GenerateMission());
            }
            PrintShop(shopType);
        }
        
    }

    public void PrintShop(int shopType = 0) {
        if (shopType == 1) {
            foreach (var item in _itemShop) {
                CreateItemShopCard(item);
            }
        } else if (shopType == 0) {
            foreach (var hero in _heroShop) {
                CreateHeroShopCard(hero);
            }
        } else {
            foreach (var mission in _missionShop) {
                CreateMissionShopCard(mission);
            }
        }
    }












    public void CreateMissionShopCard(Mission mission) {
    Border card = new Border {
        Width = 175,
        Height = 300,
        Margin = new Thickness(10),
        CornerRadius = new CornerRadius(15),
        Background = GetDifficultyBackground(mission.Difficulty),
        BorderBrush = GetDifficultyBorderBrush(mission.Difficulty),
        BorderThickness = new Thickness(3),
        BoxShadow = new BoxShadows(new BoxShadow {
            Blur = 20,
            Color = GetDifficultyColor(mission.Difficulty),
            OffsetY = 5
        })
    };

    StackPanel content = new StackPanel {
        Margin = new Thickness(15)
    };

    TextBlock missionName = new TextBlock {
        Text = mission.Name,
        FontSize = 16,
        FontWeight = FontWeight.Bold,
        Foreground = Brushes.White,
        HorizontalAlignment = HorizontalAlignment.Center,
        TextWrapping = TextWrapping.Wrap,
        Margin = new Thickness(0, 0, 0, 10)
    };

    Border difficultyBadge = new Border {
        Background = new SolidColorBrush(GetDifficultyColor(mission.Difficulty)),
        CornerRadius = new CornerRadius(15),
        Padding = new Thickness(10, 3),
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 0, 0, 8)
    };
    
    TextBlock difficultyText = new TextBlock {
        Text = "★ " + mission.Difficulty.ToString().ToUpper() + " ★",
        FontSize = 11,
        FontWeight = FontWeight.Bold,
        Foreground = Brushes.White
    };
    difficultyBadge.Child = difficultyText;

    StackPanel typePanel = new StackPanel {
        Orientation = Orientation.Vertical,
        HorizontalAlignment = HorizontalAlignment.Center,
        Spacing = 5,
        Margin = new Thickness(0, 0, 0, 8)
    };

    Border typeBadge = CreateInfoBadge(mission.Type.ToString(), Color.FromRgb(70, 130, 180));
    Border terrainBadge = CreateInfoBadge(mission.TerrainType.ToString(), Color.FromRgb(130, 180, 70));
    
    typePanel.Children.Add(typeBadge);
    typePanel.Children.Add(terrainBadge);

    // Info panel
    StackPanel infoPanel = new StackPanel {
        Spacing = 4,
        Margin = new Thickness(0, 0, 0, 10)
        
    };

    infoPanel.Children.Add(CreateStatRow("Time ", mission.ExecutionTimer + " cycle", Brushes.Cyan));
    infoPanel.Children.Add(CreateStatRow("Max Heroes ", mission.NbHero.ToString() , Brushes.Orange));


    // Bouton Select
    Button selectButton = new Button {
        Content = "Select",
        HorizontalAlignment = HorizontalAlignment.Stretch,
        HorizontalContentAlignment = HorizontalAlignment.Center,
        Background = new SolidColorBrush(Color.FromRgb(0, 150, 113)),
        Foreground = Brushes.White,
        FontWeight = FontWeight.Bold,
        FontSize = 14,
        Padding = new Thickness(10),
        CornerRadius = new CornerRadius(10),
        Cursor = new Cursor(StandardCursorType.Hand)
    };

    selectButton.Click += (s, e) => SelectMission(mission,card);

    content.Children.Add(missionName);
    content.Children.Add(difficultyBadge);
    content.Children.Add(typePanel);
    content.Children.Add(infoPanel);
    content.Children.Add(selectButton);

    card.Child = content;
    ShopList.Children.Add(card);
}

private IBrush GetDifficultyBackground(Difficulty difficulty) {
    var color = difficulty switch {
        Difficulty.Easy => Color.FromRgb(40, 80, 40),
        Difficulty.Normal => Color.FromRgb(80, 80, 40),
        Difficulty.Hard => Color.FromRgb(100, 50, 30),
        Difficulty.Gamble => Color.FromRgb(120, 20, 40),
        Difficulty.Impossible => Color.FromRgb(70, 0, 150),
        _ => Color.FromRgb(50, 50, 50)
    };
    return new SolidColorBrush(color);
}

public IBrush GetDifficultyBorderBrush(Difficulty difficulty) {
    return new SolidColorBrush(GetDifficultyColor(difficulty));
}

public Color GetDifficultyColor(Difficulty difficulty) {
    return difficulty switch {
        Difficulty.Easy => Color.FromRgb(100, 200, 100),
        Difficulty.Normal => Color.FromRgb(255, 200, 0),
        Difficulty.Hard => Color.FromRgb(255, 100, 50),
        Difficulty.Gamble => Color.FromRgb(120, 20, 40),
        Difficulty.Impossible => Color.FromRgb(70, 0, 150),
        _ => Color.FromRgb(100, 100, 100)
    };
}
    
private void SelectMission(Mission mission, Border card) {
    var missionSelection = new MissionSelection(mission, this,card);
    GamePage.OpenPart(missionSelection);
    missionSelection.PrintMissionSelection();
    GamePage.ToNext.IsVisible = false;
    
    //! here
}
    
    
    
    
    
    
    
    
    
    
    
    
    
    private void CreateHeroShopCard(Hero hero) {
    Border card = new Border {
        Width = 175,
        Height = 300,
        Margin = new Thickness(10),
        CornerRadius = new CornerRadius(15),
        Background = GetRarityBackground(hero.Rarity),
        BorderBrush = GetRarityBorderBrush(hero.Rarity),
        BorderThickness = new Thickness(3),
        BoxShadow = new BoxShadows(new BoxShadow {
            Blur = 20,
            Color = GetRarityColor(hero.Rarity),
            OffsetY = 5
        })
    };

    StackPanel content = new StackPanel {
        Margin = new Thickness(15)
    };

    Border imageContainer = new Border {
        Width = 90, 
        Height = 90,
        CornerRadius = new CornerRadius(10),
        Background = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0)),
        Margin = new Thickness(0, 0, 0, 10),
        ClipToBounds = true,
        HorizontalAlignment = HorizontalAlignment.Center 
    };

    Image heroImage = new Image {
        Source = new Bitmap(AssetLoader.Open(new Uri($"avares://{hero.Race.Img}"))),
        Stretch = Stretch.Uniform
    };
    imageContainer.Child = heroImage;
    

    StackPanel raceClassPanel = new StackPanel {
        Orientation = Orientation.Horizontal,
        HorizontalAlignment = HorizontalAlignment.Center,
        Spacing = 5,
        Margin = new Thickness(0, 0, 0, 8)
    };

    Border raceBadge = CreateInfoBadge(hero.Race.Name, Color.FromRgb(70, 130, 180));
    Border classBadge = CreateInfoBadge(hero.GameClass.Name, Color.FromRgb(180, 70, 130));
    
    raceClassPanel.Children.Add(raceBadge);
    raceClassPanel.Children.Add(classBadge);

    Border rarityBadge = new Border {
        Background = new SolidColorBrush(GetRarityColor(hero.Rarity)),
        CornerRadius = new CornerRadius(15),
        Padding = new Thickness(10, 3),
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 0, 0, 8)
    };
    
    TextBlock rarityText = new TextBlock {
        Text = "★ " + hero.Rarity.ToString().ToUpper() + " ★",
        FontSize = 11,
        FontWeight = FontWeight.Bold,
        Foreground = Brushes.White
    };
    rarityBadge.Child = rarityText;

    StackPanel statsPanel = new StackPanel {
        Spacing = 4,
        Margin = new Thickness(0, 0, 0, 10)
    };

    statsPanel.Children.Add(CreateStatRow("HP", hero.HpMax.ToString(), Brushes.Red));
    statsPanel.Children.Add(CreateStatRow("Food", hero.FoodMax.ToString(), Brushes.Orange));
    statsPanel.Children.Add(CreateStatRow("Sal.", hero.Salary + " G", Brushes.Gold));

    int price = CalculatePrice(hero);
    Border priceContainer = new Border {
        Background = new SolidColorBrush(Color.FromArgb(220, 255, 215, 0)),
        CornerRadius = new CornerRadius(8),
        Padding = new Thickness(5),
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Margin = new Thickness(0, 0, 0, 8)
    };

    TextBlock priceText = new TextBlock {
        Text = $"{price} Gold",
        FontSize = 16,
        FontWeight = FontWeight.Bold,
        Foreground = new SolidColorBrush(Color.FromRgb(139, 69, 19)),
        HorizontalAlignment = HorizontalAlignment.Center
    };
    priceContainer.Child = priceText;

    Button buyButton = new Button {
        Content = "Recruter",
        HorizontalAlignment = HorizontalAlignment.Stretch,
        HorizontalContentAlignment = HorizontalAlignment.Center,
        Background = new SolidColorBrush(Color.FromRgb(0,150, 113)),
        Foreground = Brushes.White,
        FontWeight = FontWeight.Bold,
        FontSize = 14,
        Padding = new Thickness(10),
        CornerRadius = new CornerRadius(10),
        Cursor = new Cursor(StandardCursorType.Hand)
    };

    buyButton.Click += (s, e) => BuyHero(hero, price, card);

    content.Children.Add(imageContainer);
    content.Children.Add(raceClassPanel);
    content.Children.Add(rarityBadge);
    content.Children.Add(statsPanel);
    content.Children.Add(priceContainer);
    content.Children.Add(buyButton);

    card.Child = content;
    ShopList.Children.Add(card);
}

    private Border CreateInfoBadge(string text, Color color) {
        Border badge = new Border {
            Background = new SolidColorBrush(color),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(10, 4)
        };

        TextBlock badgeText = new TextBlock {
            Text = text,
            FontSize = 8,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        };

        badge.Child = badgeText;
        return badge;
    }

    private StackPanel CreateStatRow(string label, string value, IBrush valueColor) {
        StackPanel row = new StackPanel {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        TextBlock labelText = new TextBlock {
            Text = label + ": ",
            FontSize = 14,
            Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220))
        };

        TextBlock valueText = new TextBlock {
            Text = value,
            FontSize = 14,
            FontWeight = FontWeight.Bold,
            Foreground = valueColor
        };

        row.Children.Add(labelText);
        row.Children.Add(valueText);
        return row;
    }
    
    private IBrush GetRarityBackground(Rarity rarity) {
        var color = rarity switch {
            Rarity.Common => Color.FromRgb(60, 60, 60),
            Rarity.Rare => Color.FromRgb(30, 60, 100),
            Rarity.Epic => Color.FromRgb(80, 40, 120),
            Rarity.Legendary => Color.FromRgb(120, 80, 20),
            Rarity.Mythic => Color.FromRgb(120, 20, 40),
            _ => Color.FromRgb(50, 50, 50)
        };
        return new SolidColorBrush(color);
    }

    private IBrush GetRarityBorderBrush(Rarity rarity) {
        return new SolidColorBrush(GetRarityColor(rarity));
    }

    private Color GetRarityColor(Rarity rarity) {
        return rarity switch {
            Rarity.Common => Color.FromRgb(158, 158, 158),
            Rarity.Rare => Color.FromRgb(94, 152, 217),
            Rarity.Epic => Color.FromRgb(163, 53, 238),
            Rarity.Legendary => Color.FromRgb(255, 128, 0),
            Rarity.Mythic => Color.FromRgb(255, 40, 90),
            _ => Color.FromRgb(100, 100, 100)
        };
    }

    private int CalculatePrice(Hero hero) {
        int basePrice = hero.Rarity switch {
            Rarity.Common => Global.Random(5,15),
            Rarity.Rare => Global.Random(10,25),
            Rarity.Epic => Global.Random(20,40),
            Rarity.Legendary => Global.Random(30,60),
            Rarity.Mythic => Global.Random(50,80),
            _ => 1
        };
        return basePrice;
    }

    private void BuyHero(Hero hero, int price, Border card) {
        if (Guild.Money >= price && Guild.AddHero(hero)) {
            Guild.ModifyMoney(-price);
            GamePage.UpdateMoney();
            GamePage.Inventory.UpdateCharacterInfo();
            card.Child = null;
            card.Background = new SolidColorBrush(0xFF3E3E3E);
            card.BorderBrush = new SolidColorBrush(0xFF1C1C1C);
            card.BoxShadow = new BoxShadows(new BoxShadow {
                Blur = 0,
                Color = Colors.Transparent
            });
            card.Child = new TextBlock {
                Text = "SOLD",
                Foreground = Brushes.Gray,
                FontWeight = FontWeight.Bold,
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

        }
    }
    
    
    
    
    
    
    
    
    
    
   private void CreateItemShopCard(Item item) {
    
    Border card = new Border {
        Width = 175,
        Height = 300,
        Margin = new Thickness(10),
        CornerRadius = new CornerRadius(15),
        Background = GetRarityBackground(item.Rarity),
        BorderBrush = GetRarityBorderBrush(item.Rarity),
        BorderThickness = new Thickness(3),
        BoxShadow = new BoxShadows(new BoxShadow {
            Blur = 20,
            Color = GetRarityColor(item.Rarity),
            OffsetY = 5
        })
    };

    StackPanel content = new StackPanel {
        Margin = new Thickness(15)
    };

   
    Border imageContainer = new Border {
        Width = 90,
        Height = 90,
        CornerRadius = new CornerRadius(10),
        Background = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0)),
        Margin = new Thickness(0, 0, 0, 10),
        ClipToBounds = true,
        HorizontalAlignment = HorizontalAlignment.Center
    };

    
    Image itemImage = new Image {
        Source = new Bitmap(AssetLoader.Open(new Uri($"avares://{item.Img}"))),
        Stretch = Stretch.Uniform,
        Margin = new Thickness(5)
    };
    
    imageContainer.Child = itemImage;

    
    bool isEquipment = item is Equipment;
    string typeName = isEquipment ? "Equipment" : "Consumable";
    Color typeColor = isEquipment ? Color.FromRgb(100, 149, 237) : Color.FromRgb(46, 204, 113); // Bleu ou Vert

    Border typeBadge = CreateInfoBadge(typeName, typeColor);
    typeBadge.HorizontalAlignment = HorizontalAlignment.Center;
    typeBadge.Margin = new Thickness(0, 0, 0, 5);

    TextBlock nameText = new TextBlock {
        Text = item.Name,
        FontSize = 14,
        FontWeight = FontWeight.Bold,
        Foreground = Brushes.White,
        HorizontalAlignment = HorizontalAlignment.Center,
        TextWrapping = TextWrapping.Wrap,
        TextAlignment = TextAlignment.Center,
        Margin = new Thickness(0, 0, 0, 5)
    };

    Border rarityBadge = new Border {
        Background = new SolidColorBrush(GetRarityColor(item.Rarity)),
        CornerRadius = new CornerRadius(15),
        Padding = new Thickness(10, 3),
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 0, 0, 8)
    };

    TextBlock rarityText = new TextBlock {
        Text = "★ " + item.Rarity.ToString().ToUpper() + " ★",
        FontSize = 10,
        FontWeight = FontWeight.Bold,
        Foreground = Brushes.White
    };
    rarityBadge.Child = rarityText;

    StackPanel infoPanel = new StackPanel {
        Spacing = 4,
        Margin = new Thickness(0, 0, 0, 8),
        Height = 50 
    };

    if (isEquipment) {
        Equipment equip = (Equipment)item; 
        infoPanel.Children.Add(CreateStatRow("Durability", $"{equip.DurabilityMax}", Brushes.LightBlue));
    }
    
    TextBlock descText = new TextBlock {
        Text = item.Description,
        FontSize = 10,
        Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
        TextWrapping = TextWrapping.Wrap,
        TextTrimming = TextTrimming.CharacterEllipsis,
        MaxLines = 3,
        TextAlignment = TextAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
    };
    infoPanel.Children.Add(descText);

    int price = CalculateItemPrice(item);
    Border priceContainer = new Border {
        Background = new SolidColorBrush(Color.FromArgb(220, 255, 215, 0)),
        CornerRadius = new CornerRadius(8),
        Padding = new Thickness(5),
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Margin = new Thickness(0, 0, 0, 8)
    };

    TextBlock priceText = new TextBlock {
        Text = $"{price} Gold",
        FontSize = 16,
        FontWeight = FontWeight.Bold,
        Foreground = new SolidColorBrush(Color.FromRgb(139, 69, 19)),
        HorizontalAlignment = HorizontalAlignment.Center
    };
    priceContainer.Child = priceText;

    Button buyButton = new Button {
        Content = "Acheter",
        HorizontalAlignment = HorizontalAlignment.Stretch,
        HorizontalContentAlignment = HorizontalAlignment.Center,
        Background = new SolidColorBrush(Color.FromRgb(0, 150, 113)),
        Foreground = Brushes.White,
        FontWeight = FontWeight.Bold,
        FontSize = 14,
        Padding = new Thickness(10),
        CornerRadius = new CornerRadius(10),
        Cursor = new Cursor(StandardCursorType.Hand)
    };

    buyButton.Click += (s, e) => BuyItem(item, price, card);

    content.Children.Add(imageContainer);
    content.Children.Add(typeBadge);
    content.Children.Add(nameText);
    content.Children.Add(rarityBadge);
    content.Children.Add(infoPanel);
    content.Children.Add(priceContainer);
    content.Children.Add(buyButton);

    card.Child = content;
    ShopList.Children.Add(card);
}


private int CalculateItemPrice(Item item) {
    int basePrice = item.Rarity switch {
        Rarity.Common => Global.Random(5,15),
        Rarity.Rare => Global.Random(10,25),
        Rarity.Epic => Global.Random(20,40),
        Rarity.Legendary => Global.Random(30,60),
        Rarity.Mythic => Global.Random(50,80),
        _ => 1
    };
    return basePrice;
}

private void BuyItem(Item item, int price, Border card) {
    if (Guild.Money >= price && Guild.AddInventory(item)) {
        Guild.ModifyMoney(-price);
        GamePage.UpdateMoney();
        GamePage.Inventory.UpdateInventoryInfo(); 
        
        card.Child = null;
        card.Background = new SolidColorBrush(0xFF3E3E3E);
        card.BorderBrush = new SolidColorBrush(0xFF1C1C1C);
        card.BoxShadow = new BoxShadows(new BoxShadow {
            Blur = 0,
            Color = Colors.Transparent
        });
        card.Child = new TextBlock {
            Text = "SOLD",
            Foreground = Brushes.Gray,
            FontWeight = FontWeight.Bold,
            FontSize = 24,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
    }
}

    
}