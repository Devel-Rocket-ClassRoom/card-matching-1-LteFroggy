using System;
class NumberCard : CardBase {

    public NumberCard(int value) : base(value) { }

    public override string GetPrintValue(bool isPreview) {
        if ((isPreview) || (IsOpened && IsMatched)) {
            return $"{Value, 2}  ";
        } else if (IsOpened) {
            return $"[{Value}] ";
        } else {
            return $"**  ";
        }
    }

    public override ConsoleColor GetColor(bool isPreview) {
        return ConsoleColor.White;
    }
}
