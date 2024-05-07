using LAB_TH_API.Data;
using LAB_TH_API.Models;
using LAB_TH_API.Models.DTO;

namespace LAB_TH_API.Repository
{
    public class PublisherRepository:IPublisherRepository
    {
        private readonly AppDbContext _dbContext;
        public PublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            
            var allPublishers = _dbContext.Publishers.ToList();

            var allPublisherDTO = new List<PublisherDTO>();
            foreach (var publisherDomain in allPublishers)
            {
                allPublisherDTO.Add(new PublisherDTO()
                {
                    Id = publisherDomain.Id,
                    Name = publisherDomain.Name

                });
            }
            return allPublisherDTO;
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            
            var publisherWithId = _dbContext.Publishers.FirstOrDefault(x => x.Id == id);
            if (publisherWithId != null)
            { 
                var publisherNoIdDTO = new PublisherNoIdDTO
                {
                    Name = publisherWithId.Name,
                };

                return publisherNoIdDTO;
            }
            return null;
        }

        public AddPublisherDTO AddPublisher(AddPublisherDTO addPublisherDTO)
        {
            var publisherDomainModel = new Publishers
            {
                Name = addPublisherDTO.Name,

            };
            _dbContext.Publishers.Add(publisherDomainModel);
            _dbContext.SaveChanges();
            return addPublisherDTO;
        }
        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                publisherDomain.Name = publisherNoIdDTO.Name;

                _dbContext.SaveChanges();
            }
            return null;
        }

        public Publishers? DeletePublisherById(int id)
        {
            var publisherDomain = _dbContext.Publishers.FirstOrDefault(n => n.Id == id);
            if (publisherDomain != null)
            {
                _dbContext.Publishers.Remove(publisherDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}
