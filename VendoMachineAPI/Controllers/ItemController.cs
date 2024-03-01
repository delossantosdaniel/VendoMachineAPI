using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendoMachineAPI.Data;
using VendoMachineAPI.Models.Domain;
using VendoMachineAPI.Models.DTO;

namespace VendoMachineAPI.Controllers
{
    [Route("api/[controller]")] //Custom URL routing
    [ApiController] // Acts as Controller, and handles http request
    public class ItemController : ControllerBase
    {
        private readonly DataContextEF _dbContext;

        //Injecting DataContextEF
        public ItemController(DataContextEF dbContext)
        {
            _dbContext = dbContext;
        }

        //GET : Retrieve All
        //GET : https://localhost:portnumber/api/Item
        [HttpGet]
        public IActionResult GetAll() //Action Method
        {

            var ItemDomain = _dbContext.Items.ToList(); // Retrieve all items from the database, toList
            var itemsDto = new List<ItemDto>(); // Mapping my domain model to DTO for response
            foreach (var itemDomain in ItemDomain) //Iterating each and map it to DTO
            {
                itemsDto.Add(new ItemDto() //Creates a new instance for ItemDto
                {
                    itemId = itemDomain.itemId,
                    itemFood = itemDomain.itemFood,
                    itemPrice = itemDomain.itemPrice,
                    itemStock = itemDomain.itemStock
                });
            }
            return Ok(itemsDto); //Returns the list of DTO instead of Domain
        }

        //GET: Retrieve by ID
        //GET : https://localhost:portnumber/api/Item/{id}
        [HttpGet("{id}")]
        public IActionResult GetItemById([FromRoute] int id) //Binding the ID to the url
        {
            // Retrieve the item from the database by its ID, retrieves the first element or default
            var itemDomain = _dbContext.Items.FirstOrDefault(x => x.itemId == id); //Representing itemId as x(param) in the collection.
            
            //checks whether the itemDomain is null
            if (itemDomain == null)
            {
                return NotFound("Item not found.");
            }

            var itemsDto = new ItemDto //Create new object for ItemDto for Mapping
            {
                itemId = itemDomain.itemId,
                itemFood = itemDomain.itemFood,
                itemPrice = itemDomain.itemPrice,
                itemStock = itemDomain.itemStock
            };
            return Ok(itemsDto); //returns the retrieved item but as DTO
        }
        [HttpPost]
        //POST: Create or post
        //POST: https://localhost:portnumber/api/Item
        public IActionResult Create([FromBody] AddItemDto addItemDto) //Binding addItemDto to request body as format
        {

            // Check if the item already exists in the database
            if (_dbContext.Items.Any(i => i.itemFood == addItemDto.itemFood))
            {
                return BadRequest("The Item already exists in the database");
            }

            var itemDomainModel = new Item //if item not exist, create a new item domain model object
            {
                itemFood = addItemDto.itemFood,
                itemPrice = addItemDto.itemPrice,
                itemStock = addItemDto.itemStock
            };

            //Use Domain Model to Create User
            _dbContext.Items.Add(itemDomainModel);
            _dbContext.SaveChanges();

            var itemsDto = new ItemDto
            {
                itemId = itemDomainModel.itemId,
                itemFood = itemDomainModel.itemFood,
                itemPrice = itemDomainModel.itemPrice,
                itemStock = itemDomainModel.itemStock
            };
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateId([FromRoute] int id, [FromBody] UpdateItemDto updateItemDto)
        {

            var itemDomainModel = _dbContext.Items.FirstOrDefault(x => x.itemId == id);
            if (itemDomainModel == null)
            {
                return NotFound("Item does not exist.");
            }

            itemDomainModel.itemFood = updateItemDto.itemFood;
            itemDomainModel.itemStock = updateItemDto.itemStock;
            itemDomainModel.itemPrice = updateItemDto.itemPrice;

            _dbContext.SaveChanges();

            var itemsDto = new ItemDto
            {
                itemId = itemDomainModel.itemId,
                itemFood = itemDomainModel.itemFood,
                itemPrice = itemDomainModel.itemPrice,
                itemStock = itemDomainModel.itemStock
            };
            return Ok(itemsDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteId([FromRoute] int id)
        {
            var itemDomainModel = _dbContext.Items.FirstOrDefault(x => x.itemId == id);
            if (itemDomainModel == null)
            {
                return NotFound("Item Id does not exist.");
            }
            _dbContext.Items.Remove(itemDomainModel);
            _dbContext.SaveChanges(true);

            var itemsDto = new ItemDto
            {
                itemId = itemDomainModel.itemId,
                itemFood = itemDomainModel.itemFood,
                itemPrice = itemDomainModel.itemPrice,
                itemStock = itemDomainModel.itemStock
            };
            return Ok(itemsDto);
        }

    }
}
