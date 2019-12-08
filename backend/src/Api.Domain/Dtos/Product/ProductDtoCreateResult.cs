using System;

namespace Api.Domain.Dtos.Product
{
  public class ProductDtoCreateResult
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public float UnityPrice { get; set; }

    public DateTime CreateAt { get; set; }
  }
}
