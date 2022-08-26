namespace Footballers.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using System.Linq;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportCoachesDto[]), new XmlRootAttribute("Coaches"));

            using var reader = new StringReader(xmlString);

            var coachesDto = (ImportCoachesDto[])serializer.Deserialize(reader);

            List<Coach> coaches = new List<Coach>();

            StringBuilder sb = new StringBuilder();

            foreach (var coachDto in coachesDto)
            {
                
                if (!IsValid(coachDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var coach = new Coach
                {
                    Name = coachDto.Name,
                    Nationality = coachDto.Nationality
                };

                bool isContractStartDateValid;
                DateTime validContractStartDate;

                bool isContractEndDateValid;
                DateTime validContractEndDate;

                foreach (var footballerDto in coachDto.Footballers)
                {
                    
                    isContractStartDateValid = DateTime.TryParseExact(footballerDto.ContractStartDate, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validContractStartDate);

                    isContractEndDateValid = DateTime.TryParseExact(footballerDto.ContractEndDate, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validContractEndDate);

                    if (!IsValid(footballerDto) || !isContractEndDateValid || !isContractStartDateValid ||
                        validContractStartDate > validContractEndDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    coach.Footballers.Add(new Footballer
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = validContractStartDate,
                        ContractEndDate = validContractEndDate,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType
                    });
                }
                coaches.Add(coach);
                sb.AppendLine($"Successfully imported coach - {coach.Name} with {coach.Footballers.Count} footballers.");
            }
            context.Coaches.AddRange(coaches);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            var teamsDto = JsonConvert.DeserializeObject<ImportTeamsDto[]>(jsonString);

            List<Team> teams = new List<Team>();

            StringBuilder sb = new StringBuilder();

            foreach (var teamDto in teamsDto)
            {
                if (!IsValid(teamDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var team = new Team
                {
                    Name = teamDto.Name,
                    Nationality = teamDto.Nationality,
                    Trophies = teamDto.Trophies
                };
                
                var uniqueFootballers = teamDto.Footballers.Distinct().ToArray();

                var footbalersIds = context.Footballers.Select(x => x.Id).ToArray();

                foreach (var uniqueFootballer in uniqueFootballers)
                {
                    if (!footbalersIds.Contains(uniqueFootballer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    team.TeamsFootballers.Add(new TeamFootballer { FootballerId = uniqueFootballer});
                }

                teams.Add(team);
                sb.AppendLine($"Successfully imported team - {team.Name} with {team.TeamsFootballers.Count} footballers.");
            }
            context.Teams.AddRange(teams);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
