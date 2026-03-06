using System;
using System.Threading;

class GameManager {
    private int _maxMatchCount;
    private int _matchCount;

    private int _maxAttemptCount;
    private int _attemptCount;

    private int _previewTime;

    private Board _board;

    private CardSkin _skin;
    private Difficulty _diff;

    /// <summary>
    /// 게임의 시작 및 종료를 담당
    /// </summary>
    /// <returns>재시작 여부</returns>
    public bool PlayGame() {
        Console.Clear();
        Console.WriteLine($"=== 카드 짝 맞추기 게임 ===");
        Console.WriteLine();

        // 카드 스킨 지정
        _skin = GetCardSkin();
        _diff = GetDifficulty();

        // 초기화
        Initialize(_diff, _skin);

        // 카드 미리보기
        PreviewBoard();

        // 게임 루프 진행 후 종료 처리
        GameEndState end = MainLoop();
        if (end == GameEndState.Clear) {
            Console.WriteLine($"=== 게임 클리어 ===");
        } else {
            Console.WriteLine($"=== 게임 오버! ===");
            Console.WriteLine($"시도 횟수를 모두 사용했습니다.");
            Console.WriteLine($"찾은 쌍 : {_matchCount}/{_maxMatchCount}");
        }

        Console.WriteLine($"총 시도 횟수 : {_attemptCount}");
        Console.WriteLine();

        do {
            Console.Write($"새 게임을 하시겠습니까? : (Y/N) : ");
            string input = Console.ReadLine();
            switch (input.ToLower()) {
                case "y" :
                    Console.WriteLine($"게임을 다시 시작합니다.");
                    return true;
                case "n" :
                    Console.WriteLine($"게임을 종료합니다.");
                    return false;
                default :
                    Console.WriteLine($"올바른 값을 입력해주세요.");
                    continue;
            }
        } while(true);
        
    }

    /// <summary>
    /// 게임을 실행하는 메인 루프
    /// </summary>
    /// <returns>게임 종료 결과</returns>
    public GameEndState MainLoop() {
        do {
            // 초기 출력
            Console.Clear();
            _board.PrintBoard();
            Console.WriteLine();
            Console.WriteLine();
            
            // 유저 입력 받을 차례라면, 입력 받기
            int row, col;
            if (_board.BoardState != BoardState.Matching) {
                Console.WriteLine($"시도 횟수 : {_attemptCount} | 찾은 쌍 : {_matchCount}/{_maxMatchCount}");
                Console.WriteLine();

                GetUserInput(out row, out col);
                try {
                    _board.FlipCard(row, col);
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(500);
                    continue;
                }
            } 

            // 두 값의 비교를 해야 할 차례면, 비교
            else if (_board.BoardState == BoardState.Matching) {
                _attemptCount++;
                // 두 카드가 같을 경우
                if (_board.MatchCard()) {
                    _matchCount++;
                    Console.WriteLine($"짝을 찾았습니다!");
                    Console.WriteLine();
                } else {
                    Console.WriteLine($"짝이 맞지 않습니다!");
                    Console.WriteLine();
                }
                Thread.Sleep(1000);
            }
            
            // 매 뒤집기 종료 후 종료 조건 체크
            if (_attemptCount == _maxAttemptCount) {
                return GameEndState.OverAttempt;
            } else if (_matchCount == _maxMatchCount) {
                return GameEndState.Clear;
            }
        } while (true);
    }


    /// <summary>
    /// GameManager의 초기 상태 설정
    /// </summary>
    /// <param name="difficulty">난이도</param>
    public void Initialize(Difficulty diff, CardSkin skin) {
        ICard[] cards;
        // cardskin에 따른 카드 생성
        switch (skin) {
            case CardSkin.Alphabet:
                cards = new AlphabetCard[24];
                for (int i = 0; i < 24; i++) {
                    cards[i] = new AlphabetCard(i / 2 + 1);
                }
                break;
            case CardSkin.Sign:
                cards = new SignCard[24];
                for (int i = 0; i < 24; i++) {
                    cards[i] = new SignCard(i / 2 + 1);
                }
                break;
            case CardSkin.Number :
            default :
                cards = new NumberCard[24];
                for (int i = 0; i < 24; i++) {
                    cards[i] = new NumberCard(i / 2 + 1);
                }
                break;
        }
        _board = new Board(cards);
        _board.Start(diff);

        Console.WriteLine($"카드를 섞는 중...");
        Console.WriteLine();

        // 값 세팅
        switch (diff) {
            case Difficulty.Easy :
                _maxMatchCount = 4;
                _maxAttemptCount = 10;
                _previewTime = 5000;
                break;
            case Difficulty.Normal :
                _maxMatchCount = 8;
                _maxAttemptCount = 20;
                _previewTime = 3000;
                break;
            case Difficulty.Hard :
                _maxMatchCount = 12;
                _maxAttemptCount = 30;
                _previewTime = 2000;
                break;
        }
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

    public Difficulty GetDifficulty() {
        do {
            Console.WriteLine($"난이도를 선택하세요 : ");
            Console.WriteLine($"1. 쉬움 (2 x 4)");
            Console.WriteLine($"2. 보통 (4 x 4)");
            Console.WriteLine($"3. 어려움 (6 x 4)");
            Console.WriteLine();
            Console.Write("선택 : ");
            if (Int32.TryParse(Console.ReadLine(), out int input)) { }
            else {
                Console.WriteLine($"정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            if (!Enum.IsDefined(typeof(Difficulty), --input)) {
                Console.WriteLine($"적절한 정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            
            return (Difficulty)input;
        } while (true);
    }

    public void PreviewBoard() {
        Console.Clear();
        _board.PrintBoard(true);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"잘 기억하세요! ({_previewTime / 1000}초 후 뒤집힙니다.)");
        Thread.Sleep(_previewTime);
    }

    public CardSkin GetCardSkin() {
        do {
            Console.WriteLine($"카드 스킨을 선택하세요 : ");
            Console.WriteLine($"1. 숫자 스킨");
            Console.WriteLine($"2. 알파벳 스킨");
            Console.WriteLine($"3. 기호 스킨");
            Console.WriteLine();
            Console.Write("선택 : ");
            if (Int32.TryParse(Console.ReadLine(), out int input)) { }
            else {
                Console.WriteLine($"정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            if (!Enum.IsDefined(typeof(CardSkin), --input)) {
                Console.WriteLine($"적절한 정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            
            return (CardSkin)input;
        } while (true);
    }
}