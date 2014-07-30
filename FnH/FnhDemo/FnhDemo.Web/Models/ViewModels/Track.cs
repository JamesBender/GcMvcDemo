namespace FnhDemo.Web.Models.ViewModels
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Artist { get; set; }
        public CD CD { get; set; }
        public int CdId { get; set; }
    }
}