local settingsWindow = ui.CreateWindow();
settingsWindow.Width = 400;
settingsWindow.Height = 200;
settingsWindow.Text = ui.GetText("settingsWindowTitle");
settingsWindow.IconVisible = false;
settingsWindow.CloseButtonVisible = false;
settingsWindow.Movable = false;
settingsWindow.Resizable = false;
context.SettingsWindow = settingsWindow;

local groupBox = ui.CreateGroupBox();
groupBox.Text = ui.GetText("menuLocaleTitle");
groupBox.Width = settingsWindow.Width - 50;
groupBox.Height = 50;
groupBox.Left = settingsWindow.Width/2 - groupBox.Width / 2 - 10;
groupBox.Top = 10;
groupBox.TextColor = ui.GetColor("White");

settingsWindow.Add(groupBox);

local comboBox = ui.CreateComboBox();
comboBox.Width = groupBox.Width - 25;
comboBox.Height = 25;
comboBox.Left = groupBox.Width / 2 - comboBox.Width / 2;
comboBox.TextColor = ui.GetColor("White");
context.LanguageComboBox = comboBox;
context.InitLanguageComboBox();

groupBox.Add(comboBox);

local btnSave = ui.CreateButton();
btnSave.Text = ui.GetText("btnSaveAndClose");
btnSave.Width = settingsWindow.Width / 2 - 25;
btnSave.Left = 10;
btnSave.Top = settingsWindow.Height - btnSave.Height - 50;
ui.SetCallMethod(btnSave, context, "SettingsSaveAndCloseOnClick");

settingsWindow.Add(btnSave);
ui.AddToManager(settingsWindow);
context.SettingsWindow.Close();