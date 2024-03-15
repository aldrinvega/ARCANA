using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Price_Mode;

[Route("api/price-change"), ApiController]

public class ExportPriceChange : ControllerBase
{
    private readonly IMediator _mediator;

    public ExportPriceChange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] ExportPriceChangeQuery query)
    {
        var filePath = $"Price Change {query.EffectivityDate.Date}.xlsx";
        try
        {
            await _mediator.Send(query);

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var result = File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                filePath);
            System.IO.File.Delete(filePath);
            return result;
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class ExportPriceChangeQuery : IRequest<Result>
    {
        public DateTime EffectivityDate { get; set; }
    }

    public class Handler : IRequestHandler<ExportPriceChangeQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ExportPriceChangeQuery request, CancellationToken cancellationToken)
        {

            // Retrieve price changes for items associated with a particular price mode
            var priceChanges = await _context.PriceModeItems
                .Include(x => x.ItemPriceChanges)
                .Include(i => i.Item)
                .Include(pm => pm.PriceMode) 
                .Select(priceChanges => new
                {
                    // Select required properties
                    priceChanges.Id,
                    priceChanges.Item.ItemCode,
                    priceChanges.Item.ItemDescription,
                    priceChanges.PriceMode.PriceModeDescription,
                    // Calculate current price based on the most recent price change that is effective on or before the provided date
                    CurrentPrice = priceChanges.ItemPriceChanges
                        .Where(pc => pc.EffectivityDate <= request.EffectivityDate)
                        .OrderByDescending(pc => pc.EffectivityDate)
                        // Select the price from the first (most recent) price change
                        .Select(pc => pc.Price)
                        // Retrieve the first price from the sequence, or a default value if the sequence is empty
                        .FirstOrDefault()
                })
                // Execute the query asynchronously and return the results as a list
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"Price Change Report");

                var headers = new List<string>
            {
                "Id", 
                "Item Code",
                "Item Description",
                "Price Mode",
                "Current Price"
            };

                var range = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, headers.Count));

                range.Style.Fill.BackgroundColor = XLColor.Azure;
                range.Style.Font.Bold = true;
                range.Style.Font.FontColor = XLColor.Black;
                range.Style.Border.TopBorder = XLBorderStyleValues.Thick;
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                for (var index = 1; index <= headers.Count; index++)
                {
                    worksheet.Cell(1, index).Value = headers[index - 1];
                }

                for (var index = 1; index <= priceChanges.Count; index++)
                {
                    var row = worksheet.Row(index + 1);

                    row.Cell(1).Value = priceChanges[index - 1].Id;
                    row.Cell(2).Value = priceChanges[index - 1].ItemCode;
                    row.Cell(3).Value = priceChanges[index - 1].ItemDescription;
                    row.Cell(4).Value = priceChanges[index - 1].PriceModeDescription;
                    row.Cell(5).Value = priceChanges[index - 1].CurrentPrice;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs($"Price Change {request.EffectivityDate.Date}.xlsx");
            }

            return Result.Success();
        }
    }
}
