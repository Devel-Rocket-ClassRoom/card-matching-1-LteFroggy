using System;

class BoardRenderer {
    /// <summary>
    /// 보드를 매개변수로 받아 출력을 담당하는 함수. isPreivew가 true라면 가리는 표시 없이 진행
    /// </summary>
    /// <param name="board">출력할 보드</param>
    /// <param name="isPreview">미리보기 여부</param>
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