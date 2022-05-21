namespace SkiRental
{
    public class Ski
    {
        private string manufacter;
        private string model;
        private int year;


        public Ski(string manufacter, string model, int year)
        {
            Manufacturer = manufacter;
            Model = model;
            Year = year;
        }

        public override string ToString()
        {
            return $"{manufacter} - {model} - {year}";
        }

        public string Manufacturer { get => manufacter; set => manufacter = value; }
        public string Model { get => model; set => model = value; }
        public int Year { get => year; set => year = value; }
    }
}
