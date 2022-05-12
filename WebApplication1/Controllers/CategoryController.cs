using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository categoryRepository;


        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet("getAllCategories")]
        public IActionResult getAll()
        {
            List<Category> cat = categoryRepository.getAll();
            return Ok(cat);

        }
        [HttpGet("getonecategory")]
        public IActionResult getById(int id)
        {
            Category category = categoryRepository.getById(id);

            if (category == null)
                return BadRequest("broduct is null");
            return Ok(category);

        }
        [HttpPost("addNewCategory")]
        public IActionResult New(Category newCategory)
        {
            categoryRepository.insert(newCategory);
            return Ok(newCategory);
        }
        [HttpGet("getCatwithprodNames/{id:int}")]
        public IActionResult CatWithProd(int id)
        {
            Category category = categoryRepository.getById(id);
            CategoryWithProducts catproduct = new CategoryWithProducts();
            catproduct.CatId = category.Id;
            catproduct.CatName = category.Name;
            foreach(var item in category.Product)
            {
                catproduct.ProdNames.Add(item.Name);
            }
            return Ok(catproduct);
        }
        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Category cat)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    
                    categoryRepository.update(id, cat);

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
