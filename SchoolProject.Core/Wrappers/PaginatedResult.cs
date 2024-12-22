namespace SchoolProject.Core.Wrappers
{
	public class PaginatedResult<T>
	{
		public List<T> Data { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPage { get; set; }
		public int TotalCount { get; set; }
		public object Meta { get; set; }
		public int PageSize { get; set; }
		public bool HasPreviousPage => CurrentPage > 1;
		public bool HasNextPage => CurrentPage < TotalPage;
		public List<string> Message { get; set; } = new();
		public bool Succeded { get; set; }

		public PaginatedResult(List<T> data)
		{
			Data = data;
		}
		public PaginatedResult(bool succeded, List<T> data = default, List<string> message = null, int count = 0, int page = 1, int pageSize = 10)
		{
			Data = data;
			CurrentPage = page;
			Succeded = succeded;
			PageSize = pageSize;
			TotalPage = (int)Math.Ceiling(count / (double)PageSize);
			TotalCount = count;
		}
		public static PaginatedResult<T> Success(List<T> data, int count, int page, int PageSize)
		{
			return new(true, data, null, count, page, PageSize);
		}

	}
}
