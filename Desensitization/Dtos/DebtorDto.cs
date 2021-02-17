using Desensitization.Desensitize.Attributes;

namespace Desensitization.Dtos
{
    public class DebtorDto
    {
        [RightHidden(DesensitizionKey.PF, 8)]
        [RangeSideDisplay(4, 4)]
        public string IDNumber { get; set; }

        [LeftDisplay(DesensitizionKey.PF, 1)]
        public string DebtorName { get; set; }
    }
}