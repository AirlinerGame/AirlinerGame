local sidebar = ui.CreateSideBar();
sidebar.Width = 175;
sidebar.Height = ui.GetScreenHeight();
sidebar.BackColor = ui.CreateColor("Black", 0.65);

local buttonTest = ui.CreateImageButton();
buttonTest.Image = ui.TextureFromFile("Content/AirlinerGame/home.png");
buttonTest.Height = 50;
buttonTest.Width = sidebar.OriginWidth - 2;
buttonTest.Left = 0;
buttonTest.Top = 0;
buttonTest.Text = "Dashboard";
buttonTest.TextColor = ui.GetColor("White");
buttonTest.Color = ui.CreateColor("White", 0.1);
buttonTest.HoverColor = ui.CreateColor("Red", 0.4);
sidebar.Add(buttonTest);

local financeButton = ui.CreateImageButton();
financeButton.Image = ui.TextureFromFile("Content/AirlinerGame/dollar.png");
financeButton.Height = 50;
financeButton.Width = sidebar.OriginWidth - 2;
financeButton.Left = 0;
financeButton.Top = buttonTest.Height;
financeButton.Text = "Finanzen";
financeButton.TextColor = ui.GetColor("White");
financeButton.Color = ui.CreateColor("White", 0.1);
financeButton.HoverColor = ui.CreateColor("Red", 0.4);
sidebar.Add(financeButton);

local panelTest = ui.CreateSideBar();
panelTest.Width = ui.GetScreenWidth() - sidebar.Width;
panelTest.Height = ui.GetScreenHeight() / 11;
panelTest.Left = sidebar.Width;
--panelTest.BackColor = ui.GetColor("White");

local companyLogo = ui.CreateImageBox();
companyLogo.Image = context.Data.Airline.AirlinePicture;
companyLogo.Height = panelTest.Height;
companyLogo.Width = companyLogo.Height;
companyLogo.Color = ui.GetColor("White");
ui.SetImageMode(companyLogo, "Stretched");

panelTest.Add(companyLogo);

local headerLabel = ui.CreateHeaderLabel();
headerLabel.Height = panelTest.Height / 2.5;
headerLabel.Width = panelTest.Width - headerLabel.Left - 200;
headerLabel.Left = companyLogo.Width + 20;
headerLabel.Top = panelTest.Height / 2 - headerLabel.Height / 2;
headerLabel.Text = context.Data.Airline.Name .. " Dashboard";

panelTest.Add(headerLabel);

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