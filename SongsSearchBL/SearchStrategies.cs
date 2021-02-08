
namespace SongSearchBL
{
    public enum SearchStrategies
    {
        MultiFieldParser = 0,
        TermQuery = 1,
        QueryParser = 2,
        MultiFieldParserWithBooleanQuery = 3,
        MultiFieldParserWithBooleanQueryAndSorting = 4,
        MultiFieldParserWithBooleanQueryAndSortingAndFiltering = 5,
    }
}
