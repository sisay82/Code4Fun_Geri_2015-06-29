using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Utility
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        ///  Fills an array of size N with the numbers from 1 to N in random order without duplicates
        /// </summary>
        /// <param name="arrayToFill">Array To fill</param>
        public static void RandomArrayFill(Object[] arrayToFill)
        {

            int n = arrayToFill.Length;

            Int32 currentNum = Int32.MinValue;
            Random randNum = new Random();

            //Input cleaning
            for (int i = 0; i < n; i++)
            {
                arrayToFill[i] = Int32.MinValue;
            }

            //Array filled by rondom number form 1 to n
            for (int i = 0; i < n; i++)
            {
                currentNum = randNum.Next(0, n) + 1;

                while (arrayToFill.Contains(currentNum))
                {
                    currentNum = randNum.Next(0, n) + 1;
                }

                arrayToFill[i] = currentNum;

            }


        }

        /// <summary>
        /// Encode every letter into its corresponding numbered position in alphabet
        /// </summary>
        /// <param name="value">String to encode</param>
        /// <returns>Encoded string</returns>
        public static String GetLatinLetterEncoded(String value)
        {
            String result = String.Empty;

            foreach (Char c in value.ToUpper())
            {
                if (c < 'A' || c > 'Z')
                {
                    result += c;
                }
                else
                {
                    result += (Convert.ToInt32(c) - Convert.ToInt32('A') + 1).ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// The brackets are correctly matched and each one is accounted for. Only round brackets are allowed.
        /// </summary>
        /// <param name="value">String to check</param>
        /// <returns></returns>
        public static Boolean CheckBackets(String value)
        {
            Stack<Char> bracketsStack = new Stack<char>(value.Length);
            foreach (Char c in value)
            {
                if (c == '(')
                    bracketsStack.Push(c);

                if (c == ')')
                {
                    if (bracketsStack.Count == 0) return false;

                    bracketsStack.Pop();
                }
            }

            if (bracketsStack.Count > 0) return false;

            return true;
        }

    }
}
