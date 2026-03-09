class AlphabetCard : CardBase {

    public AlphabetCard(int value) : base(value) { }

    public override string GetPrintValue(bool isPreview) {
        if ((isPreview) || (IsOpened && IsMatched)) {
            return $"{(char)(Value + 'A' - 1), 2}  ";
        } else if (IsOpened) {
            return $"[{(char)(Value + 'A' - 1)}] ";
        } else {
            return $"**  ";
        }
    }
}