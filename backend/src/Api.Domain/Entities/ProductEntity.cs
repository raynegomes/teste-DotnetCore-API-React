namespace Api.Domain.Entities
{
  public class ProductEntity : BaseEntity
  {
    public string Name { get; set; }
    public int Amount { get; set; }
    public float UnityPrice { get; set; }
  }
}
