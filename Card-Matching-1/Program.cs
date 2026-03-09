using System;

Console.OutputEncoding = System.Text.Encoding.UTF8;

do {
    GameSettings settings = new GameSettings();
    Menu.ShowMainMenu();
    settings.Mode = Menu.GetGameMode();
    settings.Skin = Menu.GetCardDisplayStyle();
    settings.Diff = Menu.SelectDifficulty();

    Board board = BoardFactory.Create(settings);
    GameModeBase gameMode = GameModeFactory.Create(settings);
    BoardRenderer boardRenderer = new BoardRenderer();
    GameManager gameManager = new GameManager(gameMode, settings, board, boardRenderer);

    gameManager.PlayGame();

} while (Menu.AskReplay());
