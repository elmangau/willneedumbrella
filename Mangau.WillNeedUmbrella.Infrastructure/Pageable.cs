using System;
using System.Collections.Generic;
using System.Text;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    #region PageRequest
    public class PageRequest
    {
        public const int MaxPerPage = 100;
        private int _page;
        private int _size;

        public PageRequest()
        {
            Page = 0;
            Size = MaxPerPage;
        }

        public PageRequest(int? page, int? size)
        {
            Page = page ?? 0;
            Size = size ?? MaxPerPage;
        }

        public int Page
        {
            get => _page;
            set => _page = Math.Max(0, value);
        }

        public int Size
        {
            get => _size;
            set => _size = Math.Max(1, Math.Min(value, MaxPerPage));
        }

        internal int PageIndex { get => Page * Size; }
    }
    #endregion

    #region PageResponse
    public class PageResponse<TEntity>
    {
        private readonly PageRequest _request;

        public PageResponse(PageRequest request, int totalCount, IEnumerable<TEntity> content)
        {
            _request = request;

            Content = content;
            TotalCount = totalCount;
            PageCount = (TotalCount / request.Size) + Math.Min(TotalCount % request.Size, 1);

            var tmp = PageCount - 1;

            PrevPage = Math.Max(0, request.Page - 1);
            NextPage = Math.Min(tmp, request.Page + 1);

            HasPrev = PrevPage > 0;
            HasNext = NextPage < tmp;
        }

        public int TotalCount { get; }

        public IEnumerable<TEntity> Content { get; }

        public bool HasNext { get; }

        public bool HasPrev { get; }

        public int PageCount { get; }

        public int NextPage { get; }

        public int PrevPage { get; }

        public PageRequest NextPageRequest { get => new PageRequest(NextPage, _request.Size); }

        public PageRequest PrevPageRequest { get => new PageRequest(PrevPage, _request.Size); }
    }
    #endregion
}
