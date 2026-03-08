struct GameSettings {
    private CardDisplayStyle _skin;
    private Difficulty _diff;
    private GameMode _mode;

    public CardDisplayStyle Skin { 
        get => _skin; 
        set => _skin = value; 
    }
    public Difficulty Diff { 
        get => _diff; 
        set => _diff = value; 
    }
    public GameMode Mode { 
        get => _mode; 
        set => _mode = value; 
    }
}