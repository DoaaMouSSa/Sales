using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Reflection;
using AspNetCore.Reporting;
using Acc_Sys_5_Api.Models;
using DtoReport.Dto;
using IRepository.IRepository;

namespace Acc_Sys_5_Api.services
{
    public interface IReportService
    {
        byte[] GenerateReportAsync(string reportName, string reportType);
    }

    public class ReportService : IReportService
    {
        private readonly IProuductRepository prouductRepository;
        public ReportService(IProuductRepository _prouductRepository)
        {
            prouductRepository = _prouductRepository;

        }
        public byte[] GenerateReportAsync(string reportName, string reportType)
        {
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("Acc_Sys_5_Api.dll", string.Empty);
            string rdlcFilePath = string.Format("{0}ReportFiles\\{1}.rdlc", fileDirPath, reportName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");

            LocalReport report = new LocalReport(rdlcFilePath);
            List<dtoProductReport> List = new List<dtoProductReport>();
            List = prouductRepository.ReadForReport();
            report.AddDataSource("DataSet1", List);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
          var result = report.Execute(GetRenderType(reportType), 1, parameters);
            return result.MainStream;
        }
        private RenderType GetRenderType(string reportType)
        {
            var renderType = RenderType.Pdf;

            switch (reportType.ToUpper())
            {
                default:
                case "PDF":
                    renderType = RenderType.Pdf;
                    break;
                case "XLS":
                    renderType = RenderType.Excel;
                    break;
                case "WORD":
                    renderType = RenderType.Word;
                    break;
            }

            return renderType;
        }
    }
}
