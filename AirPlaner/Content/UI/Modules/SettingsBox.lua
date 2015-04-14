settingsWindow = ui.CreateWindow();
settingsWindow.Width = ui.GetScreenWidth() / 3;
settingsWindow.Left = ui.GetScreenWidth() / 2 - settingsWindow.Width / 2;
settingsWindow.Height = ui.GetScreenHeight() / 2;
settingsWindow.Top = ui.GetScreenHeight() / 2 - settingsWindow.Height / 2;
settingsWindow.Text = ui.GetText("settingsWindowTitle");
settingsWindow.IconVisible = false;
settingsWindow.CloseButtonVisible = false;
settingsWindow.Movable = false;
settingsWindow.Resizable = false;

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

local soundGroupBox = ui.CreateGroupBox();
soundGroupBox.Text = "Sound Settings";
soundGroupBox.Width = settingsWindow.Width - 50;
soundGroupBox.Height = 120;
soundGroupBox.Left = settingsWindow.Width/2 - soundGroupBox.Width / 2 - 10;
soundGroupBox.Top = groupBox.Top + groupBox.Height + 10;

settingsWindow.Add(soundGroupBox);

local musicVolumeLabel = ui.CreateLabel();
musicVolumeLabel.Text = "Music Volume:";
musicVolumeLabel.Width = soundGroupBox.Width - 25;
musicVolumeLabel.Left = soundGroupBox.Width / 2 - musicVolumeLabel.Width / 2;

soundGroupBox.Add(musicVolumeLabel);

musicVolume = ui.CreateTrackBar();
musicVolume.Width = soundGroupBox.Width - 25;
musicVolume.Left = soundGroupBox.Width / 2 - musicVolume.Width / 2;
musicVolume.Top = musicVolumeLabel.Top + musicVolumeLabel.Height + 5;
musicVolume.Range = 100;
musicVolume.StepSize = 1;

soundGroupBox.Add(musicVolume);

local soundEffectLabel = ui.CreateLabel();
soundEffectLabel.Width = soundGroupBox.Width - 25;
soundEffectLabel.Left = soundGroupBox.Width / 2 - soundEffectLabel.Width / 2;
soundEffectLabel.Top = musicVolume.Top + musicVolume.Height + 5;
soundEffectLabel.Text = "Sound FX Volume:"

soundGroupBox.Add(soundEffectLabel);

soundEffectVolume = ui.CreateTrackBar();
soundEffectVolume.Width = soundGroupBox.Width - 25;
soundEffectVolume.Left = soundGroupBox.Width / 2 - soundEffectVolume.Width / 2;
soundEffectVolume.Top = soundEffectLabel.Top + soundEffectLabel.Height + 5;
soundEffectVolume.Range = 100;
soundEffectVolume.StepSize = 1;
soundGroupBox.Add(soundEffectVolume);

local btnSave = ui.CreateButton();
btnSave.Text = ui.GetText("btnSaveAndClose");
btnSave.Width = settingsWindow.Width / 2 - 25;
btnSave.Left = 10;
btnSave.Top = settingsWindow.Height - btnSave.Height - 50;
ui.SetCallMethod(btnSave, context, "SettingsSaveAndCloseOnClick");

settingsWindow.Add(btnSave);
settingsWindow.Visible = false;
ui.AddToManager(settingsWindow);
