using System.Reflection.Metadata.Ecma335;

namespace WebApplication1.Models.Dtos
{
    public class SearchDto
    {
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; } = 0;
        public string? SortBy { get; set; }
        public string? SortType { get; set; } = "asc";

        public int? Page { get; set; } = 1;

        public int? PageSize { get; set; } = 9;

    }
}
