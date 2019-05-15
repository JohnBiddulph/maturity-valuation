using System;

namespace MaturityValuation
{
    public static class MaturityCalculator
    {
        public static decimal CalculateMaturityValue(Policy policy)
        {
            var premiumsMinusManagementFee = policy.TotalPremiumsPaid * CalculateManagementFeeModifier(policy);
            var discretionaryBonusAmountToAdd = CalculateDiscretionaryBonusAmountToAdd(policy);
            var valueBeforeUplift = premiumsMinusManagementFee + discretionaryBonusAmountToAdd;
            var upliftPercentage = policy.UpliftPercentage;
            var upliftModifier = 1M + upliftPercentage / 100M;

            return valueBeforeUplift * upliftModifier;
        }

        private static decimal CalculateManagementFeeModifier(Policy policy)
        {
            switch (policy.PolicyType)
            {
                case PolicyTypeEnum.TypeA:
                    return 0.97M;
                case PolicyTypeEnum.TypeB:
                    return 0.95M;
                case PolicyTypeEnum.TypeC:
                    return 0.93M;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static decimal CalculateDiscretionaryBonusAmountToAdd(Policy policy)
        {
            var discretionaryBonusAmount = policy.DiscretionaryBonusAmount;
            decimal discretionaryBonusAmountToAdd;
            switch (policy.PolicyType)
            {
                case PolicyTypeEnum.TypeA when policy.IsStartDateBefore1990:
                case PolicyTypeEnum.TypeB when policy.ConfersMembershipRights:
                case PolicyTypeEnum.TypeC when !policy.IsStartDateBefore1990 && policy.ConfersMembershipRights:
                    discretionaryBonusAmountToAdd = discretionaryBonusAmount;
                    break;
                default:
                    discretionaryBonusAmountToAdd = 0M;
                    break;
            }

            return discretionaryBonusAmountToAdd;
        }
    }
}