namespace Product.Interfaces;

public interface IProductService
{
      IQueryable<Entity.Product> GetAllProduct(bool trackChanges);
      Entity.Product GetOneProduct(int id,bool trackChanges);
      void CreateOneProduct(Entity.Product product);
      void UpdateOneProduct(int id,Entity.Product product,bool trackChanges);
      void DeleteOneProduct(int id,bool trackChanges);
      

}