using WebApplication1.EFModels;

namespace WebApplication1.Models.Dtos
{
    public class SpotsPagingDto
    {
        public int TotalPages { get; set; }
        public List<SpotImagesSpot> ?SpotsReslut { get; set; }
    }
}
