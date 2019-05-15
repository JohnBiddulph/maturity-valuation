using System.Collections.Generic;
using System.Xml;

namespace MaturityValuation
{
    public class MaturityFileWriter : IMaturityFileWriter
    {
        public void WriteValuedMaturitiesToFile(IEnumerable<ValuedMaturity> valuedMaturities, string fileName)
        {
            using (var xmlWriter = XmlWriter.Create(fileName))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("valuedMaturities");

                foreach (var valuedMaturity in valuedMaturities)
                {
                    xmlWriter.WriteStartElement("valuedMaturity");
                    xmlWriter.WriteAttributeString("policyNumber", valuedMaturity.PolicyNumber);
                    xmlWriter.WriteAttributeString("maturityValue", valuedMaturity.MaturityValue);
                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }
    }
}