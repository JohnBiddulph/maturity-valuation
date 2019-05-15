using System.Collections.Generic;

namespace MaturityValuation
{
    public interface IPolicyMapper
    {
        IEnumerable<Policy> MapPolicies(IList<string> lines);
    }
}