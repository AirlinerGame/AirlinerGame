-- Include Main Layout
ui.Include("Content/UI/AirplanerMainTemplate.lua")

local padding = 10;
local panelWidth = (ui.GetScreenWidth() - sidebar.Width - padding * 3) / 2;
local panelHeight = (ui.GetScreenHeight() - topBar.Height - padding * 3) / 2; 

local financePanel = ui.CreateGroupPanel();
financePanel.Height = panelHeight;
financePanel.Width = panelWidth;
financePanel.Left = sidebar.Width + padding;
financePanel.Top = topBar.Height + padding;
financePanel.Text = ui.GetText("txtCashflow");
financePanel.Color = ui.CreateColor("White", 0.85);

local airlinePanel = ui.CreateGroupPanel();
airlinePanel.Height = financePanel.Height;
airlinePanel.Width = financePanel.Width;
airlinePanel.Left = sidebar.Width + padding + financePanel.Width + padding;
airlinePanel.Top = topBar.Height + padding;
airlinePanel.Text = ui.GetText("txtAirlineRouteSuccess");
airlinePanel.Color = ui.CreateColor("White", 0.85);

local calendarPanel = ui.CreateGroupPanel();
calendarPanel.Height = financePanel.Height;
calendarPanel.Width = financePanel.Width;
calendarPanel.Left = sidebar.Width + padding;
calendarPanel.Top = topBar.Height + padding + financePanel.Height + padding;
calendarPanel.Text = ui.GetText("txtCalendar");
calendarPanel.Color = ui.CreateColor("White", 0.85);

local suggestionPanel = ui.CreateGroupPanel();
suggestionPanel.Height = financePanel.Height;
suggestionPanel.Width = financePanel.Width;
suggestionPanel.Left = sidebar.Width + padding + calendarPanel.Width + padding;
suggestionPanel.Top = topBar.Height + padding + airlinePanel.Height + padding;
suggestionPanel.Text = ui.GetText("txtSuggestion");
suggestionPanel.Color = ui.CreateColor("White", 0.85);

ui.AddToManager(financePanel);
ui.AddToManager(airlinePanel);
ui.AddToManager(calendarPanel);
ui.AddToManager(suggestionPanel);