namespace Catalog.Host.Models.Dtos;

public class TypeDto : BaseResponce
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
}