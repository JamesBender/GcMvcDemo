namespace SqlDemo.Data.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Artist { get; set; }
        public virtual CD CD { get; set; } 
    }
}