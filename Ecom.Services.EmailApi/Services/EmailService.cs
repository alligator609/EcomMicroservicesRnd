using Ecom.Services.EmailApi.Data;
using Ecom.Services.EmailApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services.EmailApi.Services
{
    public class EmailService : IEmailServicecs
    {
        private DbContextOptions<ApplicationDbContext> _options;
        public EmailService(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public Task EmailCartLod(CartDto cartDto)
        {
            var message = $"Cart with Id {cartDto.CartHeader.Id} has been processed successfully";
            return LogAnEmail(message, cartDto.CartHeader.Email);
        }

        private async Task<bool> LogAnEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLogger = new EmailLogger
                {
                    Email = email,
                    Message = message,
                    LogDate = new DateTime()
                };
                await using var _db = new ApplicationDbContext(_options);
                await _db.EmailLoggers.AddAsync(emailLogger);
                await _db.SaveChangesAsync();
                return true;
            }    
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
