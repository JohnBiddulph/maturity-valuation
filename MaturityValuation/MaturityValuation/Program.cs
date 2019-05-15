using System;
using System.Collections.Generic;
using System.Globalization;

namespace MaturityValuation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IUserInterfaceManager userInterface = new UserInterfaceManager();

            try
            {
                IPolicyMapper policyMapper = new PolicyMapper(userInterface);
                IMaturityFileReader maturityFileReader = new MaturityFileReader(userInterface);
                IMaturityFileWriter maturityFileWriter = new MaturityFileWriter();

                var csvInputFilePath = args[0];
                var xmlOutputFilePath = args[1];

                var lines = maturityFileReader.GetLinesFromFile(csvInputFilePath);
                var policies = policyMapper.MapPolicies(lines);
                var valuedMaturities = new List<ValuedMaturity>();

                foreach (var policy in policies)
                {
                    var policyNumber = policy.PolicyNumber;
                    var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);
                    var valuedMaturity = new ValuedMaturity()
                    {
                        PolicyNumber = policyNumber,
                        MaturityValue = maturityValue.ToString(CultureInfo.CurrentCulture)
                    };
                    valuedMaturities.Add(valuedMaturity);
                }

                maturityFileWriter.WriteValuedMaturitiesToFile(valuedMaturities, xmlOutputFilePath);
            }
            catch (Exception exception)
            {
                userInterface.ShowMessage(exception.ToString());
            }
            finally
            {
                userInterface.Close();
            }
        }
    }
}