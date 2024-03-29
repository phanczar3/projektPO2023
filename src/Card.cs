using System;
using System.Collections.Generic;
public class Card {
    public enum Suit {Spades=0, Hearts, Clubs, Diamonds};
    public enum Face {Two=2, Three, Four, Five, Six, Seven,
    Eight, Nine, Ten, Jack, Queen, King, Ace};
    public static List<Card.Face> magicCards = new List<Card.Face>() {
        Card.Face.Two,
        Card.Face.Three,
        Card.Face.Four,
        Card.Face.Jack,
        Card.Face.Queen,
        Card.Face.King,
        Card.Face.Ace
    };
    public Suit suit {get;}
    public Face face {get;}

    public Card(Suit s, Face f) {
        suit = s;
        face = f;
    }
    public static string FaceToString(Face face) {
        string f = ((int)face).ToString();
        switch(f) {
            case "11":
                f = "J";
                break;
            case "12":
                f = "Q";
                break;
            case "13":
                f = "K";
                break;
            case "14":
                f = "A";
                break;
        }
        return f;
    }
    public static string SuitToString(Suit suit) {
        string s = ((int)suit).ToString();
        switch(s) {
            case "0":
                s = "\u2660";
                break;
            case "1":
                s = "\u2665";
                break;
            case "2":
                s = "\u2666";
                break;
            case "3":
                s = "\u2663";
                break;
        }
        return s;
    }
    public static List<Card> generateStandardDeck() {
        List<Card> cards = new List<Card>();
        foreach(Card.Suit s in Enum.GetValues(typeof(Card.Suit))) {
            foreach(Card.Face f in Enum.GetValues(typeof(Card.Face))) {
                cards.Add(new Card(s, f));
            }
        }
        return cards;
    }
    public override string ToString() {
        return Card.FaceToString(face)+Card.SuitToString(suit);
    }
}