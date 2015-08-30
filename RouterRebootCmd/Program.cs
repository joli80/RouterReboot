using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouterRebootCmd
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                string usage = "RouterRebootCmd password [ip]";
                string ip = "192.168.0.1";
                string password = "";

                if (args.Length > 2 || args.Length < 1)
                {
                    Console.WriteLine(usage);
                    throw new ArgumentException("Invalid command line arguments");
                }

                password = args[0];
                if (args.Length == 2)
                    ip = args[1];


                DLinkScript.Application app = new DLinkScript.Application(ip);
                Console.WriteLine("Logging in...");
                app.Login(password);
                Console.WriteLine("Logged in");
                app.Reboot();
                Console.WriteLine("Rebooting...");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
