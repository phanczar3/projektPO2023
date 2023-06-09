public class User : Player {
    private string name;
    static private IOHandler ioh;
    public User(string s, IOHandler i) {
        name = s;
        ioh = i;
    }
    public override Move makeMove(Card c) {
        return ioh.askForMove(name, hand);
    }
}