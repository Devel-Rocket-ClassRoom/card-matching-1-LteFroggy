using System;

internal class Board {
    private ICard[] _cards;
    private int[] _shuffledIdx;

    private BoardState _boardState;

    private int _colSize;
    private int _boardSize;

    private int _firstFlippedIdx;
    private int _secondFlippedIdx;

    public BoardState BoardState => _boardState;
    public int ColSize => _colSize;
    public int BoardSize => _boardSize;
    public int RowSize => _boardSize / _colSize;

    public Board(ICard[] cards) { 
        // 최대값인 24장으로 일단 초기화
        _cards = cards;
    }

    /// <summary>
    /// 보드 초기화 함수
    /// </summary>
    /// <param name="diff">난이도 (Easy, Normal, Hard)</param>
    public void Start(Difficulty diff) {
        // 값 초기화

        _firstFlippedIdx = -1;
        _secondFlippedIdx = -1;
        _boardState = BoardState.WaitingFirst;

        // 난이도에 따른 규칙 적용
        switch (diff) {
            case Difficulty.Easy :
                _boardSize = 8;
                _colSize = 4;
                
                break;

            case Difficulty.Normal :
                _boardSize = 16;
                _colSize = 4;
                break;

            case Difficulty.Hard :
                _boardSize = 24;
                _colSize = 4;
                break;
        }

        // 카드 상태 초기화
        for (int i = 0; i < _boardSize; i++) {
            _cards[i].IsOpened = false;
            _cards[i].IsMatched = false;
        }

        // 보드판 섞기
        Shuffle.shuffle(_boardSize, _colSize, out _shuffledIdx);
    }

    /// <summary>
    /// 보드판 상태 출력용 함수. 미리보기라면 모두 오픈한 상태로 공개
    /// </summary>
    /// <param name="isPreview">미리보기 여부</param>
    public void PrintBoard(bool isPreview = false) {
        Console.Write($"    1열 2열 3열 4열");
        for (int i = 0; i < _boardSize; i++) {
            // colsize마다 줄바꿈
            if (i % _colSize == 0) { 
                Console.Write($"\n{(i / _colSize) + 1}행  ");
            }
            Console.ForegroundColor = _cards[_shuffledIdx[i]].GetColor(isPreview);
            Console.Write($"{_cards[_shuffledIdx[i]].GetPrintValue(isPreview)}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// 카드 뒤집기
    /// </summary>
    /// <param name="row">행</param>
    /// <param name="col">열</param>
    public void FlipCard(int row, int col) {
        int idx = row * _colSize + col;

        // 이미 뒤집힌 카드를 다시 뒤집으려 하는 경우
        if (_cards[_shuffledIdx[idx]].IsOpened) {
            throw new InvalidOperationException("이미 뒤집힌 카드입니다.");
        } 
        // 첫 카드를 뒤집는 상태
        else if (_boardState == BoardState.WaitingFirst) {
            _firstFlippedIdx = _shuffledIdx[idx];
            _cards[_firstFlippedIdx].IsOpened= true;
            _boardState = BoardState.WaitingSecond;
        } 
        // 두 번째 카드를 뒤집는 상태
        else if (_boardState == BoardState.WaitingSecond) {
            _secondFlippedIdx = _shuffledIdx[idx];
            if (_secondFlippedIdx == _firstFlippedIdx) { throw new InvalidOperationException("같은 카드를 다시 뒤집을 수 없습니다."); }
            _cards[_secondFlippedIdx].IsOpened = true;
            _boardState = BoardState.Matching;
        }
    }
    /// <summary>
    /// 두 카드를 뒤집은 상태에서, 두 카드가 동일한지 체크한다.
    /// </summary>
    public bool MatchCard() {
        if (_boardState != BoardState.Matching) {
            throw new InvalidOperationException("아직 두 카드를 뒤집지 않았습니다.");
        }

        _boardState = BoardState.WaitingFirst;

        // 두 값이 같다면, 두 값의 match 상태를 변경
        if (_cards[_firstFlippedIdx].Value == _cards[_secondFlippedIdx].Value) {
            _cards[_firstFlippedIdx].IsMatched = true;
            _cards[_secondFlippedIdx].IsMatched = true;
            return true;
        }
        // 다르다면, 오픈을 false 로 변경
        else {
            _cards[_firstFlippedIdx].IsOpened = false;
            _cards[_secondFlippedIdx].IsOpened = false;
            return false;
        }
    }
}
