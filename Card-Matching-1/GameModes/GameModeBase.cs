abstract class GameModeBase {
    private readonly Difficulty _difficulty;

    protected int _matchCount;
    protected readonly int _maxMatchCount;

    protected int _attemptCount;

    protected Difficulty Difficulty { get => _difficulty; }

    protected GameModeBase(GameSettings settings) {
        _attemptCount = 0;
        _matchCount = 0;
        _difficulty = settings.Diff;

        // Difficulty에 따른 게임 종료 조건 설정
        _maxMatchCount = settings.Diff switch {
            Difficulty.Easy => 4,
            Difficulty.Normal => 8,
            Difficulty.Hard => 12,
            _ => throw new System.Exception("Invalid difficulty setting")
        };
    }

    /// <summary>
    /// 시작 시 로직이 필요한 모드들만 오버라이드해서 사용
    /// </summary>
    public virtual void Start() { }
    public abstract void PrintGameRule();
    public abstract GameState GetGameState();
    public abstract void PrintStatusText();
    public abstract void PrintGameResult(GameState endState);
    public abstract string UpdateGame(bool matched);
}