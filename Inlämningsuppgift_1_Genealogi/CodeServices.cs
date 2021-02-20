using System;
using System.Collections.Generic;
using System.Text;

namespace Inlämningsuppgift_1_Genealogi
{
    class CodeServices
    {
        private static string[] checkBox = new string[8];

        public static Array CheckBox()
        {
            checkBox[0] = "[ ]";
            checkBox[1] = "[ ]";
            checkBox[2] = "[ ]";
            checkBox[3] = "[ ]";
            checkBox[4] = "[ ]";
            checkBox[5] = "[ ]";
            checkBox[6] = "[ ]";
            checkBox[7] = "[ ]";
            checkBox[8] = "[ ]";

            return checkBox;
        }


        public void ClearLastLine() //Clears last line.
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

    }
}
