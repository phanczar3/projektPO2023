public class User : Player {
    static public IOHandler ioh;
    public User(string s) {
        name = s;
    }
    public override Move makeMove(Card c) {
        return ioh.askForMove(name, hand);
    }
}