using System;
using System.Threading;

class GameManager {
    private GameModeBase _gameMode;
    private int _previewTime;
    private Board _board;
    private BoardRenderer _boardRenderer;

    public GameManager(GameModeBase gameMode, GameSettings settings, Board board, BoardRenderer boardRenderer) {
        _gameMode = gameMode;
        _board = board;
        _boardRenderer = boardRenderer;
        _previewTime = settings.Diff switch {
            Difficulty.Easy => 5000,
            Difficulty.Normal => 3000,
            Difficulty.Hard => 1000
        };
    }

    /// <summary>
    /// 게임 시작 메서드
    /// </summary>
    public void PlayGame() {
        Console.Clear();

        // 게임 규칙 출력
        _gameMode.PrintGameRule();

        // 카드 미리보기 출력
        PreviewBoard();

        // 게임 루프 진행
        _gameMode.Start();
        GameState endState = MainLoop();

        // 게임 결과에 따른 출력
        _gameMode.PrintGameResult(endState);
    }

    /// <summary>
    /// 게임을 실행하는 메인 루프
    /// </summary>
    /// <returns>게임 종료 결과</returns>
    public GameState MainLoop() {
        do {
            // 초기 출력
            Console.Clear();
            _boardRenderer.RenderBoard(_board);
            Console.WriteLine();
            Console.WriteLine();
            
            // 유저 입력 받을 차례라면, 입력 받기
            int row, col;
            if (_board.BoardState != BoardState.Matching) {
                _gameMode.PrintStatusText();

                GetUserInput(out row, out col);

                // 이미 뒤집힌 카드를 다시 뒤집는 경우를 잡기 위한 try-catch
                try { _board.FlipCard(row, col); }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(500);
                    continue;
                }
            } 

            // 두 값의 비교를 해야 할 차례면, 비교
            else if (_board.BoardState == BoardState.Matching) {
                bool result = _board.MatchCard();

                // 게임 상태 업데이트하고, 결과 출력
                Console.WriteLine(_gameMode.UpdateGame(result));
                Console.WriteLine();

                Thread.Sleep(1000);
            }
            
            // 매 뒤집기 종료 후 종료 조건 체크
            switch (_gameMode.GetGameState()) {
                case GameState.Playing:
                    break;
                case GameState.Clear :
                    return GameState.Clear;
                case GameState.GameOver:
                    return GameState.GameOver;
            }
        } while (true);
    }

    /// <summary>
    /// 유저가 어떤 위치의 카드를 뒤집을 지 값을 받는다.
    /// </summary>
    /// <param name="row">행</param>
    /// <param name="col">열</param>
    public void GetUserInput(out int row, out int col) { 
        do {
            if (_board.BoardState == BoardState.WaitingFirst) {
                Console.Write($"첫 번째 카드를 선택하세요 (행 열) : ");
            } else if (_board.BoardState == BoardState.WaitingSecond) {
                Console.Write($"두 번째 카드를 선택하세요 (행 열) : ");
            }
            string[] str = Console.ReadLine().Split(' ');
            try {
                if (Int32.TryParse(str[0], out row) && Int32.TryParse(str[1], out col)) { }
                else {
                    Console.WriteLine($"숫자를 입력해주세요.");
                    Console.WriteLine();
                    continue;
                }
            } catch {
                Console.WriteLine($"띄어쓰기로 구분하여 두 개의 숫자를 입력해주세요.");
                Console.WriteLine();
                continue;
            }

            // row, col 인덱스 0에 맞도록 변경
            row--; col--;
            if (row < 0 || col < 0 || row >= _board.RowSize || col >= _board.ColSize) {
                Console.WriteLine($"범위에 맞는 값을 입력해주세요");
                Console.WriteLine();
                continue;
            }

            break;
        } while (true);
    }

    public void PreviewBoard() {
        // isPreview를 true로 보드판 출력
        _boardRenderer.RenderBoard(_board, true);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"잘 기억하세요! ({_previewTime / 1000}초 후 뒤집힙니다.)");
        Thread.Sleep(_previewTime);
    }

}