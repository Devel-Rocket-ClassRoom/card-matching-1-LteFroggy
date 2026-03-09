struct Position {
    public readonly int Row { get; }
    public readonly int Col { get; }

    public Position(int row, int col) {
        Row = row;
        Col = col;
    }
}