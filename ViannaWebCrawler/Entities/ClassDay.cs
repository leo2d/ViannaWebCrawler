namespace ViannaWebCrawler.Entities
{
    public class ClassDay
    {
        public string DayOfWeek { get; set; }
        public TimeTableDiscipline FirstClass { get; set; }
        public TimeTableDiscipline SecondClass { get; set; }
        public TimeTableDiscipline ThirdClass { get; set; }
        public TimeTableDiscipline FourthClass { get; set; }
    }
}
