using System;

abstract class CardBase {
    private readonly int _value;

    private bool _isOpened;
    private bool _isMatched;
    public int Value => _value;

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
    protected CardBase(int value) {
        _value = value;
    }

    public bool IsOpened {
        get => _isOpened;
        set => _isOpened = value;
    }
    public bool IsMatched {
        get => _isMatched;
        set => _isMatched = value;
    }

    public abstract string GetPrintValue(bool isPreview);
    public virtual ConsoleColor GetColor(bool isPreview) {
        if (!IsOpened && !isPreview) {
            return _colorMap[0];
        } else {
            return _colorMap[Value];
        }
    }
}