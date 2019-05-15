using System.Collections.Generic;
using System.IO;

namespace MaturityValuation
{
    public class MaturityFileReader : IMaturityFileReader
    {
        private readonly IUserInterfaceManager _userInterface;

        public MaturityFileReader(IUserInterfaceManager userInterface)
        {
            _userInterface = userInterface;
        }

        public IList<string> GetLinesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _userInterface.ShowMessage(
                    $"The file was not found at {filePath}. " +
                    "Please check the file name and location and try again.");
                _userInterface.Close();
            }

            var lines = new List<string>();
            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}