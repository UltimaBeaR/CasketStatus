using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasketStatusWebSite.Models
{
    // Модель с текущим статусом
    public class CurrentStatusViewModel
    {
        // Статус-код
        public Logic.StatusCode StatusCode { get; set; }
        // Запланированная дата работ
        public DateTime ScheduledWorkDate { get; set; }
    }
}
