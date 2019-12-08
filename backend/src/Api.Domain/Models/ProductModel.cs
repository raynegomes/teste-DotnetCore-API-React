using System;

namespace Api.Domain.Models
{
  public class ProductModel
  {
    private Guid _id;
    public Guid Id
    {
      get { return _id; }
      set { _id = value; }
    }

    private string _name;
    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    private int _amount;
    public int Amount
    {
      get { return _amount; }
      set { _amount = value; }
    }

    private float _unityPrice;
    public float UnityPrice
    {
      get { return _unityPrice; }
      set { _unityPrice = value; }
    }

    private DateTime _createAt;
    public DateTime CreateAt
    {
      get { return _createAt; }
      set { _createAt = value == null ? DateTime.UtcNow : value; }
    }

    private DateTime _updateAt;
    public DateTime UpdateAt
    {
      get { return _updateAt; }
      set { _updateAt = value; }
    }
  }
}
