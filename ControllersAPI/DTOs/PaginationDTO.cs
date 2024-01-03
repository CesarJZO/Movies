namespace ControllersAPI.DTOs;

public sealed class PaginationDTO
{
    private const int MaxRecordsPerPage = 50;
    
    public int Page { get; set; }

    public int RecordsPerPage
    {
        get => _recordsPerPage;
        set => _recordsPerPage = Math.Clamp(value, 1, MaxRecordsPerPage);
    }

    private int _recordsPerPage = 10;
}
