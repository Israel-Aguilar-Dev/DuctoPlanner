using Aspose.Cells;
using Calculo_ductos_winUi_3.Models;
using Calculo_ductos_winUi_3.Params;
using Calculo_ductos_winUi_3.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Style = Aspose.Cells.Style;


namespace Calculo_ductos_winUi_3.ViewModels
{
    public class StateViewModel 
    {
        public FloorDescriptionViewModel FloorVM { get; }
        public DuctsViewModel DuctsVM { get; }
        public ComponentsViewModel ComponentsVM { get; }
        public StateViewModel()
        {
            FloorVM = new FloorDescriptionViewModel();
            DuctsVM = new DuctsViewModel();
            ComponentsVM = new ComponentsViewModel();
        }

        public void CalculateDucts(object sender, RoutedEventArgs e)
        {
            DuctsVM.CalculateDuctsCommand.Execute(FloorVM.FloorList);

            ComponentCalculationParams args = new ComponentCalculationParams() { 
                Floors = FloorVM.FloorList,
                Ducts = DuctsVM.DucList.MapDuctsToDictionary()
            };

            ComponentsVM.CalculateComponentsCommand.Execute(args);
        }

        public async Task Export(string filePath)
        {
            this.ExportToExcel(filePath);
            this.FinishExport(filePath);
            

            // Aquí usamos un MemoryStream para hacer la escritura realmente async
            //using var memoryStream = new MemoryStream();
            
            //memoryStream.Seek(0, SeekOrigin.Begin);

            //using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
            //await memoryStream.CopyToAsync(fileStream);
        }
       


        #region libreria closedXML
        //public async Task CreateTemplateSheet(Worksheet worksheet)
        //{
        //    worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //    WriteHeaders(worksheet, out int currentRow);

        //    KitCollection dataTemplate = await LoadKitsFromJsonAsync();

        //    WriteKitList(dataTemplate.Ducts, worksheet, ref currentRow);

        //    currentRow++;
        //    currentRow++;
        //    worksheet.Cell(currentRow, 1).Value = "Kit";
        //    worksheet.Cell(currentRow, 2).Value = "Descripcion";
        //    worksheet.Cell(currentRow, 9).Value = "Cantidad";
        //    worksheet.Cell(currentRow, 10).Value = "Total";
        //    worksheet.Range($"B{currentRow}:H{currentRow}").Merge();
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Style.Font.FontColor = XLColor.FromColor(ColorTranslator.FromHtml("#00B0AC"));
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Style.Font.Bold = true;
        //    currentRow++;

        //    WriteKitList(dataTemplate.Guillotine, worksheet, ref currentRow, XLAlignmentHorizontalValues.Left);
        //    currentRow++;

        //    WriteKitList(dataTemplate.Container, worksheet, ref currentRow, XLAlignmentHorizontalValues.Left);
        //    currentRow++;

        //    WriteKitList(dataTemplate.General, worksheet, ref currentRow, XLAlignmentHorizontalValues.Left);
        //    currentRow++;

        //    WriteFooters(worksheet,ref currentRow);
        //    // Opcional: Estilo básico
        //    worksheet.Columns().AdjustToContents();
        //}
        //
        //private void WriteKitList(List<KitModel> list, IXLWorksheet worksheet,ref int currentRow, XLAlignmentHorizontalValues alignment = XLAlignmentHorizontalValues.Center)
        //{
        //    foreach (KitModel item in list) 
        //    {
        //        worksheet.Cell(currentRow, 1).Value = item.Kit;
        //        worksheet.Cell(currentRow, 2).Value = item.Description;
        //        worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = alignment;
        //        worksheet.Cell(currentRow, 9).Value = item.Count;
        //        worksheet.Cell(currentRow, 10).FormulaA1 = $"I{currentRow}";
        //        worksheet.Cell(currentRow, 14).Value = item.InstalationTime;
        //        worksheet.Cell(currentRow, 15).FormulaA1 = $"J{currentRow}*N{currentRow}";
        //        worksheet.Range($"B{currentRow}:H{currentRow}").Merge();
        //        currentRow++;
        //    }
        //}

        //private void WriteHeaders(IXLWorksheet worksheet, out int currentRow)
        //{
        //    //#00B0AC fondo verde aqua y letras
        //    //# FFF2CC fondo Amarillo
        //    //# FFD966 letras amarillas
        //    //# FF0000 letras rojas
        //    var greenBarckground = XLColor.FromColor(ColorTranslator.FromHtml("#00B0AC")); 
        //    var yellowBackground = XLColor.FromColor(ColorTranslator.FromHtml("#FFF2CC")); 
        //    var yellowWords = XLColor.FromColor(ColorTranslator.FromHtml("#FFD966")); 
        //    var redWords = XLColor.FromColor(ColorTranslator.FromHtml("#FF0000")); 

        //    worksheet.Cell(1, 1).Value = "DUCTO DE BASURA";
        //    worksheet.Range("A1:J1").Merge();

        //    worksheet.Range("A1:J1").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range("A1:J1").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range("A1:J1").Style.Font.Bold = true;

        //    worksheet.Cell(2, 2).Value = "Proyecto:";
        //    worksheet.Cell(2, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Range("C2:E2").Merge();
        //    worksheet.Range("G2:H2").Merge();
        //    worksheet.Range("C2:E5").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("C2:E5").Style.Font.Bold = true;
        //    worksheet.Range("G2:H5").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("G2:H5").Style.Font.Bold = true;
        //    worksheet.Range("J2:J4").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("J2:J4").Style.Font.Bold = true;
        //    worksheet.Range("I5:J5").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range("I5:J5").Style.Font.FontColor = yellowWords;

        //    worksheet.Cell(2, 6).Value = "Obra:";
        //    worksheet.Cell(2, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(2, 9).Value = "Version:";
        //    worksheet.Cell(2, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Range("N2:O2").Merge();
        //    worksheet.Range("N2:O2").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range("N2:O2").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range("N2:O2").Style.Font.Bold = true;

        //    worksheet.Cell(3, 2).Value = "Sistema:";
        //    worksheet.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(3, 6).Value = "Oportunidad:";
        //    worksheet.Cell(3, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(3, 9).Value = "Fecha:";
        //    worksheet.Cell(3, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Range("C3:E3").Merge();
        //    worksheet.Range("G3:H3").Merge();
        //    worksheet.Range("O3:O4").Style.Font.FontColor = redWords;

        //    worksheet.Cell(4, 2).Value = "Diametro:";
        //    worksheet.Cell(4, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(4, 6).Value = "Unidad:";
        //    worksheet.Cell(4, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(4, 9).Value = "Realizo:";
        //    worksheet.Cell(4, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Range("C4:E4").Merge();
        //    worksheet.Range("G4:H4").Merge();

        //    worksheet.Cell(5, 2).Value = "Contacto:";
        //    worksheet.Cell(5, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(5, 6).Value = "Estimacion:";
        //    worksheet.Cell(5, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(5, 9).Value = "Dias:";
        //    worksheet.Cell(5, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        //    worksheet.Cell(5, 10).FormulaA1 = "O107"; // fórmula de celda como en el Excel original
        //    worksheet.Range("C5:E5").Merge();
        //    worksheet.Range("G5:H5").Merge();
        //    worksheet.Range("N5:O5").Merge();

        //    worksheet.Cell(6, 1).Value = "Lamina Galvanizada (Optimizado Calibre 18 - 20)";
        //    worksheet.Range("A6:J6").Merge();
        //    worksheet.Range("N6:N7").Merge();
        //    worksheet.Range("O6:O7").Merge();
        //    worksheet.Range("A6:J6").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range("A6:J6").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range("N5:O6").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range("N5:O6").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range("N5:O6").Style.Font.Bold = true;
        //    worksheet.Range("A7:J7").Style.Font.FontColor = greenBarckground;
        //    worksheet.Range("A7:J7").Style.Font.Bold = true;
        //    worksheet.Range("O8:O52").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O8:O52").Style.Font.Bold = true;
        //    worksheet.Range("O56:O73").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O56:O73").Style.Font.Bold = true;
        //    worksheet.Range("O75:O77").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O75:O77").Style.Font.Bold = true;
        //    worksheet.Range("O79:O89").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O79:O89").Style.Font.Bold = true;
        //    worksheet.Range("O91:O92").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O91:O92").Style.Font.Bold = true;
        //    worksheet.Range("O95:O98").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O95:O98").Style.Font.Bold = true;
        //    worksheet.Range("O101:O104").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O101:O104").Style.Font.Bold = true;
        //    worksheet.Range("O106:O107").Style.Fill.BackgroundColor = yellowBackground;
        //    worksheet.Range("O106:O107").Style.Font.Bold = true;

        //    worksheet.Cell(7, 1).Value = "Kit";
        //    worksheet.Cell(7, 2).Value = "Descripcion";
        //    worksheet.Cell(7, 9).Value = "Cantidad";
        //    worksheet.Cell(7, 10).Value = "Total";
        //    worksheet.Range("B7:H7").Merge();

        //    worksheet.Cell(2, 14).Value = "UBICACIÓN";
        //    worksheet.Cell(3, 14).Value = "CDMX Y ZM";
        //    worksheet.Cell(4, 14).Value = "FORÁNEO";
        //    worksheet.Cell(3, 15).Value = "0";
        //    worksheet.Cell(4, 15).Value = "1";
        //    worksheet.Cell(5, 14).Value = "TIEMPOS DE EJECUCIÓN";
        //    worksheet.Cell(6, 14).Value = "HRS DE INSTALACIÓN";
        //    worksheet.Cell(6, 15).Value = "TOTAL";

        //    currentRow = 8;
        //}
        //private void WriteFooters(IXLWorksheet worksheet, ref int currentRow)
        //{
        //    var greenBarckground = XLColor.FromColor(ColorTranslator.FromHtml("#00B0AC"));

        //    worksheet.Cell(currentRow, 1).Value = "Notas importantes:";
        //    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

        //    worksheet.Cell(currentRow, 12).Value = "Horas de instalación";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=LET(x, SUMA(O9:O52,O56:O70,O79:O89), x)";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Jornadas de trabajo";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=O91/8";

        //    currentRow++;

        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    currentRow++;

        //    worksheet.Cell(currentRow, 12).Value = "ACTIVIDADES ADICIONALES DE OBRA";
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Merge();

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Acarreo y distribución de ducto por nivel";
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=(0.4*J8)";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Entrega por nivel, instalación de marco y limpieza de puertas";
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=(1*J8+1)";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Horas de instalación";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=SUMA((O95:O96))";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Jornadas de trabajo";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=O97/8";

        //    currentRow++;
        //    worksheet.Range($"A{currentRow}:J{currentRow}").Merge();
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "ACTIVIDADES ADMINISTRATIVAS";
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:O{currentRow}").Merge();

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Recepción, acopio, almacenamiento de materiales y recorrido en obra";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).Value = "8";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Traslado de personal (redondo y solo foraneas)";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=O4*8";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Horas de instalación";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=SUMA((O101:O102))";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "Jornadas de trabajo";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=O103/8";

        //    currentRow++;
        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "HORAS TOTALES";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=SUMA((O91,O97,O103))";

        //    currentRow++;
        //    worksheet.Cell(currentRow, 12).Value = "JORNADAS DE TRABAJO";
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.Bold = true;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Font.FontColor = XLColor.White;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Style.Fill.BackgroundColor = greenBarckground;
        //    worksheet.Range($"L{currentRow}:N{currentRow}").Merge();
        //    worksheet.Cell(currentRow, 15).FormulaA1 = "=SUMA((O92,O98,O104))";
        //}
        #endregion
    }
}
