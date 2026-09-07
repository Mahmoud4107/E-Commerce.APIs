namespace E_Commerce.APIs.Helpers
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; } = new List<T>();

        public Pagination(int pageSize,int pageIndex,IEnumerable<T> data,int count)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;
        }

    }
}
