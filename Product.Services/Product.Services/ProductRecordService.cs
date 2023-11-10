using System;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Product.Entity;
using Product.Interfaces;
using StackExchange.Redis;
using Product.Repositories;
using IDatabase = StackExchange.Redis.IDatabase;


namespace Product.Services;

public class ProductRecordService : IProductionRecordService
{
    private readonly IRepositoryManager _repositoryManager;
   
   
    public ProductRecordService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
        
    }
    
    public void ProduceProduct(int productId, int quantity) //üretim kısmı
    { 
        
        bool trackChanges = false;
        var product = _repositoryManager.ProductRepository.GetProductById(productId, trackChanges);
    
        if (product is null)
        {
            throw new Exception("Product not found");
        }
        var productMaterials = _repositoryManager.ProductMaterialRepository.GetProductMaterialsByProductId(productId);
    
        if (productMaterials.Count < 3)
        {
            throw new Exception("At least 3 materials are required to produce the product");
        }
        foreach (var productMaterial in productMaterials)
        {
            var material =
                _repositoryManager.MaterialRepository.GetMaterialById(productMaterial.MaterialId, trackChanges);
              
    
            if (material == null)
            {
                throw new Exception("Material Not Found !");
            }
    
            if (material.MaterialUnit < quantity * productMaterial.Quantity)
            {
                throw new Exception($"Insufficient material stock {material.MaterialUnit}");
            }
    
            // Malzeme stokunu güncelleme
            material.MaterialUnit -= quantity * productMaterial.Quantity;
            _repositoryManager.MaterialRepository.UpdateOneMaterial(material);
            
            var productionRecord = new ProductionRecord
            {
           
                ProductId = productId,
                Quantity = quantity,
                ProductionDate = DateTime.UtcNow,
               
            };
           
          
             _repositoryManager.ProductionRecordRepository.AddProductionRecord(productionRecord);
        }
       
        
            // Ürün stokunu artırma
            product.ProductStock += quantity;
            _repositoryManager.ProductRepository.UpdateOneProduct(product);
            _repositoryManager.Save();
         
    }
    
}