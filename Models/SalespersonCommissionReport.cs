namespace Models
{
    //class containing information we want to display in DisplayCommissionReportsView
    public class SalespersonCommissionReport
    {
        public int SalespersonId { get; set; }
        public string SalespersonFullName { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public float SalesCommissionAmount { get; set; }

        public string QuarterYear
        {
            get
            {
                return Quarter.ToString() + " " + Year.ToString();
            }
        }
    }
}