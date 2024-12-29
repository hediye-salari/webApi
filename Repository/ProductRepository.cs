using hediyeCrudApi.DTO;
using hediyeCrudApi.Entity;
using hediyeCrudApi.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace hediyeCrudApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Prouduct> _products = new();
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            //return _products.Select(p => new ProductDto
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price
            //}).ToList();

            var pro = _context.prouducts.ToList();
            var listdto = new List<ProductDto>();

            //foreach (var item in pro)
            //{
            //    listdto
            //}


            var dtos = pro.Select(c => new ProductDto
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price
            });
            return dtos;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
        //to do 
        //public async Task<ActionResult<ProductDto>>GetExistproduct(int id)
        //{
        //    var product = _products.Find(x => x.Id == id);
        //    if (product != null)
        //    {
        //        return ok
        //    }
        //    throw new Exception("کالا موجود نیست");

        //}   

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = new Prouduct
            {
                Id = _products.Count + 1,
                Name = productDto.Name,
                Price = productDto.Price
            };

            _products.Add(product);

            return productDto;
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = _products.FirstOrDefault(p => p.Id == productDto.Id);
            if (product == null) return;

            product.Name = productDto.Name;
            product.Price = productDto.Price;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null) _products.Remove(product);
        }
    }
}
