﻿local playerPanel = ui.CreateGroupPanel();
playerPanel.Width = ui.GetScreenWidth() / 2 - 50;
playerPanel.Height = 225;
playerPanel.Top = 20;
playerPanel.Left = 40;

playerPanel.Text = ui.GetText("createGameUserSettingsCaption");
ui.SetColor("CornflowerBlue", playerPanel);

local boxQuarter = (playerPanel.Width / 4);
local playerPicture = ui.CreateImageBox();
playerPicture.Width = boxQuarter * 0.9;
playerPicture.Height = playerPicture.Width;
playerPicture.Left = 20;
playerPicture.Top = 20;
ui.SetImage(playerPicture, context.ProfilePicture);
ui.SetImageMode(playerPicture, "Stretched");
playerPanel.Add(playerPicture);

local changePictureBtn = ui.CreateButton();
changePictureBtn.Width = playerPicture.Width;
changePictureBtn.Text = ui.GetText("createGameUserSettingsChangePicture");
changePictureBtn.Left = playerPicture.Left;
changePictureBtn.Top = playerPicture.Top + playerPicture.Height + 5;
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
lastName.Init();
lastName.Refresh();

context.Lastname = lastName;

playerGroupBox.Add(firstNameLabel);
playerGroupBox.Add(firstName);
playerGroupBox.Add(lastNameLabel);
playerGroupBox.Add(lastName);

playerPanel.Add(playerGroupBox);

local airlinePanel = ui.CreateGroupPanel();
airlinePanel.Width = playerPanel.Width;
airlinePanel.Height = 300;
airlinePanel.Top = 20;
airlinePanel.Left = playerPanel.Left + playerPanel.Width + 20;

airlinePanel.Text = ui.GetText("createGameAirlineSettingsCaption");

ui.SetColor("CornflowerBlue", airlinePanel);

ui.AddToManager(playerPanel);
--ui.AddToManager(airlinePanel);

local startGameBtn = ui.CreateButton();
startGameBtn.Text = ui.GetText("menuStartGame");
startGameBtn.Width = 200;
startGameBtn.Top = playerPanel.Top + playerPanel.Height + 10;
startGameBtn.Left = playerPanel.Left;
startGameBtn.Color = ui.GetColor("LawnGreen");
ui.SetCallMethod(startGameBtn, context, "StartGameButtonOnClick");

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