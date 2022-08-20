using System;
using System.IO;

namespace Interpreter{
    class Interpreter{
        static void Main(string[] args){
            // Initialize Machine.
            byte[] tape = new byte[30000]; // Data tape containing 30000 cells.
            int pointer = 0; // data pointer which operates on tape.
            int cursor = 0; // instruction cursor which reads the instructions.
            int bracketsCounter = 0; // counts unpaired brackets encountered.
            // 
            
            
            // check for the file path.
            if (args.Length == 0){
                Console.WriteLine("Source file path not entered!");
                return;
            }
            string path = args[0];
            // try to open the file and handle not being able to.

            if (!File.Exists(path)){
                Console.WriteLine("File does not exists!");
                return;
            }

            // read the file.
            StreamReader file = File.OpenText(path);
            string data = file.ReadToEnd();

            for (cursor = 0; cursor < data.Length; cursor++){
                switch(data[cursor]){
                    case('+'):
                        tape[pointer]++;
                        break;
                    case('-'):
                        tape[pointer]--;
                        break;
                    case('>'):
                        pointer++;
                        if (pointer > tape.Length){
                            pointer = 0;
                        }
                        break;
                    case('<'):
                        pointer--;
                        if (pointer < 0){
                            pointer = tape.Length;
                        }
                        break;
                    case('.'):
                        Console.Write((char)tape[pointer]);
                        break;
                    case(','):
                        tape[pointer] = (byte)Console.ReadKey().KeyChar;
                        break;
                    case('['):
                        if (tape[pointer] == 0){
                            bracketsCounter++;
                            while (data[cursor] != ']' || bracketsCounter != 0)
                            {
                                cursor++;

                                if (data[cursor] == '[')
                                {
                                    bracketsCounter++;
                                }
                                else if (data[cursor] == ']')
                                {
                                    bracketsCounter--;
                                }
                            }
                        }
                        break;
                    case(']'):
                        if (tape[pointer] != 0){
                            bracketsCounter++;
                            while (data[cursor] != '[' || bracketsCounter != 0)
                            {
                                cursor--;

                                if (data[cursor] == ']')
                                {
                                    bracketsCounter++;
                                }
                                else if (data[cursor] == '[')
                                {
                                    bracketsCounter--;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            
        }
    }

}