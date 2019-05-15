using System;

namespace MaturityValuation
{
    public class Policy
    {
        public string PolicyNumber { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public decimal TotalPremiumsPaid { get; set; }
        public bool ConfersMembershipRights { get; set; }
        public decimal DiscretionaryBonusAmount { get; set; }
        public decimal UpliftPercentage { get; set; }

        public PolicyTypeEnum PolicyType
        {
            get
            {
                switch (PolicyNumber.ToUpper()[0])
                {
                    case 'A':
                        return PolicyTypeEnum.TypeA;
                    case 'B':
                        return PolicyTypeEnum.TypeB;
                    case 'C':
                        return PolicyTypeEnum.TypeC;
                    default:
                        throw new InvalidOperationException($"{nameof(PolicyNumber)} must begin with 'A', 'B' or 'C'");
                }
            }
        }

        public bool IsStartDateBefore1990 => PolicyStartDate.Year < 1990;
    }
}