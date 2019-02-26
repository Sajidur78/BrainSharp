using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrainSharp
{
    class Program
    {
        public static byte[] Buffer = new byte[ushort.MaxValue];
        public static int Index;

        static void Main(string[] args)
        {
            if(args.Length < 1)
                return;

            string Program = File.ReadAllText(args[0]);
            Program = Regex.Replace(Program, "//.+", string.Empty);
            Program = Regex.Replace(Program, @"/\*.+\*/", string.Empty, RegexOptions.Singleline);
            for (int i = 0; i < Program.Length; i++)
            {
                char c = Program[i];
                switch (c)
                {
                    case '>':
                        Index++;
                        break;

                    case '<':
                        Index--;
                        break;

                    case '.':
                        Console.Write((char)Buffer[Index]);
                        break;

                    case ',':
                        Buffer[Index] = (byte)Console.ReadKey(true).KeyChar;
                        break;

                    case '+':
                        Buffer[Index]++;
                        break;

                    case '-':
                        Buffer[Index]--;
                        break;

                    case '[':
                    {
                        if (Buffer[Index] == 0)
                        {
                            int loop = 1;
                            while (loop > 0)
                            {
                                i++;
                                char ch = Program[i];
                                if (ch == '[')
                                {
                                    loop++;
                                }
                                else if (ch == ']')
                                {
                                    loop--;
                                }
                            }
                        }
                        break;
                    }

                    case ']':
                    {
                        int loop = 1;
                        while (loop > 0)
                            {
                                i--;
                                char ch = Program[i];
                                if (ch == '[')
                                {
                                    loop--;
                                }
                                else if (ch == ']')
                                {
                                    loop++;
                                }
                            }
                        i--;
                        break;
                    }

                    default:
                        break;
                }
            }
        }
    }
}
