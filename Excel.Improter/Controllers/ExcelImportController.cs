using EFCore.BulkExtensions;
using Excel.Improter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Excel.Improter.Controllers
{
    public class ExcelImportController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DataContext dataContext;

        public ExcelImportController(IWebHostEnvironment webHostEnvironment, DataContext dataContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dataContext = dataContext;
        }
        public IActionResult Index(string searchText = "", int searchType = 1)
        {
            if(searchType!=1)
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    TempData["ErrorMessage"] = "Zəhmət olmasa axtarış bölməsin doldurun";
                    return RedirectToAction("Index");
                }
            }
            var predicate = GetExpression(searchType, searchText);
            var data = dataContext.ExcelDatas.Where(predicate).ToList();

            return View(data);
        }

        public IActionResult ImportExcel(IFormFile file)
        {
            int failedCount = 0;
            var rootPath = webHostEnvironment.WebRootPath;
            var folder = Path.Combine(rootPath, @"Excels");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var fileName = Guid.NewGuid().ToString().Replace("-", "").ToUpper() + file.FileName;
            if (!fileName.EndsWith(".xlsx") && !fileName.EndsWith(".xls"))
            {
                ViewBag.ErrorMessage = "UnCorrect Excel File";
                return View("Index");
            }
            var fullPath = Path.Combine(folder, fileName);

            using (var fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                file.CopyTo(fs);
            }
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            List<DatabaseModel> list = new List<DatabaseModel>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(fullPath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                for (int i = 8; i <= worksheet.Dimension.Rows; i++)
                {
                    try
                    {
                        var yataginYerleshdiyiErazi = worksheet.Cells[i, 2].Value;
                        var yataginKodu = worksheet.Cells[i, 3].Value;
                        var yataginAdi = worksheet.Cells[i, 4].Value;
                        var saheninKodu = worksheet.Cells[i, 5].Value;
                        var saheninAdi = worksheet.Cells[i, 6].Value;
                        var dmaaNomresi = worksheet.Cells[i, 7].Value;
                        var dmaaQeydiyyatTarixi = worksheet.Cells[i, 8].Value;
                        var dmaaBitmeTarixi = worksheet.Cells[i, 9].Value;
                        var faydaliQazintiNovu = worksheet.Cells[i, 10].Value;
                        var ehtiyyatinKategoriyasi = worksheet.Cells[i, 11].Value;
                        var istisimarVeziyyeti = worksheet.Cells[i, 12].Value;
                        var ilkinEhtiyyat = worksheet.Cells[i, 13].Value;
                        var ilUzreHasilatHecmi = worksheet.Cells[i, 14].Value;
                        var hasilatqaliqlari = worksheet.Cells[i, 15].Value;
                        var medaxilEmaliyyati = worksheet.Cells[i, 16].Value;
                        var ayrilanSahe = worksheet.Cells[i, 17].Value;
                        var voen = worksheet.Cells[i, 18].Value;
                        var milliGeolojiTesdiq = worksheet.Cells[i, 19].Value;
                        var koordinat = worksheet.Cells[i, 20].Value;
                        var yataginTamAdi = worksheet.Cells[i, 21].Value;
                        DatabaseModel databaseModel = new DatabaseModel
                        {
                            YataginInzibatiErazisi = yataginYerleshdiyiErazi is null ? "" : yataginYerleshdiyiErazi.ToString(),
                            YataginAdi = Convert.ToString(yataginTamAdi),
                            YataginKodu = Convert.ToString(yataginKodu),
                            SaheninKodu = Convert.ToString(saheninKodu),
                            SaheninAdi = Convert.ToString(saheninAdi),
                            FaydaliQazintiNovu = faydaliQazintiNovu is null ? "" : faydaliQazintiNovu.ToString(),
                            DMAANomresi = Convert.ToString(dmaaNomresi),
                            DMAABitmeTarix = Convert.ToDateTime(dmaaBitmeTarixi),
                            DMAAQeydiyyatTarixi = Convert.ToDateTime(dmaaQeydiyyatTarixi),
                            EhtiyyatinKategoryasi = Convert.ToString(ehtiyyatinKategoriyasi),
                            IstisimarVeziyyeti = Convert.ToString(istisimarVeziyyeti),
                            IlkinEhtiyyat = Convert.ToString(ilkinEhtiyyat),
                            AyrilanSahe = Convert.ToString(ayrilanSahe),
                            VOEN = Convert.ToString(voen),
                            MilliGealojiKeshfiyyat = Convert.ToString(milliGeolojiTesdiq),
                            Kordinat = Convert.ToString(koordinat),
                            MedaxilVeziyyeti = Convert.ToString(medaxilEmaliyyati),
                            HasilatQaliqlari = hasilatqaliqlari is null ? "" : hasilatqaliqlari.ToString(),
                            IlUzreHasilatCemi = ilUzreHasilatHecmi is null ? "" : ilUzreHasilatHecmi.ToString()
                        };
                        list.Add(databaseModel);
                    }
                    catch (Exception)
                    {
                        failedCount++;
                        continue;
                    }
                }
                dataContext.ExcelDatas.BatchDelete();
                dataContext.BulkInsert(list);
            }
            return RedirectToAction("Index");
        }
        private Expression<Func<DatabaseModel, bool>> GetExpression(int key, string searchFilter)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (key == 6 || key == 7 || key == 8)
            {
                startDate = Convert.ToDateTime(searchFilter.Split('-')[0]);
                endDate = Convert.ToDateTime(searchFilter.Split('-')[1]);
            }
            Dictionary<int, Expression<Func<DatabaseModel, bool>>> keyValuePairs = new Dictionary<int, Expression<Func<DatabaseModel, bool>>>()
            {
                { 1 , i=>1==1 },
                { 2 , i=>i.VOEN.StartsWith(searchFilter)},
                { 3 , i=>i.YataginAdi == searchFilter },
                { 4 , i=>i.SaheninAdi == searchFilter },
                { 5 , i=>i.YataginInzibatiErazisi==searchFilter },
                { 6 , i=>startDate<i.DMAAQeydiyyatTarixi && i.DMAAQeydiyyatTarixi<=endDate},
                { 7 , i=>startDate<= i.DMAABitmeTarix && i.DMAABitmeTarix<=endDate }
            };
            return keyValuePairs[key];
        }
    }
}
