local playerPanel = ui.CreateGroupPanel();
playerPanel.Width = ui.GetScreenWidth() / 2 - 50;
playerPanel.Height = 300;
playerPanel.Top = 20;
playerPanel.Left = 40;

playerPanel.Text = ui.GetText("createGameUserSettingsCaption");

ui.SetColor("CornflowerBlue", playerPanel);

local airlinePanel = ui.CreateGroupPanel();
airlinePanel.Width = playerPanel.Width;
airlinePanel.Height = 300;
airlinePanel.Top = 20;
airlinePanel.Left = playerPanel.Left + playerPanel.Width + 20;

airlinePanel.Text = ui.GetText("createGameAirlineSettingsCaption");

ui.SetColor("CornflowerBlue", airlinePanel);

ui.AddToManager(playerPanel);
ui.AddToManager(airlinePanel);