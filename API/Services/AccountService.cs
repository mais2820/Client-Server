using API.Contracts;
using API.Data;
using API.DTOs.AccountDto;
using API.DTOs.EducationDto;
using API.DTOs.EmployeeDto;
using API.DTOs.UniversityDto;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly BookingDbContext _dbContext;

        public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository, BookingDbContext dbContext)
        {
            _accountRepository = accountRepository;
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
            _dbContext = dbContext;
        }

        public bool Login(LoginDto loginDto)
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);

            if (getEmployee is null)
            {
                return false; // Employee not found
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);

            if (getEmployee != null && HashingHandler.ValidateHash(loginDto.Password,getAccount.Password))
            {
                return true; // Login success
            }

            return false;
        }       

        public int Register(RegisterDto registerDto)
        {
            // ini untuk cek emaik sama phone number udah ada atau belum
            if (!_employeeRepository.IsNotExist(registerDto.Email) ||
                !_employeeRepository.IsNotExist(registerDto.PhoneNumber))
            {
                return 0; // kalau sudah ada, pendaftaran gagal.
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var university = _universityRepository.GetByCode(registerDto.UniversityCode);
                if (university is null)
                {
                    // Jika universitas belum ada, buat objek University baru dan simpan
                    var createUniversity = _universityRepository.Create(new University
                    {
                        Code = registerDto.UniversityCode,
                        Name = registerDto.UniversityName
                    });

                    university = createUniversity;
                }

                var newNik = GenerateHandler.Nik(_employeeRepository.GetLastNik()); //karena niknya generate, sebelumnya kalo ga dikasih ini niknya null jadi error
                var employeeGuid = Guid.NewGuid(); // Generate GUID baru untuk employee

                // Buat objek Employee dengan nilai GUID baru
                var employee = _employeeRepository.Create(new Employee
                {
                    Guid = employeeGuid, //ambil dari variabel yang udah dibuat diatas
                    NIK = newNik,        //ini juga
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    BirthDate = registerDto.BirthDate,
                    Gender = registerDto.Gender,
                    HiringDate = registerDto.HiringDate,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                });


                var education = _educationRepository.Create(new Education
                {
                    Guid = employeeGuid, // Gunakan employeeGuid
                    Major = registerDto.Major,
                    Degree = registerDto.Degree,
                    Gpa = registerDto.GPA,
                    UniversityGuid = university.Guid
                });

                var hashedPassword = HashingHandler.GenerateHash(registerDto.Password);

                var account = _accountRepository.Create(new Account
                {
                    Guid = employeeGuid, // Gunakan employeeGuid
                    Otp = 1,             //sementara ini dicoba gabisa diisi angka nol didepan, tadi masukin 098 error
                    IsUsed = true,
                    Password = hashedPassword
                });
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
        }

        public int ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var otp = new Random().Next(111111, 999999);
            var getAccountDetail = (from e in _employeeRepository.GetAll()
                                    join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                    where e.Email == forgotPasswordDto.Email
                                    select a).FirstOrDefault();

            if (getAccountDetail is null)
            {
                return 0; // no email found
            }

            _accountRepository.Clear();

            var hashedPassword = HashingHandler.GenerateHash(getAccountDetail.Password);
            var isUpdated = _accountRepository.Update(new Account
            {
                Guid = getAccountDetail.Guid,
                Password = hashedPassword,
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Otp = otp,
                IsUsed = false,
                CreatedDate = getAccountDetail.CreatedDate,
                ModifiedDate = getAccountDetail.ModifiedDate
            });

            if (!isUpdated)
            {
                return -1; // error update
            }

            return 1;
        }

        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var getAccount = (from e in _employeeRepository.GetAll()
                              join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                              where e.Email == changePasswordDto.Email
                              select a).FirstOrDefault();

            if (getAccount is null)
            {
                return 0;
            }
            var hashedPassword = HashingHandler.GenerateHash(changePasswordDto.Password);
            var account = new Account
            {
                Guid = getAccount.Guid,
                IsUsed = true,
                ModifiedDate = DateTime.Now,
                CreatedDate = getAccount.CreatedDate,
                Otp = getAccount.Otp,
                ExpiredTime = getAccount.ExpiredTime,
                Password = hashedPassword,
            };

            if (getAccount.Otp != changePasswordDto.OTP)
            {
                return -1;
            }

            if (getAccount.IsUsed == true)
            {
                return -2;
            }

            if (getAccount.ExpiredTime < DateTime.Now)
            {
                return -3; // OTP expired
            }

            _accountRepository.Clear();

            var isUpdated = _accountRepository.Update(account);
            if (!isUpdated)
            {
                return -4; //Account not Update
            }

            return 3;
        }

        public IEnumerable<AccountDto> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            if (!accounts.Any())
            {
                return Enumerable.Empty<AccountDto>(); // Account is null or not found;
            }

            var accountDtos = new List<AccountDto>();
            foreach (var account in accounts)
            {
                accountDtos.Add((AccountDto)account);
            }

            return accountDtos; // Account is found;
        }

        public AccountDto? GetByGuid(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public AccountDto? Create(NewAccountDto newAccountDto)
        {
            var account = _accountRepository.Create(newAccountDto);
            if (account is null)
            {
                return null; // Account is null or not found;
            }

            return (AccountDto)account; // Account is found;
        }

        public int Update(AccountDto accountDto)
        {
            var account = _accountRepository.GetByGuid(accountDto.Guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            Account toUpdate = accountDto;
            toUpdate.CreatedDate = account.CreatedDate;
            var result = _accountRepository.Update(toUpdate);

            return result ? 1 // Account is updated;
                : 0; // Account failed to update;
        }

        public int Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);
            if (account is null)
            {
                return -1; // Account is null or not found;
            }

            var result = _accountRepository.Delete(account);

            return result ? 1 // Account is deleted;
                : 0; // Account failed to delete;
        }       
    }
}
