using Application;
using ClosedXML.Excel;
using Domain;
using Fluxor;
using Microsoft.JSInterop;

namespace Presentation.Store.Deductions;

public class DeductionsReportExportEffect : Effect<DeductionsReportExportAction>
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IRepo<Domain.Transaction> _transactionRepo;
    private readonly IRepo<Domain.Deduction> _deductionRepo;

    public DeductionsReportExportEffect(IRepo<Deduction> deductionRepo, IRepo<Domain.Transaction> transactionRepo, IJSRuntime jsRuntime)
    {
        _deductionRepo = deductionRepo;
        _transactionRepo = transactionRepo;
        _jsRuntime = jsRuntime;
    }

    public override async Task HandleAsync(DeductionsReportExportAction action, IDispatcher dispatcher)
    {
        FinancialYear.GetSpan(action.FinancialYear, out var start, out var end);
        
        var deductions = await _deductionRepo.Read();
        var transactions = await _transactionRepo.Read();
        var yearDeductibleTransactions = deductions
            .Select(d => new DeductibleTransaction {
                Deduction = d,
                Transaction = transactions.First(t => t.Id == d.TransactionId)
            }).Where(d => d.Transaction.Date <= end && d.Transaction.Date >= start);

        var deductionGroups = yearDeductibleTransactions
            .GroupBy(dt => dt.Transaction.Group.Name);

        var workbook = new XLWorkbook();
        foreach (var deductionGroup in deductionGroups) 
            AddSheetForGroup(deductionGroup.Key, deductionGroup.AsEnumerable(), workbook);

        AddSummaryPage(workbook);
        
        await SaveReportToFile(action, workbook);
    }

    private void AddSummaryPage(XLWorkbook workbook)
    {
        // Now, add a summary sheet
        var summarySheet = workbook.Worksheets.Add("Summary");
        summarySheet.Position = 1; // Make it the first sheet
        
        // Add headers to the summary sheet
        summarySheet.Cell("A1").Value = "Transaction Category";
        summarySheet.Cell("A1").Style.Font.Bold = true;
        summarySheet.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
        summarySheet.Cell("B1").Value = "Total Deductibles";
        summarySheet.Cell("B1").Style.Font.Bold = true;
        summarySheet.Cell("B1").Style.Font.Underline = XLFontUnderlineValues.Single;

        int currentRow = 2; // Start from the second row, after the headers

        // Iterate over each sheet in the workbook
        foreach (var sheet in workbook.Worksheets)
        {
            // Skip the summary sheet itself
            if (sheet.Name == "Summary") continue;

            // Calculate the sum of Column B in the current sheet
            double sumOfColumnB = sheet.Column(2).CellsUsed().Where(c => c.Address.RowNumber > 1).Sum(c => c.GetValue<double>());

            // Write the sheet name and sum to the summary sheet
            summarySheet.Cell(currentRow, 1).Value = sheet.Name;
            summarySheet.Cell(currentRow, 2).Value = sumOfColumnB;

            currentRow++;
        }
        
        summarySheet.Columns().AdjustToContents();
    }

    private void AddSheetForGroup(
        string groupName,
        IEnumerable<DeductibleTransaction> deductionGroups,
        XLWorkbook workbook)
    {
        // Add a new worksheet to the workbook.
        var worksheet = workbook.Worksheets.Add(groupName);
        
        string[] headers = { "Date", "Charge", "Transaction Details", "Note", "Deduction Reason" };
        for (int i = 0; i < headers.Length; i++) {
            IXLCell cell = worksheet.Cell(1, i + 1);
            cell.Style.Font.Bold = true;
            cell.Style.Font.Underline = XLFontUnderlineValues.Single;
            cell.Value = headers[i];
        }

        // Starting from the second row, add the transaction data.
        int row = 2;
        foreach (var deductibleTransaction in deductionGroups)
        {
            worksheet.Cell(row, 1).Value = deductibleTransaction.Transaction.Date;
            worksheet.Cell(row, 2).Value = deductibleTransaction.Transaction.Charge;
            worksheet.Cell(row, 3).Value = deductibleTransaction.Transaction.Details;
            worksheet.Cell(row, 4).Value = deductibleTransaction.Transaction.Note;
            worksheet.Cell(row, 5).Value = deductibleTransaction.Deduction.Reason;
            row++;
        }

        // Adjust column widths to their content.
        worksheet.Columns().AdjustToContents();
    }

    private async Task SaveReportToFile(DeductionsReportExportAction action, XLWorkbook workbook)
    {
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        
        // Convert the MemoryStream to byte array
        var byteArray = stream.ToArray();
    
        // Convert the byte array to a Base64 string
        var base64String = Convert.ToBase64String(byteArray);

        await _jsRuntime.InvokeAsync<object>(
            "saveAsFile",
            $"{action.FinancialYear} Deductibles ({DateTime.Today.Date.ToShortDateString()}).xlsx",
            base64String);
    }

    private struct DeductibleTransaction
    {
        public Domain.Transaction Transaction { get; set; }
        public Deduction Deduction { get; set; }
    }
}