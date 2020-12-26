using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Util;

namespace WpfApp1
{
    public class ReportViewModel : IPageViewModel, INotifyPropertyChanged
    {
        private IDbCrud tm;
        private int selectedMonth;
        public int SelectedMonth
        {
            get { return selectedMonth; }
            set { selectedMonth = value; Report = tm.MonthlyReport(LoggedUserId, SelectedMonth); }
        }
        public int LoggedUserId { get; set; }

        private RelayCommand pdfSave;
        public RelayCommand PdfSave
        {
            get
            {
                return pdfSave ??
                    (pdfSave = new RelayCommand(obj =>
                    {
                        Microsoft.Win32.SaveFileDialog s = new Microsoft.Win32.SaveFileDialog();
                        string month = new DateTime(2020, selectedMonth, 1).ToString("MMMM");
                        s.FileName = "Отчет за " + month;
                        s.DefaultExt = ".pdf";
                        if (s.ShowDialog() == true)
                            PdfHelper.GeneratePdf(s.FileName, Report, month);
                        
                    }
                ));
            }
        }

        private ReportData report;
        public ReportData Report
        {
            get { return report; }
            set { report = value; OnPropertyChanged("Report"); IsReport = report != null; }
        }
        private bool isReport;
        public bool IsReport
        {
            get { return isReport; }
            set { isReport = value; OnPropertyChanged("IsReport"); }
        }
        public ReportViewModel(IDbCrud tm, int loggedUserId)
        {
            this.tm = tm;
            LoggedUserId = loggedUserId;
            SelectedMonth = 0;
        }
        PageType IPageViewModel.GetType()
        {
            return PageType.Report;
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        #endregion
    }
}
