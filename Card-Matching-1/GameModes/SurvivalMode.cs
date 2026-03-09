using System.Text;

class SurvivalMode : GameModeBase
{
    private readonly int _maxWrongStreamCount;
    private int _wrongStreamCount;

    public SurvivalMode(GameSettings settings) : base(settings) {
        _wrongStreamCount = 0;
        _maxWrongStreamCount = settings.Diff switch {
            Difficulty.Easy => 3,
            Difficulty.Normal => 5,
            Difficulty.Hard => 7,
        };
    }
    public override string GetGameRule() {
        return $"서바이벌 모드입니다." +
                $"연속 {_maxWrongStreamCount}회 이상 틀리지 않고 모든 짝을 맞춰야 합니다.\n";
    }

    public override GameState GetGameState() {
        // 최대 횟수만큼 연속으로 틀리면 게임오버
        if (_wrongStreamCount >= _maxWrongStreamCount) { return GameState.GameOver; } 
        // 다 맞췄다면 클리어
        else if (_matchCount >= _maxMatchCount) { return GameState.Clear; } 
        // 아니라면 게임 중
        else { return GameState.Playing; }
    }

    public override string GetGameResult(GameState endState) {
        StringBuilder sb = new StringBuilder();
        if (endState == GameState.Clear) {
            sb.Append($"=== 게임 클리어 ===\n");
            sb.Append($"연속으로 {_maxWrongStreamCount}회 이상 틀리지 않고 모든 쌍을 맞췄습니다.\n\n");
        } else {
            sb.Append($"=== 게임 오버! ===\n");
            sb.Append($"연속으로 {_wrongStreamCount}회 틀렸습니다.\n");
            sb.Append($"찾은 쌍 : {_matchCount}/{_maxMatchCount}\n\n");
        }
        sb.Append($"총 시도 횟수 : {_attemptCount}\n");
        
        return sb.ToString();
    }


    public override string GetStatusText() {
        return $"연속 오답 횟수 : {_wrongStreamCount}/{_maxWrongStreamCount} | 찾은 쌍 : {_matchCount}/{_maxMatchCount}\n";
    }

    public override string UpdateGame(bool matched) {
        _attemptCount++;

        if (matched) {
            _matchCount++;
            _wrongStreamCount = 0;
            return $"짝을 찾았습니다!";
        } else {
            _wrongStreamCount++;
            return $"짝이 맞지 않습니다.";
        }
    }
}