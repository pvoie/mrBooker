using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MRBooker.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace MRBooker.Wrappers
{
    public class ApplicationUserManager<T> : UserManager<T> where T : IdentityUser
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public ApplicationUserManager(IUserStore<T> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<T> passwordHasher,
            IEnumerable<IUserValidator<T>> userValidators,
            IEnumerable<IPasswordValidator<T>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<T>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _dbContext = (ApplicationDbContext)services.GetService(typeof(ApplicationDbContext));
            _logger = logger;
        }

        public T GetUserWithDataByName(string userName)
        {
            try
            {
                using (_dbContext)
                {
                    var appUser = _dbContext.Users.Include(x => x.Reservations).FirstOrDefault(x => x.UserName == userName);
                    return (T)(object)appUser;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}
