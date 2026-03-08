using System;

class NumberCard : ICard {
    private readonly int _value;
    private bool _isOpened;
    private bool _isMatched;

    public NumberCard(int value) {
        _value = value;
        _isOpened = false;
        _isMatched = false;
    }

    public int Value => _value;

    public bool IsOpened {
        get => _isOpened;
        set => _isOpened = value;
    }
    public bool IsMatched {
        get => _isMatched;
        set => _isMatched = value;
    }

    public override string ToString() {
        if (IsOpened && IsMatched) {
            return $"{_value, 2} ";
        } else if (IsOpened) {
            return $"[{_value}]";
        } else {
            return $"** ";
        }
    }

    public string GetPrintValue(bool isPreview) {
        if ((isPreview) || (IsOpened && IsMatched)) {
            return $"{Value, 2}  ";
        } else if (IsOpened) {
            return $"[{Value}] ";
        } else {
            return $"**  ";
        }
    }

    public ConsoleColor GetColor(bool isPreview) {
        return ConsoleColor.White;
    }
}
