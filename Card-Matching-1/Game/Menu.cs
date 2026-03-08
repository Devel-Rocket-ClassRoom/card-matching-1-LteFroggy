using System;
using System.Threading;

static class Menu {
    public static void ShowMainMenu() {
        Console.Clear();
        Console.WriteLine($"=== 카드 짝 맞추기 게임 ===");
        Console.WriteLine();
    }

    public static Difficulty SelectDifficulty() {
         do {
            Console.WriteLine($"난이도를 선택하세요 : ");
            Console.WriteLine($"1. 쉬움 (2 x 4)");
            Console.WriteLine($"2. 보통 (4 x 4)");
            Console.WriteLine($"3. 어려움 (6 x 4)");
            Console.WriteLine();
            Console.Write("선택 : ");
            if (Int32.TryParse(Console.ReadLine(), out int input)) { }
            else {
                Console.WriteLine($"정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            if (!Enum.IsDefined(typeof(Difficulty), --input)) {
                Console.WriteLine($"적절한 정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();
            
            return (Difficulty)input;
        } while (true);
    }

    public static CardDisplayStyle GetCardDisplayStyle() {
         do {
            Console.WriteLine($"카드 스킨을 선택하세요 : ");
            Console.WriteLine($"1. 숫자 스킨");
            Console.WriteLine($"2. 알파벳 스킨");
            Console.WriteLine($"3. 기호 스킨");
            Console.WriteLine();
            Console.Write("선택 : ");
            if (Int32.TryParse(Console.ReadLine(), out int input)) { }
            else {
                Console.WriteLine($"정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            if (!Enum.IsDefined(typeof(CardDisplayStyle), --input)) {
                Console.WriteLine($"적절한 정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();
            
            return (CardDisplayStyle)input;
        } while (true);
    }

    public static GameMode GetGameMode() {
        do {
            Console.WriteLine($"게임 모드를 선택하세요 : ");
            Console.WriteLine($"1. 클래식");
            Console.WriteLine($"2. 타임어택");
            Console.WriteLine($"3. 서바이벌");
            Console.WriteLine();
            Console.Write("선택 : ");
            if (Int32.TryParse(Console.ReadLine(), out int input)) { }
            else {
                Console.WriteLine($"정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            if (!Enum.IsDefined(typeof(GameMode), --input)) {
                Console.WriteLine($"적절한 정수값을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();
            
            return (GameMode)input;
        } while (true);
    }

    public static bool GetPlayAgainInput() {
        do {
            Console.Write($"다시 플레이하시겠습니까? (y/n) : ");
            string input = Console.ReadLine().Trim().ToLower();
            if (input == "y") {
                return true;
            } else if (input == "n") {
                return false;
            } else {
                Console.WriteLine($"y 또는 n을 입력해주세요.");
                Console.WriteLine();
                continue;
            }
        } while (true);
    }

    public static bool AskReplay() {
        do {
            Console.Write($"새 게임을 하시겠습니까? : (Y/N) : ");
            string input = Console.ReadLine();
            switch (input.ToLower()) {
                case "y" :
                    Console.WriteLine($"게임을 다시 시작합니다.");
                    Thread.Sleep(500);
                    return true;
                case "n" :
                    Console.WriteLine($"게임을 종료합니다.");
                    return false;
                default :
                    Console.WriteLine($"올바른 값을 입력해주세요.");
                    continue;
            }
        } while(true);
    }
}