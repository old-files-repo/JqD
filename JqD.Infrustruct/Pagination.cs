using System;
using System.Collections.Generic;
using System.Linq;

namespace JqD.Infrustruct
{
    public static class Number
    {
        public static IEnumerable<int> Through(this int me, int guard)
        {
            while (me <= guard)
            {
                yield return me++;
            }
        }
    }

    public class Pagination
    {
        public const int PageNumber = 5;
        private const int MaxShouldBeDisplayedNumberOfPages = 5;

        public Pagination(int offset, int hitsPerPage, int totalResults)
        {
            FirstPage = 1;
            CurrentPage = offset / hitsPerPage + 1;
            LastPage = Math.Max(1, (totalResults + (hitsPerPage - 1)) / hitsPerPage);
            Total = totalResults;
            CalculateShouldBeDisplayedStartPageAndEndPage();
        }

        private void CalculateShouldBeDisplayedStartPageAndEndPage()
        {
            if (LastPage <= MaxShouldBeDisplayedNumberOfPages)
            {
                StartPage = FirstPage;
                EndPage = LastPage;
            }
            else
            {
                var numberOfLeftPartPages = MaxShouldBeDisplayedNumberOfPages / 2;
                StartPage = (CurrentPage - numberOfLeftPartPages) < FirstPage ? FirstPage : (CurrentPage - numberOfLeftPartPages);
                if (StartPage + MaxShouldBeDisplayedNumberOfPages > LastPage + 1)
                {
                    StartPage = LastPage - MaxShouldBeDisplayedNumberOfPages + 1;
                }
                EndPage = StartPage + MaxShouldBeDisplayedNumberOfPages - 1;
            }
        }

        public bool ShowFirstPage => StartPage > FirstPage;
        public bool ShowLastPage => EndPage < LastPage;
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int StartPage { get; set; }
        public int Total { get; set; }
        public int Previous => CurrentPage - 1;
        public int Next => CurrentPage + 1;
        public bool ShowNextPage => Next <= LastPage;
        public bool ShowPreviousPage => Previous >= FirstPage && Previous <= LastPage;

        public List<int> PageRange
        {
            get
            {
                var range = new List<int>();
                StartPage.Through(EndPage).ToList().ForEach(range.Add);
                return range;
            }
        }
        public int CurrentPage { get; set; }
        public int EndPage { get; set; }
    }
}
