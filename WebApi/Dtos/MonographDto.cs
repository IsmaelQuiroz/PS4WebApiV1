using Core.Entities;

namespace WebApi.Dtos
{
    public class MonographDto
    {
        public string Title { get; set; }
        public int Code { get; set; }
        public int Stock { get; set; }
        public string? Keyword { get; set; }
        public int CategoryName { get; set; }

    }
}
