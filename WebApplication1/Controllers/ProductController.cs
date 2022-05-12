using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        IProductRepository ProductRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            ProductRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
      
        [HttpGet]
        public IActionResult GetAll()
        {
            
            List<Product> products = ProductRepository.getAll();
            return Ok(products);
        }
        [HttpGet("getprdbyCatId/{catid:int}")]
        public IActionResult getprdsbycatId(int catid)
        {
            List<Product> products = ProductRepository.getprdbyCatId( catid);
            return Ok(products);
        }


        [HttpGet("getdetail/{id:int}", Name = "getOne")]
        public IActionResult getByID(int id)
        {
            Product product = ProductRepository.getById(id);
            if (product == null)
            {
                return BadRequest("Empty product");
            }
            return Ok(product);
        }
        [HttpGet("/prdandcatDetails/{prodId:int}")]
        public IActionResult getProductWithCat (int prodId)
        {
            Product product = ProductRepository.getById(prodId);


            ProductWithCategoryDTO prod = new ProductWithCategoryDTO();


            prod.CategoryName = product.Category.Name;
            prod.ProductName = product.Name;
            prod.Price = product.Price;

            return Ok(prod);
        }
        [HttpPatch("{id:int}")]
        public IActionResult update(int id,Product product)
        {
        ProductRepository.update(id, product);

            return Ok("updated");
        }
        [HttpPatch("adminupdateproduct/{id:int}")]
        public IActionResult adminupdate(int id, Product product)
        {
            ProductRepository.adminupdate(id, product);

            return Ok("updated");
        }
        [HttpDelete ("{id:int}")]
        public IActionResult update(int id)
        {
            ProductRepository.delete(id);

            return Ok("deleted");
        }

        [HttpGet("{Name:alpha}")]
        public IActionResult getByName(string Name)
        {
            Product product = ProductRepository.getByName(Name);
            if (product == null)
            {
                return BadRequest("Empty Department");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult New(Product product)
        {


            if (ModelState.IsValid)
            {

                try
                {
                    ProductRepository.insert(product);
                    Product p = new Product();
                    string url = Url.Link("getOne", new { id = product.Id });
                    return Created(url, product);
                }
               catch (Exception ex)
               {
                   return BadRequest(ex.Message);
               }
            }
            return BadRequest(ModelState);

        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Product pro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   

                    ProductRepository.update(id,pro);

                    return StatusCode(204, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
    }
}
