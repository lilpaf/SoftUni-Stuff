namespace Footballers.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Footballers.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            var coaches = context.Coaches
                .ToArray()// judge
                .Where(x => x.Footballers.Any())
                .Select(x => new CoachesWithTheirFootballersDto
                {
                    FootballersCount = x.Footballers.Count(),
                    CoachName = x.Name,
                    Footballers = x.Footballers.Select(x => new FootballersDtoExport
                    {
                        Name = x.Name,
                        Position = x.PositionType.ToString()
                    })
                    .OrderBy(x => x.Name)
                    .ToArray()
                })
                .OrderByDescending(x => x.Footballers.Count())
                .ThenBy(x => x.CoachName)
                .ToArray();

            var serializer = new XmlSerializer(coaches.GetType(), new XmlRootAttribute("Coaches"));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using var writer = new StringWriter();

            serializer.Serialize(writer, coaches, ns);

            return writer.ToString().TrimEnd();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams
                .ToArray()
                .Where(x => x.TeamsFootballers.Any(x => x.Footballer.ContractStartDate >= date))
                .OrderByDescending(x => x.TeamsFootballers
                    .Where(x => x.Footballer.ContractStartDate >= date).Count())
                .ThenBy(x => x.Name)
                .Take(5)
                .Select(x => new
                {
                    Name = x.Name,
                    Footballers = x.TeamsFootballers
                    .Where(x => x.Footballer.ContractStartDate >= date)
                    .OrderByDescending(x => x.Footballer.ContractEndDate)
                    .ThenBy(x => x.Footballer.Name)
                    .Select(x => new
                    {
                        FootballerName = x.Footballer.Name,
                        ContractStartDate = x.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                        ContractEndDate = x.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                        BestSkillType = x.Footballer.BestSkillType.ToString(),
                        PositionType = x.Footballer.PositionType.ToString(),
                    })
                    .ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }
}
