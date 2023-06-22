using System;
using static System.Console;
class KeyboardInterface {
    private int selectedIndex;
    private string[] state;
    private string[] options;
    public KeyboardInterface(string[] s, string[] o) {
        state = s;
        options = o;
        selectedIndex = 0;  
    }
    private void swapColors() {
        ConsoleColor tmp = ForegroundColor;
        ForegroundColor = BackgroundColor;
        BackgroundColor = tmp;
    }
    private void displayInterface() {
        foreach(string s in state) {
            WriteLine(s);
        }
        for(int i = 0; i < options.Length; i++) {
            string cur = options[i];
            string pref;
            if(i == selectedIndex) {
                pref = "*";
            }
            else {
                pref = " ";
            }
            WriteLine($"    {pref} << {cur} >>");
        }
    }
    public int run() {
        bool tmp = CursorVisible;
        CursorVisible = false;
        ConsoleKey keyPressed;
        do{
            Clear();
            displayInterface();
            keyPressed = ReadKey(true).Key;
            if(keyPressed == ConsoleKey.UpArrow) {
                selectedIndex--;
                selectedIndex = Math.Max(selectedIndex, 0);
            } else if(keyPressed == ConsoleKey.DownArrow) {
                selectedIndex++;
                selectedIndex = Math.Min(selectedIndex, options.Length-1);
            }
        } while(keyPressed != ConsoleKey.Enter);
        CursorVisible = tmp;
        return selectedIndex;
    }
}