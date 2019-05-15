# royal-london-code-test
Maturity Valuation calculator for Royal London Candidate Coding Test

Processes a list of maturing policies from a CSV file and produces an XML file of maturity values by policy number.

Run MaturityValuation.exe with two command-line arguments:
	1. The path of the CSV file containing policies to be processed (including the filename and extension)
	2. The intended path for the generated XML file (including the filename)

i.e. "MaturityValuation.exe <input file path> <output file path>"

The input file should be a CSV file, with one line per policy, and an additional line at the beginning of the file containing the header.
The values on each line should be separated by commas.
The first line should be as follows:
	"policy_number,policy_start_date,premiums,membership,discretionary_bonus,uplift_percentage"

Each subsequent line should contain the following fields, in order:
	1. policy_number:
		The unique policy identifier.
		An alphanumeric sequence beginning with "A", "B" or "C".
		e.g. "B1234567890"
	2. policy_start_date:
		The date the policy was taken out.
		A 2-digit number for the day of month, a 2-digit number for the month, and a 4-digit number for the year, separated by forward slashes ('/').
		e.g. "13/03/2013" for the 13th of March 2013
	3. premiums
		The total premiums the policyholder has paid over the lifetime of the policy.
		This must be numeric and may optionally contain one decimal point ('.'). It must not contain any letters or special characters.
		e.g. "17000.50"
	4. membership
		A single character denoting whether or not the policy confers membership rights.
		This must be either "Y" (denoting "Yes, the policy does confer membership rights") or "N" (denoting "No, the policy does not confer membership rights").
		e.g. "N"
	5. discretionary_bonus
		The amount of the one-off cash bonus that may be added to the policy's value dependent on certain criteria.
		This must be numeric and may optionally contain one decimal point ('.'). It must not contain any letters or special characters.
		e.g. "1234.56"
	6. uplift_percentage
		The percentage bonus amount applied to the policy value at maturity, expressed as a percentage.
		This must be numeric and may optionally contain one decimal point ('.'). It must not contain any letters or special characters.
		e.g. "25" for a 25% uplift
	
Dependencies:
	This solution makes use of the following NuGet package:
		Name:		NUnit 3 (version 3.11.0),
		Licence:	MIT licence (https://github.com/nunit/nunit/blob/master/LICENSE.txt)
		Purpose:	Unit tests for the code that calculates policy values.