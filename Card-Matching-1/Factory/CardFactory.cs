static class CardFactory {
    public static CardBase[] Create(CardDisplayStyle skin, int size) {
        CardBase[] cards = new CardBase[size];
        
        switch (skin) {
            case CardDisplayStyle.Number:
                for (int i = 0; i < size; i++) {
                    cards[i] = new NumberCard(i / 2 + 1);
                }
                break;
            case CardDisplayStyle.Alphabet:
                for (int i = 0; i < size; i++) {
                    cards[i] = new AlphabetCard(i / 2 + 1);
                }
                break;
            case CardDisplayStyle.Sign:
                for (int i = 0; i < size; i++) {
                    cards[i] = new SignCard(i / 2  + 1);
                }
                break;
        }

        return cards;
    }
}