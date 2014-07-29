namespace FnhDemo.Data.Entities
{
    public class DVD : BaseEntity
    {
        public virtual int RunningTime { get; set; }
        public virtual bool IsSpecialEdition { get; set; }
        public virtual string Synopsis { get; set; }
        public virtual string Title { get; set; }
        public virtual string Genre { get; set; }
        public virtual int Year { get; set; } 
    }
}