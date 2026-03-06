using System;

class AlphabetCard : ICard {

    private static ConsoleColor[] _colorMap = {
        ConsoleColor.Gray,
        ConsoleColor.Yellow,
        ConsoleColor.Blue,
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Magenta,
        ConsoleColor.White,
        ConsoleColor.DarkYellow
    };

    private readonly int _value;

    private bool _isOpened;
    private bool _isMatched;

    public AlphabetCard(int value) {
        _value = value;
    }

    public int Value {
        get => _value;
    }
    public bool IsOpened {
        get => _isOpened;
        set => _isOpened = value;
    }
    public bool IsMatched {
        get => _isMatched;
        set => _isMatched = value;
    }

    public ConsoleColor GetColor(bool isPreview) {
        if (!IsOpened && !isPreview) {
            return _colorMap[0];
        } else {
            return _colorMap[Value];
        }
    }

    public string GetPrintValue(bool isPreview) {
        if ((isPreview) || (IsOpened && IsMatched)) {
            return $"{(char)(Value + 'A' - 1), 2}  ";
        } else if (IsOpened) {
            return $"[{(char)(Value + 'A' - 1)}] ";
        } else {
            return $"**  ";
        }
    }
}