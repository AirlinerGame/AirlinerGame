-- Player/Manager Settings
local playerPanel = ui.CreateGroupPanel();
playerPanel.Width = ui.GetScreenWidth() / 2 - 50;
playerPanel.Height = ui.GetScreenHeight() / 3;
playerPanel.Top = 20;
playerPanel.Left = 40;

playerPanel.Text = ui.GetText("createGameUserSettingsCaption");
playerPanel.Color = ui.CreateColor("CornflowerBlue", 0.9);

local boxQuarter = (playerPanel.Width / 4);
local playerPicture = ui.CreateImageBox();
playerPicture.Color = ui.GetColor("White");
playerPicture.Image = context.ProfilePicture;
playerPicture.Width = boxQuarter * 0.9;
playerPicture.Height = playerPicture.Width;
playerPicture.Left = 20;
playerPicture.Top = 20;
ui.SetImageMode(playerPicture, "Stretched");
context.UserImageBox = playerPicture;

playerPanel.Add(playerPicture);

local changePictureBtn = ui.CreateButton();
changePictureBtn.Width = playerPicture.Width;
changePictureBtn.Text = ui.GetText("createGameUserSettingsChangePicture");
changePictureBtn.Left = playerPicture.Left;
changePictureBtn.Top = playerPicture.Top + playerPicture.Height + 5;
ui.SetCallMethod(changePictureBtn, context, "ChangeUserPictureButtonOnClick");
playerPanel.Add(changePictureBtn);

local playerGroupBox = ui.CreateGroupBox();
playerGroupBox.Text = ui.GetText("createGameUserSettingsGBCaption");
playerGroupBox.Left = playerPicture.Left + playerPicture.Width + 20;
playerGroupBox.Top = 10;
playerGroupBox.Width = boxQuarter * 2.5;

local firstNameLabel = ui.CreateLabel();
firstNameLabel.Text = ui.GetText("createGameUserSettingsFirstname");
firstNameLabel.Left = 10;
firstNameLabel.Top = 5;

local firstName = ui.CreateTextBox();
firstName.Left = firstNameLabel.Left;
firstName.Top = firstNameLabel.Top + 20;
firstName.Width = playerGroupBox.Width / 2.5;
firstName.Color = ui.CreateColor("LightBlue", 0.8);
firstName.Init();
firstName.Refresh();

context.Firstname = firstName;

playerGroupBox.Height = firstName.Top + firstName.Height + 40;

local lastNameLabel = ui.CreateLabel();
lastNameLabel.Text = ui.GetText("createGameUserSettingsLastname");
lastNameLabel.Left = firstName.Left + firstName.Width + 25;
lastNameLabel.Top = 5;

local lastName = ui.CreateTextBox();
lastName.Left = lastNameLabel.Left;
lastName.Top = lastNameLabel.Top + 20;
lastName.Width = firstName.Width;
lastName.Color = ui.CreateColor("LightBlue", 0.8);
lastName.Init();
lastName.Refresh();

context.Lastname = lastName;

playerGroupBox.Add(firstNameLabel);
playerGroupBox.Add(firstName);
playerGroupBox.Add(lastNameLabel);
playerGroupBox.Add(lastName);

playerPanel.Add(playerGroupBox);

-- Airline Settings
local airlinePanel = ui.CreateGroupPanel();
airlinePanel.Width = playerPanel.Width;
airlinePanel.Height = ui.GetScreenHeight() / 3;
airlinePanel.Top = 20;
airlinePanel.Left = playerPanel.Left + playerPanel.Width + 20;
airlinePanel.Color = ui.CreateColor("CornflowerBlue", 0.9);
airlinePanel.Text = ui.GetText("createGameAirlineSettingsCaption");

local airlineImage = ui.CreateImageBox();
airlineImage.Color = ui.GetColor("White");
airlineImage.Image = context.AirlinePicture;
airlineImage.Width = boxQuarter * 0.9;
airlineImage.Height = airlineImage.Width;
airlineImage.Left = 20;
airlineImage.Top = 20;
ui.SetImageMode(airlineImage, "Stretched");
context.AirlineImageBox = airlineImage;
airlinePanel.Add(airlineImage);

local airlineImageChangeBtn = ui.CreateButton();
airlineImageChangeBtn.Text = ui.GetText("createGameUserSettingsChangePicture");
airlineImageChangeBtn.Width = airlineImage.Width;
airlineImageChangeBtn.Top = airlineImage.Top + airlineImage.Height + 5;
airlineImageChangeBtn.Left = airlineImage.Left;
ui.SetCallMethod(airlineImageChangeBtn, context, "ChangeAirlinePictureButtonOnClick");
airlinePanel.Add(airlineImageChangeBtn);

local airlineGroupBox = ui.CreateGroupBox();
airlineGroupBox.Text = ui.GetText("createGameAirlineGBCaption");
airlineGroupBox.Left = airlineImage.Left + airlineImage.Width + 20;
airlineGroupBox.Top = 10;
airlineGroupBox.Width = boxQuarter * 2.5;
airlineGroupBox.Height = 100;

local airlineNameLabel = ui.CreateLabel();
airlineNameLabel.Width = airlineGroupBox.Width - 20;
airlineNameLabel.Left = 10;
airlineNameLabel.Top = 5;
airlineNameLabel.Text = ui.GetText("createGameAirlineSettingsName");

local airlineName = ui.CreateTextBox();
airlineName.Top = airlineNameLabel.Top + airlineNameLabel.Height + 5;
airlineName.Left = airlineNameLabel.Left;
airlineName.Width = firstName.Width * 1.5;
airlineName.Color = ui.CreateColor("LightBlue", 0.8);
airlineName.Init();
airlineName.Refresh();

context.AirlineName = airlineName;

airlineGroupBox.Add(airlineNameLabel);
airlineGroupBox.Add(airlineName);
airlinePanel.Add(airlineGroupBox);

ui.AddToManager(playerPanel);
ui.AddToManager(airlinePanel);

--Add Game Settings Panel
local gameSettingsPanel = ui.CreateGroupPanel();
gameSettingsPanel.Width = airlinePanel.Width;
gameSettingsPanel.Height = 200;
gameSettingsPanel.Left = airlinePanel.Left;
gameSettingsPanel.Top = airlinePanel.Top + airlinePanel.Height + 20;
gameSettingsPanel.Text = "Game Settings";
gameSettingsPanel.Color = ui.CreateColor("CornflowerBlue", 0.9);

local gameSettingsTurnLabel = ui.CreateLabel();
gameSettingsTurnLabel.Left = 10;
gameSettingsTurnLabel.Top = 10;
gameSettingsTurnLabel.Width = 80;
gameSettingsTurnLabel.Text = ui.GetText("lblTurnLength");

gameSettingsPanel.Add(gameSettingsTurnLabel);

local turnComboBox = ui.CreateComboBox();
turnComboBox.Left = gameSettingsTurnLabel.Left + gameSettingsTurnLabel.Width;
turnComboBox.Top = gameSettingsTurnLabel.Top;
turnComboBox.Color = ui.CreateColor("LightBlue", 0.8);
--turnComboBox.BackColor = ui.CreateColor("LightBlue", 0.8);
turnComboBox.Width = 125;

ui.AddItems(turnComboBox, context.GetTurnLengths());
turnComboBox.ItemIndex = 1; --Set One Week as Default Turn Length

ui.SetSelectionMethod(turnComboBox, context, "ComboBoxSelectionOnChange");

turnComboBox.Init();
turnComboBox.Refresh();
context.TurnComboBox = turnComboBox;

gameSettingsPanel.Add(turnComboBox);

local gameDatabaseBtn = ui.CreateButton();
gameDatabaseBtn.Text = "Select Game Database ...";
gameDatabaseBtn.Width = gameSettingsPanel.OriginWidth / 3;
gameDatabaseBtn.Left = gameSettingsTurnLabel.Left;
gameDatabaseBtn.Top = gameSettingsTurnLabel.Top + gameSettingsTurnLabel.Height + 15;

gameSettingsPanel.Add(gameDatabaseBtn);

local gameDatabaseSelection = ui.CreateGameDatabaseSelectionDialog();
gameDatabaseSelection.Width = ui.GetScreenWidth() / 2;
gameDatabaseSelection.Height = ui.GetScreenHeight() / 3;
gameDatabaseSelection.Visible = false;
ui.AddToManager(gameDatabaseSelection);
ui.SetCallMethod(gameDatabaseBtn, context, "SelectGameDatabaseDialogButtonOnClick");
context.GameDatabaseSelectionDialog = gameDatabaseSelection;
context.GameDatabaseSelectionInit();
gameDatabaseSelection.Init();
gameDatabaseSelection.Refresh();

local startGameBtn = ui.CreateButton();
startGameBtn.Text = ui.GetText("menuStartGame");
startGameBtn.Width = 200;
startGameBtn.Top = gameSettingsPanel.Top + gameSettingsPanel.Height + 10;
startGameBtn.Left = gameSettingsPanel.Left + gameSettingsPanel.OriginWidth - startGameBtn.Width;
startGameBtn.Color = ui.GetColor("LawnGreen");
ui.SetCallMethod(startGameBtn, context, "StartGameButtonOnClick");

ui.AddToManager(gameSettingsPanel);
ui.AddToManager(startGameBtn);

-- Define Error Window for missing values
local errorWindow = ui.CreateWindow();
errorWindow.Width = 400;
errorWindow.Height = 125;
errorWindow.Top = ui.GetScreenHeight() / 2 - errorWindow.Height / 2;
errorWindow.Left = ui.GetScreenWidth() / 2 - errorWindow.Width / 2;
errorWindow.Text = ui.GetText("error");
errorWindow.IconVisible = false;
errorWindow.CloseButtonVisible = false;
errorWindow.Movable = false;
errorWindow.Resizable = false;

local errorText = ui.CreateLabel();
errorText.Top = 10;
errorText.Width = errorWindow.Width - 15;
errorWindow.Add(errorText);

local okayBtn = ui.CreateButton();
okayBtn.Width = errorWindow.Width / 4;
okayBtn.Left = errorText.Left + errorText.Width / 2 - okayBtn.Width / 2;
okayBtn.Top = errorText.Top + errorText.Height + 25;
okayBtn.Text = ui.GetText("btnOkay");
ui.SetCallMethod(okayBtn, context, "ErrorMessageOkayOnClick");
errorWindow.Add(okayBtn);

context.ErrorText = errorText;
context.Window = errorWindow;