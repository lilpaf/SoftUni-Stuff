using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiRental
{
    public class SkiRental
    {
        private string name;
        private int capacity;
        private List<Ski> data;

        public SkiRental(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            data = new List<Ski>();
        }

        public int Count => data.Count;
        public string Name
        {
            get => name; set => name = value;
        }
        public int Capacity
        {
            get => capacity; set => capacity = value;
        }

        public void Add(Ski ski)
        {
            if (Capacity > Count)
            {
                data.Add(ski);
            }
        }

        public bool Remove(string manufacturer, string model)
        {
            if (data.Any(x => x.Manufacturer == manufacturer && x.Model == model))
            {
                Ski skiRemove = data.First(x => x.Manufacturer == manufacturer && x.Model
                    == model);
                data.Remove(skiRemove);
                return true;
            }

            return false;
        }

        public Ski GetNewestSki()
        {
            if (data.Any())
            {
                return data.OrderByDescending(x => x.Year).First();
            }

            return null;
        }

        public Ski GetSki(string manufacturer, string model)
        {
            return data.FirstOrDefault(x => x.Manufacturer == manufacturer && x.Model == model);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"The skis stored in {Name}:");
            foreach (var ski in data)
            {
                sb.AppendLine(ski.ToString());
            }

            return sb.ToString().Trim();
        }

    }
}
