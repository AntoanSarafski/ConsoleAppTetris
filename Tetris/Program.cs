class Program
{
    // Settings for the Tetris game

    static int TetrisRows = 20;
    static int TetrisCols = 10;
    static int InfoCols = 10;
    static int ConsoleRows = 1 + TetrisRows + 1;
    static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;

    // State
    static int Score = 0;


    static void Main(string[] args)
    {
        Console.Clear();

        Console.Title = "Tetris v1.0";
        Console.WindowHeight = ConsoleRows + 1;
        Console.WindowWidth = ConsoleCols;
        Console.BufferHeight = ConsoleRows + 1;
        Console.BufferWidth = ConsoleCols;
        Console.CursorVisible = false;
        DrawBorder();
        DrawInfo();
        Console.ReadLine();
    }



    static void DrawBorder()
    {
        string line = "╔";
        line += new string('═', TetrisCols);

        /*for (int i = 0; i < TetrisCols; i++)
        {
            line += "═";
        }*/

        line += "╦";
        line += new string('═', InfoCols);
        line += "╗";

        Console.Write(line);

        for (int i = 0; i < TetrisRows; i++) 
        {
            string middleLIne = "║";
            middleLIne += new string(' ', TetrisCols);
            middleLIne += "║";
            middleLIne += new string(' ', InfoCols);
            middleLIne += "║";
            Console.Write(middleLIne);
        }

        string endLine = "╚";
        endLine += new string('═', TetrisCols);
        endLine += "╩";
        endLine += new string('═', InfoCols);
        endLine += "╝";
        Console.Write(endLine);


    }

    static void DrawInfo()
    {
        Write("Score:", 1, 3 + TetrisCols);
        Write(Score.ToString(), 2, 3 + TetrisCols);

    }


    static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Yellow)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(col, row);
        Console.Write(text);
        Console.ResetColor();
    }


}




