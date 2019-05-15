using System.Collections.Generic;

namespace MaturityValuation
{
    public interface IMaturityFileReader
    {
        IList<string> GetLinesFromFile(string filePath);
    }
}