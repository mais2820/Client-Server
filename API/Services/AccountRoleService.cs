using API.Contracts;
using API.DTOs.AccountRoleDto;
using API.Models;

namespace API.Services
{
    public class AccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountRoleService(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        public IEnumerable<AccountRoleDto> GetAll()
        {
            var accountroles = _accountRoleRepository.GetAll();
            if (!accountroles.Any())
            {
                return Enumerable.Empty<AccountRoleDto>(); // AccountRole is null or not found;
            }

            var accountRoleDtos = new List<AccountRoleDto>();
            foreach (var accountRole in accountroles)
            {
                accountRoleDtos.Add((AccountRoleDto)accountRole);
            }

            return accountRoleDtos; // AccountRole is found;
        }

        public AccountRoleDto? GetByGuid(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return null; // AccountRole is null or not found;
            }

            return (AccountRoleDto)accountRole; // AccountRole is found;
        }

        public AccountRoleDto? Create(NewAccountRoleDto newAccountRoleDto)
        {
            var accountRole = _accountRoleRepository.Create(newAccountRoleDto);
            if (accountRole is null)
            {
                return null; // AccountRole is null or not found;
            }

            return (AccountRoleDto)accountRole; // AccountRole is found;
        }

        public int Update(AccountRoleDto accountRoleDto)
        {
            var accountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
            if (accountRole is null)
            {
                return -1; // AccountRole is null or not found;
            }

            AccountRole toUpdate = accountRoleDto;
            toUpdate.CreatedDate = accountRole.CreatedDate;
            var result = _accountRoleRepository.Update(toUpdate);

            return result ? 1 // AccountRole is updated;
                : 0; // AccountRole failed to update;
        }

        public int Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);
            if (accountRole is null)
            {
                return -1; // AccountRole is null or not found;
            }

            var result = _accountRoleRepository.Delete(accountRole);

            return result ? 1 // AccountRole is deleted;
                : 0; // AccountRole failed to delete;
        }
    }
}
