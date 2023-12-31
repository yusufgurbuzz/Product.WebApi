﻿namespace Product.Interfaces;

public interface IProductService
{
      IQueryable<Entity.Product> GetProduct(bool trackChanges);
      Entity.Product GetProductById(int id,bool trackChanges);
      void CreateProduct(Entity.Product product);
      void UpdateProductById(int id,Entity.Product product,bool trackChanges);
      void DeleteProductById(int id,bool trackChanges);
      

}