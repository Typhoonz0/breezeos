using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Sys = Cosmos.System;
using System.Net.Http;
using System.Threading.Tasks;
using Cosmos.System.Graphics;
using System.Drawing;
using static Cosmos.HAL.BlockDevice.ATA_PIO;


namespace BreezeOS
{
    public class Kernel : Sys.Kernel
    {
        private static Sys.FileSystem.CosmosVFS FS;
        public static string file;
        String version = "0.0.1";

        protected override void BeforeRun()
        {
            FS = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(FS);
            FS.Initialize(true);
            Directory.SetCurrentDirectory(@"0:\\");
            Console.Clear();

            Console.WriteLine(".-----------------------------------------.\r\n| ____                         ___  ____  |\r\n|| __ ) _ __ ___  ___ _______ / _ \\/ ___| |\r\n||  _ \\| '__/ _ \\/ _ \\_  / _ \\ | | \\___ \\ |\r\n|| |_) | | |  __/  __// /  __/ |_| |___) ||\r\n||____/|_|  \\___|\\___/___\\___|\\___/|____/ |\r\n'-----------------------------------------'");
            Console.WriteLine("BreezeOS by xLiam 1.0");
            Console.WriteLine("Run help to get started.");
        }


        protected override void Run()
        {
            string input;
            input = Console.ReadLine();

            void MainMenu()

            {
                Console.Clear();

                Console.WriteLine(".-----------------------------------------.\r\n| ____                         ___  ____  |\r\n|| __ ) _ __ ___  ___ _______ / _ \\/ ___| |\r\n||  _ \\| '__/ _ \\/ _ \\_  / _ \\ | | \\___ \\ |\r\n|| |_) | | |  __/  __// /  __/ |_| |___) ||\r\n||____/|_|  \\___|\\___/___\\___|\\___/|____/ |\r\n'-----------------------------------------'");
                Console.WriteLine("BreezeOS by xLiam 1.0");
                Console.WriteLine("Run help to get started.");

            }
            void createdir()
            {
                Console.Write("Directory name: ");
                var dir = Console.ReadLine();
                Directory.CreateDirectory(dir);
                MainMenu();
            }

            void deldir()
            {
                Console.Write("Directory name: ");
                var dir = Console.ReadLine();
                try
                {
                    if (dir.Contains(@"\"))
                    {
                        Directory.Delete(dir);
                    }
                    else
                    {

                        if (Directory.GetCurrentDirectory().Contains(@"\"))
                        {
                            Directory.Delete(Directory.GetCurrentDirectory().Remove(Directory.GetCurrentDirectory().Length - 1) + @"\" + dir);
                        }
                        else
                        {
                            Directory.Delete(Directory.GetCurrentDirectory() + @"\" + dir);
                        }

                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Directory not found!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                MainMenu();
            }


            void cd()
            {
                // input directory
                Console.Write("Directory: ");
                var dir = Console.ReadLine();

                try
                {

                    // if dir is ..
                    if (dir == "..")
                    {
                        if (Directory.GetCurrentDirectory() == @"0:\\")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You are already in the root directory!");
                            Console.ForegroundColor = ConsoleColor.White;
                            MainMenu();
                        }
                        else
                        {
                            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);

                        }
                    }
                    else
                    {
                        // if dir is \
                        if (dir.Contains(@"\"))
                        {
                            // go to dir
                            Directory.SetCurrentDirectory(dir);
                        }
                        else
                        {
                            // go to dir in current directory
                            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + @"\" + dir);
                        }
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Directory not found!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                MainMenu();
            }


            void delfile()

            {
                Console.Clear();

                Console.WriteLine(".-----------------------------------------.\r\n| ____                         ___  ____  |\r\n|| __ ) _ __ ___  ___ _______ / _ \\/ ___| |\r\n||  _ \\| '__/ _ \\/ _ \\_  / _ \\ | | \\___ \\ |\r\n|| |_) | | |  __/  __// /  __/ |_| |___) ||\r\n||____/|_|  \\___|\\___/___\\___|\\___/|____/ |\r\n'-----------------------------------------'");
                Console.WriteLine("File deleted.");
                Console.WriteLine("Run help to get a list of commands.");

            }

            if ((input == "cdir"))
            {
                createdir();

            }
            if ((input == "ddir"))
            {
                deldir();

            }

            if ((input == "cd"))
            {
                cd();

            }

            if (input == "hello")

            { Console.WriteLine("Hello World"); }

            if (input == "about")

            {
                Console.Clear();
                Console.WriteLine("    _    _                 _   \r\n   / \\  | |__   ___  _   _| |_ \r\n  / _ \\ | '_ \\ / _ \\| | | | __|\r\n / ___ \\| |_) | (_) | |_| | |_ \r\n/_/   \\_\\_.__/ \\___/ \\__,_|\\__|");
                Console.WriteLine("BreezeOS - a simple terminal based OS.");
                Console.WriteLine("BreezeOS 1.0 is built on COSMOS and .NET frameworks.");
                Console.WriteLine("BreezeOS is made by xLiam.");
                Console.WriteLine("Vim was made by Bram Moolanaar.");
                Console.WriteLine("If there are updates available, contact xliam1 on discord to download. ");


            }

            if (input == "shutdown")

            {
                Cosmos.System.Power.Shutdown();

            }

            if (input == "specs")

            {
                Console.Clear();
                Console.WriteLine(" ____                      \r\n/ ___| _ __   ___  ___ ___ \r\n\\___ \\| '_ \\ / _ \\/ __/ __|\r\n ___) | |_) |  __/ (__\\__ \\\r\n|____/| .__/ \\___|\\___|___/\r\n      |_|                  ");
                Console.WriteLine("CPU: " + Cosmos.Core.CPU.GetCPUBrandString());
                Console.WriteLine("RAM: " + Cosmos.Core.CPU.GetAmountOfRAM() + "MB");


            }

            if (input == "vim")
            {
                MIV.StartMIV();
            }

            if (input == "v")
            {
                MIV.StartMIV();
            }

            if (input == "h")

            {
                Console.Clear();
                Console.WriteLine(" _   _      _       \r\n| | | | ___| |_ __  \r\n| |_| |/ _ \\ | '_ \\ \r\n|  _  |  __/ | |_) |\r\n|_| |_|\\___|_| .__/ \r\n             |_|    ");
                Console.WriteLine("PRESS D TO SHOW DESKTOP");
                Console.WriteLine("APPLICATIONS:");
                Console.WriteLine("Run help -a to access applications");
                Console.WriteLine("DIRECTORY FUNCTIONS:");
                Console.WriteLine("cdir - create a directory");
                Console.WriteLine("ddir - delete a directory");
                Console.WriteLine("cd - go to a directory");
                Console.WriteLine("delfile - delete a file");
                Console.WriteLine("file (or f) - a file explorer");
                Console.WriteLine("If you need further help with files, run help -f");
                Console.WriteLine("TERMINAL FUNCTIONS:");
                Console.WriteLine("hello - hello world");
                Console.WriteLine("about - about BreezeOS ");
                Console.WriteLine("clear (or c) - clear terminal ");
                Console.WriteLine("specs - get specs of your computer");
                Console.WriteLine("shutdown - turn off system ");

            }


            if (input == "help")

            {
                Console.Clear();
                Console.WriteLine(" _   _      _       \r\n| | | | ___| |_ __  \r\n| |_| |/ _ \\ | '_ \\ \r\n|  _  |  __/ | |_) |\r\n|_| |_|\\___|_| .__/ \r\n             |_|    ");
                Console.WriteLine("PRESS D TO SHOW DESKTOP");
                Console.WriteLine("APPLICATIONS:");
                Console.WriteLine("Run help -a to access applications");
                Console.WriteLine("DIRECTORY FUNCTIONS:");
                Console.WriteLine("cdir - create a directory");
                Console.WriteLine("ddir - delete a directory");
                Console.WriteLine("cd - go to a directory");
                Console.WriteLine("delfile - delete a file");
                Console.WriteLine("file (or f) - a file explorer");
                Console.WriteLine("If you need further help with files, run help -f");
                Console.WriteLine("TERMINAL FUNCTIONS:");
                Console.WriteLine("hello - hello world");
                Console.WriteLine("about - about BreezeOS ");
                Console.WriteLine("clear (or c) - clear terminal ");
                Console.WriteLine("specs - get specs of your computer");
                Console.WriteLine("shutdown - turn off system ");

            }

            if (input == "a")

            {
                Console.Clear();
                Console.WriteLine("    _                  _     _ _                          \r\n   / \\   _ __  _ __   | |   (_) |__  _ __ __ _ _ __ _   _ \r\n  / _ \\ | '_ \\| '_ \\  | |   | | '_ \\| '__/ _` | '__| | | |\r\n / ___ \\| |_) | |_) | | |___| | |_) | | | (_| | |  | |_| |\r\n/_/   \\_\\ .__/| .__/  |_____|_|_.__/|_|  \\__,_|_|   \\__, |\r\n        |_|   |_|                                   |___/ ");
                Console.WriteLine("(Tip: Just run the hotkeys from the desktop after you know them.)");
                Console.WriteLine("vim (or v) - A retro text editor");
                Console.WriteLine("calc (or ca) - A calculator");
                Console.WriteLine("clock (or cl) - A simple clock");
                Console.WriteLine("file (or f) - A file explorer");
                


            }

            if ((input == "clock"))
            {
                Console.Clear();
                Console.WriteLine(" _____ _                   ______        _       \r\n|_   _(_)_ __ ___   ___   / /  _ \\  __ _| |_ ___ \r\n  | | | | '_ ` _ \\ / _ \\ / /| | | |/ _` | __/ _ \\\r\n  | | | | | | | | |  __// / | |_| | (_| | ||  __/\r\n  |_| |_|_| |_| |_|\\___/_/  |____/ \\__,_|\\__\\___|");
                Console.WriteLine("Time: " + DateTime.Now.ToString("HH:mm:ss"));
                Console.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy"));
            }

            if ((input == "cl"))
            {
                Console.Clear();
                Console.WriteLine(" _____ _                   ______        _       \r\n|_   _(_)_ __ ___   ___   / /  _ \\  __ _| |_ ___ \r\n  | | | | '_ ` _ \\ / _ \\ / /| | | |/ _` | __/ _ \\\r\n  | | | | | | | | |  __// / | |_| | (_| | ||  __/\r\n  |_| |_|_| |_| |_|\\___/_/  |____/ \\__,_|\\__\\___|");
                Console.WriteLine("Time: " + DateTime.Now.ToString("HH:mm:ss"));
                Console.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy"));
            }

            if ((input == "d"))
            {

                MainMenu();

            }

            if ((input == "clear"))
            {

                MainMenu();

            }

            if ((input == "c"))
            {
                MainMenu();

            }

             if (input == "calc")
            {
                double a, b;
                char c;
                Console.Clear();

                Console.WriteLine("  ____      _            _       _             \r\n / ___|__ _| | ___ _   _| | __ _| |_ ___  _ __ \r\n| |   / _` | |/ __| | | | |/ _` | __/ _ \\| '__|\r\n| |__| (_| | | (__| |_| | | (_| | || (_) | |   \r\n \\____\\__,_|_|\\___|\\__,_|_|\\__,_|\\__\\___/|_|   ");
                Console.Write("Enter First Number: ");
                a = Double.Parse(Console.ReadLine());
                Console.Write("Enter Second Number: ");
                b = Double.Parse(Console.ReadLine());
                Console.Write("Enter Sign (+ - * /): ");
                c = Char.Parse(Console.ReadLine());
                switch (c)
                {
                    case '+':
                        Console.WriteLine("{0}+{1}={2}", a, b, a + b);
                        break;
                    case '-':
                        Console.WriteLine("{0}-{1}={2}", a, b, a - b);
                        break;
                    case '*':
                        Console.WriteLine("{0}*{1}={2}", a, b, a * b);
                        break;
                    case '/':
                        Console.WriteLine("{0}/{1}={2}", a, b, a / b);
                        break;
                    default:
                        Console.WriteLine("Unknown sign!");
                        break;
                }
            }

            if (input == "ca")
            {
                double a, b;
                char c;
                Console.Clear();

                Console.WriteLine("  ____      _            _       _             \r\n / ___|__ _| | ___ _   _| | __ _| |_ ___  _ __ \r\n| |   / _` | |/ __| | | | |/ _` | __/ _ \\| '__|\r\n| |__| (_| | | (__| |_| | | (_| | || (_) | |   \r\n \\____\\__,_|_|\\___|\\__,_|_|\\__,_|\\__\\___/|_|   ");
                Console.Write("Enter First Number: ");
                a = Double.Parse(Console.ReadLine());
                Console.Write("Enter Second Number: ");
                b = Double.Parse(Console.ReadLine());
                Console.Write("Enter Sign (+ - * /): ");
                c = Char.Parse(Console.ReadLine());
                switch (c)
                {
                    case '+':
                        Console.WriteLine("{0}+{1}={2}", a, b, a + b);
                        break;
                    case '-':
                        Console.WriteLine("{0}-{1}={2}", a, b, a - b);
                        break;
                    case '*':
                        Console.WriteLine("{0}*{1}={2}", a, b, a * b);
                        break;
                    case '/':
                        Console.WriteLine("{0}/{1}={2}", a, b, a / b);
                        break;
                    default:
                        Console.WriteLine("Unknown sign!");
                        break;
                }
            }

            if ((input == "delfile"))
            {
                Console.WriteLine(" ____       _      _         _____ _ _      \r\n|  _ \\  ___| | ___| |_ ___  |  ___(_) | ___ \r\n| | | |/ _ \\ |/ _ \\ __/ _ \\ | |_  | | |/ _ \\\r\n| |_| |  __/ |  __/ ||  __/ |  _| | | |  __/\r\n|____/ \\___|_|\\___|\\__\\___| |_|   |_|_|\\___|");
                Console.WriteLine("File name: ");
                var file = Console.ReadLine();
                try
                {
                    if (file.Contains(@"\"))
                    {
                        File.Delete(file);
                    }
                    else
                    {
                        if (Directory.GetCurrentDirectory().Contains(@"\"))
                        {
                            File.Delete(Directory.GetCurrentDirectory().Remove(Directory.GetCurrentDirectory().Length - 1) + @"\" + file);
                        }
                        else
                        {
                            File.Delete(Directory.GetCurrentDirectory() + @"\" + file);
                        }
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("File not found!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                delfile();
            }

            if ((input == "help -f"))
            {
                Console.Clear();
                Console.WriteLine(" _____ _ _             _   _      _       \r\n|  ___(_) | ___  ___  | | | | ___| |_ __  \r\n| |_  | | |/ _ \\/ __| | |_| |/ _ \\ | '_ \\ \r\n|  _| | | |  __/\\__ \\ |  _  |  __/ | |_) |\r\n|_|   |_|_|\\___||___/ |_| |_|\\___|_| .__/ \r\n                                   |_|    ");
                Console.WriteLine("Backslashes are directories. To use them, run cd and type the directory name.");
                Console.WriteLine("If you want to get back to the root directory, run cd and then type ..");
                Console.WriteLine("If you are in a different directory to the root directory and there are no files, its normal. Just run vim and make a file in the different directory.");
                Console.WriteLine("If you are in the root directory and nothing is showing up, power off and power on your computer/virtual machine.");
            }


            if ((input == "file"))
            {
                try
                {
                    Console.Clear();
                    // list folders and files in current directory
                    var folders = Directory.GetDirectories(Directory.GetCurrentDirectory());
                    var files = Directory.GetFiles(Directory.GetCurrentDirectory());
                    Console.Clear();
                    Console.WriteLine(" _____ _ _           \r\n|  ___(_) | ___  ___ \r\n| |_  | | |/ _ \\/ __|\r\n|  _| | | |  __/\\__ \\\r\n|_|   |_|_|\\___||___/");
                    Console.WriteLine("If you need help in the Files app, run help -f");
                    foreach (var folder in folders)
                    {
                        Console.WriteLine(@"\" + folder);
                    }
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                    }
                }
                catch (Exception)
                {
                    // Red color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fatal error!");
                    Console.ForegroundColor = ConsoleColor.White;
                    MainMenu();
                }
            }

            if ((input == "f"))
            {
                try
                {
                    Console.Clear();
                    // list folders and files in current directory
                    var folders = Directory.GetDirectories(Directory.GetCurrentDirectory());
                    var files = Directory.GetFiles(Directory.GetCurrentDirectory());
                    Console.Clear();
                    Console.WriteLine(" _____ _ _           \r\n|  ___(_) | ___  ___ \r\n| |_  | | |/ _ \\/ __|\r\n|  _| | | |  __/\\__ \\\r\n|_|   |_|_|\\___||___/");
                    Console.WriteLine("If you need help in the Files app, run help -f");
                    foreach (var folder in folders)
                    {
                        Console.WriteLine(@"\" + folder);
                    }
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                    }
                }
                catch (Exception)
                {
                    // Red color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Fatal error!");
                    Console.ForegroundColor = ConsoleColor.White;
                    MainMenu();
                }
            }


        }
    }
}
