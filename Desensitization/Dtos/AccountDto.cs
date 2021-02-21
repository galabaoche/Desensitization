using Desensitization.Desensitize.Attributes;

namespace Desensitization.Dtos
{
    [CustomRule("test", "CustomerName:equal(来自月球的客户)")]
    [CustomRule(DesensitizionKey.PF, "CustomerId:equal(67)")]
    public class AccountDto
    {
        public long CustomerId { get; set; }

        public string CustomerName { get; set; }

        [RangeSideDisplay(DesensitizionKey.PF, 6, 4)]
        [RangeSideDisplay(4, 4, InlineConstraint = ":minlength(9)")]
        [RightDisplay("test", 6,Order =9999)]
        public string OutIDNumber { get; set; }

        [DesensitizationContainer]
        public DebtorDto Debtor { get; set; }
        [RangeMiddleHiddenAttribute(4)]
        public string AccountNumber { get; set; }

        [RegexReplace(@"(^.*$)", "***")]
        public string CityName { get; set; }

        [RegexReplace(@"(^.*$)", "***")]
        public string ProvinceName { get; set; }

    }
}