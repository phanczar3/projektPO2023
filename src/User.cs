public class User : Player {
    static public IOHandler ioh;
    public User(string s) : base(s) {}
    public override Move makeMove(GameState gs, GameRules gr) {
        return ioh.askForMove(gs, gr);
    }
}