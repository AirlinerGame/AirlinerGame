local playerPanel = ui.CreateGroupPanel();
playerPanel.Width = ui.GetScreenWidth() / 2 - 50;
playerPanel.Height = 300;
playerPanel.Top = 20;
playerPanel.Left = 50;

ui.SetColor("CornflowerBlue", playerPanel);

local airlinePanel = ui.CreateGroupPanel();


ui.AddToManager(playerPanel);