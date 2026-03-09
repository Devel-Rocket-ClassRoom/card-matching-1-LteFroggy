class SignCard : CardBase {

    public SignCard(int value) : base(value) { }

    private static char[] _signMap = {
        '★',
        '♠',
        '♥',
        '♦',
        '♣',
        '●',
        '■',
        '▲'
    };

    public override string GetPrintValue(bool isPreview) {
        if ((isPreview) || (IsOpened && IsMatched)) {
            return $"{_signMap[Value - 1], 2}  ";
        } else if (IsOpened) {
            return $"[{_signMap[Value - 1]}] ";
        } else {
            return $"**  ";
        }
    }
}