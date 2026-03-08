using System;

sealed class ClassicMode : GameModeBase {
    private int _maxAttemptCount;

    public ClassicMode(GameSettings settings) : base(settings) {
        // Difficulty에 따른 게임 종료 조건 설정
        switch (Difficulty) {
            case Difficulty.Easy :
                _maxAttemptCount = 10;
                break;
            case Difficulty.Normal :
                _maxAttemptCount = 20;
                break;
            case Difficulty.Hard :
                _maxAttemptCount = 30;
                break;
        }
    }
    public override void PrintGameRule()
    {
        Console.WriteLine($"클래식 모드입니다.");
        Console.WriteLine($"{_maxAttemptCount}회 안에 모든 짝을 맞춰야 합니다.");
        Console.WriteLine();
    }

    public override void PrintGameResult(GameState endState) {
        if (endState == GameState.Clear) {
            Console.WriteLine($"=== 게임 클리어 ===");
        } else {
            Console.WriteLine($"=== 게임 오버! ===");
            Console.WriteLine($"시도 횟수를 모두 사용했습니다.");
            Console.WriteLine($"찾은 쌍 : {_matchCount}/{_maxMatchCount}");
        }

        Console.WriteLine($"총 시도 횟수 : {_attemptCount}");
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

    public override void PrintStatusText()
    {
        Console.WriteLine($"시도 횟수 : {_attemptCount} | 찾은 쌍 : {_matchCount}/{_maxMatchCount}");
        Console.WriteLine();
    }

    public override GameState GetGameState()
    {
        if (_matchCount >= _maxMatchCount) { return GameState.Clear; }
        else if (_attemptCount >= _maxAttemptCount) { return GameState.GameOver; }
        else { return GameState.Playing; }
    }

}