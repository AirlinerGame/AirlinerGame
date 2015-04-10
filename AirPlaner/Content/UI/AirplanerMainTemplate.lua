sidebar = ui.CreateSideBar();
sidebar.Width = 175;
sidebar.Height = ui.GetScreenHeight();
sidebar.BackColor = ui.CreateColor("Black", 0.65);

btnDashboard = ui.CreateImageButton();
btnDashboard.Image = ui.TextureFromFile("Content/AirlinerGame/home.png");
btnDashboard.Height = 50;
btnDashboard.Width = sidebar.OriginWidth - 2;
btnDashboard.Left = 0;
btnDashboard.Top = 0;
btnDashboard.Text = ui.GetText("txtDashboard");
btnDashboard.TextColor = ui.GetColor("White");
btnDashboard.Color = ui.CreateColor("White", 0.1);
btnDashboard.HoverColor = ui.CreateColor("Red", 0.4);
sidebar.Add(btnDashboard);

btnFinances = ui.CreateImageButton();
btnFinances.Image = ui.TextureFromFile("Content/AirlinerGame/dollar.png");
btnFinances.Height = 50;
btnFinances.Width = sidebar.OriginWidth - 2;
btnFinances.Left = 0;
btnFinances.Top = btnDashboard.Height;
btnFinances.Text = ui.GetText("txtFinances");
btnFinances.TextColor = ui.GetColor("White");
btnFinances.Color = ui.CreateColor("White", 0.1);
btnFinances.HoverColor = ui.CreateColor("Red", 0.4);
sidebar.Add(btnFinances);

topBar = ui.CreateSideBar();
topBar.Width = ui.GetScreenWidth() - sidebar.Width;
topBar.Height = ui.GetScreenHeight() / 11;
topBar.Left = sidebar.Width;
--topBar.BackColor = ui.GetColor("White");

companyLogo = ui.CreateImageBox();
companyLogo.Image = context.Data.Airline.AirlinePicture.Image;
companyLogo.Height = topBar.Height;
companyLogo.Width = companyLogo.Height;
companyLogo.Color = ui.GetColor("White");
ui.SetImageMode(companyLogo, "Stretched");

topBar.Add(companyLogo);

headerLabel = ui.CreateHeaderLabel();
headerLabel.Height = topBar.Height / 2.5;
headerLabel.Width = topBar.Width * 0.65 - headerLabel.Left - 200;
headerLabel.Left = companyLogo.Width + 20;
headerLabel.Top = topBar.Height / 2 - headerLabel.Height / 2;
headerLabel.Text = context.Data.Airline.Name .. " " .. ui.GetText("txtDashboard");

local moneyCaptionLabel = ui.CreateLabel();
moneyCaptionLabel.Width = 60;
moneyCaptionLabel.Text = "Balance:";
moneyCaptionLabel.Left = headerLabel.Left + headerLabel.Width;
moneyCaptionLabel.Top = 10;

moneyValueLabel = ui.CreateMoneyLabel();
moneyValueLabel.Width = 100;
moneyValueLabel.Left = moneyCaptionLabel.Left + moneyCaptionLabel.Width;
moneyValueLabel.Top = moneyCaptionLabel.Top;
moneyValueLabel.Value = context.Data.Airline.Money;
context.MoneyLabel = moneyValueLabel;

topBar.Add(moneyCaptionLabel);
topBar.Add(moneyValueLabel);
topBar.Add(headerLabel);

ui.AddToManager(sidebar);
ui.AddToManager(topBar);