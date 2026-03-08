using System;

static class BoardShuffler {
    /// <summary>
    /// 사용할 카드 개수에 맞춰 인덱스를 랜덤하게 섞는다
    /// </summary>
    /// <param name="size">사용할 카드 크기</param>
    /// <param name="result">섞인 인덱스가 들어갈 배열</param>
    public static void shuffle(int size, out int[] result) {
        int[][] shuffleArr = new int[size][];
        Random rand = new Random();

        // 값 할당
        for (int i = 0; i < size; i++) {
            shuffleArr[i] = new int[2];
            shuffleArr[i][0] = i;
            shuffleArr[i][1] = rand.Next(1000);
        }

        // shuffle
        Array.Sort(shuffleArr, (a, b) => a[1].CompareTo(b[1]));

        // result에 값 저장
        result = new int[size];
        for (int i = 0; i < size; i++) {
            result[i] = shuffleArr[i][0];
        }
    }
}