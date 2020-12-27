using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Util
{
    public class PdfHelper
    {
        public static void GeneratePdf(string path, ReportData data, string month)
        {
            var doc1 = new Document();
            //using (PdfDocument document = new PdfDocument()) 
            //{

            //}
            
                var streamObj = new System.IO.FileStream(path, System.IO.FileMode.Create);
            BaseFont font = BaseFont.CreateFont(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font head = new Font(font, 20f, Font.NORMAL, BaseColor.BLUE);
            Font normal = new Font(font, 18f, Font.NORMAL, BaseColor.BLACK);
            Font underline = new Font(font, 10f, Font.UNDERLINE, BaseColor.BLACK);
            PdfWriter.GetInstance(doc1, streamObj);
            doc1.Open();
            doc1.Add(new Paragraph("Отчет за " + month, head));
            doc1.Add(new Paragraph("Всего добавленных мероприятий: " + data.CountFavouriteEvents, normal));
            doc1.Add(new Paragraph("Любимая категория в этом месяце: " + data.FavouriteCategory, normal));
            doc1.Add(new Paragraph("Больше всего я ходил вместе " + data.FavouriteType.ToLower(), normal));
            doc1.Add(new Paragraph("Прошедшие избранные мероприятия:", head));
            foreach(var e in data.FavouriteEvents)
            {
                doc1.Add(new Paragraph(e.Title + "          " + e.CurrentSession.Date.ToString("d"), normal));
            }
            doc1.Close();
        }
    }
}
