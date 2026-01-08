using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Project_RPG_Game.characters;
using Project_RPG_Game.generator;
using Project_RPG_Game.missions;

namespace Project_RPG_Game.GameWindow.Pages;

public partial class Resume : UserControl {
    public Guild Guild;
    public Game GamePage;
    
    
    public Resume() {
        InitializeComponent();
    }
    public Resume(Guild guild, Game gamePage) {
        InitializeComponent();

        Guild = guild;
        GamePage = gamePage;
    }

    public void RAS() {
        ResumeContent.Children.Clear();
    
        StackPanel mainContent = new StackPanel {
            Margin = new Thickness(20),
            Spacing = 20,
            VerticalAlignment = VerticalAlignment.Center
        };

        Border rasContainer = new Border {
            Background = new SolidColorBrush(Color.FromArgb(150, 40, 60, 40)),
            CornerRadius = new CornerRadius(15),
            Padding = new Thickness(40, 30),
            BorderBrush = new SolidColorBrush(Color.FromRgb(100, 200, 100)),
            BorderThickness = new Thickness(3),
            HorizontalAlignment = HorizontalAlignment.Center,
            BoxShadow = new BoxShadows(new BoxShadow {
                Blur = 20,
                Color = Color.FromArgb(100, 100, 200, 100),
                OffsetY = 5
            })
        };

        StackPanel rasContent = new StackPanel {
            Spacing = 15
        };

        TextBlock rasTitle = new TextBlock {
            Text = "✓ Nothing Happened ✓",
            FontSize = 36,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.FromRgb(150, 255, 150)),
            HorizontalAlignment = HorizontalAlignment.Center
        };

        TextBlock rasSubtitle = new TextBlock {
            Text = "All is calm and peaceful...",
            FontSize = 16,
            Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
            HorizontalAlignment = HorizontalAlignment.Center,
            FontStyle = FontStyle.Italic
        };

        rasContent.Children.Add(rasTitle);
        rasContent.Children.Add(rasSubtitle);
        rasContainer.Child = rasContent;

        mainContent.Children.Add(rasContainer);
        ResumeContent.Children.Add(mainContent);
    }

    public void NewDay() {
        ResumeContent.Children.Clear();
        var DAY = new TextBlock {
            Text = $"DAY {GamePage.Day}",
            FontSize = 80,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Colors.Black),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            
        };
        ResumeContent.Children.Add(DAY);
    }

    public void Distribution() {
        int totalPaid = 5;
        int totalFood = 0;
        foreach (var hero in Guild.GuildHeroes) { //TODO : System de Selection -> nb food for each heroes
            
            Guild.ModifyMoney(-hero.Salary);
            totalPaid += hero.Salary;
            
            int food = hero.FoodMax - hero.Food;
            
            if (food <= Guild.FoodStock) {
                hero.ModifyFood(food);
                Guild.ModifyFoodStock(-food);
                totalFood += food;
            }
            else {
                hero.ModifyFood(Guild.FoodStock);
                Guild.ModifyFoodStock(-food);
                totalFood += Guild.FoodStock;
            }
        }
        GamePage.UpdateAll();
        ResumeContent.Children.Clear();

        StackPanel mainContent = new StackPanel {
            Margin = new Thickness(20),
            Spacing = 20
        };

        TextBlock titleBlock = new TextBlock {
            Text = "Daily Distribution",
            FontSize = 28,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 15)
        };

        mainContent.Children.Add(titleBlock);

        // === CONTAINER PRINCIPAL ===
        Border distributionContainer = new Border {
            Background = new SolidColorBrush(Color.FromArgb(150, 30, 30, 30)),
            CornerRadius = new CornerRadius(15),
            Padding = new Thickness(20),
            BorderBrush = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
            BorderThickness = new Thickness(2)
        };

        StackPanel distributionContent = new StackPanel {
            Spacing = 15
        };

        TextBlock sectionTitle = new TextBlock {
            Text = "═══ Daily Distribution ═══",
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.FromRgb(255, 200, 100)),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 15)
        };
        distributionContent.Children.Add(sectionTitle);

        Border salaryContainer = new Border {
            Background = new SolidColorBrush(Color.FromArgb(100, 255, 215, 0)),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(15, 12),
            BorderBrush = new SolidColorBrush(Color.FromRgb(255, 215, 0)),
            BorderThickness = new Thickness(2),
            Margin = new Thickness(0, 5, 0, 5)
        };

        StackPanel salaryContent = new StackPanel {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 10
        };

        TextBlock salaryLabel = new TextBlock {
            Text = "💰 Salary paid :",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180))
        };

        TextBlock salaryValue = new TextBlock {
            Text = $"{totalPaid} gold",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        };

        salaryContent.Children.Add(salaryLabel);
        salaryContent.Children.Add(salaryValue);
        salaryContainer.Child = salaryContent;
        distributionContent.Children.Add(salaryContainer);

        Border foodContainer = new Border {
            Background = new SolidColorBrush(Color.FromArgb(100, 100, 200, 100)),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(15, 12),
            BorderBrush = new SolidColorBrush(Color.FromRgb(100, 200, 100)),
            BorderThickness = new Thickness(2),
            Margin = new Thickness(0, 5, 0, 5)
        };

        StackPanel foodContent = new StackPanel {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 10
        };

        TextBlock foodLabel = new TextBlock {
            Text = "🍖 Food Distributed :",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180))
        };

        TextBlock foodValue = new TextBlock {
            Text = $"{totalFood} unit",
            FontSize = 18,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White
        };

        foodContent.Children.Add(foodLabel);
        foodContent.Children.Add(foodValue);
        foodContainer.Child = foodContent;
        distributionContent.Children.Add(foodContainer);

        distributionContainer.Child = distributionContent;
        mainContent.Children.Add(distributionContainer);

        ResumeContent.Children.Add(mainContent);

        


    }



    public void ResumeEvent() {
        
        MissionGenerator gen = new MissionGenerator(Difficulty.Win);
        Event eve = gen.EventGenerator();
        eve.SetActive(Guild);
        if (eve.ActiveHeroes.Count == 0) {
            RAS();
        }else{
            ArrayList resultInfo = eve.ExecuteResult();
            GamePage.UpdateAll();
            
            var desc = (string)resultInfo[0];
            var info = (Dictionary<object, Dictionary<ResultType, object>>)resultInfo[1];
            ResumeContent.Children.Clear();
            AddMissionInfoResult(info,desc,eve);
        }
        
    }



    public void AddMissionInfoResult(Dictionary<object, Dictionary<ResultType, object>> resultInfo, string desc, Mission mission) {
        StackPanel mainContent = new StackPanel {
            Margin = new Thickness(20),
            Spacing = 20
        };
        
        TextBlock eventName = new TextBlock {
            Text = mission.Name,
            FontSize = 28,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.White,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 10, 0, 15),
            TextWrapping = TextWrapping.Wrap
        };

        mainContent.Children.Add(eventName);

        Border descriptionContainer = new Border {
            Background = new SolidColorBrush(Color.FromArgb(150, 40, 40, 60)),
            CornerRadius = new CornerRadius(15),
            Padding = new Thickness(20),
            Margin = new Thickness(0, 0, 0, 10),
            BorderBrush = new SolidColorBrush(Color.FromRgb(80, 80, 120)),
            BorderThickness = new Thickness(2)
        };

        StackPanel descriptionContent = new StackPanel {
            Spacing = 10
        };

        TextBlock descriptionLabel = new TextBlock {
            Text = "Description",
            FontSize = 20,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.FromRgb(150, 180, 255)),
            HorizontalAlignment = HorizontalAlignment.Center
        };

        TextBlock missionDescription = new TextBlock {
            Text = mission.Description,
            FontSize = 14,
            Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220)),
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Center,
            TextAlignment = TextAlignment.Center
        };

        descriptionContent.Children.Add(descriptionLabel);
        descriptionContent.Children.Add(missionDescription);
        descriptionContainer.Child = descriptionContent;

        mainContent.Children.Add(descriptionContainer);

       

        Border resultContainer = new Border {
            Background = new SolidColorBrush(Color.FromArgb(150, 30, 30, 30)),
            CornerRadius = new CornerRadius(15),
            Padding = new Thickness(20),
            BorderBrush = new SolidColorBrush(Color.FromRgb(100, 100, 100)),
            BorderThickness = new Thickness(2)
        };

        StackPanel resultContent = new StackPanel {
            Spacing = 15
        };

        TextBlock resultTitle = new TextBlock {
            Text = "═══ Result ═══",
            FontSize = 24,
            FontWeight = FontWeight.Bold,
            Foreground = new SolidColorBrush(Color.FromRgb(255, 200, 100)),
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 10)
        };

        resultContent.Children.Add(resultTitle);

        TextBlock descriptionText = new TextBlock {
            Text = desc,
            FontSize = 16,
            Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200)),
            TextWrapping = TextWrapping.Wrap,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 20),
            TextAlignment = TextAlignment.Center
        };

        resultContent.Children.Add(descriptionText);

        foreach (var entry in resultInfo) {
            if (entry.Key is Guild guild) {
                Border guildContainer = new Border {
                    Background = new SolidColorBrush(Color.FromArgb(100, 255, 215, 0)),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(15),
                    Margin = new Thickness(0, 5, 0, 5),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(255, 215, 0)),
                    BorderThickness = new Thickness(2)
                };

                StackPanel guildContent = new StackPanel {
                    Spacing = 8
                };

                TextBlock guildTitle = new TextBlock {
                    Text = "─── Guild ───",
                    FontSize = 18,
                    FontWeight = FontWeight.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 215, 0)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                guildContent.Children.Add(guildTitle);

                foreach (var result in entry.Value) {
                    StackPanel resultRow = new StackPanel {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Spacing = 10
                    };
                    
                    TextBlock keyText = new TextBlock {
                        Text = result.Key.ToString() + ":",
                        FontSize = 14,
                        Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180)),
                        FontWeight = FontWeight.Bold
                    };

                    var textValue = result.Value.ToString();
                    if (result.Key is ResultType.GuildLoseItem) {
                        textValue = "You lost some item : check your inventory.";
                    }
                    
                    TextBlock valueText = new TextBlock {
                        Text = textValue,
                        FontSize = 14,
                        Foreground = Brushes.White,
                        FontWeight = FontWeight.Bold
                    };

                    resultRow.Children.Add(keyText);
                    resultRow.Children.Add(valueText);
                    guildContent.Children.Add(resultRow);
                }

                guildContainer.Child = guildContent;
                resultContent.Children.Add(guildContainer);
            }
            
            if (entry.Key is Hero hero) {
                Border heroContainer = new Border {
                    Background = new SolidColorBrush(Color.FromArgb(100, 100, 150, 255)),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(15),
                    Margin = new Thickness(0, 5, 0, 5),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(100, 150, 255)),
                    BorderThickness = new Thickness(2)
                };

                StackPanel heroContent = new StackPanel {
                    Spacing = 8
                };

                TextBlock heroTitle = new TextBlock {
                    Text = $"─── {hero.Name} ───",
                    FontSize = 18,
                    FontWeight = FontWeight.Bold,
                    Foreground = new SolidColorBrush(Color.FromRgb(100, 150, 255)),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                heroContent.Children.Add(heroTitle);

                foreach (var result in entry.Value) {
                    StackPanel resultRow = new StackPanel {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Spacing = 10
                    };

                    TextBlock keyText = new TextBlock {
                        Text = result.Key.ToString() + ":",
                        FontSize = 14,
                        Foreground = new SolidColorBrush(Color.FromRgb(180, 180, 180)),
                        FontWeight = FontWeight.Bold
                    };

                    TextBlock valueText = new TextBlock {
                        Text = result.Value.ToString(),
                        FontSize = 14,
                        Foreground = Brushes.White,
                        FontWeight = FontWeight.Bold
                    };

                    resultRow.Children.Add(keyText);
                    resultRow.Children.Add(valueText);
                    heroContent.Children.Add(resultRow);
                }

                heroContainer.Child = heroContent;
                resultContent.Children.Add(heroContainer);
            }
        }

        resultContainer.Child = resultContent;
        mainContent.Children.Add(resultContainer);

        ResumeContent.Children.Add(mainContent);
        }
    
    
    public void ResumeMission() {
        var missionsInfo = Guild.ExecuteMissions();
        if (missionsInfo.Count == 0) {
            RAS();
        }else{
            GamePage.UpdateAll();
            ResumeContent.Children.Clear();
            foreach (var mission in missionsInfo) {
                var desc = (string)mission.Value[0];
                var info = (Dictionary<object, Dictionary<ResultType, object>>)mission.Value[1];
                AddMissionInfoResult(info,desc,mission.Key);
            }
            
            
            
        }
    }

}