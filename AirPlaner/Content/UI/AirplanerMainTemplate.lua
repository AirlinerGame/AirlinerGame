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
btnDashboard.Text = "Dashboard";
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
btnFinances.Text = "Finanzen";
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
headerLabel.Width = topBar.Width - headerLabel.Left - 200;
headerLabel.Left = companyLogo.Width + 20;
headerLabel.Top = topBar.Height / 2 - headerLabel.Height / 2;
headerLabel.Text = context.Data.Airline.Name .. " Dashboard";

topBar.Add(headerLabel);

ui.AddToManager(sidebar);
ui.AddToManager(topBar);