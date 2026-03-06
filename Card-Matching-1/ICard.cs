using System;

interface ICard {
    int Value { get; }
    bool IsOpened { get; set; }
    bool IsMatched { get; set; }
    string GetPrintValue(bool isPreview);
    ConsoleColor GetColor(bool isPreview);
}