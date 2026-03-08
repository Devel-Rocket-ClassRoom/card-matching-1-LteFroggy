using System;

class SignCard : ICard {
    private readonly int _value;

    private bool _isOpened;
    private bool _isMatched;

    public int Value => _value;
    public bool IsOpened {
        get => _isOpened;
        set => _isOpened = value;
    }
    public bool IsMatched {
        get => _isMatched;
        set => _isMatched = value;
    }
    public SignCard(int value) {
        _value = value;
    }

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

    public string GetPrintValue(bool isPreview) {
        if ((isPreview) || (IsOpened && IsMatched)) {
            return $"{_signMap[Value - 1], 2}  ";
        } else if (IsOpened) {
            return $"[{_signMap[Value - 1]}] ";
        } else {
            return $"**  ";
        }
    }

    public ConsoleColor GetColor(bool isPreview) {
        if (!IsOpened && !isPreview) {
            return _colorMap[0];
        } else {
            return _colorMap[Value];
        }
    }
}