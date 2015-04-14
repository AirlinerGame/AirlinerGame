local sideBar = ui.CreateSideBar();
sideBar.Width = ui.GetScreenWidth() / 4;
sideBar.Height = ui.GetScreenHeight();

sideBar.Left = sideBar.Width * 3;

local buttonWidth = sideBar.Width / 1.5;
local startGameButton = ui.CreateButton();
startGameButton.Text = ui.GetText("menuStartGame");
startGameButton.Width = buttonWidth;
startGameButton.Left = sideBar.Width / 2 - startGameButton.Width / 2;
startGameButton.Top = sideBar.OriginHeight - 150;
ui.SetCallMethod(startGameButton, context, "StartGameButtonOnClick");

local loadGameButton = ui.CreateButton();
loadGameButton.Text = ui.GetText("menuLoadGame");
loadGameButton.Width = buttonWidth;
loadGameButton.Left = startGameButton.Left;
loadGameButton.Top = startGameButton.Top + 40;
ui.SetCallMethod(loadGameButton, context, "LoadGameButtonOnClick");

local settingsButton = ui.CreateButton();
settingsButton.Text = ui.GetText("menuSettings");
settingsButton.Width = buttonWidth;
settingsButton.Left = startGameButton.Left;
settingsButton.Top = loadGameButton.Top + 40;
ui.SetCallMethod(settingsButton, context, "SettingsButtonOnClick");

sideBar.Add(startGameButton);
sideBar.Add(loadGameButton);
sideBar.Add(settingsButton);
ui.AddToManager(sideBar);

ui.Include("Content/UI/Modules/SettingsBox.lua");
context.SettingsWindow = settingsWindow;
context.MusicVolumeTrackBar = musicVolume;
context.FxVolumeTrackBar = soundEffectVolume;