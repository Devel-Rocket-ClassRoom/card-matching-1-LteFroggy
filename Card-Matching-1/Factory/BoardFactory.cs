static class BoardFactory {
    public static Board Create(GameSettings settings) {
        int size;
        // 난이도에 따라 카드 수 결정
        size = settings.Diff switch {
                Difficulty.Easy => 8,
                Difficulty.Normal => 16,
                Difficulty.Hard => 24,
        };

        return new Board(CardFactory.Create(settings.Skin, size), size);
    }
}