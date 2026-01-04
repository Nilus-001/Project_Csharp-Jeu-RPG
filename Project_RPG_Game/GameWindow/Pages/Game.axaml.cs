using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Project_RPG_Game.status.custom;


namespace Project_RPG_Game.GameWindow.Pages;

public partial class Game : UserControl {
    private GameWindow Main;
    public Inventory Inventory;
    public Shop Shop;
    public Resume Resume;
    public Guild Guild ;
    
    
        
    public int Day;
    public int PhaseNum;
    public int RoadNum;

    public Game() {
        InitializeComponent();
    }
    public Game(Guild guild) {
        InitializeComponent();
        Guild = guild;
        
        
        Inventory = new Inventory(Guild,this);
        Shop =  new Shop(Guild,this);
        Resume = new Resume(Guild, this);
        
        
        Day = 0;
        PhaseNum = 0;
        RoadNum = 0;
        
        //------------ Start ------------------------------------------


        UpdateAll();
        OpenPart(Resume);
        
        
        
        //------------ Start ------------------------------------------
        
        
        
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
    
    

    public void UpdateMoney() {
        MoneyPrinter.Text = Guild.Money.ToString();
    }
    public void UpdateFoodStock() {
        FoodStockPrinter.Text = Guild.FoodStock.ToString();
    }
    public void AddDay() {
        Day += 1;
        string content = "Day ";
        if (Day < 10) {
            content += "0";
        }
        DayPrinter.Content = content + Day;
    }

    public void NextPhase() {
        string[] phases = ["Sunset","Day","Night"];
        PhaseNum++;
        if (PhaseNum > phases.Length-1) {
            PhaseNum = 0;
        }
        Background = new ImageBrush {
            Source = new Bitmap(AssetLoader.Open(new Uri($"avares://Project_RPG_Game/assets/background/Background{phases[PhaseNum]}.png")))
        }; 
        

    }
    public void OpenPart(UserControl page) {
        InteractivePart.Content = page;
    }
    

    private void OnKeyDown(object sender, KeyEventArgs e) {
        if (e.Key == Key.E) {
            OpenInventory_OnClick(null, null);
        }
    }
    
    private void OpenInventory_OnClick(object? sender, RoutedEventArgs e) {
        Main.OpenOverlay(Inventory);
    }

    public void UpdateAll() {
        UpdateMoney();
        UpdateFoodStock();
        Inventory.UpdateCharacterInfo();
        Inventory.UpdateInventoryInfo();
        UpdateMission();
    }


    private void ToNext_OnClick(object? sender, RoutedEventArgs e) {
        if (Day == 0) {
            ToNext.Content = "Next";
        }

        RoadNum++;
        switch (RoadNum) {
            case 1 :
                OpenPart(Shop); 
                Shop.RefreshShop(0); // hero
                PartInfo.Text = "Recruitment";
                break;
            case 2 :
                Shop.RefreshShop(1); // items
                PartInfo.Text = "Item Shop";
                break;
            case 3 :
                PartInfo.Text = "Resume";
                OpenPart(Resume);
                if(Day == 0){
                    Resume.RAS();
                    break;
                } 
                Resume.ResumeMission();
                break;
            case 4 :
                Resume.Distribution(); //TODO : modif
                UpdateMoney();
                UpdateFoodStock();
                PartInfo.Text = "Distribution";
                break;
            case 5 :
                OpenPart(Shop); 
                Shop.RefreshShop(2); // Mission
                PartInfo.Text = "Mission Prop.";
                break;
            case 6 :
                NextPhase();
                OpenPart(Resume);
                Resume.ResumeEvent();
                PartInfo.Text = "! Event !";
                break;
            case 7 :
                NextPhase();
                Resume.ResumeMission();
                PartInfo.Text = "Resume";
                break;
            case 8 :
                OpenPart(Shop);
                Shop.RefreshShop(2); // Mission
                PartInfo.Text = "Mission Prop.";
                break;
            case 9:
                NextPhase();
                AddDay();
                
                RoadNum = 0;
                //
                int starvingCount = 0;
                foreach (var hero in Guild.GuildHeroes){
                    // hero.ModifyFood(-10);
                    hero.AppliedAllStatus();
                    if (hero.StatusIsActive(hero, typeof(Starving)) is Starving) {
                        starvingCount++;
                    }
                }
                //
                Inventory.UpdateCharacterInfo();
                PartInfo.Text = "You Survive !";
                
                
                if (Day >= 31) { //? --------------------------------------------- WIN ----------------------------------------
                    var win = new EndPanel();
                    Main.OpenOverlay(win);
                    win.Win();
                }else if (Guild.FoodStock <= 0 && starvingCount >= 3) {
                    var end = new EndPanel();
                    Main.OpenOverlay(end);
                    end.Lose("Everyone died of starvation: no more food.");
                    
                }else if (Guild.Money <= 0) {
                    Guild.Remove1Life();
                    if (Guild.Life <= 0) {
                        var end = new EndPanel();
                        Main.OpenOverlay(end);
                        end.Lose("You have too much debt. Your guild is disbanded.");
                    }
                }else if (Guild.GuildHeroes.Count == 0) {
                    var end = new EndPanel();
                    Main.OpenOverlay(end);
                    end.Lose("Everyone is dead ... The guild chose to die too.");
                }
                
                OpenPart(Resume);
                Resume.NewDay();
                break;
        }

    }
    
    public void UpdateMission() {
        InfoGuildPhase.Children.Clear();
        
        StackPanel mainContent = new StackPanel {
            Margin = new Thickness(20),
            Spacing = 10
        };
        
        TextBlock titleBlock = new TextBlock {
            Text = "📋 Missions",
            FontSize = 12,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.Black,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 15)
        };
        mainContent.Children.Add(titleBlock);

        foreach (var mission in Guild.GuildActiveMissions) {
            Border missionCard = new Border {
                Background = new SolidColorBrush(Color.FromRgb(70, 70, 70)),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(7, 5),
                BorderBrush = Shop.GetDifficultyBorderBrush(mission.Difficulty),
                BorderThickness = new Thickness(3),
                Margin = new Thickness(0, 5, 0, 5),
                BoxShadow = new BoxShadows(new BoxShadow {
                    Blur = 15,
                    Color = Shop.GetDifficultyColor(mission.Difficulty),
                    OffsetY = 3
                })
            };
            string tip = "Hero :";
            foreach (var hero in mission.ActiveHeroes) {
                tip += $"\n{hero.Name}";
            }
            ToolTip.SetTip(missionCard,tip);
            
            StackPanel missionContent = new StackPanel {
                Spacing = 5
            };

            TextBlock missionName = new TextBlock {
                Text = mission.Name,
                FontSize = 10,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Border difficultyBadge = new Border {
                Background =Shop.GetDifficultyBorderBrush(mission.Difficulty),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(5, 2),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            TextBlock difficultyText = new TextBlock {
                Text = "★ " + mission.Difficulty.ToString().ToUpper() + " ★",
                FontSize = 12,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.Black
            };
            difficultyBadge.Child = difficultyText;

            StackPanel timerPanel = new StackPanel {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 5
            };

            string time = mission.ExecutionTimer == 1 ? "Next" : mission.ExecutionTimer.ToString();
            TextBlock timerLabel = new TextBlock {
                Text = "Time:",
                FontSize = 12,
                Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
                FontWeight = FontWeight.Bold
            };

            TextBlock timerValue = new TextBlock {
                Text = time,
                FontSize = 12,
                Foreground = Brushes.Cyan,
                FontWeight = FontWeight.Bold
            };

            timerPanel.Children.Add(timerLabel);
            timerPanel.Children.Add(timerValue);

            missionContent.Children.Add(missionName);
            missionContent.Children.Add(difficultyBadge);
            missionContent.Children.Add(timerPanel);

            missionCard.Child = missionContent;
            mainContent.Children.Add(missionCard);
        }

        InfoGuildPhase.Children.Add(mainContent);

    }
    
    
    
}