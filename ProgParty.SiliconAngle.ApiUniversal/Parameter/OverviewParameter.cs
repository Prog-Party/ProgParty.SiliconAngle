namespace ProgParty.SiliconAngle.ApiUniversal.Parameter
{
    public class OverviewParameter
    {
        public int Paging { get; set; }

        public bool StartOver { get; set; } = false;

        public ArticleCategory Category { get; set; } = ArticleCategory.All;
    }

    public enum ArticleCategory
    {
        All = 0,
        Cloud = 1,
        Mobile = 2,
        Social = 3,
        BigData = 4,
        BleedingEdge = 5
    }
}
