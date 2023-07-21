using Shop.Data.Model;
namespace Shop.Data.Interfaces;

public interface ICarsCategory {
    IEnumerable<Category> AllCategories { get; }
}
