﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReportService
    {
        List<DAL.Entities.ReportData> MonthlyReport(int userId);
    }
}
