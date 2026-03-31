using System.ComponentModel.DataAnnotations;

namespace ProService.API.DTOs.Tasks.Requests;

public record PaginatedRequest
{
    private const int MaxPageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must have value greater than 0.")]
    public int PageNumber { get; init; } = 1;

    [RangeWithConstant(1, MaxPageSize)]
    public int PageSize { get; init; } = 10;
    // Można też automatycznie przycinać większy rozmiar strony zamiast zwracać błąd
    // { 
    //     get => _pageSize;
    //     set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    // }
}

public class RangeWithConstantAttribute : RangeAttribute
{
    public RangeWithConstantAttribute(int minimum, int maximum) 
        : base(minimum, maximum)
    {
        ErrorMessage = $"Page size must have value between {minimum} and {maximum}.";
    }
}