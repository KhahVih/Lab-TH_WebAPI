using LAB_TH_API.Models.DTO;
using LAB_TH_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAB_TH_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        protected readonly IPublisherRepository _publisherRepository;
        public PublisherController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var getall = _publisherRepository.GetAllPublishers();
            return Ok(getall);
        }
        [HttpGet("Get-by-Id")]
        public IActionResult GetPublisherById(int id)
        {
            var getbyid = _publisherRepository.GetPublisherById(id);
            return Ok(getbyid);
        }
        [HttpPost("Add-Publisher")]
        public IActionResult AddPublisher(AddPublisherDTO addPublisherDTO)
        {
            var addPublisher = _publisherRepository.AddPublisher(addPublisherDTO);
            return Ok(addPublisher);
        }
        [HttpPut("Update-Publisher-By-Id")]
        public IActionResult UpdatePublisher(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var updatePublisher = _publisherRepository.UpdatePublisherById(id, publisherNoIdDTO);
            return Ok(updatePublisher);
        }
        [HttpDelete("Delete-Publisher-By-Id")]
        public IActionResult DeletePublisher(int id)
        {
            var deletePublisher = _publisherRepository.DeletePublisherById(id);
            return Ok(deletePublisher);
        }
    }
}
