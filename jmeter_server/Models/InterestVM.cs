namespace jmeter_server.Models
{
    public class InterestVM
    {
        public InterestVM(double dollars)
        {
            Dollars = dollars;
        }

        public double Dollars { get; set; }
    }
}