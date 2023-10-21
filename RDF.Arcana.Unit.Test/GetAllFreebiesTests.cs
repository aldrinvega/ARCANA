using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Features.Freebies;

namespace RDF.Arcana.Unit.Test;

public class GetAllFreebiesTests
{
    private GetAllFreebies _getAllFreebies;
    private Mock<IMediator> _mediatorMock;

    public GetAllFreebiesTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _getAllFreebies = new GetAllFreebies(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllApprovedFreebiesAsync_ReturnsOkResult_WhenQueryIsSuccessfullyProcessed()
    {
        // Arrange
        var expectedData =
            new PagedList<GetAllFreebies.GetAllFreebiesResult>(new List<GetAllFreebies.GetAllFreebiesResult>(), 1, 10,
                4);
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllFreebies.GetAllFreebiesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedData);

        // Act
        var result = await _getAllFreebies.GetAllApprovedFreebiesAsync(new GetAllFreebies.GetAllFreebiesQuery());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<QueryOrCommandResult<object>>(okResult.Value);

        Assert.NotNull(returnValue);
        Assert.True(returnValue.Success);
        Assert.Equal(StatusCodes.Status200OK, returnValue.Status);
        Assert.NotNull(returnValue.Data);
    }
}