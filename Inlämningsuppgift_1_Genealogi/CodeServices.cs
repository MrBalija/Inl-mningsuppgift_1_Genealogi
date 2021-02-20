using System;
using System.Collections.Generic;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class CodeServices
    {
        public void ClearLastLine() //Clears last line.
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }
}
