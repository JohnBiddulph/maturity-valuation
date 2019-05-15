using System;
using MaturityValuation;
using NUnit.Framework;

namespace MaturityValuationTest
{
    [TestFixture]
    public class MaturityCalculatorTest
    {
        [TestCase("0", "0", "0", "0")]
        [TestCase("10000", "0", "0", "9700")]
        [TestCase("20000", "0", "0", "19400")]
        [TestCase("10000", "1000", "40", "14980")]
        [TestCase("10000", "1000", "30", "13910")]
        [TestCase("10000", "1000", "20", "12840")]
        [TestCase("10000", "1000", "0", "10700")]
        [TestCase("10000", "2000", "0", "11700")]
        public void Calculates_Correct_Value_For_Pre1990_TypeA(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "A1",
                PolicyStartDate = new DateTime(1986, 6, 1),
                ConfersMembershipRights = false
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", "0")]
        [TestCase("10000", "0", "0", "9700")]
        [TestCase("20000", "0", "0", "19400")]
        [TestCase("10000", "1000", "40", "13580")]
        [TestCase("10000", "1000", "30", "12610")]
        [TestCase("10000", "1000", "20", "11640")]
        [TestCase("10000", "1000", "0", "9700")]
        [TestCase("10000", "2000", "0", "9700")]
        [TestCase("12500", "1350", "37.5", "16671.875")]
        public void Calculates_Correct_Value_For_Post1990_TypeA(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "A1",
                PolicyStartDate = new DateTime(1992, 6, 1),
                ConfersMembershipRights = false,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("10000", "1000", "10", "11770")]
        [TestCase("10000", "2000", "10", "12870")]
        public void Calculates_Correct_Value_For_IncrementallyPre1990_TypeA(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "A1",
                PolicyStartDate = new DateTime(1990, 1, 1, 0, 0, 0, 0).AddMilliseconds(-1),
                ConfersMembershipRights = false,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("10000", "1000", "10", "10670")]
        [TestCase("10000", "2000", "10", "10670")]
        public void Calculates_Correct_Value_For_BeginningOf1990_TypeA(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "A1",
                PolicyStartDate = new DateTime(1990, 1, 1, 0, 0, 0, 0),
                ConfersMembershipRights = false,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", 1992, "0")]
        [TestCase("0", "0", "0", 1988, "0")]
        [TestCase("10000", "0", "0", 1992, "9500")]
        [TestCase("10000", "1000", "0", 1992, "9500")]
        [TestCase("10000", "0", "10", 1992, "10450")]
        [TestCase("10000", "0", "20", 1992, "11400")]
        [TestCase("10000", "0", "0", 1988, "9500")]
        [TestCase("10000", "1000", "0", 1988, "9500")]
        [TestCase("10000", "2000", "0", 1988, "9500")]
        [TestCase("10000", "1000", "10", 1992, "10450")]
        [TestCase("20000", "1000", "10", 1992, "20900")]
        [TestCase("10000", "2000", "20", 1988, "11400")]
        [TestCase("20000", "2000", "20", 1988, "22800")]
        public void Calculates_Correct_Value_For_NonMembership_TypeB(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            int yearOfInception,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "B1",
                PolicyStartDate = new DateTime(yearOfInception, 6, 1),
                ConfersMembershipRights = false,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", 1992, "0")]
        [TestCase("0", "0", "0", 1988, "0")]
        [TestCase("10000", "0", "0", 1992, "9500")]
        [TestCase("10000", "1000", "0", 1992, "10500")]
        [TestCase("10000", "0", "10", 1992, "10450")]
        [TestCase("10000", "0", "20", 1992, "11400")]
        [TestCase("10000", "0", "0", 1988, "9500")]
        [TestCase("10000", "1000", "0", 1988, "10500")]
        [TestCase("10000", "2000", "0", 1988, "11500")]
        [TestCase("10000", "1000", "10", 1992, "11550")]
        [TestCase("20000", "1000", "10", 1992, "22000")]
        [TestCase("10000", "2000", "20", 1988, "13800")]
        [TestCase("20000", "2000", "20", 1988, "25200")]
        public void Calculates_Correct_Value_For_Membership_TypeB(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            int yearOfInception,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "B1",
                PolicyStartDate = new DateTime(yearOfInception, 6, 1),
                ConfersMembershipRights = true,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", "0")]
        [TestCase("10000", "0", "0", "9300")]
        [TestCase("20000", "0", "0", "18600")]
        [TestCase("10000", "1000", "0", "9300")]
        [TestCase("10000", "2000", "0", "9300")]
        [TestCase("10000", "0", "10", "10230")]
        [TestCase("10000", "0", "20", "11160")]
        [TestCase("20000", "1000", "0", "18600")]
        [TestCase("20000", "2000", "0", "18600")]
        [TestCase("20000", "0", "10", "20460")]
        [TestCase("20000", "0", "20", "22320")]
        [TestCase("10000", "1000", "10", "10230")]
        public void Calculates_Correct_Value_For_Pre1990_NonMembership_TypeC(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "C1",
                PolicyStartDate = new DateTime(1975, 6, 1),
                ConfersMembershipRights = false,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", "0")]
        [TestCase("10000", "0", "0", "9300")]
        [TestCase("20000", "0", "0", "18600")]
        [TestCase("10000", "1000", "0", "9300")]
        [TestCase("10000", "2000", "0", "9300")]
        [TestCase("10000", "0", "10", "10230")]
        [TestCase("10000", "0", "20", "11160")]
        [TestCase("20000", "1000", "0", "18600")]
        [TestCase("20000", "2000", "0", "18600")]
        [TestCase("20000", "0", "10", "20460")]
        [TestCase("20000", "0", "20", "22320")]
        [TestCase("10000", "1000", "10", "10230")]
        public void Calculates_Correct_Value_For_Post1990_NonMembership_TypeC(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "C1",
                PolicyStartDate = new DateTime(2010, 6, 1),
                ConfersMembershipRights = false,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", "0")]
        [TestCase("10000", "0", "0", "9300")]
        [TestCase("20000", "0", "0", "18600")]
        [TestCase("10000", "1000", "0", "9300")]
        [TestCase("10000", "2000", "0", "9300")]
        [TestCase("10000", "0", "10", "10230")]
        [TestCase("10000", "0", "20", "11160")]
        [TestCase("20000", "1000", "0", "18600")]
        [TestCase("20000", "2000", "0", "18600")]
        [TestCase("20000", "0", "10", "20460")]
        [TestCase("20000", "0", "20", "22320")]
        [TestCase("10000", "1000", "10", "10230")]
        public void Calculates_Correct_Value_For_Pre1990_Membership_TypeC(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "C1",
                PolicyStartDate = new DateTime(1960, 6, 1),
                ConfersMembershipRights = true,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("0", "0", "0", "0")]
        [TestCase("10000", "0", "0", "9300")]
        [TestCase("20000", "0", "0", "18600")]
        [TestCase("10000", "1000", "0", "10300")]
        [TestCase("10000", "2000", "0", "11300")]
        [TestCase("10000", "0", "10", "10230")]
        [TestCase("10000", "0", "20", "11160")]
        [TestCase("20000", "1000", "0", "19600")]
        [TestCase("20000", "2000", "0", "20600")]
        [TestCase("20000", "0", "10", "20460")]
        [TestCase("20000", "0", "20", "22320")]
        [TestCase("10000", "1000", "10", "11330")]
        public void Calculates_Correct_Value_For_Post1990_Membership_TypeC(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "C1",
                PolicyStartDate = new DateTime(2005, 6, 1),
                ConfersMembershipRights = true,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }

        [TestCase("10000", "1000", "10", "10230")]
        [TestCase("10000", "2000", "10", "10230")]
        public void Calculates_Correct_Value_For_IncrementallyPre1990_Membership_TypeC(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "C1",
                PolicyStartDate = new DateTime(1990, 1, 1, 0, 0, 0, 0).AddMilliseconds(-1),
                ConfersMembershipRights = true,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
        
        [TestCase("10000", "1000", "10", "11330")]
        [TestCase("10000", "2000", "10", "12430")]
        public void Calculates_Correct_Value_For_BeginningOf1990_Membership_TypeC(
            string totalPremiumsPaid,
            string discretionaryBonusAmount,
            string upliftPercentage,
            string expectedResult)
        {
            // arrange
            var policy = new Policy()
            {
                PolicyNumber = "C1",
                PolicyStartDate = new DateTime(1990, 1, 1, 0, 0, 0, 0),
                ConfersMembershipRights = true,
            };
            policy.TotalPremiumsPaid = decimal.Parse(totalPremiumsPaid);
            policy.DiscretionaryBonusAmount = decimal.Parse(discretionaryBonusAmount);
            policy.UpliftPercentage = decimal.Parse(upliftPercentage);

            // act
            var maturityValue = MaturityCalculator.CalculateMaturityValue(policy);

            // assert
            Assert.That(maturityValue, Is.EqualTo(decimal.Parse(expectedResult)));
        }
    }
}