using System;

internal class Board {
    private CardBase[] _cards;
    private int[] _shuffledIdx;

    private BoardState _boardState;

    private int _colSize = 4;
    private int _boardSize;

    private int _firstFlippedIdx;
    private int _secondFlippedIdx;

    public BoardState BoardState => _boardState;
    public int ColSize => _colSize;
    public int BoardSize => _boardSize;
    public int RowSize => _boardSize / _colSize;

    public Board(CardBase[] cards, int boardSize) { 
        _cards = cards;
        _boardSize = boardSize;
        _shuffledIdx = shuffle(_boardSize);
    }
    
    // board[i]에 접근하면 셔플된 i번째 카드에 접근 가능
    // BoardRenderer에서 사용
    public CardBase this[int idx] {
        get => _cards[_shuffledIdx[idx]];
    }

    /// <summary>
    /// 카드 한 장 뒤집기
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
    /// 두 카드를 뒤집은 상태에서, 두 카드가 동일한지 체크
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

    /// <summary>
    /// 사용할 카드 개수에 맞춰 인덱스를 랜덤하게 섞는다
    /// </summary>
    /// <param name="size">사용할 카드 더미 크기</param>
    private int[] shuffle(int size) {
        int[][] shuffleArr = new int[size][];
        int[] shuffledIdx;
        Random rand = new Random();

        // 값 할당
        for (int i = 0; i < size; i++) {
            shuffleArr[i] = new int[2];
            shuffleArr[i][0] = i;
            shuffleArr[i][1] = rand.Next(1000);
        }

        // shuffle
        Array.Sort(shuffleArr, (a, b) => a[1].CompareTo(b[1]));

        // shuffledIdx에 값 저장
        shuffledIdx = new int[size];
        for (int i = 0; i < size; i++) {
            shuffledIdx[i] = shuffleArr[i][0];
        }

        return shuffledIdx;
    }
}
