using Desensitization.Desensitize.Attributes;

namespace Desensitization.Dtos
{

    [CustomRule(DesensitizionKey.PF, "CustomerId:equal(67)")]
    public class AccountDto
    {
        public long CustomerId { get; set; }

        public string CustomerName { get; set; }

        [RangeSideDisplay(DesensitizionKey.PF, 6, 4)]
        [RangeSideDisplay(4, 4, InlineConstraint = ":minlength(9)")]
        public string OutIDNumber { get; set; }

        [DesensitizationContainer]
        public DebtorDto Debtor { get; set; }

        public string AccountNumber { get; set; }

        [RegexReplace(@"(^.*$)", "***")]
        public string CityName { get; set; }

        [RegexReplace(@"(^.*$)", "***")]
        public string ProvinceName { get; set; }

    }
}