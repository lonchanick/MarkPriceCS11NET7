
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared;

public class Product
{
    public int ProductId {get; set;}
    [Required]
    [StringLength(40)]
    public string ProductName {get; set;} = null!;
    [Column("UnitPrice", TypeName="money")]
    public decimal? Cost {get; set;}
    [Column("UnitsInStock")]
    public short? Stock {get; set;}

    public bool Discontinued {get; set;}

    // these two define the foreign key relationship
    // to the Categories table
    public int CategoryId {get; set;}
    public virtual Category category {get; set;}= null!;


    public override string ToString()
    {
        return $"Id: {ProductId}\nProduct: {ProductName}\nPrice: {Cost}\nDiscontinued: {Discontinued}\n";
    }
    public static void TableMood(ICollection<Product> products)
    {
        WriteLine("|{0,-3}|{1,-30}|{2,5}|{3,6}|{4,6}|","Id","Product Name","Cost","Stock","Disc");
        foreach(var p in products)
            WriteLine("|{0,-3}|{1,-30}|{2,-5}|{3,-6}|{4,-6}|",p.ProductId,p.ProductName,p.Cost,p.Stock,p.Discontinued);
    }
}