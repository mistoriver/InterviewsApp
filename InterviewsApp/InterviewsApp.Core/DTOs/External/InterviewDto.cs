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
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Название позиции
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// Уникальный идентификатор вакансии
        /// </summary>
        public Guid PositionId { get; set; }
        /// <summary>
        /// Название компании
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Уникальный идентификатор компании
        /// </summary>
        public Guid CompanyId { get; set; }
    }
}
