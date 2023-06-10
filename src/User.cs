public class User : Player {
    static public IOHandler ioh;
    public User(string s) : base(s) {}
    public override Move makeMove(Card c) {
        return ioh.askForMove(name, hand);
    }
}