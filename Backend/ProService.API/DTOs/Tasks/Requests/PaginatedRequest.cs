using System.ComponentModel.DataAnnotations;

namespace ProService.API.DTOs.Tasks.Requests;

public class PaginatedRequest
{
    private const int MaxPageSize = 10;

    [Range(1, int.MaxValue, ErrorMessage = "Page number must have value greater than 0.")]
    public int PageNumber { get; set; }

    [RangeWithConstant(1, MaxPageSize)]
    public int PageSize { get; set; }
    // Jeżeli wolimy to można automatycznie przycinać rozmiar strony zamiast rzucać błędem
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