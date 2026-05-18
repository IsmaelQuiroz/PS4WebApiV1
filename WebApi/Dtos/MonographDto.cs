using Core.Entities;

namespace WebApi.Dtos
{
    public class MonographDto
    {
        public string Title { get; set; }
        public int Code { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
        public string? Keyword { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }            

    }
}
