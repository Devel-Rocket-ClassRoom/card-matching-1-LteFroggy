using System;

class BoardRenderer {
    public void RenderBoard(Board board, bool isPreview = false) {
        Console.Write($"    1열 2열 3열 4열");
        for (int i = 0; i < board.BoardSize; i++) {
            // colsize마다 줄바꿈
            if (i % board.ColSize== 0) { 
                Console.Write($"\n{(i / board.ColSize) + 1}행  ");
            }
            Console.ForegroundColor = board[i].GetColor(isPreview);
            Console.Write($"{board[i].GetPrintValue(isPreview)}");
            Console.ResetColor();
        }
    }
}