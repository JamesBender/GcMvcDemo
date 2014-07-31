namespace SqlDemo.Web.Models.ViewModels
{
    public class DVD
    {
        public int Id { get; set; }
        public int RunningTime { get; set; }
        public bool IsSpecialEdition { get; set; }
        public string Synopsis { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }
}
