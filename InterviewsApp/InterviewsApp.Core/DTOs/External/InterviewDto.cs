using InterviewsApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.DTOs.External
{
    public class InterviewDto :BaseExternalDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Путь к комментарию
        /// </summary>
        public string PathToComment { get; set; }

        /// <summary>
        /// Уникальный идентификатор вакансии
        /// </summary>
        public Guid PositionId { get; set; }
    }
}
