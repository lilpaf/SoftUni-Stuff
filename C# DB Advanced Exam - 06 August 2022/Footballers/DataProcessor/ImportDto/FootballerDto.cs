using Footballers.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Footballers.DataProcessor.ImportDto
{
    [XmlType("Footballer")]
    public class FootballerDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required] //DateTime
        public string ContractStartDate { get; set; }

        [Required] //DateTime
        public string ContractEndDate { get; set; }

        [EnumDataType(typeof(BestSkillType))]
        public int BestSkillType { get; set; }
        
        [EnumDataType(typeof(PositionType))]
        public int PositionType { get; set; }
    }
}