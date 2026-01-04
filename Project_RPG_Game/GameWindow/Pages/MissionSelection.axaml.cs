using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Project_RPG_Game.characters;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.GameWindow.Pages;

public partial class MissionSelection : UserControl {
    public Mission Mission;
    public Border MissionPrinter;
    public Shop Shop;
    public MissionSelection() {
        InitializeComponent();
    }
    public MissionSelection(Mission mission, Shop shop , Border card) {
        InitializeComponent();
        Mission = mission;
        Shop = shop;
        MissionPrinter = card;

    }

    public void PrintMissionSelection() {
        
        StackPanel infoContent = new StackPanel {
            Margin = new Thickness(20),
            Spacing = 15
        };

        // Nom de la mission
        TextBlock nameText = new TextBlock {
            Text = Mission.Name,
            FontSize = 24,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 10)
        };

        // Description
        TextBlock descriptionText = new TextBlock {
            Text = Mission.Description,
            FontSize = 14,
            Foreground = Brushes.White,
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 0, 10)
        };

        // Badge de difficulté
        Border difficultyBadge = new Border {
            Background = new SolidColorBrush(Shop.GetDifficultyColor(Mission.Difficulty)),
            CornerRadius = new CornerRadius(15),
            Padding = new Thickness(10, 5),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 10)
        };
        
        TextBlock difficultyText = new TextBlock {
            Text = "★ " + Mission.Difficulty.ToString().ToUpper() + " ★",
            FontSize = 16,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        };
        difficultyBadge.Child = difficultyText;

        // Type et TerrainType
        StackPanel typePanel = new StackPanel {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 14,
            Margin = new Thickness(0, 0, 0, 10)
        };

        Border typeBadge = CreateInfoBadge(Mission.Type.ToString(), Color.FromRgb(70, 130, 180));
        Border terrainBadge = CreateInfoBadge(Mission.TerrainType.ToString(), Color.FromRgb(130, 180, 70));
        
        typePanel.Children.Add(typeBadge);
        typePanel.Children.Add(terrainBadge);

        // ExecutionTimer
        TextBlock timerText = new TextBlock {
            Text = "Time: " + Mission.ExecutionTimer + " cycle",
            FontSize = 16,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.Cyan,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        infoContent.Children.Add(nameText);
        infoContent.Children.Add(descriptionText);
        infoContent.Children.Add(difficultyBadge);
        infoContent.Children.Add(typePanel);
        infoContent.Children.Add(timerText);

        MissionInfo.Child = infoContent;

        StackPanel interactionContent = new StackPanel {
            Margin = new Thickness(20),
            Spacing = 15
        };

        TextBlock counterText = new TextBlock {
            Text = $"Heroes: {Mission.ActiveHeroes.Count} / {Mission.NbHero}",
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 20)
        };

        ScrollViewer heroScrollViewer = new ScrollViewer {
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            MaxHeight = 250
        };

        StackPanel heroButtonsPanel = new StackPanel {
            Spacing = 10,
            HorizontalAlignment = HorizontalAlignment.Center,
            
            
        };

        foreach (var hero in Shop.Guild.GuildHeroes) {
            bool isBusy = false ;
            foreach (var mission in Shop.Guild.GuildActiveMissions) {
                if (mission.ActiveHeroes.Contains(hero)) {
                    isBusy = true;
                    break;
                }
            }
            if (!isBusy) {
                Button heroButton = new Button {
                    Content = hero.Name,
                    Width = 200,
                    Height = 40,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Background = new SolidColorBrush(HeroColorRaceBonus(hero)),
                    Foreground = Brushes.White,
                    FontWeight = FontWeight.Bold,
                    FontSize = 14,
                    Padding = new Thickness(10),
                    CornerRadius = new CornerRadius(8),
                    BorderBrush = new SolidColorBrush(HeroColorGameClassBonus(hero)),
                    BorderThickness = new Thickness(3),
                    Cursor = new Cursor(StandardCursorType.Hand)
                };

                heroButton.Click += (s, e) => ToggleHeroSelection(hero, heroButton, counterText);

                heroButtonsPanel.Children.Add(heroButton);
            }
        }

        heroScrollViewer.Content = heroButtonsPanel;

        interactionContent.Children.Add(counterText);
        interactionContent.Children.Add(heroScrollViewer);

        MissionInteraction.Child = interactionContent;
    }

    private Border CreateInfoBadge(string text, Color color) {
        Border badge = new Border {
            Background = new SolidColorBrush(color),
            CornerRadius = new CornerRadius(12),
            Padding = new Thickness(10, 4)
        };

        TextBlock badgeText = new TextBlock {
            Text = text,
            FontSize = 10,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        };

        badge.Child = badgeText;
        return badge;
    }

    
        

    private void ToggleHeroSelection(Hero hero, Button button, TextBlock counterText) {
        if (Mission.ActiveHeroes.Contains(hero)) {
            Mission.ActiveHeroes.Remove(hero);
            
            button.Background = new SolidColorBrush(HeroColorRaceBonus(hero));
            button.BorderBrush = new SolidColorBrush(HeroColorGameClassBonus(hero));
            
            counterText.Text = $"Heroes: {Mission.ActiveHeroes.Count} / {Mission.NbHero}";
            counterText.Foreground = Brushes.White;
        }
        else if (Mission.SetActiveHero(hero)) {
            button.Background = new SolidColorBrush(Color.FromRgb(50,50,150));
            button.BorderBrush = Brushes.Blue;

            counterText.Text = $"Heroes: {Mission.ActiveHeroes.Count} / {Mission.NbHero}";
            if (Mission.ActiveHeroes.Count == Mission.NbHero) {
                counterText.Foreground = Brushes.CadetBlue;
            }
        }
    }

    private Color HeroColorRaceBonus(Hero hero) {
        int colorValue = 0;
        foreach (var terrain in hero.Race.TerrainModifier) {
            if (Mission.TerrainType == terrain.Key) {
                if (terrain.Value > 0) {
                    colorValue = 1;
                } else {
                    colorValue = -1;
                }
            }
        }
        
        return colorValue switch {
            1 => Color.FromRgb(0, 150, 0),
            -1 => Color.FromRgb(200, 0, 10),
            _ => Color.FromRgb(100, 100, 100)
        };
    }
    private Color HeroColorGameClassBonus(Hero hero) {
        int colorValue = 0;
        foreach (var type in hero.GameClass.Types) {
            if (Mission.Type == type) {
                colorValue = 1;
            }
        }
        return colorValue switch {
            1 => Color.FromRgb(100, 200, 0),
            _ => Color.FromRgb(50, 50, 50)
        };
    }

    private void Accept_OnClick(object? sender, RoutedEventArgs e) {
        if (Mission.ActiveHeroes.Count != 0) {
            Shop.GamePage.OpenPart(Shop);
            Shop.Guild.AddMission(Mission);
            Shop.GamePage.UpdateMission();
            MissionPrinter.Child = null;
            MissionPrinter.Background = new SolidColorBrush(0xFF3E3E3E);
            MissionPrinter.BorderBrush = new SolidColorBrush(0xFF1C1C1C);
            MissionPrinter.BoxShadow = new BoxShadows(new BoxShadow {
                Blur = 0,
                Color = Colors.Transparent
            });
            MissionPrinter.Child = new TextBlock {
                Text = "Accepted",
                Foreground = Brushes.Gray,
                FontWeight = FontWeight.Bold,
                FontSize = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Shop.GamePage.ToNext.IsVisible = true;
        }
        
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e) {
        Shop.GamePage.OpenPart(Shop);
        Shop.GamePage.ToNext.IsVisible = true;
    }
}