using System;

static class GameModeFactory {
    public static GameModeBase Create(GameSettings settings) {
        if (settings.Mode == GameMode.Classic) { return new ClassicMode(settings); }
        else if (settings.Mode == GameMode.TimeAttack) { return new TimeAttackMode(settings); }
        else if (settings.Mode == GameMode.Survival) { return new SurvivalMode(settings); }
        else { throw new InvalidOperationException($"Unsupported game mode: {settings.Mode}"); }
    }
}