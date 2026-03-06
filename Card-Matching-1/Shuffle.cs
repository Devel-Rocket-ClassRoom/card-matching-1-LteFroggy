using System;

static class Shuffle {
    
    public static void shuffle(int size, int colsize, out int[] result) {
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