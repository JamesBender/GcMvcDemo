namespace EfDemo.Web.Models.ViewModels
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Artist { get; set; }
//        public Nullable<int> CDId { get; set; }
        public int? CDId { get; set; }

        public virtual CD CD { get; set; }
 
    }
}