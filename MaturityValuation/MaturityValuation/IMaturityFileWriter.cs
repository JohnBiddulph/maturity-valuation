using System.Collections.Generic;

namespace MaturityValuation
{
    public interface IMaturityFileWriter
    {
        void WriteValuedMaturitiesToFile(IEnumerable<ValuedMaturity> valuedMaturities, string fileName);
    }
}