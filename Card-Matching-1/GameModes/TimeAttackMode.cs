using System;

class TimeAttackMode : GameModeBase
{
    private DateTime _startTime;
    private int _timeLimitSeconds;

    private double TimePassed => (DateTime.Now - _startTime).TotalSeconds;

    public TimeAttackMode(GameSettings settings) : base(settings) {
        _timeLimitSeconds = settings.Diff switch {
            Difficulty.Easy => 60,
            Difficulty.Normal => 90,
            Difficulty.Hard => 120,
        };
    }

    public override void Start() {
        _startTime = DateTime.Now;
    }

    public override GameState GetGameState()
    {
        if (_matchCount >= _maxMatchCount) { return GameState.Clear; }
        else if (TimePassed >=_timeLimitSeconds) { return GameState.GameOver; } 
        else { return GameState.Playing; }
    }

    public override void PrintGameResult(GameState endState)
    {
        if (endState == GameState.Clear) {
            Console.WriteLine($"=== 게임 클리어 ===");
            Console.WriteLine($"{TimePassed}초 만에 모든 쌍을 맞췄습니다.");
        } else {
            Console.WriteLine($"=== 게임 오버! ===");
            Console.WriteLine($"{_timeLimitSeconds}초 안에 모든 쌍을 맞추지 못했습니다.");
            Console.WriteLine($"찾은 쌍 : {_matchCount}/{_maxMatchCount}");
        }
        Console.WriteLine($"총 시도 횟수 : {_attemptCount}");
        Console.WriteLine();        
    }

    public override void PrintGameRule()
    {
        Console.WriteLine($"타임어택 모드입니다.");
        Console.WriteLine($"{_timeLimitSeconds}초 내에 모든 쌍을 맞춰야 합니다.");
        Console.WriteLine();
    }

    public override void PrintStatusText()
    {
        Console.WriteLine($"경과 시간 : {TimePassed:F1}초 | 찾은 쌍 : {_matchCount}/{_maxMatchCount}");
        Console.WriteLine();
    }

    public override string UpdateGame(bool matched)
    {
        // 시도 카운트는 항상 증가
        _attemptCount++;
        // 두 카드가 같을 경우 매치 카운트 중가
        if (matched) { 
            _matchCount++;
            return "짝을 찾았습니다!";
        } else {
            return "짝이 맞지 않습니다!";
        }
    }
}