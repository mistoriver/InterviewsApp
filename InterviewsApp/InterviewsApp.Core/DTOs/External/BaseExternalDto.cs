using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewsApp.Core.DTOs.External
{
    public abstract class BaseExternalDto
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; init; }
    }
}
