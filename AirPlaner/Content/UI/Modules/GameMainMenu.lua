local mainMenu = ui.CreateWindow();
mainMenu.Width = ui.GetScreenWidth() / 5;
mainMenu.Height = ui.GetScreenHeight() / 2;
mainMenu.Top = ui.GetScreenHeight() / 2 - mainMenu.Height / 2;
mainMenu.Left = ui.GetScreenWidth() / 2 - mainMenu.Width / 2;
mainMenu.Text = "Mainmenu";
mainMenu.IconVisible = false;
mainMenu.CloseButtonVisible = false;
mainMenu.Movable = false;
mainMenu.Resizable = false;

context.MainMenu = mainMenu;
ui.AddToManager(mainMenu);

local padding = 10;
local btnHeight = mainMenu.OriginHeight / 7;

local btnBackToGame = ui.CreateImageButton();
btnBackToGame.Image = ui.TextureFromFile("Content/AirlinerGame/MainMenu/return.png");
btnBackToGame.Width = mainMenu.ClientWidth;
btnBackToGame.Height = btnHeight;
btnBackToGame.Top = 0;
btnBackToGame.Left = 0;
btnBackToGame.Text = "Back To Game";
ui.SetCallMethod(btnBackToGame, context, "MenuResumeGameButtonOnClick");
mainMenu.Add(btnBackToGame);

local btnSaveGame = ui.CreateImageButton();
btnSaveGame.Image = ui.TextureFromFile("Content/AirlinerGame/MainMenu/save.png");
btnSaveGame.Width = mainMenu.OriginWidth - 15;
btnSaveGame.Height = btnHeight;
btnSaveGame.Top = btnBackToGame.Top + btnBackToGame.Height;
btnSaveGame.Left = 0;
btnSaveGame.Text = "Save Game";
ui.SetCallMethod(btnSaveGame, context, "MenuSaveGameButtonOnClick");
mainMenu.Add(btnSaveGame);

local btnSaveGameAs = ui.CreateImageButton();
btnSaveGameAs.Image = ui.TextureFromFile("Content/AirlinerGame/MainMenu/save.png");
btnSaveGameAs.Width = mainMenu.OriginWidth - 15;
btnSaveGameAs.Height = btnHeight;
btnSaveGameAs.Top = btnSaveGame.Top + btnSaveGame.Height;
btnSaveGameAs.Left = 0;
btnSaveGameAs.Text = "Save Game As...";
ui.SetCallMethod(btnSaveGameAs, context, "MenuSaveGameAsButtonOnClick");
mainMenu.Add(btnSaveGameAs);

local btnSettings = ui.CreateImageButton();
btnSettings.Image = ui.TextureFromFile("Content/AirlinerGame/MainMenu/settings.png");
btnSettings.Width = mainMenu.OriginWidth - 15;
btnSettings.Height = btnHeight;
btnSettings.Top = btnSaveGameAs.Top + btnSaveGameAs.Height;
btnSettings.Left = 0;
btnSettings.Text = "Game Settings";
mainMenu.Add(btnSettings);

local btnExitGame = ui.CreateImageButton();
btnExitGame.Image = ui.TextureFromFile("Content/AirlinerGame/MainMenu/close.png");
btnExitGame.Width = mainMenu.OriginWidth - 15;
btnExitGame.Height = btnHeight;
btnExitGame.Top = btnSettings.Top + btnSettings.Height;
btnExitGame.Left = 0;
btnExitGame.Text = "Return to Desktop";
mainMenu.Add(btnExitGame);

local btnSaveAndExit = ui.CreateImageButton();
btnSaveAndExit.Image = ui.TextureFromFile("Content/AirlinerGame/MainMenu/standby.png");
btnSaveAndExit.Width = mainMenu.OriginWidth - 15;
btnSaveAndExit.Height = btnHeight;
btnSaveAndExit.Top = btnExitGame.Top + btnExitGame.Height;
btnSaveAndExit.Left = 0;
btnSaveAndExit.Text = "Save & Exit Game";
mainMenu.Add(btnSaveAndExit);