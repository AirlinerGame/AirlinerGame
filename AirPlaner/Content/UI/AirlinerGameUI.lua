local sidebar = ui.CreateSideBar();
sidebar.Width = 175;
sidebar.Height = ui.GetScreenHeight();
sidebar.BackColor = ui.CreateColor("Black", 0.65);

local buttonTest = ui.CreateImageButton();
buttonTest.Image = ui.TextureFromFile("Content/AirlinerGame/icon_global.png");
buttonTest.Height = 50;
buttonTest.Width = sidebar.OriginWidth - 2;
buttonTest.Left = 0;
buttonTest.Top = 0;
buttonTest.Text = "Hallo Welt";
buttonTest.TextColor = ui.GetColor("White");
buttonTest.Color = ui.CreateColor("White", 0.1);
sidebar.Add(buttonTest);

local panelTest = ui.CreateSideBar();
panelTest.Width = ui.GetScreenWidth() - sidebar.Width;
panelTest.Height = ui.GetScreenHeight() / 8;
panelTest.Left = sidebar.Width;
--panelTest.BackColor = ui.GetColor("White");

local padding = 10;
local panelWidth = (ui.GetScreenWidth() - sidebar.Width - padding * 3) / 2;
local panelHeight = (ui.GetScreenHeight() - panelTest.Height - padding * 3) / 2; 

local financePanel = ui.CreateGroupPanel();
financePanel.Height = panelHeight;
financePanel.Width = panelWidth;
financePanel.Left = sidebar.Width + padding;
financePanel.Top = panelTest.Height + padding;
financePanel.Text = "Finanzen";
financePanel.Color = ui.CreateColor("White", 0.85);

local airlinePanel = ui.CreateGroupPanel();
airlinePanel.Height = financePanel.Height;
airlinePanel.Width = financePanel.Width;
airlinePanel.Left = sidebar.Width + padding + financePanel.Width + padding;
airlinePanel.Top = panelTest.Height + padding;
airlinePanel.Text = "Routen Erfolge";
airlinePanel.Color = ui.CreateColor("White", 0.85);

local calendarPanel = ui.CreateGroupPanel();
calendarPanel.Height = financePanel.Height;
calendarPanel.Width = financePanel.Width;
calendarPanel.Left = sidebar.Width + padding;
calendarPanel.Top = panelTest.Height + padding + financePanel.Height + padding;
calendarPanel.Text = "Kalendar";
calendarPanel.Color = ui.CreateColor("White", 0.85);

local suggestionPanel = ui.CreateGroupPanel();
suggestionPanel.Height = financePanel.Height;
suggestionPanel.Width = financePanel.Width;
suggestionPanel.Left = sidebar.Width + padding + calendarPanel.Width + padding;
suggestionPanel.Top = panelTest.Height + padding + airlinePanel.Height + padding;
suggestionPanel.Text = "Empfehlung";
suggestionPanel.Color = ui.CreateColor("White", 0.85);

ui.AddToManager(sidebar);
ui.AddToManager(panelTest);
ui.AddToManager(financePanel);
ui.AddToManager(airlinePanel);
ui.AddToManager(calendarPanel);
ui.AddToManager(suggestionPanel);