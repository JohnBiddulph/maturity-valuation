using System;
using System.Collections.Generic;
using System.Globalization;

namespace MaturityValuation
{
    public class PolicyMapper : IPolicyMapper
    {
        private readonly IUserInterfaceManager _userInterface;

        public PolicyMapper(IUserInterfaceManager userInterface)
        {
            _userInterface = userInterface;
        }

        public IEnumerable<Policy> MapPolicies(IList<string> lines)
        {
            int lineNumber = 0;

            // Check the header contains the correct field names
            var header = lines[0];
            lineNumber++;
            CheckHeader(header);
            lines.RemoveAt(0);

            var policies = new List<Policy>();
            foreach (var line in lines)
            {
                lineNumber++;
                var policy = MapPolicy(line, lineNumber);
                if (policy != null)
                {
                    policies.Add(policy);
                }
            }

            _userInterface.ShowMessage(
                $"{policies.Count} of {lineNumber - 1} policies successfully read from file.");

            return policies;
        }

        private Policy MapPolicy(string line, int lineNumber)
        {
            const int policyNumberIndex = 0;
            const int policyStartDateIndex = 1;
            const int premiumsIndex = 2;
            const int membershipIndex = 3;
            const int discretionaryBonusIndex = 4;
            const int upliftPercentageIndex = 5;

            var values = line.Split(',');

            int invalidFieldCount = 0;

            var policyNumber = values[policyNumberIndex];
            var policyInitial = policyNumber.ToUpper()[0];
            if (policyInitial != 'A' && policyInitial != 'B' && policyInitial != 'C')
            {
                _userInterface.ShowMessage(
                    $"Error on Line {lineNumber}: The value of '{InputFileFieldNames.PolicyNumber}' was invalid. " +
                    $"'{InputFileFieldNames.PolicyNumber}' must start with 'A', 'B' or 'C'.");
                invalidFieldCount++;
            }

            const string dateFormatString = "dd/MM/yyyy";
            var unitedKingdomCultureInfo = new CultureInfo("en-GB");
            if (!DateTime.TryParseExact(values[policyStartDateIndex], dateFormatString, unitedKingdomCultureInfo,
                DateTimeStyles.None, out var policyStartDate))
            {
                _userInterface.ShowMessage(
                    $"Error on Line {lineNumber}: The value of '{InputFileFieldNames.PolicyStartDate}' was invalid. " +
                    $"'{InputFileFieldNames.PolicyStartDate}' must be a date in the format 'DD/MM/YYYY'.");
                invalidFieldCount++;
            }

            if (!decimal.TryParse(values[premiumsIndex], out var premiums))
            {
                _userInterface.ShowMessage(
                    $"Error on Line {lineNumber}: The value of '{InputFileFieldNames.Premiums}' was not a valid decimal number.");
                invalidFieldCount++;
            }

            bool membership;
            switch (values[membershipIndex].ToUpper())
            {
                case "Y":
                    membership = true;
                    break;
                case "N":
                    membership = false;
                    break;
                default:
                    _userInterface.ShowMessage(
                        $"Error on Line {lineNumber}: The value of '{InputFileFieldNames.Membership}' was invalid. " +
                        "Valid values are 'Y' and 'N'.");
                    invalidFieldCount++;

                    // set membership to false here as the compiler doesn't know UserInterface.Close() exits the app
                    membership = false;
                    break;
            }

            if (!decimal.TryParse(values[discretionaryBonusIndex], out var discretionaryBonus))
            {
                _userInterface.ShowMessage(
                    $"Error on Line {lineNumber}: The value of '{InputFileFieldNames.DiscretionaryBonus}' was not a valid decimal number.");
                invalidFieldCount++;
            }

            if (!decimal.TryParse(values[upliftPercentageIndex], out var upliftPercentage))
            {
                _userInterface.ShowMessage(
                    $"Error on Line {lineNumber}: The value of '{InputFileFieldNames.UpliftPercentage}' was not a valid decimal number.");
                invalidFieldCount++;
            }

            if (invalidFieldCount > 0)
            {
                return null;
            }

            return new Policy()
            {
                PolicyNumber = policyNumber,
                PolicyStartDate = policyStartDate,
                TotalPremiumsPaid = premiums,
                ConfersMembershipRights = membership,
                DiscretionaryBonusAmount = discretionaryBonus,
                UpliftPercentage = upliftPercentage
            };
        }

        private void CheckHeader(string header)
        {
            var expectedHeader =
                $"{InputFileFieldNames.PolicyNumber}," +
                $"{InputFileFieldNames.PolicyStartDate}," +
                $"{InputFileFieldNames.Premiums}," +
                $"{InputFileFieldNames.Membership}," +
                $"{InputFileFieldNames.DiscretionaryBonus}," +
                $"{InputFileFieldNames.UpliftPercentage}";
            if (!string.Equals(header, expectedHeader))
            {
                _userInterface.ShowMessage("Warning: The input file header is not as expected.");
                _userInterface.ShowMessage($"Expected: '{expectedHeader}'");
                _userInterface.ShowMessage($"Found: '{header}'");
            }
        }
    }
}