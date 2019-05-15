using System;

namespace MaturityValuation
{
    public class UserInterfaceManager : IUserInterfaceManager
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void Close()
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}