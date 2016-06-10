using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasketStatusWebSite.Models
{
    // Модель с текущим статусом
    public class CurrentStatusViewModel
    {
        // Статус-код
        [Required]
        [Display(Name = "Статус")]
        public Logic.StatusCode StatusCode { get; set; }
        // Запланированная дата работ (Актуальна в случае StatusCode = WorkScheduled)
        [Display(Name = "Дата")]
        public DateTime ScheduledWorkDate { get; set; }
    }
}
