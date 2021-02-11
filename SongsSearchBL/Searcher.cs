namespace SongSearchBL
{
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Store;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class Searcher
    {
        public static List<SongsReadModel> Search(SearchParameters searchParams, SearchStrategies strategy = SearchStrategies.MultiFieldParser, string indexDirectory = @"c:\Lucene\Index")
        {

            // Setting up the File system folder where to create the index.
            var indexDirectoryInfo = new DirectoryInfo(indexDirectory);
            using (var searcher = new IndexSearcher(FSDirectory.Open(indexDirectoryInfo.FullName), true))
            {
                TopDocs results = PerformSearch(searchParams, searcher, strategy);

                return MapSearchHitsToSongsReadModel(searcher, results);
            }
        }

        private static TopDocs PerformSearch(SearchParameters searchParams, IndexSearcher searcher, SearchStrategies strategy)
        {

            if (strategy == SearchStrategies.QueryParser)
            {
                return QueryParserSearch(searchParams.Terms, searcher);
            }
            else if (strategy == SearchStrategies.TermQuery)
            {
                return TermQuerySearch(searchParams.Terms, searcher);
            }
            else if (strategy == SearchStrategies.MultiFieldParser)
            {
                return MultipleFieldsSearch(searchParams.Terms, searcher);
            }
            else if (strategy == SearchStrategies.MultiFieldParserWithBooleanQuery)
            {
                return MultipleFieldsSearchWithBooleanQuery(searchParams.Terms, searcher);
            }
            else if (strategy == SearchStrategies.MultiFieldParserWithBooleanQueryAndSorting)
            {
                return MultipleFieldsSearchWithBooleanQueryWithSorting(searchParams.Terms, searcher);
            }
            else if (strategy == SearchStrategies.MultiFieldParserWithBooleanQueryAndSortingAndFiltering)
            {
                return MultipleFieldsSearchWithBooleanQueryWithSortingAndBooleanFilter(searchParams.Terms, searchParams.Language, searcher);
            }
            else
            {
                throw new Exception("Search strategy is invalid.");
            }
        }

        private static TopDocs QueryParserSearch(string[] terms, IndexSearcher searcher)
        {
            var parser = new QueryParser(
                Lucene.Net.Util.Version.LUCENE_30,
                SongFieldNames.FieldLyrics,
                new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));

            var results = searcher.Search(parser.Parse($"+{terms[0]}*"), 100);
            return results;
        }

        private static TopDocs TermQuerySearch(string[] terms, IndexSearcher searcher)
        {
            var query = new TermQuery(new Term(SongFieldNames.FieldLyrics, terms[0]));

            var results = searcher.Search(query, 100);
            return results;
        }

        private static TopDocs MultipleFieldsSearch(string[] terms, IndexSearcher searcher)
        {
            MultiFieldQueryParser queryParser = GetMultipleFieldParser();

            var results = searcher.Search(queryParser.Parse($"+{terms[0]}*"), 100);
            return results;
        }

        private static TopDocs MultipleFieldsSearchWithBooleanQuery(string[] terms, IndexSearcher searcher)
        {
            MultiFieldQueryParser queryParser = GetMultipleFieldParser();

            BooleanQuery query = GetBooleanQuery(terms, queryParser);
            var results = searcher.Search(query, 100);
            return results;
        }

        private static BooleanQuery GetBooleanQuery(string[] terms, MultiFieldQueryParser queryParser)
        {
            BooleanQuery query = BuildBooleanQuery(terms, queryParser);
            return query;
        }

        private static TopDocs MultipleFieldsSearchWithBooleanQueryWithSorting(string[] terms, IndexSearcher searcher)
        {
            MultiFieldQueryParser queryParser = GetMultipleFieldParser();

            BooleanQuery query = BuildBooleanQuery(terms, queryParser);
            var sort =  new Sort(new SortField(SongFieldNames.FieldReleaseDate, SortField.LONG, true));
            var results = searcher.Search(query, null, 100, sort);
            return results;
        }

        private static TopDocs MultipleFieldsSearchWithBooleanQueryWithSortingAndBooleanFilter(string[] terms, Languages languageFilter, IndexSearcher searcher)
        {
            MultiFieldQueryParser queryParser = GetMultipleFieldParser();

            BooleanQuery query = BuildBooleanQuery(terms, queryParser);

            BooleanFilter booleanFilter = new BooleanFilter();
            var filter = new TermsFilter();
            filter.AddTerm(new Term(SongFieldNames.FieldLanguage, Enum.GetName(typeof(Languages), languageFilter)));
            booleanFilter.Add(new FilterClause(filter, Occur.MUST));


            var sort = new Sort(new SortField(SongFieldNames.FieldReleaseDate, SortField.LONG, true));
            var results = searcher.Search(query, booleanFilter, 100, sort);

            //var explanationHighestScorer = searcher.Explain(query,
            //    results.ScoreDocs.First(d =>
            //    d.Score == results.ScoreDocs
            //    .Max(sd => sd.Score)).Doc);

            return results;
        }

        private static MultiFieldQueryParser GetMultipleFieldParser()
        {
            var searchFields = new string[]
            {
                     SongFieldNames.FieldTitle,
                     SongFieldNames.FieldArtist,
                     SongFieldNames.FieldLyrics,
                     SongFieldNames.FieldJson
            };

            MultiFieldQueryParser queryParser = new MultiFieldQueryParser(
                 Lucene.Net.Util.Version.LUCENE_30,
                 searchFields,
                 new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30))
            {
                DefaultOperator = QueryParser.Operator.AND,
            };

            queryParser.AllowLeadingWildcard = false;
            return queryParser;
        }

        private static BooleanQuery BuildBooleanQuery(string[] terms, MultiFieldQueryParser queryParser)
        {
            BooleanQuery termsBooleanQuery = new BooleanQuery();
            foreach (var term in terms)
            {
                termsBooleanQuery.Add(queryParser.Parse($"+{term}*~"), Occur.SHOULD);
            }

            return termsBooleanQuery;
        }


        private static List<SongsReadModel> MapSearchHitsToSongsReadModel(IndexSearcher searcher, TopDocs results)
        {
            List<SongsReadModel> songs = new List<SongsReadModel>();

            for (int i = 0; i < results.ScoreDocs.Length; i++)
            {
                var documentToParse = searcher.Doc(results.ScoreDocs[i].Doc);
                songs.Add(new SongsReadModel(documentToParse));
            }

            return songs;
        } 
    }
}
